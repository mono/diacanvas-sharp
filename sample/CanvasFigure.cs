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
		ellipse.center = new Point (20, 20);
		ellipse.width = 20;
		ellipse.height = 20;
		ellipse.line_width = 1;
	}
}
