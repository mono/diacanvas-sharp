/// DiaCanvas# sample
///
/// Copyright (C) 2003  Martin Willemoes Hansen <mwh@sysrq.dk>
/// 
/// This program is free software; you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation; either version 2 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections;

using Dia;
using GLib;
using Gtk;
using GtkSharp;
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
		XML gui = new XML (null, "glade/gui.glade", "main", null);
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
		image1.Pixbuf = new Pixbuf (null, "pixmaps/selection.png");
		image2.Pixbuf = new Pixbuf (null, "pixmaps/gimp-zoom.png");
		image3.Pixbuf = new Pixbuf (null, "pixmaps/line.png");
		image4.Pixbuf = new Pixbuf (null, "pixmaps/box.png");
		image5.Pixbuf = new Pixbuf (null, "pixmaps/glade-image.png");
	}

	void CreateItemsProgramatically() {
		Dia.CanvasLine line = new Dia.CanvasLine();
		line.LineWidth = 10;
		line.Color = 8327327;
		DashStyle style = new DashStyle (10);
		line.Dash = style;
		line.HeadPos = new Dia.Point (50, 50);;
		line.TailPos = new Dia.Point (100, 150);
		line.Cap = Dia.CapStyle.Butt;
		line.Move (100, 150);
		canvas.Root.Add (line);

		CanvasBox box = new CanvasBox();
		box.Move (300, 200);
		box.BorderWidth = 8.5;
		box.Color = 2134231;
		canvas.Root.Add (box);

		Dia.CanvasText text = new Dia.CanvasText();
		text.Move (250, 150);
		text.Text = "Hello, World!";
		text.Font = FontDescription.FromString ("sans 20");
		text.Height = 50;
		text.Width = 200;
		canvas.Root.Add (text);

		CanvasTextBox textbox = new CanvasTextBox();
		textbox.Move (50, 150);
		canvas.Root.Add (textbox);

		CanvasImage image = new CanvasImage (new Pixbuf (null, "pixmaps/logo.png"));
		image.Move (50, 50);
		canvas.Root.Add (image);

	        view.UnselectAll();
		// Trigers a bug, same as in gtksourceview-sharp
		//CanvasViewItem vitem = view.FindViewItem (image);
		//view.Focus (vitem);
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
			view.ButtonPressEvent += new ButtonPressEventHandler (Zoom);
		} else
			view.ButtonPressEvent -= new ButtonPressEventHandler (Zoom);		
	}

	void LineTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (Dia.CanvasLine), 
					       "LineWidth", 8, 
					       "Color", 480975UL); 
		view.Tool.ButtonPressEvent += new DiaSharp.ButtonPressEventHandler (UnsetTool);
	}

	void BoxTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (CanvasBox));
		view.Tool.ButtonPressEvent += new DiaSharp.ButtonPressEventHandler (UnsetTool);
	}

	void ImageTool (object sender, EventArgs args)
	{
		Pixbuf pixbuf = new Pixbuf (null, "pixmaps/logo.png");
		view.Tool = new PlacementTool (typeof (CanvasImage), 
					       "Image", pixbuf,
					       "Width", pixbuf.Width,
					       "Height", pixbuf.Height);
		view.Tool.ButtonPressEvent += new DiaSharp.ButtonPressEventHandler (UnsetTool);
	}

	[Glade.Widget] RadioButton tool1;
	void UnsetTool (object sender, DiaSharp.ButtonPressEventArgs args)
	{
		if ((args.Button.state & (uint)ModifierType.ControlMask) == 
		    (uint)ModifierType.ControlMask)
			return;

		tool1.Active = true;
	}

	void Zoom (object sender, ButtonPressEventArgs args)
	{
		
		if ((args.Event.state & (uint)ModifierType.ControlMask) == 
		    (uint)ModifierType.ControlMask)
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

	void Quit (object sender, EventArgs args) { Application.Quit(); }

	static void Main()
	{
		Application.Init();
		new Sample();
		Application.Run();
	}
}
