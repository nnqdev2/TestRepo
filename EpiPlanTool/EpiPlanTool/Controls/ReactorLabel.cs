using System;
using System.Windows;
using System.Windows.Controls;
using Siltronic.Wpf.Controls;

namespace EpiPlanTool.Controls {
  public class ReactorLabel : TimelineLabel  {
    static ReactorLabel() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ReactorLabel), new FrameworkPropertyMetadata(typeof(ReactorLabel)));
    }
  }
}
