using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Linq;
using System.ComponentModel;
using FPMO = System.Windows.FrameworkPropertyMetadataOptions;

namespace EpiPlanTool.Controls {

  using EpiPlanTool.Adorners;
  using EpiPlanTool.ViewModels;
  using EpiPlanTool.ViewModels.Collections;

  public class ReactorTask : ListBoxItem {

    #region Private Fields
    private AdornerLayer layer;
    private FocusAdorner focusRect;
    #endregion

    #region Constructors
    static ReactorTask() {
      DefaultStyleKeyProperty.OverrideMetadata(
        typeof(ReactorTask), new FrameworkPropertyMetadata(typeof(ReactorTask)));
    }

    public ReactorTask() {
      focusRect = new FocusAdorner(this);
    }
    #endregion

    #region Method Overrides
    protected override void OnGotFocus(RoutedEventArgs e) {
      base.OnGotFocus(e);
      if (layer == null) layer = AdornerLayer.GetAdornerLayer(this);
      if (layer != null) {
        layer.Add(focusRect);
        e.Handled = true;
      }
    }

    protected override void OnLostFocus(RoutedEventArgs e) {
      base.OnLostFocus(e);
      if (layer != null && focusRect != null) {
        layer.Remove(focusRect);
        e.Handled = true;
      }
    }
    #endregion

    #region Public Methods
    internal void OnFocusedTasksChanged(TaskCollection tasks) {
      IsHighlightActive = tasks != null && tasks.Count > 0;
      IsFocusedTask = ((tasks != null) && tasks.Contains(DataContext));
    }
    #endregion

    #region Dependency Properties
    public static readonly DependencyProperty AttachedOrderProperty =
      DependencyProperty.Register("AttachedOrder",
        typeof(BookedOrderViewModel),
        typeof(ReactorTask));

    public BookedOrderViewModel AttachedOrder {
      get { return (BookedOrderViewModel)GetValue(AttachedOrderProperty); }
      set { SetValue(AttachedOrderProperty, value); }
    }

    public static readonly DependencyProperty IsFocusedTaskProperty =
      DependencyProperty.Register("IsFocusedTask",
        typeof(bool),
        typeof(ReactorTask),
        new FrameworkPropertyMetadata(
          false,
          FPMO.AffectsRender
        ));

    public bool IsFocusedTask {
      get { return (bool)GetValue(IsFocusedTaskProperty); }
      private set { SetValue(IsFocusedTaskProperty, value); }
    }

    public static readonly DependencyProperty IsHighlightActiveProperty =
      DependencyProperty.Register("IsHighlightActive",
      typeof(bool),
      typeof(ReactorTask),
      new FrameworkPropertyMetadata(
        false,
        FPMO.AffectsRender
      ));

    public bool IsHighlightActive {
      get { return (bool)GetValue(IsHighlightActiveProperty); }
      private set { SetValue(IsHighlightActiveProperty, value); }
    }
    #endregion
  }
}


