/* diatypes.c : Glue to create DashStyle's
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003, 2004 Martin Willemoes Hansen
 *
 */
 
#include <diacanvas/dia-canvas-line.h>
 
/* Forward declarations */
void diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line, 
						   gdouble dash_size); 
/* */

void 
diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line, 
					      gdouble dash_size)
{
	g_assert (dash_size >= 0);

	g_free (line->dash);
	line->dash = NULL;
	line->n_dash = 0;

	DiaDashStyle * style = dia_dash_style_new (1, dash_size);
	g_object_set (line, "dash", style, NULL);

	//Test code to see if DiaCanvasLine supports > 1 dashes.
	//DiaDashStyle * style = dia_dash_style_new (3, 5, 10, 20);
	//g_object_set (line, "dash", style, NULL);

	//g_print ("dash_n: %d\n", style->n_dash);
        //g_print ("dash [0]: %f\n", style->dash [0]);
        //g_print ("dash [1]: %f\n", style->dash [1]);
        //g_print ("dash [2]: %f\n", style->dash [2]);
        //g_print ("size of diadashstyle: %d\n", sizeof (style));
}


