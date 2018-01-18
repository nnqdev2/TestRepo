using System;
using System.Windows;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SiltronicCorp.Controls;
using EpiPlanTool.Models;
using GongSolutions.Wpf.DragDrop;
using NLog;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace EpiPlanTool.ViewModels {

  public class EpiPlanViewModel : DependencyObject {

    #region Private Section
    private static readonly bool designTime =
      System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());

    private static Logger log = LogManager.GetCurrentClassLogger();
    private DragDropHandler dropHandler;

    private PlanContext ctx;
    private PlanContext Context {
      get {
        if(ctx == null) { ctx = new PlanContext(); }
        return ctx;
      }
    }

    #endregion

    #region public properties / methods
    public DragDropHandler DropHandler { get { return dropHandler; } }
    public ObservableCollection<BookedOrder> Orders { get { return Context.BookedOrders.Local; } }

    public EpiPlanViewModel() {
      dropHandler = new DragDropHandler();
      DropHandler.AddHandler(typeof(ScheduleTasksPresenter), typeof(BookedOrder), AddOrder);
    }

    public void OnLoaded(object sender, RoutedEventArgs e) {
      Start = DateTime.Now.Date;
      End = DateTime.Now.Date.AddDays(15);
      LoadBookedOrders();
      CreateNewSchedule();
    }

    public void SaveChanges() {
      Context.SaveChanges();
    }

    public void LoadBookedOrders() {
      log.Trace("LoadBookedOrders");
      (from
         ord in Context.BookedOrders
       where
         ord.MAPL.StartsWith("AE")
         && ord.CustShortName != "FAB2"
       select ord
       ).Load();
    }

    public void CreateNewSchedule() {
      log.Trace("CreateNewSchedule");

      Context.Reactors.Load();
      var reactors = Context.Reactors.Local;

      Context.EpiSchedules.Local.Clear();

      EpiSchedule schedule = new EpiSchedule() {
        DateCreated = DateTime.Now,
        PublishedBy = "EPI_PLAN_TOOL"
      };
      Context.EpiSchedules.Add(schedule);
      foreach(Reactor reactor in reactors){
        schedule.ReactorSchedules.Add(
          new ReactorSchedule(){
            Schedule = schedule,
            Reactor = reactor }
          );
      }
      SaveChanges();

      ReactorGroups =
        new ObservableCollection<ReactorGroup>(){
          new ReactorGroup(){ 
            GroupName = "ASM" ,
            GroupRows = new ObservableCollection<ReactorSchedule>(
              from sched in schedule.ReactorSchedules
              where sched.Reactor.ReactType == "ASM" 
              select sched)
          },
          new ReactorGroup(){ 
            GroupName = "CENTURA",
            GroupRows = new ObservableCollection<ReactorSchedule>(
              from sched in schedule.ReactorSchedules
              where sched.Reactor.ReactType == "CENTURA" 
              select sched)
          }
        };
    }

    public void AddOrder(object target, object source, IDropInfo dropInfo ) {
      Console.WriteLine("AddOrder");
      //ObservableCollection<Models.Task> tasks = dropInfo.TargetCollection as ObservableCollection<Models.Task>;
      ScheduleTasks tasks = null;
      BookedOrder order = source as BookedOrder;
      DateTime start = Start;
      if(tasks.Count > 0) { start = tasks.Last().End.AddMinutes(1); }
      var procTime = order.BookQty / order.WPD;
      DateTime end = Start.AddDays((double)procTime);
      ScheduleTask task = new ScheduleTask() {
        Description = String.Format("{0} {1}",
            order.CustShortName,
            order.CustSpecNickName,
            order.OrdNum,
            order.OrdItem),
        Start = start,
        End = end
      };
      tasks.Add(task);
    }

    public void Login() {
    }

    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty ReactorGroupsProperty =
        DependencyProperty.Register("ReactorGroups", typeof(ObservableCollection<ReactorGroup>), typeof(EpiPlanViewModel));

    public ObservableCollection<ReactorGroup> ReactorGroups {
      get { return (ObservableCollection<ReactorGroup>)GetValue(ReactorGroupsProperty); }
      set { SetValue(ReactorGroupsProperty, value); }
    }

    public static readonly DependencyProperty StartProperty =
      DependencyProperty.Register("Start", typeof(DateTime), typeof(EpiPlanViewModel));

    public DateTime Start {
      get { return (DateTime)GetValue(StartProperty); }
      set { SetValue(StartProperty, value); }
    }

    public static readonly DependencyProperty EndProperty =
      DependencyProperty.Register("End", typeof(DateTime), typeof(EpiPlanViewModel));

    public DateTime End {
      get { return (DateTime)GetValue(EndProperty); }
      set {SetValue(EndProperty, value); }
    }

    #endregion
  }
  
}
