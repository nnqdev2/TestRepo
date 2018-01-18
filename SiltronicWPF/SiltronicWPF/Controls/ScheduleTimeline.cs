using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Siltronic.Wpf.Controls {
  public class ScheduleTimeline : ListBox {

    #region Constructors
    static ScheduleTimeline() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ScheduleTimeline), new FrameworkPropertyMetadata(typeof(ScheduleTimeline)));
    }
    #endregion

    #region Method Overrides
    protected override void OnMouseDoubleClick(MouseButtonEventArgs e) {
      if (e.LeftButton == MouseButtonState.Pressed) {
        if (this.SelectedItem != null) {
          RaiseEvent(new RoutedEventArgs(SelectedItemDoubleClickedEvent, this.SelectedItem));
        }
        e.Handled = true;
      }
      base.OnMouseDoubleClick(e);
    }
    #endregion

    #region Routed Events
    public static readonly RoutedEvent SelectedItemDoubleClickedEvent =
      EventManager.RegisterRoutedEvent("SelectedItemDoubleClicked", RoutingStrategy.Tunnel,
      typeof(RoutedEventHandler), typeof(ScheduleTimeline));

    public event RoutedEventHandler SelectedItemDoubleClicked {
      add { AddHandler(SelectedItemDoubleClickedEvent, value); }
      remove { RemoveHandler(SelectedItemDoubleClickedEvent, value); }
    }
    #endregion

  }
}



