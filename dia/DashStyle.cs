// DashStyle.cs - custom version of DashStyle
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
//
// This code is inserted after the automatically generated code.

namespace Dia {
	public class DashStyle {
		double dash_size;

		public DashStyle (double dash_size)
		{
			this.dash_size = dash_size < 0 ? 0 : dash_size;
		}

		public double DashSize { get { return dash_size; } }
	}
}
