PREFIX=		/usr

INCLUDE_API=	$(PREFIX)/share/gapi/art-api.xml		\
		$(PREFIX)/share/gapi/pango-api.xml		\
		$(PREFIX)/share/gapi/atk-api.xml  		\
		$(PREFIX)/share/gapi/gnome-api.xml    	\
		$(PREFIX)/share/gapi/gtk-api.xml		\
		$(PREFIX)/share/gapi/gdk-api.xml  		\

all:
	gapi_codegen.exe --include $(INCLUDE_API) --generate dia-api.xml

clean:
	rm -f dia-api.xml
