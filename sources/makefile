PACKAGE=diacanvas2

all:
	gapi.pl dia-sharp.sources ../api

get-source-code:
	#wget http://aleron.dl.sourceforge.net/sourceforge/diacanvas/$(PACKAGE).tar.gz \
	#--output-document=- | tar -xz;

	export CVS_PASSWORD=""
	cvs -z3 -d:pserver:anonymous@cvs.sf.net:/cvsroot/diacanvas co $(PACKAGE)

distclean:
	rm -rf $(PACKAGE)
