PACKAGE=diacanvas2

all:
	gapi.pl dia-sharp.sources ../api

get-source-code:
	#wget http://aleron.dl.sourceforge.net/sourceforge/diacanvas/$(PACKAGE).tar.gz \
	#--output-document=- | tar -xz;

	export CVS_PASSWORD=""
	cvs -z3 -d:pserver:anonymous@cvs.sf.net:/cvsroot/diacanvas co $(PACKAGE)

	patch $(PACKAGE)/src/dia-canvas-groupable.h ../patches/dia-canvas-groupable.h.diff
	patch $(PACKAGE)/src/dia-canvas-item.h ../patches/dia-canvas-item.h.diff
	patch $(PACKAGE)/src/dia-canvas-item.c ../patches/dia-canvas-item.c.diff
	patch $(PACKAGE)/src/dia-storage.h ../patches/dia-storage.h.diff
	patch $(PACKAGE)/demos/dia-textbox.c ../patches/dia-textbox.c.diff

distclean:
	rm -rf $(PACKAGE)
