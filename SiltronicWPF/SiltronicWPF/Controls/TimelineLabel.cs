using System;
using System.Windows;
using System.Windows.Controls;

namespace Siltronic.Wpf.Controls {
  public class TimelineLabel : ListBoxItem {
    static TimelineLabel() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineLabel), new FrameworkPropertyMetadata(typeof(TimelineLabel)));
    }

    public static readonly DependencyProperty CaptionProperty =
      DependencyProperty.Register("Caption", typeof(String), typeof(TimelineLabel),
      new FrameworkPropertyMetadata(
        String.Empty
      ));

    public String Caption {
      get { return (String)GetValue(CaptionProperty); }
      set { SetValue(CaptionProperty, value); }
    }

  }

}
