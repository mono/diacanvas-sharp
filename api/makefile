PREFIX=		/usr

API=		dia-api.xml	\
		dia-symbols.xml

INCLUDE_API=	$(PREFIX)/share/gapi/art-api.xml		\
		$(PREFIX)/share/gapi/pango-api.xml		\
		$(PREFIX)/share/gapi/atk-api.xml  		\
		$(PREFIX)/share/gapi/gnome-api.xml    		\
		$(PREFIX)/share/gapi/gtk-api.xml		\
		$(PREFIX)/share/gapi/gdk-api.xml		\
		$(PREFIX)/share/gapi/gdk-symbols.xml

all: generated-stamp

generated-stamp: $(API) 
	gapi_codegen.exe --include $(INCLUDE_API) --generate $(API) && touch generated-stamp

install:
	install -o root -g root -m 644 dia-api.xml $(PREFIX)/share/gapi
	install -o root -g root -m 644 dia-symbols.xml $(PREFIX)/share/gapi

clean:
	rm -f generated-stamp

distclean:
	rm -r CVS
	rm .cvsignore
