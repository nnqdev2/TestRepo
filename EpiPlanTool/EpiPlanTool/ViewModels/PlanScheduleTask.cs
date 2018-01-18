using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using EpiPlanTool.Models;

namespace EpiPlanTool.ViewModels {

  public class PlanScheduleTask : DependencyObject {

    public PlanScheduleTask() : base() {
    }

  }

  public class PlanScheduleTasks : ObservableCollection<PlanScheduleTask> { };

}
