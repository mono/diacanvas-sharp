/* dia-canvas-view.c : Glue for accessing fields in the DiaCanvasView class.
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */
 
#include <diacanvas/dia-canvas-view.h>
 
/* Forward declarations */
GList * diasharp_canvas_view_get_selected_items (DiaCanvasView * view);
DiaCanvasViewItem * diasharp_canvas_view_get_focus_item (DiaCanvasView * view);
/* */
 
GList *
diasharp_canvas_view_get_selected_items (DiaCanvasView * view)
{
        return view->selected_items;
}

DiaCanvasViewItem * 
diasharp_canvas_view_get_focus_item (DiaCanvasView * view)
{
	return view->focus_item;
}
