using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;
using System.Collections;

namespace Siltronic.Wpf.Controls {

  public class Schedule : ItemsControl {

    private static readonly TimeSpan TS_ROUND = TimeSpan.FromSeconds(15);

    #region Private Fields
    private Grid _layoutRoot;
    private ContentControl _labelsContainer;
    private Border _headersContainer;
    private StackPanel _headersPanel;
    private List<TimelineHeader> _headers;
    private ContentControl _timelinesContainer;
    private ItemsControl _labels;
    private ItemsControl _timelines;
    private StackPanel _fixedColumn;
    private Border _scrollColumn;
    #endregion

    static Schedule() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Schedule), new FrameworkPropertyMetadata(typeof(Schedule)));
    }

    public Schedule() {
      _headers = new List<TimelineHeader>();
    }

    #region Private/Internal Methods
    private static TimeSpan RoundTimespan(TimeSpan t1, TimeSpan t2) {
      return new TimeSpan(((t1.Ticks + t2.Ticks - 1) / t2.Ticks) * t2.Ticks);
    }

    #endregion

    #region Base Class Overrides
    protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue) {
      base.OnItemsSourceChanged(oldValue, newValue);
      if (_labels != null) _labels.ItemsSource = newValue;
      if (_timelines != null) _timelines.ItemsSource = newValue;
      RefreshLayout();
    }

    public override void OnApplyTemplate() {
      _layoutRoot = (Grid)GetTemplateChild("PART_LayoutRoot");
      _fixedColumn = (StackPanel)GetTemplateChild("PART_FixedColumn");
      _scrollColumn = (Border)GetTemplateChild("PART_ScrollColumn");
      _labelsContainer = (ContentControl)GetTemplateChild("PART_Labels");
      _timelinesContainer = (ContentControl)GetTemplateChild("PART_Timelines");
      _headersContainer = (Border)GetTemplateChild("PART_HeadersContainer");
      _headersPanel = (StackPanel)GetTemplateChild("PART_Headers");
      _labels = new TimelineLabels(this);
      _timelines = new ScheduleTimelines(this);
      if (_labelsContainer != null) _labelsContainer.Content = _labels;
      if (_timelinesContainer != null) _timelinesContainer.Content = _timelines;
      if (_headersPanel != null) {       /* Hookup headers */
        foreach (var th in _headers)
          _headersPanel.Children.Add(th);
      }
    }
    #endregion

    #region Public Virtual Methods
    public virtual ItemsControl GetTimelineControl() {
      return new ScheduleTimeline();
    }

    public virtual Control GetLabelControl() {
      return new TimelineLabel();
    }
    #endregion

    #region Conversion functions
    public void RefreshLayout() {
      if (_scrollColumn != null && _headers != null && _headersContainer != null) {
        _labelsContainer.Margin = new Thickness(0, _headersPanel.ActualHeight, 0, 0);
      }
      foreach (var panel in ControlHelpers.FindVisualChildren<TimelineHeaderPanel>(this))
        panel.InvalidateMeasure();
      foreach (var panel in ControlHelpers.FindVisualChildren<TimelineTasksPanel>(this))
        panel.InvalidateMeasure();
    }

    #endregion

    #region public Dependency Properties
    public List<TimelineHeader> Headers {
      get { return _headers; }
    }

    public static readonly DependencyProperty LabelWidthProperty =
      DependencyProperty.Register("LabelWidth", typeof(double), typeof(Schedule),
      new FrameworkPropertyMetadata(
        80d,
        FPMO.AffectsMeasure | FPMO.AffectsArrange,
        (d, e) => { }
      ));

    public double LabelWidth {
      get { return (double)GetValue(LabelWidthProperty); }
      set { SetValue(LabelWidthProperty, value); }
    }

    public static readonly DependencyProperty StartProperty =
      DependencyProperty.RegisterAttached("Start", typeof(DateTime), typeof(Schedule),
      new FrameworkPropertyMetadata(
        DateTime.Now,
        FPMO.Inherits | FPMO.AffectsMeasure | FPMO.AffectsArrange
      ));

    public static DateTime GetStart(DependencyObject d) {
      return (DateTime)d.GetValue(StartProperty);
    }

    public static void SetStart(DependencyObject d, DateTime value) {
      d.SetValue(StartProperty, value);
    }

    public DateTime Start {
      get { return GetStart(this); }
      set { SetStart(this, value); }
    }

    public static readonly DependencyProperty EndProperty =
      DependencyProperty.RegisterAttached("End", typeof(DateTime), typeof(Schedule),
      new FrameworkPropertyMetadata(
        DateTime.Now,
        FPMO.Inherits | FPMO.AffectsMeasure | FPMO.AffectsArrange
      ));

    public static DateTime GetEnd(DependencyObject d) {
      return (DateTime)d.GetValue(EndProperty);
    }

    public static void SetEnd(DependencyObject d, DateTime value) {
      d.SetValue(EndProperty, value);
    }

    public DateTime End {
      get { return GetEnd(this); }
      set { SetEnd(this, value); }
    }

    public static readonly DependencyProperty TickDensityProperty =
      DependencyProperty.RegisterAttached("TickDensity", 
        typeof(TimeSpan), 
        typeof(Schedule),
        new FrameworkPropertyMetadata(
          new TimeSpan(0,5,0),
          FPMO.Inherits | FPMO.AffectsMeasure | FPMO.AffectsArrange,
          null,
          (d, val) => {
            //return Schedule.RoundTimespan((TimeSpan)val, TS_ROUND);
            return val;
          }
      ));

    public static TimeSpan GetTickDensity(DependencyObject d) {
      return (TimeSpan)d.GetValue(TickDensityProperty);
    }

    public static void SetTickDensity(DependencyObject d, TimeSpan value) {
      d.SetValue(TickDensityProperty, value);
    }

    public TimeSpan TickDensity {
      get { return GetTickDensity(this); }
      set { SetTickDensity(this, value); }
    }
    #endregion

    public void ScaleTickDensity(double scale){
      TickDensity = new TimeSpan((long)(TickDensity.Ticks * scale));
    }
  }

}
