/* dia-canvas-line.c : Glue for DiaCanvasLine HeadPos/TailPos set properties.
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */

#include <diacanvas/dia-canvas-line.h>
 
/* Forward declarations */
void diasharp_canvas_line_set_point_property (DiaCanvasLine * line,
					      gchar * property,
					      gdouble x, gdouble y);

/* */
void diasharp_canvas_line_set_point_property (DiaCanvasLine * line, 
					      gchar * property,
					      gdouble x, gdouble y)
{
	// get current point and free it if != null
	DiaPoint * point = g_new (DiaPoint, 1);
	point->x = x;
	point->y = y;

	g_object_set (line, property, point, NULL);
}
