// Dia.Event.cs - Custom event wrapper 
//
// Author: Martin Willemoes Hansen <mwh@sysrq.dk> 
//
// Copyright (C) 2003 Martin Willemoes Hansen

namespace Dia {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

	public class Event : GLib.Boxed {

		[DllImport("gtksharpglue")]
		static extern EventType diasharp_dia_event_get_event_type (IntPtr evt);

		public Event(IntPtr raw) : base(raw) {}

		public EventType Type {
			get {
				return diasharp_dia_event_get_event_type (Handle);
			}
		}

		public bool IsValid {
			get {
				return (Handle != IntPtr.Zero);
			}
		}
	}

}
