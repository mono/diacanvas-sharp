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
		view.Show();

		PlacementTool tool = new PlacementTool (CanvasLine.GType);
		view.Tool = tool;

		ScrolledWindow scrwin = new ScrolledWindow();
		scrwin.SetPolicy (PolicyType.Always, PolicyType.Always);
		scrwin.Add (view);
		
		Window window = new Window ("DiaCanvas C# sample");
		window.SetDefaultSize (300, 225);
		window.Add (scrwin);
		window.ShowAll();
		window.DeleteEvent += new DeleteEventHandler (Quit);

		Application.Run();
	}

	static void Quit (object sender, DeleteEventArgs args) 
	{
		Application.Quit();
		args.RetVal = true;
	}
}
