ASSEMBLIES=	-r glib-sharp 			\
		-r gtk-sharp			\
		-r glade-sharp			\
		-r gdk-sharp			\
		-r pango-sharp			\
		-r gnome-sharp			\
		-r ../dia/diacanvas-sharp.dll

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

$(SAMPLE): $(CODE) ../dia/diacanvas-sharp.dll
	mcs $(ASSEMBLIES) $(RESOURCES) $(CODE) -o $(SAMPLE)

install:
	@echo "Have a nice day!"

clean:
	rm -f $(SAMPLE)

distclean:
	rm -r CVS
	rm -r glade/CVS
	rm -r pixmaps/CVS
	rm .cvsignore
