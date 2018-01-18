using System;
using System.Windows;
using System.Windows.Controls;
using Siltronic.Wpf.Controls;
using Ninject;
using EpiPlanTool.Services;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Views {
  public partial class ScheduleView : UserControl {

    public ScheduleView() {
      InitializeComponent();
    }

    private void Schedule_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e) {
      double prevWidth = ActualWidth;
      double currWidth = ActualWidth + e.Delta;
      double scale = prevWidth / currWidth;
      Schedule.ScaleTickDensity(scale);
    }

    private void TimelineHeader_SizeChanged(object sender, SizeChangedEventArgs e) {
      var hdr = sender as TimelineHeader;
      if (hdr.Name == "hours") {
        if (hdr.TickDensity.TotalMinutes > 2.5) {
          hdr.SetValue(TextBlock.FontWeightProperty, FontWeights.Normal);
          hdr.FontSize = 8;
        }
        else {
          hdr.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
          hdr.FontSize = 9;
        }

        if(hdr.TickDensity.TotalMinutes > 7) {
          hdr.Interval = TimeSpan.FromHours(12);
        }
        else if(hdr.TickDensity.TotalMinutes > 5) {
          hdr.Interval = TimeSpan.FromHours(6);
        }
        else if (hdr.TickDensity.TotalMinutes > 3) {
          hdr.Interval = TimeSpan.FromHours(4);
        }
        else {
          hdr.Interval = TimeSpan.FromHours(1);
        }
      }
    }

  }
}
