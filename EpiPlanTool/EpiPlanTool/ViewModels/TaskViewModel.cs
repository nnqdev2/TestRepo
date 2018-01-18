using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using PropertyChanged;
using Commander;
using ExpressMapper;
using System.Windows;

namespace EpiPlanTool.ViewModels {

   using EpiPlanTool.Services;
   using EpiPlanTool.Models;
   using System.Runtime.CompilerServices;

   static class ColorExt {
      public static string ToHex(this System.Windows.Media.Color color) {
         return String.Format("#{0:x2}{1:x2}{2:x2}", color.R, color.G, color.B);
      }
   }

   [ImplementPropertyChanged]
   public class TaskViewModel : IEditableObject, IDataErrorInfo {
      #region Private Fields
      private IAppRepository _repo;
      private ReactorViewModel _reactor;
      private BookedOrderViewModel _attachedOrder;
      private List<AllocLot> _lots = new List<AllocLot>(25);
      private Task Copy;
      //private 
      #endregion

      #region Constructors
      public TaskViewModel() { }

      public TaskViewModel(IAppRepository repo, ReactorViewModel reactor) {
         var wc = StaticRepository.Workcells.ToList();
         if (repo == null || reactor == null)
            throw new ArgumentNullException();
         _reactor = reactor;
         _repo = repo;
         StartWorkcell = wc.FindIndex(w => w.Name == "BIN");
         EndWorkcell = wc.FindIndex(w => w.Name == "WC09");
      }
      #endregion

      #region Private Methods
      private void UpdateProcessTime() {
         if (AttachedOrder != null) {
            AttachedOrder.UpdateProcessTimes();
            Reactor.RefreshLayout();
         }
      }

      internal void CancelEdit() {
         if (Copy != null) Mapper.Map(Copy, this);
         IsEditing = false;
      }

      internal void EndEdit() {
         IsEditing = false;
         SaveChanges();
      }

      internal void BeginEdit() {
         Copy = this.ToModel();
         IsEditing = true;
      }
      #endregion

      #region Private PropertyChanged Methods
      private void OnStartChanged() {
         UpdateProcessTime();
      }

      private void OnStartWorkcellChanged() {
         UpdateProcessTime();
      }

      private void OnEndWorkcellChanged() {
         UpdateProcessTime();
      }

      private void OnUpdateDurationChanged() {
         UpdateProcessTime();
      }

      private void OnTaskTypeChanged() {
         SetTaskColor();
      }

      private void OnEndChanged() {
         SetTaskColor();
      }
      #endregion

      #region Private Methods
      private void SetTaskColor() {
         switch (TaskType) {
            case "O":
               if (this._attachedOrder != null && this._attachedOrder.MasterSchedDate >= this.End)
                  Color = Colors.LawnGreen.ToHex();
               else
                  Color = Colors.Orange.ToHex();
               break;
            case "P":
               Color = Colors.MediumPurple.ToHex();
               break;
            default:
               Color = Colors.White.ToHex();
               break;
         }
      }
      #endregion

      #region Private Method Commands
      [OnCommand("OnEdit")]
      void OnEdit() {
         BeginEdit();
         IsEditorOpen = true;
      }

      [OnCommand("OnCancel")]
      void OnCancel() {
         Copy = null;
         this._reactor.SelectedIndex = -9999;
         CancelEdit();
         IsEditorOpen = false;
      }

      [OnCommand("OnSubmit")]
      void OnSubmit() {
         EndEdit();
         IsEditorOpen = false;
      }
      #endregion

      #region Public Properties
      public bool IsEditing { get; set; }
      public bool IsEditorOpen { get; set; }

      public ReactorViewModel Reactor {
         get { return _reactor; }
         //set { _reactor = value; }
      }
      public IAppRepository Repository { get { return _repo; } }
      public long TaskID { get; set; }
      public long ReactorScheduleID { get { return _reactor.ReactorScheduleID; } }
      public DateTime Start { get; set; }
      public DateTime End { get; set; }
      public string Description { get; set; }
      public string OrderID { get; set; }
      public string TaskType { get; set; }
      public long TaskIndex { get; set; }
      [DependsOn("TaskType")]
      public string Color { get; set; }
      public bool IsPinned { get; set; }
      public int StartWorkcell { get; set; }
      public int EndWorkcell { get; set; }
      public bool HasOrder { get { return AttachedOrder != null; } }
      public bool AllowCustomColor { get { return AttachedOrder == null; } }
      public long? BookedOrderID { get; set; }
      [DependsOn("IsPinned")]
      public bool NotIsPinned { get { return !IsPinned; } }
      public DateTime DateCreated { get; set; }
      public string CreatedBy { get; set; }
      public DateTime? DateUpdated { get; set; }
      public string UpdatedBy { get; set; }
      public bool IsFocusedOrder { get; internal set; }
      public string TaskDescription { get; set; }
      public string TaskDescriptionTooltip { get; set; }
      [DependsOn("Start", "End")]
      public TimeSpan Duration {
         get { return End - Start; }
         set { End = Start + value; }
      }
      public int UpdateDuration { get; set; }

      public BookedOrderViewModel AttachedOrder {
         get { return _attachedOrder; }
         set {
            if (_attachedOrder != null)
               _attachedOrder.DetachTask(this);
            this.TaskType = "T";
            this.Description = "Task";
            this.OrderID = "";
            this.BookedOrderID = null;
            _attachedOrder = value;
            if (_attachedOrder != null) {
               this.OrderID = _attachedOrder.OrderID;
               this.TaskType = "O";
               this.Description = _attachedOrder.CustSpecNickName;
               this.BookedOrderID = _attachedOrder.BookedOrderID;
               _attachedOrder.AttachTask(this);
            }
         }
      }

      public List<AllocLot> Lots { get { return _lots; } }

      #endregion

      #region Public Methods
      public void MoveByTimeSpan(TimeSpan timeSpan) {
         this.Start = this.Start + timeSpan;
         this.End = this.End + timeSpan;
      }

      public void MoveToTimestamp(DateTime timestamp) {
         var dur = this.End - this.Start;
         this.Start = timestamp;
         if (dur.Ticks > 0) this.End = timestamp + dur;
         else { this.End = this.Start; }
      }

      public Task ToModel() {
         return Mapper.Map<TaskViewModel, Task>(this);
      }

      public void SaveChanges() {
         var task = Repository.SaveTask(ToModel());
         this.TaskID = task.TaskID;
      }

      public void Delete() {
         Repository.DeleteTask(ToModel());
      }

      public void Delete2() {
         Repository.DeleteTask2(ToModel());
      }
      #endregion

      #region IEditable Implementation
      void IEditableObject.BeginEdit() {
         BeginEdit();
      }

      void IEditableObject.CancelEdit() {
         CancelEdit();
      }

      void IEditableObject.EndEdit() {
         EndEdit();
      }
      #endregion

      #region IDataErrorInfo Members

      string IDataErrorInfo.Error {
         get { return null; }
      }

      string IDataErrorInfo.this[string columnName] {
         get {
            if (columnName == "Duration") {
               if (Duration.Ticks <= 1000) {
                  return "Not allowed to set task to zero duration.";
               }
            }
            return null;
         }
      }
      #endregion
   }

}
