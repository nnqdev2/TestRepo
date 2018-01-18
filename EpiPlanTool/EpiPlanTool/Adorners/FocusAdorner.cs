using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;

namespace EpiPlanTool.Adorners {
  class FocusAdorner : Adorner {

    public FocusAdorner(UIElement adornedElement) : base(adornedElement) {
      this.IsHitTestVisible = false;
    }

    protected override void OnRender(DrawingContext drawingContext) {
      Rect elemRect = new Rect(this.AdornedElement.RenderSize);
      SolidColorBrush brush = new SolidColorBrush(Colors.Transparent);
      Pen pen = new Pen(new SolidColorBrush(Colors.Black), .75);
      pen.DashStyle = DashStyles.Dash;
      Rect rect = new Rect(0, 0, elemRect.Width, elemRect.Height);
      rect.Inflate(-1,-1);
      drawingContext.DrawRectangle(brush, pen, rect);
    }

  }
}
