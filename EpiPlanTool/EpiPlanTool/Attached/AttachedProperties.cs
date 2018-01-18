using System;
using System.Windows;
using System.Windows.Data;

namespace EpiPlanTool.AttachedProperties {

  public static class AttachedStrings {

    public static readonly DependencyProperty PropertyNameProperty =
      DependencyProperty.RegisterAttached(
        "PropertyName",
        typeof(string),
        typeof(AttachedStrings)
      );

    [AttachedPropertyBrowsableForChildren]
    public static string GetPropertyName(DependencyObject d) {
      return (string)d.GetValue(PropertyNameProperty);
    }

    [AttachedPropertyBrowsableForChildren]
    public static void SetPropertyName(DependencyObject d, object value) {
      d.SetValue(PropertyNameProperty, value);
    }

  }
}
