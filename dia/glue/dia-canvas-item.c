/* dia-canvas-item.c : Glue for accessing fields in the DiaCanvasItem class.
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */

#include <diacanvas/dia-canvas-item.h>
 
/* Forward declarations */
GList * diasharp_canvas_item_get_handles (DiaCanvasItem * item);
void diasharp_canvas_item_set_canvas (DiaCanvasItem * item, DiaCanvas * canvas);
/* */
 
GList * 
diasharp_canvas_item_get_handles (DiaCanvasItem * item)
{
        return item->handles;
}

void
diasharp_canvas_item_set_canvas (DiaCanvasItem * item, DiaCanvas * canvas)
{
	if (item->canvas == NULL)
		item->canvas = canvas;
}

