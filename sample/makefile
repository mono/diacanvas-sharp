ASSEMBLIES=	-r glib-sharp 		\
		-r gtk-sharp		\
		-r glade-sharp		\
		-r gdk-sharp		\
		-r ../dia/dia-sharp.dll

RESOURCES=	-resource:glade/gui.glade	\
		-resource:pixmaps/selection.png	\
		-resource:pixmaps/line.png	\
		-resource:pixmaps/box.png

SAMPLE=		sample.exe

all: sample.cs
	mcs $(ASSEMBLIES) $(RESOURCES) sample.cs -o $(SAMPLE)

clean:
	rm -f $(SAMPLE)
