using System;

using Dia;
using DiaSharp;
using Pango;

public class CanvasTextBox : CanvasGroup {
	
	static GLib.Type gtype;
	CanvasText text;

	static CanvasTextBox()
	{
		gtype = RegisterGType (typeof (CanvasTextBox));
	}

	public CanvasTextBox() : base (gtype)
	{
		text = new CanvasText();
		text.Font = FontDescription.FromString ("sans 20");
		text.Text = "Hi, im editable";
		text.Width =  200;
		text.Height = 100;
		Add (text);
		text.EditingDone += new EditingDoneHandler (editing_done);
	}

	void editing_done (object sender, EditingDoneArgs args)
	{
		Console.WriteLine ("Editing Done");
		// Change view back to text box
		// get canvas
		// get canvasview
		// focus this
	}
}
