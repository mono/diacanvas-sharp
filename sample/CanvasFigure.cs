using System;
using Dia;

public class CanvasFigure : CanvasBox {
	
	static GLib.GType gtype;

	static CanvasFigure()
	{
		gtype = RegisterGType (typeof (CanvasFigure));
	}

	public CanvasFigure() : base (gtype)
	{
		ShapeEllipse ellipse = new ShapeEllipse();
		ellipse.Center = new Point (20, 20);
		ellipse.Width = 20;
		ellipse.Height = 20;
		ellipse.LineWidth = 1;
	}
}
