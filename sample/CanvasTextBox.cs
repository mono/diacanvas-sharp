using System;
using System.Runtime.InteropServices;

using Dia;
using Pango;

public class CanvasTextBox : CanvasBox {
	
	CanvasText text;

	public CanvasTextBox() : base()
	{
		text = new CanvasText();
		AddConstruction (text);
		//text.Font = FontDescription.FromString ("sans 20");
		text.Text = "Im a Canvas Text Box";
		text.Width = Width - 20;
		text.Height = 40;
		text.Move (10, 30);
	}

	[DllImport("diacanvas2")]
	static extern void dia_canvas_groupable_add_construction(IntPtr raw, IntPtr item);
	public void AddConstruction(Dia.CanvasItem item) {
		dia_canvas_groupable_add_construction(Handle, item.Handle);
	}
}
