/// DiaCanvas# sample
/// Copyright (C) 2003 2004  Martin Willemoes Hansen <mwh@sysrq.dk>
///
/// This library is free software; you can redistribute it and/or
/// modify it under the terms of the GNU Lesser General Public
/// License as published by the Free Software Foundation; either
/// version 2.1 of the License, or (at your option) any later version.
///
/// This library is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
/// Lesser General Public License for more details.
///
/// You should have received a copy of the GNU Lesser General Public
/// License along with this library; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections;
using System.Reflection;

using Dia;
using GLib;
using Gtk;
using Gdk;
using Pango;
using Glade;
using Gnome;

public class Sample {

	[Glade.Widget] Gtk.Window main;
	[Glade.Widget] ScrolledWindow scrolledwindow;

	CanvasView view;
	Dia.Canvas canvas;

	public Sample() 
	{
		XML gui = new XML (null, "gui.glade", "main", null);
		gui.Autoconnect (this);

		canvas = new Dia.Canvas();
		canvas.AllowUndo = true;
		view = new CanvasView (canvas, true);
		scrolledwindow.Add (view);

		SetupTools();
		CreateItemsProgramatically();
		main.ShowAll();
	}

	[Glade.Widget] Gtk.Image image1, image2, image3, image4, image5;
	void SetupTools()
	{
		image1.Pixbuf = new Pixbuf (null, "selection.png");
		image2.Pixbuf = new Pixbuf (null, "gimp-zoom.png");
		image3.Pixbuf = new Pixbuf (null, "line.png");
		image4.Pixbuf = new Pixbuf (null, "box.png");
		image5.Pixbuf = new Pixbuf (null, "glade-image.png");
	}

	void CreateItemsProgramatically() {
		// Adding image
		CanvasImage image = new CanvasImage (new Pixbuf (null, "logo.png"));
		image.Move (50, 50);
		canvas.Root.Add (image);

		// Adding textbox
		CanvasTextBox textbox = new CanvasTextBox();
		textbox.Move (50, 150);
		canvas.Root.Add (textbox);

		// Adding text
		Dia.CanvasText text = new Dia.CanvasText();
		text.Text = "Hello, World!";
		text.Font = FontDescription.FromString ("sans 20");
		text.Height = 50;
		text.Width = 190;
		text.Move (300, 150);
		canvas.Root.Add (text);

		// Adding line
		Dia.CanvasLine line = new Dia.CanvasLine();
		line.LineWidth = 10;
		line.Color = 8327327;
		DashStyle style = new DashStyle (10);
		line.Dash = style;
		line.HeadPos = new Dia.Point (50, 70);;
		line.TailPos = new Dia.Point (100, 170);
		line.Cap = Dia.CapStyle.Butt;
		line.Move (50, 150);
		canvas.Root.Add (line);

		// Adding box
		CanvasBox box = new CanvasBox();
		box.BorderWidth = 8.5;
		box.Color = 2134231;
		box.Move (250, 225);
		canvas.Root.Add (box);

		// Adding custom clock
		CanvasClock clock = new CanvasClock ();
		clock.Width = 100;
		clock.Height = 100;
		clock.Move (450, 225);
		canvas.Root.Add (clock);
		
		// Adding custom figure
		CanvasFigure figure = new CanvasFigure();
		figure.Move (50, 325);
		canvas.Root.Add (figure);

		view.UnselectAll();
		CanvasViewItem vitem = view.FindViewItem (image);
		view.Focus (vitem);
	}

	void SelectionTool (object sender, EventArgs args)
	{
		view.Tool = new StackTool();
	}

	bool zoom_enabled = false;
	void ZoomTool (object sender, EventArgs args)
	{
		zoom_enabled = !zoom_enabled;

		if (zoom_enabled) {
			view.Tool = new Tool (IntPtr.Zero);
			view.ButtonPressEvent += new Gtk.ButtonPressEventHandler (Zoom);
		} else
			view.ButtonPressEvent -= new Gtk.ButtonPressEventHandler (Zoom);		
	}

	void LineTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (Dia.CanvasLine), 
					       "LineWidth", 8, 
					       "Color", 480975UL); 
		view.Tool.ButtonPressEvent += new Dia.ButtonPressEventHandler (UnsetTool);
	}

	void BoxTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (CanvasBox));
		view.Tool.ButtonPressEvent += new Dia.ButtonPressEventHandler (UnsetTool);
	}

	void ImageTool (object sender, EventArgs args)
	{
		Pixbuf pixbuf = new Pixbuf (null, "logo.png");
		view.Tool = new PlacementTool (typeof (CanvasImage), 
					       "Image", pixbuf,
					       "Width", pixbuf.Width,
					       "Height", pixbuf.Height);
		view.Tool.ButtonPressEvent += new Dia.ButtonPressEventHandler (UnsetTool);
	}

	[Glade.Widget] RadioButton tool1;
	void UnsetTool (object sender, Dia.ButtonPressEventArgs args)
	{
		if ((args.Button.State & ModifierType.ControlMask) == 
		    ModifierType.ControlMask)
			return;

		tool1.Active = true;
	}

	void Zoom (object sender, Gtk.ButtonPressEventArgs args)
	{
		
		if ((args.Event.State & ModifierType.ControlMask) == 
		    ModifierType.ControlMask)
			ZoomOut (this, null);
		else
			ZoomIn (this, null);
	}

	void ExportSVG (object sender, EventArgs args)
	{
		ExportSVG svg = new ExportSVG();
		svg.Render (canvas);

		try {
			svg.Save ("./test.svg");
		} catch (Exception e) {
			Console.WriteLine (e);
		}
	}

	void SelectAll (object sender, EventArgs args)
	{
		view.SelectAll();
	}

	void UnselectAll (object sender, EventArgs args)
	{
		view.UnselectAll();
	}

	void DeleteSelectedItems (object sender, EventArgs args)
	{
		foreach (CanvasViewItem view_item in view.SelectedItems) {
			view_item.Item.Parent.Remove (view_item.Item);
		}
	}

	void Print (object sender, EventArgs args)
	{
		PrintJob pj = new PrintJob();
		PrintDialog dialog = new PrintDialog (pj, "Print diagram");
		int response = dialog.Run();
		
		if (response == (int) ResponseType.Cancel ||
		    response == (int) ResponseType.DeleteEvent) {
			dialog.Destroy();
			return;
		}

		PrintContext ctx = pj.Context;
		Gnome.Print.Beginpage (ctx, "demo"); 
		Dia.Global.ExportPrint (pj, canvas);
		Gnome.Print.Showpage (ctx);
		pj.Close();
		
		switch (response) {
		case (int)PrintButtons.Print: 
			if (pj.Config.Get ("Settings.Transport.Backend") == "file")
				pj.PrintToFile (pj.Config.Get ("Settings.Transport.Backend.FileName"));
			pj.Print(); 
			break;
		case (int) PrintButtons.Preview:
			new PrintJobPreview (pj, "Diagram").Show();
			break;
		}
		dialog.Destroy();
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

	void About (object sender, EventArgs args)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();

		string [] authors = new String [] {
			"Martin Willemoes Hansen <mwh@sysrq.dk> (Maintainer and founder)",
			"Mario Fuentes <mario@gnome.cl>",
		};

		string [] documenters = new String [] {};
		string translators = null;
		Pixbuf pixbuf = new Pixbuf(null, "logo.png");
			
		new Gnome.About ("DiaCanvas# Sample", assembly.GetName().Version.ToString(),
				 @"Copyright (C) 2003 2004 Martin Willemoes Hansen
DiaCanvas# Sample comes with ABSOLUTELY NO WARRANTY;
This is free software, and you are welcome to
redistribute it under certain conditions;
see the text file: COPYRIGHT, distributed
with this program.",
				 "DiaCanvas, Mono and Gtk# rock!", 
				 authors, documenters, translators, pixbuf).Show();
	}

	void Quit (object sender, EventArgs args) { Application.Quit(); }

	static void Main()
	{
		Application.Init();
		DiaCanvas.Init();
		new Sample();
		Application.Run();
	}
}
