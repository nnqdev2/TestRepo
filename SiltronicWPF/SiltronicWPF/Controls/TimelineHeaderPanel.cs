using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Siltronic.Wpf.Controls {
  public class TimelineHeaderPanel : StackPanel {

    private DependencyPropertyDescriptor pdTickDenisty =
      DependencyPropertyDescriptor.FromProperty(Schedule.TickDensityProperty, typeof(TimelineHeaderPanel));

    static TimelineHeaderPanel() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineHeaderPanel), 
        new FrameworkPropertyMetadata(typeof(TimelineHeaderPanel)));
    }

    public TimelineHeaderPanel() {
      pdTickDenisty.AddValueChanged(this, (s, e) => InvalidateMeasure());
      this.UseLayoutRounding = true;
    }

    #region protected override
    protected override Size MeasureOverride(Size availableSize) {
      Size desiredSize = new Size(0, availableSize.Height);
      if (InternalChildren.Count > 0) {
        double width = (Interval.TotalSeconds / TickDensity.TotalSeconds);
        desiredSize.Width = width;
        foreach (TimelineHeaderCell child in InternalChildren) {
          child.Measure(desiredSize);
          if (desiredSize.Height == double.PositiveInfinity) desiredSize.Height = child.DesiredSize.Height;
          desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
        }
        desiredSize.Width = width * InternalChildren.Count;
      }
      return desiredSize;
    }

    protected override Size ArrangeOverride(Size finalSize) {
      Size arrangedSize = new Size(0, finalSize.Height);
      if (InternalChildren.Count > 0) {
        double left = 0;
        if(DateTime.Now > Start)
          left = -((DateTime.Now - Start).TotalSeconds / TickDensity.TotalSeconds); 
        double width = (Interval.TotalSeconds / TickDensity.TotalSeconds);
        arrangedSize.Width = width;
        foreach (TimelineHeaderCell child in InternalChildren) {
          Rect rect = new Rect(new Point(left, 0), new Size(width, finalSize.Height));
          child.Arrange(rect);
          left += width;
        }
        arrangedSize.Width = width * InternalChildren.Count;
      }
      return arrangedSize;
    }
    #endregion

    public TimeSpan Interval {
      get { return TimelineHeader.GetInterval(this); }
    }

    public TimeSpan TickDensity {
      get { return Schedule.GetTickDensity(this); }
    }

    public DateTime Start {
      get { return Schedule.GetStart(this); }
    }

    public DateTime End {
      get { return Schedule.GetEnd(this); }
    }

  }
}
