using System;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using GongSolutions.Wpf.DragDrop;
using MvvmFoundation.Wpf;
using Ninject;
using ExpressMapper;
using PropertyChanged;
using EpiPlanTool.Services;
using EpiPlanTool.Models;

namespace EpiPlanTool.ViewModels {

   using Collections;

   public enum RtcStatus {
      Off,
      FabClosed,
      Down,
      Production,
      Setup,
      Matflow,
      QDEG,
      Test,
      Other
   }

   public delegate void TaskSelectedHandler(ReactorViewModel reactor, TaskViewModel task);

   [ImplementPropertyChanged]
   public class ReactorViewModel : IDropTarget {

      #region Private Fields
      private readonly AuthenticationService _authService;
      private readonly StatusMessageService _messageService;
      private readonly IAppRepository _repo;
      private readonly ITaskViewModelFactory _factory;
      private readonly IBookedOrderLookup _orders;
      private TaskCollection _tasks { get; set; }
      private ICommand _insertCustomTaskCommand;
      private ICommand _deleteTaskCommand;
      private ICommand _doubleClickCommand;
      private IReadOnlyList<Workcell> _workcells;
      #endregion

      #region Constructors

      [Inject]
      public ReactorViewModel(
        long id,
        long reactorId,
        string caption,
        string reactType,
        short reactNumber,
        IAppRepository repo,
        AuthenticationService authService,
        StatusMessageService messageService,
        IBookedOrderLookup orders,
        ITaskViewModelFactory factory
      ) {
         ReactorScheduleID = id;
         ReactorID = reactorId;
         Caption = caption;
         ReactType = reactType;
         ReactorNumber = reactNumber;
         _repo = repo;
         _authService = authService;
         _messageService = messageService;
         _factory = factory;
         _orders = orders;
         _tasks = new TaskCollection();
         _insertCustomTaskCommand = new RelayCommand<String>((type) => InsertCustomTask(type), (type) => AuthenticationService.IsPlanner);
         _deleteTaskCommand = new RelayCommand(RemoveSelectedTask, () => AuthenticationService.IsPlanner);
         _doubleClickCommand = new RelayCommand<TaskViewModel>(OnDoubleClick);
         _workcells = StaticRepository.Workcells.ToList();         
      }
      #endregion

      #region Private Methods
      private DateTime GetTimestamp(int index) {
         if (index >= 0 && index < _tasks.Count) {
            return (_tasks[index].Start < DateTime.Now ? DateTime.Now : _tasks.Last().Start);
            //return _tasks[index].Start;
         }
         if (index >= _tasks.Count && _tasks.Count > 0) {
            return (_tasks.Last().End < DateTime.Now ? DateTime.Now : _tasks.Last().End);
         }
         return DateTime.Now;
      }

      private void ShowTaskDetails(TaskViewModel task) {
         //var viewer = new EpiPlanTool.Views.ScheduleTaskView();
         var viewer = new EpiPlanTool.Views.ScheduleTaskView(task);
         viewer.ShowDialog();
      }

      private void OnDoubleClick(TaskViewModel task) {
         ShowTaskDetails(task);
      }

      private void RemoveSelectedTask() {
         if (SelectedTask != null) {
            var saveSelectedIndex = SelectedIndex;
            var task = SelectedTask;
            SelectedTask = null;
            _tasks.Remove(task);
            task.AttachedOrder = null;
            task.Delete();
            for (int idx = saveSelectedIndex; idx < _tasks.Count; idx++) {
               _tasks[idx].TaskIndex = _tasks[idx].TaskIndex - 1;
               if ((!_tasks[idx].IsPinned) && (_tasks.Count > 1)) {
                  TimeSpan duration = TimeSpan.Zero;
                  if (_tasks[idx].TaskType != "O") {
                     duration = (_tasks[idx].End - (_tasks[idx].Start < DateTime.Now ? DateTime.Now : _tasks[idx].Start));
                  }
                  if (idx == saveSelectedIndex) {
                     if (saveSelectedIndex == 0)
                        _tasks[idx].Start = DateTime.Now;
                     else
                        _tasks[idx].Start = (_tasks[idx - 1].End >= DateTime.Now) ? _tasks[idx - 1].End : DateTime.Now;
                  }                     
                  else
                     _tasks[idx].Start = _tasks[idx - 1].End;
                  if (_tasks[idx].TaskType != "O") {
                     _tasks[idx].End = _tasks[idx].Start + duration;
                  }
               }
               // todo handle pinned
               _tasks[idx].SaveChanges();
            }
            RefreshLayout();
         }
      }

      private TaskViewModel CreateCustomTask(String TaskType, int index = 0) {
         var task = _factory.Create(this);
         task.TaskType = TaskType;
         if (TaskType == "P") task.Description = "PLC";
         if (TaskType == "T") task.Description = "New Task";
         task.Start = GetTimestamp(index);
         task.Duration = new TimeSpan(4, 0, 0);
         task.TaskIndex = index;
         return task;
      }

      private void InsertCustomTask(String TaskType) {
         int index = SelectedTask != null ? SelectedIndex : _tasks.Count;
         TaskViewModel task = CreateCustomTask(TaskType, index);
         ShowTaskDetails(task);
         _tasks.Insert(index, task);
         for (int idx = index + 1; idx < _tasks.Count; idx++) {
            var xx = _tasks[idx];
            var zz = xx.TaskIndex;
            TimeSpan duration = TimeSpan.Zero;
            _tasks[idx].TaskIndex = zz + 1;
            if (_tasks[idx].TaskType != "O") {
               duration = (_tasks[idx].End - (_tasks[idx].Start < DateTime.Now ? DateTime.Now : _tasks[idx].Start));
            }
            _tasks[idx].Start = _tasks[idx - 1].End;
            if (_tasks[idx].TaskType != "O") {
               _tasks[idx].End = _tasks[idx].Start + duration;
            }
            _tasks[idx].SaveChanges();
         }
         RefreshLayout();
      }

      private string GetWorkcellDisplay(string workcell) {
         var name = !String.IsNullOrWhiteSpace(workcell) && workcell.Length >= 4? workcell.Substring(0, 2) : workcell;
         return (name=="WC"?workcell.Substring(workcell.Length-1,1):workcell);
      }
      #endregion

      #region Drag / Drop handling
      void IDropTarget.DragOver(IDropInfo dropInfo) {
         if (AuthenticationService.IsPlanner) {
            if (dropInfo.Data is BookedOrderViewModel) {
               BookedOrderViewModel source = dropInfo.Data as BookedOrderViewModel;
               if ((source.ReactQual == ReactType || source.ReactQual == "DUAL") && source.IsPlannable) {
                  dropInfo.Effects = DragDropEffects.Copy;
                  dropInfo.NotHandled = false;
               }
            }
            if (dropInfo.Data is TaskViewModel) {
               TaskViewModel source = dropInfo.Data as TaskViewModel;
               dropInfo.Effects = DragDropEffects.Move;
               dropInfo.NotHandled = false;
            }
            if (dropInfo.Data is ReactorViewModel) {
               ReactorViewModel reactor = dropInfo.Data as ReactorViewModel;
               if (reactor.ReactType == this.ReactType) {
                  dropInfo.Effects = DragDropEffects.Move;
                  dropInfo.NotHandled = false;
               }
            }
         }
      }

      void IDropTarget.Drop(IDropInfo dropInfo) {
         if (dropInfo.Data is BookedOrderViewModel) {
            var order = dropInfo.Data as BookedOrderViewModel;
            if (order.IsPlannable) {
               int index = dropInfo.InsertIndex;
               TaskViewModel task = _factory.Create(this, order);
               task.Start = GetTimestamp(index);
               task.TaskIndex = index;
               if ((index < 0 || index > _tasks.Count)
                 || (dropInfo.TargetItem == null && dropInfo.TargetCollection != null)) {
                  _tasks.Add(task);
               }
               else {
                  _tasks.Insert(index, task);
               }
               task.SaveChanges();
               for (int idx = index + 1; idx < _tasks.Count; idx++) {
                  var xx = _tasks[idx];
                  var zz = xx.TaskIndex;
                  TimeSpan duration = TimeSpan.Zero;
                  _tasks[idx].TaskIndex = zz + 1;
                  if (_tasks[idx].TaskType != "O") {
                     duration = (_tasks[idx].End - (_tasks[idx].Start < DateTime.Now ? DateTime.Now : _tasks[idx].Start));
                  }
                  _tasks[idx].Start = _tasks[idx - 1].End;
                  if (_tasks[idx].TaskType != "O") {
                     _tasks[idx].End = _tasks[idx].Start + duration;
                  }
                  _tasks[idx].SaveChanges();
               }
               RefreshLayout();
            }
         }
      }
      #endregion

      #region Property Changed methods
      private void OnStatusChanged() {
         var chambers = new List<String>();
         chambers.Add(Status.A);
         if (ReactType == "CENTURA") {
            chambers.Add(Status.B);
            chambers.Add(Status.C);
         }
         if (Status.A == "FAB_CLOSED") {
            ToolStatus = RtcStatus.FabClosed;
         }
         else {
            if (chambers.Any(s => s == "PM" || s == "REPQ" || s == "REPM")) {
               ToolStatus = RtcStatus.Down;
            }
            else {
               if (chambers.All(s => s == "PROD")) ToolStatus = RtcStatus.Production;
               else if (chambers.Any(s => s == "SETUP")) ToolStatus = RtcStatus.Setup;
               else if (chambers.Any(s => s == "TEST")) ToolStatus = RtcStatus.Test;
               else if (chambers.Any(s => s == "QDEG")) ToolStatus = RtcStatus.QDEG;
               else if (chambers.Any(s => s == "MATFLOW")) ToolStatus = RtcStatus.Matflow;
            }
         }
         Chambers = String.Join(",", chambers);
      }

      private void OnSelectedTaskChanged() {
         TaskSelected(this, SelectedTask);
      }

      private void OnSelectedIndexChanged() {
         OnSelectedTaskChanged();
      }
      #endregion

      #region Public Properties
      public long ReactorScheduleID { get; private set; }
      public long ReactorID { get; private set; }
      public String Caption { get; private set; }
      public String ReactType { get; private set; }
      public short ReactorNumber { get; private set; }
      public bool IsLoading { get; private set; }
      public AuthenticationService AuthenticationService { get { return _authService; } }
      public StatusMessageService StatusMessageService { get { return _messageService; } }
      public IAppRepository Repository { get { return _repo; } }
      public IBookedOrderLookup Orders { get { return _orders; } }
      public TaskCollection Tasks { get { return _tasks; } }
      public DateTime Start { get; set; }
      public DateTime End { get; set; }
      public ReactorStatus Status { get; set; }
      [DependsOn("Status")]
      public RtcStatus ToolStatus { get; set; }
      public String Chambers { get; set; }
      [AlsoNotifyFor("SelectedIndex")]
      public TaskViewModel SelectedTask { get; set; }
      [AlsoNotifyFor("SelectedTask")]
      public int SelectedIndex { get; set; }
      public string npType { get; set; }
      #endregion

      #region Public Methods
      public void Load(List<Task> tasks) {
         if (!IsLoading) {
            IsLoading = true;
            var timeNow = DateTime.Now;
            StatusMessageService.Message = "Loading Reactor " + this.Caption;
            Tasks.AddRange(
              from task in tasks
              where task.IsDeleted == false
              //where (task.IsDeleted == false && (task.Start >= DateTime.Now || task.End > DateTime.Now))
              orderby task.TaskIndex, task.Start, task.DateCreated
              select _factory.Create(this, task)
            );
            foreach (var task in Tasks.Where(t => t.TaskType == "O")) {
               if (task.BookedOrderID.HasValue) {
                  BookedOrderViewModel order = Orders[task.BookedOrderID.Value];
                  task.AttachedOrder = order;
                  if (task.TaskIndex == 0) {
                     task.Start = timeNow;
                  }
               }
            };
            StatusMessageService.Message = this.Caption + " loaded.";
            IsLoading = false;
         }
      }

      public void RefreshLayout() {
         if (_tasks.Count == 0) return;
         var timestamp = _tasks[0].Start;
         var timeNow = DateTime.Now;
         for (int idx = 0; idx < _tasks.Count; idx++) {
            var task = _tasks[idx];
            //task.TaskIndex = idx;
            if (!task.IsPinned) {
               if ((idx == 0) && (task.Start > timeNow)) {
                  task.MoveToTimestamp(timeNow);
               }
               else {
                  task.MoveToTimestamp(timestamp);
                }
               if (task.AttachedOrder != null) {
                  var startWorkcell = _workcells.First(x => x.WorkcellID == task.StartWorkcell);
                  var endWorkcell = _workcells.First(x => x.WorkcellID == task.EndWorkcell);
                  task.TaskDescription = "(" + GetWorkcellDisplay(endWorkcell.Name) + ") "
                                       + task.AttachedOrder.CustShortName + " " + task.Description
                                       + (" (" + GetWorkcellDisplay(startWorkcell.Name) + ")");
               }
               else
                  task.TaskDescription = task.Description;
               task.TaskDescriptionTooltip = task.TaskDescription + " (starts at " + task.Start + ")";
               task.SaveChanges();
               timestamp = task.End;
            }
         }
      }

      public void MoveTasks(ReactorViewModel toReactor) {
         var Tasks1 = this.Tasks.ToList();
         var Tasks2 = toReactor.Tasks.ToList();
         Tasks.Clear();
         Tasks.AddRange(Tasks2);
         toReactor.Tasks.Clear();
         toReactor.Tasks.AddRange(Tasks1);
      }

      public List<Task> GetModels() {
         return (
           from task in Tasks
           select (Task)Mapper.Map<TaskViewModel, Task>(task)
          ).ToList();
      }

      public void RemoveProcessedTasks() {
         if (_tasks.Count == 0) return;
         var timeNow = DateTime.Now;
         for (int idx = 0; idx < _tasks.Count; idx++) {
            var task = _tasks[idx];
            if ( (task.TaskType == "O") &&  (task.AttachedOrder == null || (task.AttachedOrder != null && task.AttachedOrder.IsDone))
               || ((task.TaskType != "O") && (task.End < timeNow))  ) {
                  _tasks.Remove(task);
                  task.AttachedOrder = null;
                  task.Delete2();
            }
         }
         for (int idx = 0; idx < _tasks.Count; idx++) {
            _tasks[idx].TaskIndex = idx;
            _tasks[idx].SaveChanges();
         }
      }
      #endregion

      #region Public Event Handlers
      public event TaskSelectedHandler TaskSelected;
      #endregion

      #region Commands
      public ICommand InsertCustomTaskCommand { get { return _insertCustomTaskCommand; } }
      public ICommand DeleteTaskCommand { get { return _deleteTaskCommand; } }
      public ICommand DoubleClickCommand { get { return _doubleClickCommand; } }
      #endregion
   }
}
