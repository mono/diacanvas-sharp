ASSEMBLIES=	-r glib-sharp 		\
		-r gtk-sharp		\
		-r glade-sharp		\
		-r gdk-sharp		\
		-r pango-sharp		\
		-r gnome-sharp		\
		-r ../dia/dia-sharp.dll

RESOURCES=	-resource:glade/gui.glade		\
		-resource:pixmaps/logo.png		\
		-resource:pixmaps/selection.png		\
		-resource:pixmaps/gimp-zoom.png		\
		-resource:pixmaps/line.png		\
		-resource:pixmaps/box.png		\
		-resource:pixmaps/glade-image.png 	\
		-resource:pixmaps/text.png

SAMPLE=		sample.exe

CODE=		CanvasTextBox.cs	\
		sample.cs

all: $(SAMPLE)

$(SAMPLE): $(CODE)
	mcs $(ASSEMBLIES) $(RESOURCES) $(CODE) -o $(SAMPLE)

clean:
	rm -f $(SAMPLE)
