/* dia-canvas-line.c : Glue to set the dash property of DiaCanvasLine
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003, 2004 Martin Willemoes Hansen
 *
 */
 
#include <diacanvas/dia-canvas-line.h>
 
#define DASH_SIZE(n_dash) (sizeof (DiaDashStyle) + sizeof (gdouble) * MAX (0, (n_dash) - 1))

/* Forward declarations */
void
diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line,
					      gint n_dash,
					      gdouble dashes []);
/* */

void
diasharp_canvas_line_set_dash_style_property (DiaCanvasLine * line,
					      gint n_dash,
					      gdouble dashes [])
{
	/* Right now 
	 * DiaDashStyle * dia_dash_style_new (gint n_dash, gdouble dash1, ...)
	 * do not support P/Invoke because of ... (ellipsis)
	 * A va_list may work instead of simply a gdouble array.
	 * For now I just borrowed the code to make a DiaDashStyle.
	 */

	g_assert (n_dash >= 0);

	g_free (line->dash);
	line->dash = NULL;
	line->n_dash = 0;

        DiaDashStyle * style = g_malloc (DASH_SIZE (n_dash));
        style->n_dash = n_dash;

	gint i = 0;
        while (i < n_dash) {
                style->dash[i] = dashes [i++];
        }

	g_object_set (line, "dash", style, NULL);

	/*
	Test code, to see if DiaDashStyle is okay
	g_print ("dash_n: %d\n", style->n_dash);
        g_print ("dash [0]: %f\n", style->dash [0]);
        g_print ("dash [1]: %f\n", style->dash [1]);
        g_print ("dash [2]: %f\n", style->dash [2]);
        g_print ("size of diadashstyle: %d\n", sizeof (style));
	*/
}


