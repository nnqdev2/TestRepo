using System;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.Generic;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace Siltronic.Wpf.Controls {

  public class TimelineTasksPanel : StackPanel {

    #region Constructors
    static TimelineTasksPanel() {
      DefaultStyleKeyProperty.OverrideMetadata(
        typeof(TimelineTasksPanel), 
        new FrameworkPropertyMetadata(typeof(TimelineTasksPanel)));
    }

    public TimelineTasksPanel() {
      this.Orientation = Orientation.Horizontal;
    }
    #endregion

    #region protected override
    protected override Size MeasureOverride(Size availableSize) {
      Size desiredSize = new Size(0, 0);
      foreach (UIElement child in InternalChildren) {
        TimeSpan range = GetEnd(child) - GetStart(child);
        double width = range.TotalSeconds / TickDensity.TotalSeconds;
        if (width < 0) width = 0;
        desiredSize.Width = width;
        child.Measure(desiredSize);
      }
      TimeSpan schedRange = Schedule.GetEnd(this) - Schedule.GetStart(this);
      desiredSize.Width = schedRange.Ticks / TickDensity.Ticks;
      return desiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize) {
      Size desiredSize = new Size(0, 0);
      DateTime Start = Schedule.GetStart(this);
      double offset = 0;
      if (DateTime.Now > Start)
        offset = -((DateTime.Now - Start).TotalSeconds / TickDensity.TotalSeconds);
      foreach (UIElement child in InternalChildren) {
        DateTime startTime = GetStart(child);
        DateTime endTime = GetEnd(child);
        DateTime start = Schedule.GetStart(this);
        var range = endTime - startTime;
        double left = offset + 
          (startTime - start).TotalSeconds / TickDensity.TotalSeconds;
        double width = range.TotalSeconds / TickDensity.TotalSeconds;
        if (width < 0) width = 0;
        Rect rect = new Rect(new Point(left, 0), new Size(width, finalSize.Height));
        child.Arrange(rect);
      }
      TimeSpan schedRange = Schedule.GetEnd(this) - Schedule.GetStart(this);
      finalSize.Width = schedRange.Ticks / TickDensity.Ticks;
      return finalSize;
    }
    #endregion

    #region Public Methods 
    public void RefreshLayout() {
      InvalidateMeasure();
      UpdateLayout();
    }
    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty StartProperty = DependencyProperty.RegisterAttached(
      "Start", 
      typeof(DateTime),
      typeof(TimelineTasksPanel),
      new FrameworkPropertyMetadata(
        DateTime.Now,
        FPMO.AffectsArrange | FPMO.AffectsMeasure,
        (d, e) => {
          var panel = ControlHelpers.GetAncestorByType<TimelineTasksPanel>(d);
          if (panel != null) panel.RefreshLayout();
        }
      )
    );

    public static readonly DependencyProperty EndProperty = DependencyProperty.RegisterAttached(
      "End", 
      typeof(DateTime),
      typeof(TimelineTasksPanel),
      new FrameworkPropertyMetadata(
        DateTime.Now,
        FPMO.AffectsArrange | FPMO.AffectsMeasure,
        (d, e) => {
          var panel = ControlHelpers.GetAncestorByType<TimelineTasksPanel>(d);
          if(panel != null) panel.RefreshLayout();
        }
      )
    );

    public static DateTime GetStart(UIElement element) {
      if (element == null) { throw new ArgumentNullException("element"); }
      return (DateTime)element.GetValue(StartProperty);
    }

    public static void SetStart(UIElement element, DateTime start) {
      if (element == null) { throw new ArgumentNullException("element"); }
      element.SetValue(StartProperty, start);
    }

    public static DateTime GetEnd(UIElement element) {
      if (element == null) { throw new ArgumentNullException("element"); }
      return (DateTime)element.GetValue(EndProperty);
    }

    public static void SetEnd(UIElement element, DateTime end) {
      if (element == null) { throw new ArgumentNullException("element"); }
      element.SetValue(EndProperty, end);
    }

    public TimeSpan Interval {
      get { return TimelineHeader.GetInterval(this); }
    }

    public TimeSpan TickDensity {
      get { return Schedule.GetTickDensity(this); }
    }
    #endregion
  }

}
