using System;
using System.Windows;
using System.ComponentModel;
using EpiPlanTool.Models;

namespace EpiPlanTool.ViewModels {

  public class PlanSchedule : DependencyObject {

    private PlanContext _ctx;

    public PlanSchedule() {
      _ctx = new PlanContext();
    }

    ~PlanSchedule() {
      _ctx.Dispose();
      _ctx = null;
    }

    public PlanContext Context {
      get { return _ctx; }
    }

    public void CreateSchedule() {
      Groups =
        new PlanScheduleGroups(){
          new PlanScheduleGroup(){ 
            GroupName = "ASM" 
          },
          new PlanScheduleGroup(){ 
            GroupName = "CENTURA"
          }
        };
    }

    public static readonly DependencyProperty GroupsProperty =
        DependencyProperty.Register("Groups", typeof(PlanScheduleGroups), typeof(PlanViewModel));

    public PlanScheduleGroups Groups {
      get { return (PlanScheduleGroups)GetValue(GroupsProperty); }
      set { SetValue(GroupsProperty, value); }
    }

  }

}

/*
  GroupRows = new ObservableCollection<ReactorSchedule>(
    from sched in schedule.ReactorSchedules
    where sched.Reactor.ReactType == "CENTURA" 
    select sched)
*/