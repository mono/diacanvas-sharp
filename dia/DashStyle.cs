// DashStyle.cs - custom version of DashStyle
//
// Author: Martin Willemoes Hansen <mwh@sysrq.dk>
//
// Copyright (C) 2003 Martin Willemoes Hansen
//
// This code is inserted after the automatically generated code.

namespace Dia {
	public class DashStyle {
		double dash_on;
		double dash_off;

		public DashStyle (double dash_on, double dash_off)
		{
			this.dash_on = dash_on < 0 ? 0 : dash_on;
			this.dash_off = dash_off < 0 ? 0 : dash_off;
		}

		public DashStyle (double dash_on) : this (dash_on, 0) {}

		public double DashOn { get { return dash_on; } }
		public double DashOff { get { return dash_off; } }
	}
}
