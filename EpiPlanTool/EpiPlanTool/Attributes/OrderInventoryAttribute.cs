using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using EpiPlanTool.ViewModels;

namespace EpiPlanTool.Attributes {

  [AttributeUsage(AttributeTargets.Property)]
  public class OrderInventoryAttribute : System.Attribute, IItemsSource {

    private static List<PropertyInfo> _properties;
    private static ItemCollection _names;

    public string Caption { get; set; }

    static OrderInventoryAttribute() {
      _properties = (
        from prop in typeof(BookedOrderViewModel).GetProperties()
        where
          prop.IsDefined(typeof(OrderInventoryAttribute))
       select
         prop
      ).ToList();
    }

    public OrderInventoryAttribute() {
    }

    public OrderInventoryAttribute(string caption) {
      Caption = caption;
    }

    public static List<PropertyInfo> WipProperties {
      get { return _properties; }
    }

    public ItemCollection GetValues() {
      if (_names == null) {
        _names = new ItemCollection();
        foreach (var prop in _properties) _names.Add(prop.Name);
      }
      return _names;
    }

  }

}