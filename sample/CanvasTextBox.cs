using System;
using System.Runtime.InteropServices;

using Dia;
using Pango;

public class CanvasTextBox : CanvasBox {
	
	CanvasText text;

	public CanvasTextBox() : base()
	{
		text = new CanvasText();
		//text.Font = FontDescription.FromString ("sans 20");
		text.Text = "Hi";
		text.Width =  200;
		text.Height = 40;
		text.Move (10, 30);
		AddConstruction (text);
	}
}
