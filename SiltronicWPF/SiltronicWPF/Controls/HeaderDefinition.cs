using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace Siltronic.Wpf.Controls {

  public class HeaderDefinition : FrameworkElement {
    static HeaderDefinition() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderDefinition), new FrameworkPropertyMetadata(typeof(HeaderDefinition)));
    }

    public static readonly DependencyProperty IntervalProperty =
      DependencyProperty.Register("Interval", typeof(TimeSpan), typeof(TimelineHeader),
      new FrameworkPropertyMetadata(
        TimeSpan.Zero,
        FPMO.AffectsRender
      ));

    public TimeSpan Interval {
      get { return (TimeSpan)GetValue(IntervalProperty); }
      set { SetValue(IntervalProperty, value); }
    }

    public static readonly DependencyProperty DateFormatProperty =
      DependencyProperty.Register("DateFormat", typeof(String), typeof(TimelineHeader),
      new FrameworkPropertyMetadata("M/d/y", FPMO.AffectsRender));

    public String DateFormat {
      get { return (String)GetValue(DateFormatProperty); }
      set { SetValue(DateFormatProperty, value); }
    }
  }

  public class HeaderDefinitionsCollection : ObservableCollection<HeaderDefinition> { }
}
