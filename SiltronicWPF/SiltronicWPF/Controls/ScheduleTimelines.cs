using System.Windows;
using System.Windows.Controls;

namespace Siltronic.Wpf.Controls {

  public class ScheduleTimelines : ListBox {
    private Schedule _schedule;

    static ScheduleTimelines() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ScheduleTimelines), new FrameworkPropertyMetadata(typeof(ScheduleTimelines)));
    }

    public ScheduleTimelines(Schedule schedule) {
      _schedule = schedule;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
      base.PrepareContainerForItemOverride(element, item);
      var elem = element as FrameworkElement;
    }

    protected override bool IsItemItsOwnContainerOverride(object item) {
      return item is ScheduleTimeline;
    }

    protected override DependencyObject GetContainerForItemOverride() {
      return _schedule.GetTimelineControl();
    }

  }
}
