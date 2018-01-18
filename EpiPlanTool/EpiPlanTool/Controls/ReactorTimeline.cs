using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Siltronic.Wpf.Controls;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Controls {

  public class ReactorTimeline : ScheduleTimeline {

    private ICommand _leftDoubleClick;

    #region Constructors
    static ReactorTimeline() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ReactorTimeline), new FrameworkPropertyMetadata(typeof(ReactorTimeline)));
    }

    public ReactorTimeline() : base() {
      this.SelectionMode = SelectionMode.Single;
      this.IsSynchronizedWithCurrentItem = true;
      DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
      ReactorViewModel vm = DataContext as ReactorViewModel;
      this.InputBindings.Clear();
      if(vm != null){
        InputBindings.Add( 
          new KeyBinding { 
            Key = Key.Delete, 
            Command = vm.DeleteTaskCommand
          });
        _leftDoubleClick = vm.DoubleClickCommand;
      }
    }
    #endregion

    #region Method Overrides
    protected override DependencyObject GetContainerForItemOverride() {
      return new ReactorTask();
    }

    protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e) {
      if (e.LeftButton == MouseButtonState.Pressed && this.SelectedItem != null) {
        TaskViewModel task = this.SelectedItem as TaskViewModel;
        e.Handled = _leftDoubleClick.CanExecute(this.SelectedItem);
        if (e.Handled) _leftDoubleClick.Execute(this.SelectedItem);
      }
      base.OnMouseDoubleClick(e);
    }

    protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e) {
      //base.OnMouseLeftButtonUp(e);
      //if (e.OriginalSource is TimelineTasksPanel) this.SelectedItem = null;
	  var test = e.Source;
      var test2 = e.OriginalSource;

      if (e.OriginalSource is Border && e.Source is Border) 
      	this.SelectedItem = null;
    }
    #endregion
  }
}
