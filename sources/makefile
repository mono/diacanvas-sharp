PACKAGE=diacanvas2-0.9.2

all:
	gapi.pl dia-sharp.sources ../api

get-source-code:
	wget http://aleron.dl.sourceforge.net/sourceforge/diacanvas/$(PACKAGE).tar.gz \
	--output-document=- | tar -xz;

	patch $(PACKAGE)/src/dia-canvas.c ../patches/dia-canvas.c.diff
	patch $(PACKAGE)/src/dia-canvas-group.c ../patches/dia-canvas-group.c.diff
	patch $(PACKAGE)/src/dia-canvas.h ../patches/dia-canvas.h.diff
	patch $(PACKAGE)/src/dia-canvas-item.c ../patches/dia-canvas-item.c.diff
	patch $(PACKAGE)/src/dia-canvas-view.c ../patches/dia-canvas-view.c.diff
	patch $(PACKAGE)/src/dia-canvas-view-item.c ../patches/dia-canvas-view-item.c.diff
	patch $(PACKAGE)/src/dia-export-svg.c ../patches/dia-export-svg.c.diff
	patch $(PACKAGE)/demos/dia-textbox.c ../patches/dia-textbox.c.diff
	patch $(PACKAGE)/demos/test-canvas.c ../patches/test-canvas.c.diff
	patch $(PACKAGE)/unit_tests/test-canvas-item.c ../patches/test-canvas-item.c.diff
	patch $(PACKAGE)/unit_tests/test-item.c ../patches/test-item.c.diff
	patch $(PACKAGE)/unit_tests/test-shape-art.c ../patches/test-shape-art.c.diff

distclean:
	rm -rf $(PACKAGE)
