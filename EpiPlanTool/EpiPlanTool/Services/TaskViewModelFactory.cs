using System;
using Mehdime.Entity;
using Ninject;
using EpiPlanTool.Context;
using EpiPlanTool.Models;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Services {
  public class TaskViewModelFactory {

    #region Private Fields
    private AuthenticationService _authService;
    private DbContextScopeFactory _scopeFactory;
    private IAmbientDbContextLocator _locator;
    #endregion

    #region Constructors
    [Inject]
    public TaskViewModelFactory(AuthenticationService service) {
      Console.WriteLine("TaskViewModelFactory({0})", service);
      _authService = service;
    }
    #endregion

    #region Public Properties
    public string UserID { get { return _authService.UserID; } } 
    #endregion

    #region Public Methods
    public TaskViewModel Create(ReactorViewModel reactor, Task fromTask = null, BookedOrderViewModel order = null) {
      var newTask = fromTask;
      if (newTask == null) {
        newTask = new Task();
        newTask.ReactorScheduleID = reactor.ReactorScheduleID;
        newTask.CreatedBy = _authService.UserID;
        var ctx = _locator.Get<PlanContext>();
        ctx.Tasks.Add(newTask);
        fromTask = newTask;
      }
//    TaskViewModel task = new TaskViewModel(fromTask, reactor);
      //if (order != null) task.AttachedOrder = order;
      //return task;
      return null;
    }

    public void Delete(TaskViewModel task) {
      //var ctx = _locator.Get<PlanContext>();
      //var Task = ctx.Tasks.Find(task.ID);
      //Task.ReactorSchedule = null;
      //ctx.Tasks.Remove(Task);
    }

    #endregion
  }
}