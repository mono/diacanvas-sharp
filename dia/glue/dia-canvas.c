/* dia-canvas.c : Glue for accessing fields in the DiaCanvas class.
 *
 * Author: Martin Willemoes Hansen
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */
 
#include <diacanvas/dia-canvas.h>
 
/* Forward declarations */
DiaCanvasItem * diasharp_canvas_get_root (DiaCanvas * canvas);
/* */
 
DiaCanvasItem *
diasharp_canvas_get_root (DiaCanvas * canvas)
{
        return canvas->root;
}
