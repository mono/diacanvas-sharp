using System;

using Dia;
using GLib;
using Gtk;
using GtkSharp;
using Gdk;
using Glade;
using Gnome;

public class Sample {

	[Glade.Widget] Gtk.Window main;
	[Glade.Widget] ScrolledWindow scrolledwindow;

	CanvasView view;
	Dia.Canvas canvas;

	public Sample() 
	{
		XML gui = new XML (null, "glade/gui.glade", "main", null);
		gui.Autoconnect (this);

		canvas = new Dia.Canvas();
		canvas.AllowUndo = true;
		view = new CanvasView (canvas, true);
		main.KeyPressEvent += new KeyPressEventHandler (CtrlPressed);
		main.KeyReleaseEvent += new KeyReleaseEventHandler (CtrlReleased);
		scrolledwindow.Add (view);
		SetupTools();

		main.ShowAll();
	}

	[Glade.Widget] Gtk.Image image1, image2, image3, image4, image5;
	void SetupTools()
	{
		image1.Pixbuf = new Pixbuf (null, "pixmaps/selection.png");
		image2.Pixbuf = new Pixbuf (null, "pixmaps/gimp-zoom.png");
		image3.Pixbuf = new Pixbuf (null, "pixmaps/line.png");
		image4.Pixbuf = new Pixbuf (null, "pixmaps/box.png");
		image5.Pixbuf = new Pixbuf (null, "pixmaps/text.png");
	}

	void SelectionTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new StackTool();
	}

	void ZoomTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new Tool (IntPtr.Zero);
		view.ButtonPressEvent += new ButtonPressEventHandler (Zoom);
	}

	void LineTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new PlacementTool (Dia.CanvasLine.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void BoxTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new PlacementTool (Dia.CanvasBox.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void TextTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new PlacementTool (Dia.CanvasText.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void ToolCleanUp()
	{
		view.ButtonPressEvent -= new ButtonPressEventHandler (Zoom);
	}

	[Glade.Widget] RadioButton tool1;

	void UnsetTool (object sender, DiaSharp.ButtonReleaseEventArgs args)
	{
		if (ctrl)
			return;

		tool1.Active = true;
	}

	void Zoom (object sender, ButtonPressEventArgs args)
	{
		if (ctrl)
			ZoomIn (this, null);
		else
			ZoomOut (this, null);
	}

	void ExportSVG (object sender, EventArgs args)
	{
		ExportSVG svg = new ExportSVG();
		svg.Render (canvas);

		try {
			svg.Save ("test.svg");
		} catch (GException ex) {
			Console.WriteLine (ex);
		}
	}

	void Undo (object sender, EventArgs args) { canvas.PopUndo(); }
	void Redo (object sender, EventArgs args) { canvas.PopRedo(); }
	
	void ZoomIn (object sender, EventArgs args) { view.Zoom += 0.1;	}

	void ZoomOut (object sender, EventArgs args) { view.Zoom -= 0.1; }

	void Zoom100 (object sender, EventArgs args) { view.Zoom = 1; }

	void SnapToGrid (object sender, EventArgs args)
	{
		canvas.SnapToGrid = !canvas.SnapToGrid;
	}

	bool ctrl = false;
	void CtrlPressed (object sender, KeyPressEventArgs args)
	{
		if (args.Event.keyval == 65507)
			ctrl = true;
	}

	void CtrlReleased (object sender, KeyReleaseEventArgs args)
	{
		if (args.Event.keyval == 65507)
			ctrl = false;
	}

	void About (object sender, EventArgs args)
	{
		string [] authors = new String [] {
			"Martin Willemoes Hansen",
		};

		string [] documenters = new String [] {};
		string translators = null;
		Pixbuf pixbuf = new Pixbuf(null, "pixmaps/logo.png");
			
		new Gnome.About ("DiaCanvas# Sample", "0.1",
				 @"Copyright (C) 2003 Martin Willemoes Hansen
DiaCanvas# Sample comes with ABSOLUTELY NO WARRANTY;
This is free software, and you are welcome to
redistribute it under certain conditions;
see the text file: COPYRIGHT, distributed
with this program.",
				 "DiaCanvas, Mono and Gtk# rocks!", 
				 authors, documenters, translators, pixbuf).Show();
	}

	void Quit (object sender, EventArgs args)
	{
		Application.Quit();
	}

	static void Main()
	{
		Application.Init();
		new Sample();
		Application.Run();
	}
}
