// Dia.Event.cs - Custom event wrapper 
//
// Author: Martin Willemoes Hansen <mwh@sysrq.dk>
// Copyright (C) 2003 2004  Martin Willemoes Hansen <mwh@sysrq.dk>
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

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
