ASSEMBLIES=	-r gtk-sharp.dll \
		-r glib-sharp.dll \
		-r gnome-sharp.dll \
		-r gdk-sharp.dll \
		-r pango-sharp.dll \
		-r art-sharp.dll

LIBRARY=	diacanvas2-sharp.dll

all:	generated/*.cs
	mcs --unsafe --target library $(ASSEMBLIES) --recurse '*.cs' -o $(LIBRARY)

clean:
	rm -f $(LIBRARY)
	rm -rf generated
