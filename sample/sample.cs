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
		
		Window window = new Window ("DiaCanvas C# sample");
		window.Add (view);
		window.Show();
		window.DeleteEvent += new DeleteEventHandler (Quit);

		Application.Run();
	}

	static void Quit (object sender, DeleteEventArgs args) 
	{
		Application.Quit();
		args.RetVal = true;
	}
}
