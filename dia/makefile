PREFIX=		/usr

APIS=		diacanvas-api.xml diacanvas-symbols.xml

INCLUDE_APIS=	$(PREFIX)/share/gapi/art-api.xml                \
		$(PREFIX)/share/gapi/pango-api.xml              \
		$(PREFIX)/share/gapi/atk-api.xml                \
		$(PREFIX)/share/gapi/gnome-api.xml              \
		$(PREFIX)/share/gapi/gtk-api.xml                \
		$(PREFIX)/share/gapi/gdk-api.xml                \
		$(PREFIX)/share/gapi/gdk-symbols.xml

ASSEMBLIES=	-r gtk-sharp.dll \
		-r glib-sharp.dll \
		-r gnome-sharp.dll \
		-r gdk-sharp.dll \
		-r pango-sharp.dll \
		-r art-sharp.dll

LIBRARY=	diacanvas-sharp.dll

all: $(LIBRARY)

generated-stamp: $(APIS) *.custom Dia.metadata
	gapi-fixup.exe --api=diacanvas-api.xml --metadata=Dia.metadata && \
	gapi_codegen.exe --include $(INCLUDE_APIS) --generate $(APIS) \
	--outdir=generated --customdir=. --assembly-name=diacanvas-sharp && touch generated-stamp

$(LIBRARY): generated-stamp *.cs
	mcs --unsafe -nowarn:0660,0661 --target library $(ASSEMBLIES) --recurse '*.cs' -o $(LIBRARY)

install:
	install -o root -g root -m 644 diacanvas-api.xml $(PREFIX)/share/gapi
	install -o root -g root -m 644 diacanvas-symbols.xml $(PREFIX)/share/gapi
	install -o root -g root -m 644 $(LIBRARY) $(PREFIX)/lib

clean:
	rm -f $(LIBRARY)
	rm -rf generated
	rm -f generated-stamp

distclean:
	rm -r CVS
	rm .cvsignore
