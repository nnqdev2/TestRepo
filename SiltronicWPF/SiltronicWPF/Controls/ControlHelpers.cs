using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Siltronic.Wpf.Controls
{
    public static class ControlHelpers
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject {
          if(depObj != null) {
            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
              DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
              if(child != null && child is T) {
                yield return (T)child;
              }
              foreach(T childOfChild in FindVisualChildren<T>(child)) {
                yield return childOfChild;
              }
            }
          }
        }

        public static List<UIElement> GetChildren(this DependencyObject parent, Type targetType)
        {
            List<UIElement> elements = new List<UIElement>();

            int count = VisualTreeHelper.GetChildrenCount(parent);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    UIElement child = (UIElement)VisualTreeHelper.GetChild(parent, i);
                    if (child.GetType() == targetType || targetType.IsAssignableFrom(child.GetType()))
                    {
                        elements.Add(child);
                    }
                    elements.AddRange(GetChildren(child, targetType));
                }
            }
            return elements;
        }

        public static T FindControlWithTag<T>(this DependencyObject parent, string tag) where T : UIElement
        {
            List<UIElement> elements = new List<UIElement>();

            int count = VisualTreeHelper.GetChildrenCount(parent);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (typeof(FrameworkElement).IsAssignableFrom(child.GetType())
                        && ((string)((FrameworkElement)child).Tag == tag))
                    {
                        return child as T;
                    }
                    var item = FindControlWithTag<T>(child, tag);
                    if (item != null) return item as T;
                }
            }
            return null;
        }

        public static T FindParent<T>(FrameworkElement element) where T : class {
          for(
              FrameworkElement element2 = element.TemplatedParent as FrameworkElement; 
              element2 != null; 
              element2 = element2.TemplatedParent as FrameworkElement) {
            T local = element2 as T;
            if(local != null)
              return local;
          }
          return default(T);
        }

        // Searches the visual tree for the element of the specified type
        public static T GetAncestorByType<T>(DependencyObject element) where T : class {
          if(element == null)
            return default(T);

          if(element as T != null)
            return element as T;

          return GetAncestorByType<T>(VisualTreeHelper.GetParent(element));
        }



    }
}
