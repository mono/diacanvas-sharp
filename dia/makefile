PREFIX=		/usr

ASSEMBLIES=	-r gtk-sharp.dll \
		-r glib-sharp.dll \
		-r gnome-sharp.dll \
		-r gdk-sharp.dll \
		-r pango-sharp.dll \
		-r art-sharp.dll

LIBRARY=	dia-sharp.dll

all: $(LIBRARY)

$(LIBRARY): *.cs generated/*.cs
	mcs --unsafe -nowarn:0660,0661 --target library $(ASSEMBLIES) --recurse '*.cs' -o $(LIBRARY)

install:
	install -o root -g root -m 644 $(LIBRARY) $(PREFIX)/lib

clean:
	rm -f $(LIBRARY)
	rm -rf generated

distclean:
	rm -r CVS
	rm .cvsignore