/* diatypes.c : Glue to create DashStyle's
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */
 
#include <diacanvas/dia-canvas-line.h>
 
/* Forward declarations */
void diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line, 
						   gdouble dash_on, gdouble dash_off);
/* */

void 
diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line, 
					      gdouble dash_on, gdouble dash_off)
{
	g_assert (dash_on >= 0);
	g_assert (dash_off >= 0);

	line->dash = NULL;
	line->n_dash = 0;

	if (dash_off == 0) {
		line->n_dash = 1;
		line->dash = g_new (gdouble, 1);
		memcpy (line->dash, &dash_on, sizeof (gdouble) * 1);
	}

	// Use DiaDashStyle * dia_dash_style_new (gint n_dash, gdouble dash1, ...)
	// When it has been fixed
	//
	//DiaDashStyle * old_style = NULL;
	//g_object_get (line, "dash", old_style, NULL);
	//dia_dash_style_free (old_style);

	//g_object_set (line, "dash", style, NULL);
}


