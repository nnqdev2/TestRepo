using System;
using System.Windows;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace EpiPlanTool.Views {

  using EpiPlanTool.ViewModels;

  public partial class ScheduleTaskView : Window {

    public ScheduleTaskView(TaskViewModel task) {
      InitializeComponent();
      OrderDetailsView.propertyGrid.IsReadOnly = true;
      this.DataContext = task;
      task.BeginEdit();
      task.IsEditorOpen = true;
      DataContextChanged += (s, e) => {
        this.TaskDetailsView.DataContext = this.DataContext;
      };
    }

    public static DependencyProperty IsEditingProperty =
       DependencyProperty.Register(
        "IsEditing",
        typeof(bool),
        typeof(ScheduleTaskView),
        new FrameworkPropertyMetadata(
          false,
          FPMO.AffectsMeasure | FPMO.AffectsArrange,
          (d, e) => {
            ScheduleTaskView view = d as ScheduleTaskView;
            var newValue = (bool)e.NewValue;
            view.TaskDetailsView.propertyGrid.IsReadOnly = !newValue;
          }
        ));

    public bool IsEditing {
      get { return (bool)GetValue(IsEditingProperty); }
      set { SetValue(IsEditingProperty, value); }
    }

    public static DependencyProperty IsOpenProperty =
       DependencyProperty.Register(
        "IsOpen",
        typeof(bool),
        typeof(ScheduleTaskView),
        new FrameworkPropertyMetadata(
          false,
          FPMO.None,
          (d, e) => {
            var window = d as Window;
            var newValue = (bool)e.NewValue;
            if (newValue == false) window.Close();
          }
        ));

    public bool IsOpen {
      get { return (bool)GetValue(IsOpenProperty); }
      set { SetValue(IsOpenProperty, value); }
    }

    public static DependencyProperty HasOrderProperty =
      DependencyProperty.Register(
        "HasOrder",
        typeof(bool),
        typeof(ScheduleTaskView),
        new FrameworkPropertyMetadata(
          false,
          FPMO.None,
          (d, e) => {
            var view = d as ScheduleTaskView;
            var newValue = (bool)e.NewValue;
            if (newValue == false) {
              view.Lots.Close();
              view.OrderDetails.Close();
            }
            else {
              view.Lots.Show();
              view.OrderDetails.Show();
            }
          }
      ));

    public bool HasOrder {
      get { return (bool)GetValue(HasOrderProperty); }
      set { SetValue(HasOrderProperty, value); }
    }
  }
}





//DataContext = value;
//        TaskDetailsView.Task = _task;

