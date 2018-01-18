using System;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using GongSolutions.Wpf.DragDrop;
using Ninject;
using PropertyChanged;
using Commander;

namespace EpiPlanTool.ViewModels {

  using EpiPlanTool.Collections;
  using EpiPlanTool.Models;
  using EpiPlanTool.Services;
  using Collections;

  [ImplementPropertyChanged]
  public class ScheduleViewModel : IDropTarget, IInitializable {

    #region private fields;
    private long ID { get; set; } 
    private readonly IAppRepository _repo;
    private readonly AuthenticationService _authService;
    private readonly StatusMessageService _messageService;
    private readonly IReactorViewModelFactory _factory;
    private readonly BookedOrdersViewModel _bookedOrders;
    private readonly DispatcherTimer _timer = new DispatcherTimer();
    private int _startTimeFakeOut = 0;
    private const int NbrOfDaysOut = 8 * 7;
    private EpiSchedule _schedule;
    #endregion

    #region Cosntructors
    public ScheduleViewModel(
      IAppRepository repo, 
      AuthenticationService authService, 
      StatusMessageService messageService,
      IReactorViewModelFactory reactorFactory,
      BookedOrdersViewModel orders) {
      _repo = repo;
      _factory = reactorFactory;
      _authService = authService;
      _messageService =  messageService;
      _bookedOrders = orders;
    }
    #endregion

    #region Private Methods
    private DateTime RoundUpDateTime(DateTime dt, TimeSpan d) {
      return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
    }

    void IInitializable.Initialize() {
      Start = RoundUpDateTime(DateTime.Now.AddDays(-1), TimeSpan.FromDays(1));
      End = Start.AddDays(NbrOfDaysOut);
      ScheduleCode = ScheduleCodes.MasterSchedCode;
      Reactors = new ObservableRangeCollection<ReactorViewModel>();
      _timer.Interval = TimeSpan.FromMinutes(5);
      _timer.Tick += new EventHandler(UpdateStartTime);
      _timer.Start();
    }

    private void ReactorTaskSelected(ReactorViewModel reactor, TaskViewModel task){
      SelectedTask = task;
    }

    private void SwapReactors(ReactorViewModel fromReactor, ReactorViewModel toReactor) {
      Repository.MoveTasksToReactor(
        fromReactor.ReactorScheduleID, 
        toReactor.ReactorScheduleID);
      fromReactor.MoveTasks(toReactor);
    }

    private void UpdateStartTime(Object sender, EventArgs e) {
       if (!Loading) {
          //foreach (var reactorViewModel in Reactors.ToList()) {
          //   reactorViewModel.RemoveProcessedTasks();
          //   reactorViewModel.RefreshLayout();
          //}
          if (_startTimeFakeOut == 0) {
             Start = RoundUpDateTime(DateTime.Now.AddDays(-2), TimeSpan.FromDays(2));
             End = Start.AddDays(NbrOfDaysOut + 2);
             _startTimeFakeOut = 1;
          }
          else {
             Start = RoundUpDateTime(DateTime.Now.AddDays(-1), TimeSpan.FromDays(1));
             End = Start.AddDays(NbrOfDaysOut + 1);
             _startTimeFakeOut = 0;
          }
       }
    }
    #endregion

    #region Public Properties
    public AuthenticationService AuthenticationService { get { return _authService; } }
    public StatusMessageService StatusMessageService { get { return _messageService; } }
    public IAppRepository Repository { get { return _repo; } }
    public BookedOrdersViewModel BookedOrders { get { return _bookedOrders; } }
    public bool Loading { get; private set; }
    public String ScheduleCode { get; set; }
    public ObservableRangeCollection<ReactorViewModel> Reactors { get; private set; }
    public DateTime? DatePublished { get; set; }
    public DateTime Start { set; get; }
    public DateTime End { set; get; }
    public DateTime SelectedStart { set; get; }
    public DateTime SelectedEnd { set; get; }
    [DependsOn("ScheduleCode")]
    public String ScheduleCodeName { get { return ScheduleCodes.Name(this.ScheduleCode); } }
    public BookedOrderViewModel SelectedOrder { get; set; }
    public TaskViewModel SelectedTask { get; set; }
    public TaskCollection FocusedTasks { 
      get; set; 
    }

    #endregion

    #region Public Methods
    public void Load() {
      if (!Loading) {
        Loading = true;
        StatusMessageService.Message = String.Format("Loading {0} schedule.", ScheduleCodeName);
        _schedule = Repository.LoadSchedule(this.ScheduleCode);
        if (_schedule == null) _schedule = Repository.CreateSchedule(this.ScheduleCode);
        ID = _schedule.ScheduleID;
        StatusMessageService.Message = "Getting Reactor Statuses...";
        var reactorStatus = Repository.GetStatuses();
        StatusMessageService.Message = "Loading Schedules...";
        BookedOrders.Load();
        var reactors = new List<ReactorViewModel>(16);
        foreach (var rs in _schedule.ReactorSchedules.OrderBy(r => r.Reactor.ReactorNumber)) {
          ReactorStatus status = reactorStatus.Find(r => r.ReactorID == rs.ReactorID);
          var reactor = _factory.Create(rs);
          StatusMessageService.Message = "Loading " + reactor.Caption;
          reactors.Add(reactor);
          reactor.Start = Start;
          reactor.End = End;
          reactor.Status = status;
          reactor.TaskSelected += ReactorTaskSelected;
          reactor.Load(rs.Tasks.OrderBy(t => t.TaskIndex).ToList()); 
          reactor.RemoveProcessedTasks();
          reactor.RefreshLayout();         
        }
        Reactors.Clear();
        Reactors.AddRange(reactors);
        Loading = false;
        StatusMessageService.Message = "Loading Complete.";
        BookedOrders.Orders.View.Refresh();
      }
    }

    public void Publish() {
      DatePublished = DateTime.Now;
      StatusMessageService.Message = "Publishing Plan...";
      var models = (
        from rctr in this.Reactors
        select new ReactorSchedule {
          ReactorID = rctr.ReactorID,
          Tasks = rctr.GetModels()
        }).ToList();
      Repository.CreateSchedule(ScheduleCodes.PublishSchedCode, models);
      //foreach (var reactor in this.Reactors) {
      //   reactor.RemoveProcessedTasks();
      //}
      _schedule.DatePublished = DatePublished;
      _schedule.PublishedBy = AuthenticationService.UserID;
      Repository.SaveSchedule(_schedule);
      StatusMessageService.Message =
        String.Format("Plan published: {0}", DatePublished.Value.ToString("MM/d/yyyy HH:mm"));
    }
    #endregion

    #region Drag / Drop handling
    void IDropTarget.DragOver(IDropInfo dropInfo) {
      if(dropInfo.Data is ReactorViewModel) {
        ReactorViewModel source = dropInfo.Data as ReactorViewModel;
        ReactorViewModel target = dropInfo.TargetItem as ReactorViewModel;
        if (target != null && source.ReactType == target.ReactType) {
          dropInfo.Effects = DragDropEffects.Move;
          dropInfo.NotHandled = false;
        }
      }
    }

    void IDropTarget.Drop(IDropInfo dropInfo) {
      if (dropInfo.Data is ReactorViewModel) {
        ReactorViewModel source = dropInfo.Data as ReactorViewModel;
        ReactorViewModel target = dropInfo.TargetItem as ReactorViewModel;
        if (source.ReactorID != target.ReactorID) {
           SwapReactors(source, target);
        }        
      }
    }
    #endregion
  }
}