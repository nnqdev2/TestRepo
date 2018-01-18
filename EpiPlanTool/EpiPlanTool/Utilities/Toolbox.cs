﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace EpiPlanTool.Utilities {
  public static class Toolbox {
    public static void RemoveRoutedEventHandlers(UIElement element, RoutedEvent routedEvent) {
      // Get the EventHandlersStore instance which holds event handlers for the specified element.
      // The EventHandlersStore class is declared as internal.
      var eventHandlersStoreProperty = typeof(UIElement).GetProperty(
          "EventHandlersStore", BindingFlags.Instance | BindingFlags.NonPublic);
      object eventHandlersStore = eventHandlersStoreProperty.GetValue(element, null);

      // If no event handlers are subscribed, eventHandlersStore will be null.
      // Credit: http://stackoverflow.com/a/16392387/1149773
      if (eventHandlersStore == null)
        return;

      // Invoke the GetRoutedEventHandlers method on the EventHandlersStore instance 
      // for getting an array of the subscribed event handlers.
      var getRoutedEventHandlers = eventHandlersStore.GetType().GetMethod(
          "GetRoutedEventHandlers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      var routedEventHandlers = (RoutedEventHandlerInfo[])getRoutedEventHandlers.Invoke(
          eventHandlersStore, new object[] { routedEvent });

      // Iteratively remove all routed event handlers from the element.
      foreach (var routedEventHandler in routedEventHandlers)
        element.RemoveHandler(routedEvent, routedEventHandler.Handler);
    }

  }
}
