// PlacementTool.cs - Custom placement tool.
//
// Author: Martin Willemoes Hansen <mwh@sysrq.dk>
//
// Copyright (C) 2003 Martin Willemoes Hansen

using System;
using System.Reflection;

namespace Dia {

	public class PlacementTool : Tool {
 
		static GLib.GType gtype;
		object [] properties;
		Type type;

		static PlacementTool()
		{
			gtype = RegisterGType (typeof (PlacementTool));
		}

		public PlacementTool (Type type, params object [] properties) 
			: base (gtype)
		{
			if (properties.Length % 2 == 1)
				throw new ArgumentException ("A property name does not have a value associated.");

			this.properties = properties;
			this.type = type;

			ButtonPressEvent += new ButtonPressEventHandler (ButtonPress);
		}

		void ButtonPress (object o, ButtonPressEventArgs args)
		{
			CanvasItem item = CreateItem();
			args.View.Canvas.Root.Add (item);
			MoveItem (args.View, args.Button, item);
			//GrabHandle (args.View, args.Button, item);
		}

		CanvasItem CreateItem()
		{
			object item =  Activator.CreateInstance (type);

			Binder binder = Type.DefaultBinder;
			for (int i = 0; i < properties.Length; i += 2) {
				PropertyInfo prop = type.GetProperty ((string)properties [i]);
				Type t = prop.PropertyType;
				object o = binder.ChangeType (properties [i + 1], t, null);
				prop.SetValue (item, o, null);
			}

			return (CanvasItem) item;

			/* When array marshalling works this can replace the above

			for (int i = 0; i < properties.Length; i += 2) {
				item.SetProperty ((string)properties [i], 
						  new GLib.Value (properties [i + 1]));				
			}
			*/
		}

		void MoveItem (CanvasView view, Gdk.EventButton evnt, CanvasItem item)
		{
			double wx, wy;
			view.WindowToWorld (evnt.X, evnt.Y, out wx, out wy);
			item.Move (wx, wy);
		}

		void GrabHandle (CanvasView view, Gdk.EventButton evnt, CanvasItem item)
		{
			if (item is CanvasLine) {
				/*
				  wx, wy = view.window_to_world(event.x, event.y)
				  dist, glue, glue_to = view.canvas.glue_handle (first, wx, wy)
				  if glue_to and (dist <= view.handle_layer.glue_distance):
				  glue_to.connect_handle(first)
				  view.handle_layer.grab_handle(last)
				*/

				Handle first = null;
				Handle last = null;
				bool started = true;
				foreach (Handle handle in item.Handles) {
					if (started)
						first = handle;
					else
						last = handle;

					started = false;
				}
				view.HandleLayer.GrabHandle (first);

			} else if (item is CanvasElement) {
				Handle handle_se = null;

				int counter = 0;
				foreach (Handle handle in item.Handles) {
					if (counter++ != (int)CanvasElementHandle.Se) {
						continue;
					}
					handle_se = handle;
				}

				view.HandleLayer.GrabHandle (handle_se);
			}
		}
	}
}

