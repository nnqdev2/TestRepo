using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Siltronic.Wpf.Controls {
  public class TimelineLabels : ListBox {

    private static Type ItemType { get; set; }
    private Schedule _schedule;

    static TimelineLabels() {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(TimelineLabels), new FrameworkPropertyMetadata(typeof(TimelineLabels)));
    }

    public TimelineLabels(Schedule schedule) {
      _schedule = schedule;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
      base.PrepareContainerForItemOverride(element, item);
      var elem = element as FrameworkElement;
    }

    protected override bool IsItemItsOwnContainerOverride(object item) {
      return item is TimelineLabel;
    }

    protected override DependencyObject GetContainerForItemOverride() {
      return _schedule.GetLabelControl();
    }

  }

 
}
