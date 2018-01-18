using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Documents;

namespace EpiPlanTool.Adorners {

  public class ResizeGripAdorner : Adorner {
    public ResizeGripAdorner(UIElement adornedElement) : base(adornedElement) {   }

    protected override void OnRender(DrawingContext drawingContext) {
      Rect adornedElementRect = new Rect(this.AdornedElement.RenderSize);
      Pen pen = new Pen(new SolidColorBrush(Colors.Black), 2.0);
      pen.DashStyle = DashStyles.Dot;
      Rect rect = new Rect(0,0,adornedElementRect.Width,adornedElementRect.Height);
      rect.Inflate(0,-1);
      rect.Offset(-2, 0);
      drawingContext.DrawLine(pen, rect.TopRight, rect.BottomRight);
    }

  }
}


