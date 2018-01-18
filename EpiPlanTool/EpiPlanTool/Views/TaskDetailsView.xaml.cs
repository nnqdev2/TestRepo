using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;
using Visibility = System.Windows.Visibility;
using Xceed.Wpf.Toolkit.PropertyGrid;
using ExpressMapper;

namespace EpiPlanTool.Views {

  public partial class TaskDetailsView : UserControl {

    #region Constructors
    public TaskDetailsView() {
      InitializeComponent();
      DataContextChanged += (s, e) => {
        propertyGrid.SelectedObject = DataContext;
      };
    }
    #endregion

    #region Private Methods
    private void UpdateProperties(){
      var propsDict = new Dictionary<string, PropertyItem>();
      foreach (PropertyItem prop in propertyGrid.Properties) {
        propsDict.Add(prop.PropertyDescriptor.DisplayName, prop);
        prop.Visibility = Visibility.Visible;
      }
      var taskType = (string)GetValue(TaskTypeProperty);
      switch(taskType) {
        case "P":
          propsDict["IsPinned"].Visibility = Visibility.Collapsed;
          propsDict["Color"].Visibility = Visibility.Collapsed;
          propsDict["StartWorkcell"].Visibility = Visibility.Collapsed;
          propsDict["EndWorkcell"].Visibility = Visibility.Collapsed;
          break;
        case "O":
          propsDict["IsPinned"].Visibility = Visibility.Collapsed;
          propsDict["Color"].Visibility = Visibility.Collapsed;
          propsDict["Duration"].Visibility = Visibility.Collapsed;
          break;
        case "T":
          propsDict["Color"].Visibility = Visibility.Collapsed;
          propsDict["Duration"].Visibility = Visibility.Collapsed;
          propsDict["StartWorkcell"].Visibility = Visibility.Collapsed;
          propsDict["EndWorkcell"].Visibility = Visibility.Collapsed;
          break;
      }
      propertyGrid.Update();
    }
    #endregion

    public static DependencyProperty TaskTypeProperty =
       DependencyProperty.Register(
        "TaskType",
        typeof(string),
        typeof(TaskDetailsView),
        new FrameworkPropertyMetadata(
          "?",
          FPMO.None,
          (d, e) => {
            (d as TaskDetailsView).UpdateProperties();
          }
        ));

  }
}
