using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Siltronic.Wpf.Controls {

  public class TimelineHeaderCell : Control {

    private Schedule _schedule;
    private Thumb _thumb;
    private Thumb _rightThumb;
    private Thumb _leftThumb;
    private double _delta;

    static TimelineHeaderCell() {
      DefaultStyleKeyProperty.OverrideMetadata(
        typeof(TimelineHeaderCell), 
        new FrameworkPropertyMetadata(typeof(TimelineHeaderCell)));
    }

    public TimelineHeaderCell() {    }

    public TimelineHeaderCell(TimelineHeader parent, String caption, DateTime timestamp) {
      TimelineHeader header = parent;
      Caption = caption;
      Timestamp = timestamp;
    }

    public static readonly DependencyProperty CaptionProperty =
      DependencyProperty.Register("Caption", typeof(String), typeof(TimelineHeaderCell),
      new FrameworkPropertyMetadata(
        String.Empty
      ));

    public String Caption {
      get { return (String)GetValue(CaptionProperty); }
      set { SetValue(CaptionProperty, value); }
    }

    public static readonly DependencyProperty TimestampProperty =
      DependencyProperty.Register("Timestamp", typeof(DateTime), typeof(TimelineHeaderCell),
      new FrameworkPropertyMetadata(
        DateTime.Now
      ));

    public DateTime Timestamp {
      get { return (DateTime)GetValue(TimestampProperty); }
      set { SetValue(TimestampProperty, value); }
    }

    public override void OnApplyTemplate() {
      base.OnApplyTemplate();
      _rightThumb = GetTemplateChild("PART_RightThumb") as Thumb;
      _leftThumb = GetTemplateChild("PART_LeftThumb") as Thumb;
      _rightThumb.Loaded += _thumb_Loaded;
      _leftThumb.Loaded += _thumb_Loaded;
    }

    void _thumb_Loaded(object sender, RoutedEventArgs e) {
      _thumb = sender as Thumb;
      _thumb.DragDelta += _thumb_DragDelta;
      _thumb.DragCompleted += _thumb_DragCompleted;
      _schedule = ControlHelpers.GetAncestorByType<Schedule>(this);
    }

    void _thumb_DragCompleted(object sender, DragCompletedEventArgs e) {
      double prevWidth = ActualWidth;
      double currWidth = ActualWidth + _delta;
      double scale = prevWidth / currWidth;
      if (_schedule != null) _schedule.ScaleTickDensity(scale);
    }

    void _thumb_DragDelta(object sender, DragDeltaEventArgs e) {
      _delta = e.HorizontalChange;
    }

  }
}
