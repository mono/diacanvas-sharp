INCLUDE_API=	/usr/share/gapi/art-api.xml		\
		/usr/share/gapi/pango-api.xml		\
		/usr/share/gapi/atk-api.xml  		\
		/usr/share/gapi/gnome-api.xml    	\
		/usr/share/gapi/gtk-api.xml		\
		/usr/share/gapi/gdk-api.xml  		\

all:
	gapi_codegen.exe --include $(INCLUDE_API) --generate dia-api.xml

clean:
	rm -f dia-api.xml
