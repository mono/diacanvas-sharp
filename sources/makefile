PACKAGE=diacanvas2-0.9.2

all:
	gapi.pl dia-sharp.sources ../api

get-source-code:
	wget http://aleron.dl.sourceforge.net/sourceforge/diacanvas/$(PACKAGE).tar.gz \
	--output-document=- | tar -xz;

	patch $(PACKAGE)/src/dia-canvas.h ../patches/dia-canvas.h.diff  
	patch $(PACKAGE)/src/dia-canvas-view.c ../patches/dia-canvas-view.c.diff

distclean:
	rm -rf $(PACKAGE)
