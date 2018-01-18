using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup.Primitives;
using System.Windows.Media;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace EpiPlanTool.Utilities {

  static class ExtensionMethods {

    public static string ConvertToUnsecureString(this SecureString securePassword) {
      if (securePassword == null)
        throw new ArgumentNullException("securePassword");
      IntPtr unmanagedString = IntPtr.Zero;
      try {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
        return Marshal.PtrToStringUni(unmanagedString);
      }
      finally {
        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
      }
    }

    public static U GetOrInsertNew<T, U>(this Dictionary<T, U> dic, T key) where U : new() {
      if (dic.ContainsKey(key)) return dic[key];
      U newObj = new U();
      dic[key] = newObj;
      return newObj;
    }

    public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
      if (depObj != null) {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
          DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
          if (child != null && child is T) {
            yield return (T)child;
          }
          foreach (T childOfChild in FindVisualChildren<T>(child)) {
            yield return childOfChild;
          }
        }
      }
    }

    public static List<UIElement> GetChildren(this DependencyObject parent, Type targetType) {
      List<UIElement> elements = new List<UIElement>();
      int count = VisualTreeHelper.GetChildrenCount(parent);
      if (count > 0) {
        for (int i = 0; i < count; i++) {
          UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);
          if (child.GetType() == targetType || targetType.IsAssignableFrom(child.GetType())) {
            elements.Add(child);
          }
          elements.AddRange(GetChildren(child, targetType));
        }
      }
      return elements;
    }

    public static T FindControlWithTag<T>(this DependencyObject parent, string tag) where T : UIElement {
      List<UIElement> elements = new List<UIElement>();
      int count = VisualTreeHelper.GetChildrenCount(parent);
      if (count > 0) {
        for (int i = 0; i < count; i++) {
          DependencyObject child = VisualTreeHelper.GetChild(parent, i);
          if (typeof(FrameworkElement).IsAssignableFrom(child.GetType())
              && ((string)((FrameworkElement)child).Tag == tag)) {
            return child as T;
          }
          var item = FindControlWithTag<T>(child, tag);
          if (item != null) return item as T;
        }
      }
      return null;
    }

    public static T FindParent<T>(FrameworkElement element) where T : class {
      for (
          FrameworkElement element2 = element.TemplatedParent as FrameworkElement;
          element2 != null;
          element2 = element2.TemplatedParent as FrameworkElement) {
        T local = element2 as T;
        if (local != null)
          return local;
      }
      return default(T);
    }

    // Searches the visual tree for the element of the specified type
    public static T GetAncestorByType<T>(DependencyObject element) where T : class {
      if (element == null)
        return default(T);
      if (element as T != null)
        return element as T;
      return GetAncestorByType<T>(VisualTreeHelper.GetParent(element));
    }

    public static List<BindingBase> GetBindingObjects(this DependencyObject element) {
      Console.WriteLine("GetBindingObjects -> {0}", element);
      List<BindingBase> bindings = new List<BindingBase>();
      List<DependencyProperty> dpList = new List<DependencyProperty>();
      dpList.AddRange(GetDependencyProperties(element));
      //dpList.AddRange(GetAttachedProperties(element));
      foreach (DependencyProperty dp in dpList) {
        Console.WriteLine("{1}.{0}", dp.Name, element);
        BindingBase b = BindingOperations.GetBindingBase(element as DependencyObject, dp);
        if (b != null) {
          bindings.Add(b);
        }
      }
      int childrenCount = VisualTreeHelper.GetChildrenCount(element);
      Console.WriteLine("{1}.ChildrenCount: {0}", childrenCount, element);
      if (childrenCount > 0) {
        for (int i = 0; i < childrenCount; i++) {
          DependencyObject child = VisualTreeHelper.GetChild(element, i);
          bindings.AddRange(GetBindingObjects(child));
        }
      }
      return bindings;
    }

    public static List<DependencyProperty> GetDependencyProperties(Object element) {
      List<DependencyProperty> properties = new List<DependencyProperty>();
      MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element);
      if (markupObject != null) {
        foreach (MarkupProperty mp in markupObject.Properties) {
          Console.WriteLine("{1} : MarkupProperty = {0}", mp.Name, element);
          if (mp.DependencyProperty != null ) {
            properties.Add(mp.DependencyProperty);
          }
        }
      }
      return properties;
    }

    public static List<DependencyProperty> GetAttachedProperties(Object element) {
      List<DependencyProperty> attachedProperties = new List<DependencyProperty>();
      MarkupObject markupObject = MarkupWriter.GetMarkupObjectFor(element);
      if (markupObject != null) {
        foreach (MarkupProperty mp in markupObject.Properties) {
          if (mp.IsAttached) {
            attachedProperties.Add(mp.DependencyProperty);
          }
        }
      }
      return attachedProperties;
    }

  }


}
