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
		scrolledwindow.Add (view);
		SetupTools();

		main.ShowAll();
	}

	[Glade.Widget] Image image1, image2, image3, image4;
	void SetupTools()
	{
		image1.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/selection.png");
		image2.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/line.png");
		image3.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/box.png");
		//image4.Pixbuf = new Gdk.Pixbuf (null, "pixmaps/text.png");
	}

	void SelectionTool (object sender, EventArgs args)
	{
		view.Tool = new StackTool();
	}

	void LineTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (Dia.CanvasLine.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void BoxTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (Dia.CanvasBox.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void TextTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (Dia.CanvasText.GType);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void UnsetTool (object sender, DiaSharp.ButtonReleaseEventArgs args)
	{
		args.View.Tool = new StackTool();
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
