using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Views {
  public class ScheduleDataTemplateSelector : DataTemplateSelector {
    
    public override DataTemplate SelectTemplate(object item, DependencyObject container) {
      ScheduleViewModel schedule = item as ScheduleViewModel;
      FrameworkElement elem = container as FrameworkElement;
      DataTemplate selectedTemplate = elem.FindResource(schedule.ScheduleCodeName+"ScheduleTemplate") as DataTemplate;
      return selectedTemplate;
    }

  }

}