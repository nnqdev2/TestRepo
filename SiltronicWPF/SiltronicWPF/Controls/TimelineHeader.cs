using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;
using Vis = System.Windows.Visibility;

namespace Siltronic.Wpf.Controls {

  public class TimelineHeader : ItemsControl {

    private static readonly long CELL_LIMIT = TimeSpan.FromDays(365).Ticks;

    private DependencyPropertyDescriptor pdStartProp =
      DependencyPropertyDescriptor.FromProperty(Schedule.StartProperty, typeof(TimelineHeader));

    private DependencyPropertyDescriptor pdEndProp =
      DependencyPropertyDescriptor.FromProperty(Schedule.EndProperty, typeof(TimelineHeader));

    private DependencyPropertyDescriptor pdTickDenisty =
      DependencyPropertyDescriptor.FromProperty(Schedule.TickDensityProperty, typeof(TimelineHeader));

    #region constructors
    static TimelineHeader() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineHeader), new FrameworkPropertyMetadata(typeof(TimelineHeader)));
    }

    public TimelineHeader() {
      pdStartProp.AddValueChanged(this, (s, e) => GenerateCells());
      pdEndProp.AddValueChanged(this, (s, e) => GenerateCells());
      pdTickDenisty.AddValueChanged(this, (s, e) => {
        double width = Interval.Ticks / TickDensity.Ticks;
        this.Visibility = (width < 4) ? Vis.Collapsed : Vis.Visible;
      });
    }
    #endregion

    #region private methods
    private DateTime RoundUp(DateTime dt, TimeSpan d) {
      return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
    }

    private IEnumerable<TimelineHeaderCell> GetHeaderCells() {
      var interval = this.Interval;
      if (interval.Ticks <= 0) { yield break; }
      var round = TimeSpan.FromDays(1);
      DateTime start = RoundUp(Start, round);
      DateTime end = RoundUp(End, round);
      double ticks = (end - start).Ticks;
      if((long)ticks / interval.Ticks > CELL_LIMIT) { yield break; }
      if(ticks <=0 || Double.IsInfinity(ticks / interval.Ticks)) { yield break; }
      TimeSpan remaining = TimeSpan.Zero;
      for(var currTime = Start; currTime < end; currTime += (interval) ) {
        var cell = new TimelineHeaderCell(this, currTime.ToString(DateFormat), currTime);
        yield return cell;
      }
    }

    private void GenerateCells(){
      this.ItemsSource = GetHeaderCells();
    }
    #endregion

    #region Item Container Generator Methods
    protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
      var cell = item as TimelineHeaderCell;
      cell.ToolTip = cell.Timestamp.ToString("MM/dd/yy hh:mm");
    }

    protected override bool IsItemItsOwnContainerOverride(object item) {
      return item is TimelineHeaderCell;
    }
    #endregion

    #region Public Properties
    public DateTime Start {
      get { return (DateTime)GetValue(Schedule.StartProperty); }
      set { SetValue(Schedule.StartProperty, value); }
    }

    public DateTime End {
      get { return (DateTime)GetValue(Schedule.EndProperty); }
      set { SetValue(Schedule.EndProperty, value); }
    }
    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty IntervalProperty =
      DependencyProperty.RegisterAttached("Interval", typeof(TimeSpan), typeof(TimelineHeader),
      new FrameworkPropertyMetadata(
        TimeSpan.Zero,
        FPMO.AffectsRender | FPMO.Inherits,
        (d, e) => {
          var hdr = d as TimelineHeader;
          if (hdr != null) { hdr.GenerateCells(); }
        }
      ));

    public static TimeSpan GetInterval(DependencyObject d) {
      return (TimeSpan)d.GetValue(IntervalProperty);
    }

    public static void SetInterval(DependencyObject d, TimeSpan value) {
      d.SetValue(IntervalProperty, value);
    }

    public TimeSpan Interval {
      get { return (TimeSpan)GetValue(IntervalProperty); }
      set { SetValue(IntervalProperty, value); }
    }

    public TimeSpan TickDensity {
      get { return Schedule.GetTickDensity(this); }
    }

    public static readonly DependencyProperty DateFormatProperty =
      DependencyProperty.Register("DateFormat", typeof(String), typeof(TimelineHeader),
      new FrameworkPropertyMetadata("M/d/y", FPMO.AffectsRender));

    public String DateFormat {
      get { return (String)GetValue(DateFormatProperty); }
      set { SetValue(DateFormatProperty, value); }
    }
    #endregion
  }
}
