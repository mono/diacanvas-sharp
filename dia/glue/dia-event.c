/* dia-event.c : Glue to access fields in DiaEvent.
 *
 * Author: Martin Willemoes Hansen  <mwh@sysrq.dk>
 *
 * Copyright (C) 2003 Martin Willemoes Hansen
 *
 */

#include <glib-object.h>
#include <diacanvas/dia-event.h>

/* Forward declarations */
DiaEventType diasharp_dia_event_get_event_type (DiaEvent * event);
/* */

DiaEventType
diasharp_dia_event_get_event_type (DiaEvent * event)
{
	return event->type;
}


