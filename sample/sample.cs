using System;

using Dia;
using Gtk;
using GtkSharp;
using Glade;

public class Sample {

	[Glade.Widget] Window main;
	[Glade.Widget] ScrolledWindow scrolledwindow;

	CanvasView view;

	public Sample() 
	{
		XML gui = new XML ("glade/gui.glade", "main", null);
		gui.Autoconnect (this);

		Dia.Canvas canvas = new Dia.Canvas();
		view = new CanvasView (canvas, true);
		main.KeyPressEvent += new KeyPressEventHandler (CtrlPressed);
		main.KeyReleaseEvent += new KeyReleaseEventHandler (CtrlReleased);
		scrolledwindow.Add (view);
		SetupTools();

		main.ShowAll();
	}

	[Glade.Widget] Image image1, image2, image3, image4, image5;
	void SetupTools()
	{
		image1.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/selection.png");
		image2.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/gimp-zoom.png");
		image3.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/line.png");
		image4.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/box.png");
		image5.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/text.png");
	}

	void SelectionTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new StackTool();
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

	void ZoomTool (object sender, EventArgs args)
	{
		ToolCleanUp();
		view.Tool = new Tool (IntPtr.Zero);
		view.ButtonPressEvent += new ButtonPressEventHandler (Zoom);
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
			view.Zoom -= 0.2;

		view.Zoom += 0.1;
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
