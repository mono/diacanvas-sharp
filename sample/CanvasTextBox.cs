using System;
using System.Runtime.InteropServices;

using Dia;
using Pango;

public class CanvasTextBox : CanvasGroup {
	
	CanvasText text;

	public CanvasTextBox() : base()
	{
		text = new CanvasText();
		AddConstruction (text);
		//text.Font = FontDescription.FromString ("sans 20");
		text.Text = "Im a Canvas Text Box";
		text.Width =  200;
		text.Height = 40;
		text.Move (10, 30);
	}
}
