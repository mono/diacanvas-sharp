using System;

using Dia;
using Gtk;
using GtkSharp;

public class Sample {

	static void Main()
	{
       		Application.Init();

		Dia.Canvas canvas = new Dia.Canvas();
		CanvasView view = new CanvasView (canvas, true);

		Tool tool = new PlacementTool (CanvasLine.GType);
		tool.ButtonPressEvent += new DiaSharp.ButtonPressEventHandler (UnsetTool);
		view.Tool = tool;

		ScrolledWindow scrwin = new ScrolledWindow();
		scrwin.SetPolicy (PolicyType.Always, PolicyType.Always);
		scrwin.Add (view);
		
		Window window = new Window ("DiaCanvas C# sample");
		window.SetDefaultSize (300, 225);
		window.DeleteEvent += new DeleteEventHandler (Quit);

		window.Add (scrwin);
		window.ShowAll();
		Application.Run();
	}

	static void UnsetTool (object sender, DiaSharp.ButtonPressEventArgs args)
	{
		Console.WriteLine ("Event worked!");
		args.View.Tool = null;
	}

	static void Quit (object sender, DeleteEventArgs args) 
	{
		Application.Quit();
		args.RetVal = true;
	}
}
