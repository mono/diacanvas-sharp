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
/* */
 
GList * 
diasharp_canvas_item_get_handles (DiaCanvasItem * item)
{
        return item->handles;
}

