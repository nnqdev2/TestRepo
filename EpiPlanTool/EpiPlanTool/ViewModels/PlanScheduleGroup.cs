using System;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace EpiPlanTool.ViewModels {

  public class PlanScheduleGroup : DependencyObject {

    public PlanScheduleGroup() {
      GroupRows = new PlanScheduleRows();
    }

    public String GroupName { get; set; }
    public PlanScheduleRows GroupRows {get; set;}

  }

  public class PlanScheduleGroups : ObservableCollection<PlanScheduleGroup> { }

}
