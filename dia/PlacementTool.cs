// PlacementTool.cs - Custom placement tool.
//
// Author: Martin Willemoes Hansen <mwh@sysrq.dk>
//
// Copyright (C) 2003 Martin Willemoes Hansen

using System;

namespace Dia {

	public class PlacementTool : Tool {
 
		static GLib.Type gtype;
		object [] attr;
		Type type;

		static PlacementTool()
		{
			gtype = RegisterGType (typeof (PlacementTool));
		}

		public PlacementTool (Type type, params object [] attributes) 
			: base (gtype)
		{
			if (attributes.Length % 2 == 1)
				throw new ArgumentException ("A pair of attributes has only 1 member, it must have 2.");

			this.attr = attributes;
			this.type = type;

			ButtonPressEvent += new DiaSharp.ButtonPressEventHandler (ButtonPress);
		}

		public void ButtonPress (object o, DiaSharp.ButtonPressEventArgs args)
		{
			CanvasItem item = CreateItem();
			MoveItem (args.View, args.Button, item);
			args.View.Canvas.Root.Add (item);
			GrabHandle (args.View, args.Button, item);
		}

		CanvasItem CreateItem()
		{
			CanvasItem item = (CanvasItem) Activator.CreateInstance (type);
			
			for (int i = 0; i < attr.Length; i += 2) {
				item.SetProperty ((string)attr [i], 
						  new GLib.Value (attr [i + 1]));				
			}
			return item;
		}

		void MoveItem (CanvasView view, Gdk.EventButton evnt, CanvasItem item)
		{
			double wx, wy;
			view.WindowToWorld (evnt.x, evnt.y, out wx, out wy);
			item.Move (wx, wy);
		}

		void GrabHandle (CanvasView view, Gdk.EventButton evnt, CanvasItem item)
		{

		}
	}
}

