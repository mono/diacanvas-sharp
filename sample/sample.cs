/// DiaCanvas# sample
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

public class Sample {

	[Glade.Widget] Gtk.Window main;
	[Glade.Widget] ScrolledWindow scrolledwindow;

	CanvasView view;
	Canvas canvas;

	public Sample() 
	{
		XML gui = new XML (null, "glade/gui.glade", "main", null);
		gui.Autoconnect (this);

		canvas = new Canvas();
		canvas.AllowUndo = true;
		view = new CanvasView (canvas, true);
		main.KeyPressEvent += new KeyPressEventHandler (CtrlPressed);
		main.KeyReleaseEvent += new KeyReleaseEventHandler (CtrlReleased);
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
		CanvasLine line = new CanvasLine();
		line.LineWidth = 10;
		line.Color = 8327327;
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

		CanvasText text = new CanvasText();
		text.Move (250, 150);
		text.Text = "Hello, World!";
		//text.Font = FontDescription.FromString ("sans 20");
		text.Height = 50;
		text.Width = 100;
		canvas.Root.Add (text);

		CanvasTextBox textbox = new CanvasTextBox();
		textbox.Move (50, 225);
		canvas.Root.Add (textbox);

		CanvasImage image = new CanvasImage (new Pixbuf (null, "pixmaps/logo.png"));
		image.Move (50, 50);
		canvas.Root.Add (image);

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
			view.ButtonPressEvent += new ButtonPressEventHandler (Zoom);
		} else
			view.ButtonPressEvent -= new ButtonPressEventHandler (Zoom);		
	}

	void LineTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (CanvasLine), 
					       "line_width", 4, 
					       "color", 480975); 
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void BoxTool (object sender, EventArgs args)
	{
		view.Tool = new PlacementTool (typeof (CanvasBox));
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
	}

	void ImageTool (object sender, EventArgs args)
	{
		Pixbuf pixbuf = new Pixbuf (null, "pixmaps/logo.png");
		view.Tool = new PlacementTool (typeof (CanvasImage), 
					       "image", pixbuf,
					       "width", pixbuf.Width,
					       "height", pixbuf.Height);
		view.Tool.ButtonReleaseEvent += new DiaSharp.ButtonReleaseEventHandler (UnsetTool);
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
			ZoomOut (this, null);
		else
			ZoomIn (this, null);
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
		/*
		GtkWidget *dialog;
		gint response;
		GnomePrintJob *pj = gnome_print_job_new(NULL);
		GnomePrintContext *ctx;
		//gnome_print_master_print_to_file(pm, "test.ps");

		dialog = gnome_print_dialog_new (pj, "Sample print dialog", 0);
		// Run the dialog
		gtk_widget_show (dialog);
		response = gtk_dialog_run (GTK_DIALOG (dialog));
		if (response == GNOME_PRINT_DIALOG_RESPONSE_CANCEL) {
			g_print ("Printing was canceled\n");
		} else {
			gnome_print_job_print_to_file(pj, "test.ps");
			ctx = gnome_print_job_get_context (pj);
			gnome_print_beginpage(ctx, "demo");
			dia_export_print (pj, canvas);
			gnome_print_showpage(ctx);
			gnome_print_job_close(pj);

			if (response == GNOME_PRINT_DIALOG_RESPONSE_PRINT)
				gnome_print_job_print(pj);
			else if (response == GNOME_PRINT_DIALOG_RESPONSE_PREVIEW)
				gtk_widget_show (GTK_WIDGET (gnome_print_job_preview_new (pj, //gnome_print_job_get_config(pj),
							"Title goes here")));
		}
		gtk_widget_hide(dialog);
		gtk_widget_destroy (dialog);
		*/
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

	void Quit (object sender, EventArgs args) { Application.Quit(); }

	static void Main()
	{
		Application.Init();
		new Sample();
		Application.Run();
	}
}
