/* dia-canvas-item.c : Glue for accessing fields in the DiaCanvasItem class.
 *
 * Authors: Martin Willemoes Hansen
 *          Mario Fuentes <mario@gnome.cl>
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 * Copyright (C) 2004 Mario Fuentes <mario@gnome.cl>
 *
 */

#include <diacanvas/dia-canvas-item.h>
 
/* Forward declarations */
GList * diasharp_canvas_item_get_handles (DiaCanvasItem * item);
void diasharp_canvas_item_set_canvas (DiaCanvasItem * item, DiaCanvas * canvas);
void diasharp_canvas_item_base_update (DiaCanvasItem *item, gdouble *affine);
void diasharp_canvas_item_override_update (GType gtype, gpointer callback);
gboolean diasharp_canvas_item_base_get_shape_iter (DiaCanvasItem *item, DiaCanvasIter *iter);
void diasharp_canvas_item_override_get_shape_iter (GType gtype, gpointer callback);
gboolean diasharp_canvas_item_base_shape_next (DiaCanvasItem *item, DiaCanvasIter *iter);
void diasharp_canvas_item_override_shape_next (GType gtype, gpointer callback);
DiaShape *diasharp_canvas_item_base_shape_value (DiaCanvasItem *item, DiaCanvasIter *iter);
void diasharp_canvas_item_override_shape_value (GType gtype, gpointer callback);
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

void
diasharp_canvas_item_base_update (DiaCanvasItem *item, gdouble *affine)
{
	DiaCanvasItemClass *parent = g_type_class_peek_parent (G_OBJECT_GET_CLASS (item));

	if (parent->update)
		(*parent->update) (item, affine);
}

void
diasharp_canvas_item_override_update (GType gtype, gpointer callback)
{
	DiaCanvasItemClass *klass = g_type_class_peek (gtype);

	if (!klass)
		klass = g_type_class_ref (gtype);

	((DiaCanvasItemClass *) klass)->update = callback;
}

gboolean
diasharp_canvas_item_base_get_shape_iter (DiaCanvasItem *item, DiaCanvasIter *iter)
{
	DiaCanvasItemClass *parent = g_type_class_peek_parent (G_OBJECT_GET_CLASS (item));

	if (parent->get_shape_iter)
		return ((*parent->get_shape_iter) (item, iter));
	
	return FALSE;
}

void
diasharp_canvas_item_override_get_shape_iter (GType gtype, gpointer callback)
{
	DiaCanvasItemClass *klass = g_type_class_peek (gtype);
	
	if (!klass)
		klass = g_type_class_ref (gtype);

	((DiaCanvasItemClass *) klass)->get_shape_iter = callback;
}

gboolean
diasharp_canvas_item_base_shape_next (DiaCanvasItem *item, DiaCanvasIter *iter)
{
	DiaCanvasItemClass *parent = g_type_class_peek_parent (G_OBJECT_GET_CLASS (item));

	if (parent->shape_next)
		return ((*parent->shape_next) (item, iter));
	
	return FALSE;	
}

void
diasharp_canvas_item_override_shape_next (GType gtype, gpointer callback)
{
	DiaCanvasItemClass *klass = g_type_class_peek (gtype);

	if (!klass)
		klass = g_type_class_ref (gtype);

	((DiaCanvasItemClass *) klass)->shape_next = callback;
}

DiaShape *
diasharp_canvas_item_base_shape_value (DiaCanvasItem *item, DiaCanvasIter *iter)
{
	DiaCanvasItemClass *parent = g_type_class_peek_parent (G_OBJECT_GET_CLASS (item));

	if (parent->shape_value)
		return ((*parent->shape_value) (item, iter));
	
	return NULL;	
}

void
diasharp_canvas_item_override_shape_value (GType gtype, gpointer callback)
{
	DiaCanvasItemClass *klass;
	
	klass = g_type_class_peek (gtype);

	if (!klass)
		klass = g_type_class_ref (gtype);

	((DiaCanvasItemClass *) klass)->shape_value = callback;
}
