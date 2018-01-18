using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Mehdime.Entity;
using ExpressMapper;
using EpiPlanTool.Context;
using EpiPlanTool.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;

namespace EpiPlanTool.Services {

  public interface IAppRepository {
    EpiSchedule LoadSchedule(string schedCode);
    EpiSchedule CreateSchedule(String schedCode, List<ReactorSchedule> models);
    EpiSchedule CreateSchedule(String schedCode);
    void SaveSchedule(EpiSchedule schedule);
    void MoveTasksToReactor(long oldId, long newId);
    Task GetTask(long id);
    void DeleteTask(Task task);
    void DeleteTask2(Task task);
    void DeleteTasks(IEnumerable<Task> tasks);
    Task SaveTask(Task task);
    List<BookedOrder> GetOrders();
    List<ReactorStatus> GetStatuses();
    void SetDatePublished(string schedcode, DateTime? DatePublished);
  }

  public class AppRepository : IAppRepository {

    private DbContextScopeFactory _scopeFactory = new DbContextScopeFactory();
    private IAmbientDbContextLocator _locator = new AmbientDbContextLocator();
    private readonly AuthenticationService _authService;
    private readonly StatusMessageService _messageService;

    public AppRepository(
      AuthenticationService authService,
      StatusMessageService messageService
    ) {
      _authService = authService;
      _messageService = messageService;
    }

    public AuthenticationService AuthenticationService { get { return _authService; } }
    public StatusMessageService StatusMessageService { get { return _messageService; } }

    public EpiSchedule GetSchedule(long id) {
      var scope = _scopeFactory.Create();
      var ctx = scope.DbContexts.Get<PlanContext>();
      return ctx.EpiSchedules.Find(id);
    }

    public EpiSchedule LoadSchedule(string schedCode) {
      var scope = _scopeFactory.Create();
      var ctx = scope.DbContexts.Get<PlanContext>();
      var sched = (
        from schedule in ctx.EpiSchedules
          .Include(s => s.ReactorSchedules.Select(rs => rs.Reactor))
          .Include(s => s.ReactorSchedules.Select(rs => rs.Tasks))
        where
          schedule.SchedCode == schedCode &&
          schedule.Status == "A"
        select schedule
      ).FirstOrDefault<EpiSchedule>();
      return sched;
    }

    public EpiSchedule CreateSchedule(String schedCode, List<ReactorSchedule> models) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        foreach(var sched in 
          ctx.EpiSchedules.Where(s => s.SchedCode == schedCode & s.Status == "A")
        ) sched.Status = "I";
        EpiSchedule schedule = new EpiSchedule() {
          SchedCode = schedCode,
          Status = "A",
          DateCreated = DateTime.Now,
        };
        foreach(var model in models) {
          var rs = new ReactorSchedule {
            Reactor = ctx.Reactors.Find(model.ReactorID)
          };
          StatusMessageService.Message = "Adding: " + rs.Reactor.Caption;
          int count = model.Tasks.Count;
          int index = 1;
          foreach (var task in model.Tasks) {
            var pubTask = new Task();
            Mapper.Map(task, pubTask);
            pubTask.TaskID = 0;
            pubTask.MasterTaskID = task.TaskID;
            StatusMessageService.Message = 
              String.Format("Adding task: {0} of {1}", index, count);
            ctx.Tasks.Add(pubTask);
            rs.Tasks.Add(pubTask);
            index++;
          }
          ctx.ReactorSchedules.Add(rs);
          schedule.ReactorSchedules.Add(rs);
        }
        ctx.EpiSchedules.Add(schedule);
        schedule.DateCreated = DateTime.Now;
        schedule.DatePublished = DateTime.Now;
        schedule.CreatedBy = AuthenticationService.UserID;
        ctx.SaveChanges();
        return schedule;
      }
    }

    public EpiSchedule CreateSchedule(String schedCode) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        var models = (
          from r in ctx.Reactors
          select new { ReactorID = r.ReactorID }
        )
        .ToList()
        .Select(r => new ReactorSchedule { ReactorID = r.ReactorID })
        .ToList();
        return CreateSchedule(schedCode, models);
      }
    }

    public void SaveSchedule(EpiSchedule schedule) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        ctx.Set<EpiSchedule>().AddOrUpdate(schedule);
        ctx.SaveChanges();
      }
    }

    public void MoveTasksToReactor(long oldId, long newId) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        var tasks1 = from task in ctx.Tasks where task.ReactorScheduleID == oldId select task;
        var tasks2 = from task in ctx.Tasks where task.ReactorScheduleID == newId select task;
        foreach (var task in tasks1) task.ReactorScheduleID = newId;
        foreach (var task in tasks2) task.ReactorScheduleID = oldId;
        ctx.SaveChanges();
      }
    }

    public Task GetTask(long id) {
      var scope = _scopeFactory.Create();
      var ctx = scope.DbContexts.Get<PlanContext>();
      return ctx.Tasks.Find(id);
    }

    public void DeleteTask(Task task) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        //ctx.Tasks.Attach(task);
        //ctx.Set<Task>().Remove(task);
        ctx.Entry<Task>(task).State = EntityState.Deleted;
        ctx.SaveChanges();
      }
    }

    public void DeleteTask2(Task task) {
       using (var scope = _scopeFactory.Create()) {
          var ctx = scope.DbContexts.Get<PlanContext>();
          var sameTask = ctx.Tasks.Find(task.TaskID);
          ctx.Entry<Task>(sameTask).State = EntityState.Deleted;
          ctx.SaveChanges();
       }
    }

    public void DeleteTasks(IEnumerable<Task> tasks) {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        foreach (var t in tasks) {
          ctx.Tasks.Attach(t);
          ctx.Entry<Task>(t).State = EntityState.Deleted;
        }
        ctx.SaveChanges();
      }
    }

    public Task SaveTask(Task task) {
       using (var scope = _scopeFactory.Create()) {
          var ctx = scope.DbContexts.Get<PlanContext>();
          ctx.Set<Task>().AddOrUpdate(task);
          ctx.SaveChanges();
       }
       return task;
    }

    public List<BookedOrder> GetOrders() {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<PlanContext>();
        return (
          from order in ctx.BookedOrders
            .Include(bk => bk.Lots)
          where ((order.PostQty??0)+(order.MG??0)+(order.FG??0)) < order.BookQty 
          orderby order.BookDate, order.BookedOrderID
          select order
        ).ToList();
      }
    }

    public List<ReactorStatus> GetStatuses() {
      using (var scope = _scopeFactory.Create()) {
        var ctx = scope.DbContexts.Get<MppContext>();
        return ctx.GetReactorStatuses();
      }
    }

    public void SetDatePublished(string schedCode, DateTime? datePublished)
    {
        if (datePublished == null)
            return;
        using (var scope = _scopeFactory.Create())
        {
            var ctx = scope.DbContexts.Get<PlanContext>();
            var sql = @"update f2rep_owner.ept_schedule set date_published = :0 WHERE sched_code = :1 and status = 'A'";
            int noOfRowUpdated = ctx.Database.ExecuteSqlCommand(sql, datePublished, schedCode);
        }
    }
  }

  public static class StaticRepository {
    private static IReadOnlyList<Workcell> _workcells;

    static StaticRepository() { }

    static public IReadOnlyList<Workcell> Workcells {
      get {
        if (_workcells == null) {
          using (var ctx = new PlanContext()) {
            _workcells = (
              from wc in ctx.Workcells
              where wc.Name != "X"
              orderby wc.WorkcellID
              select wc
            ).ToList().AsReadOnly();
          }
        }
        return _workcells;
      }
    }

  }

}
