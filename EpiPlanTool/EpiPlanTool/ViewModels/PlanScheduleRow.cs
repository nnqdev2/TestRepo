using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EpiPlanTool.Models;

namespace EpiPlanTool.ViewModels {

  public class PlanScheduleRow : DependencyObject {

    private ReactorSchedule _reactorSchedule;

    public PlanScheduleRow(Reactor reactor, EpiSchedule schedule) {
      _reactorSchedule = new ReactorSchedule(){
        Schedule = schedule,
        Reactor = reactor,
        Tasks = new ObservableCollection<Task>()
      };
    }

    public String Label { get { return Reactor.Label; } }

    public ObservableCollection<Task> Tasks {
      get { return _reactorSchedule.Tasks;  }
    }

    public Reactor Reactor {
      get { return _reactorSchedule.Reactor; }
    }

    public EpiSchedule Schedule {
      get { return _reactorSchedule.Schedule; }
    }

  }

  public class PlanScheduleRows : ObservableCollection<PlanScheduleRow> { }

}
