using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace EpiPlanTool.Controls {

  using EpiPlanTool.ViewModels.Collections;
  using EpiPlanTool.Utilities;

  public class ReactorSchedule : Siltronic.Wpf.Controls.Schedule {

    #region Public overrides
    public override ItemsControl GetTimelineControl() {
      return new ReactorTimeline();
    }

    public override Control GetLabelControl() {
      return new ReactorLabel();
    }
    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty FocusedTasksProperty =
      DependencyProperty.Register("FocusedTasks",
        typeof(TaskCollection),
        typeof(ReactorSchedule),
        new FrameworkPropertyMetadata(
          null,
          FPMO.AffectsMeasure | FPMO.AffectsArrange,
          (d, e) => {
            var schedule = d as ReactorSchedule;
            var tasks = d.GetChildren(typeof(ReactorTask));
            foreach(ReactorTask task in tasks)
              task.OnFocusedTasksChanged(schedule.FocusedTasks);
          }
        ));

    public TaskCollection FocusedTasks {
      get { return (TaskCollection)this.GetValue(FocusedTasksProperty); }
      set { this.SetValue(FocusedTasksProperty, value);  }
    }

    #endregion

  }
}
