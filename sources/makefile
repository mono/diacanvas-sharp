PACKAGE=diacanvas2-0.12.0

all:
	gapi-parser diacanvas-sharp-sources.xml

get-source-code:
	wget http://dl.sourceforge.net/sourceforge/diacanvas/$(PACKAGE).tar.gz \
	--output-document=- | tar -xz;

#export CVS_PASSWORD=""
#cvs -z3 -d:pserver:anonymous@cvs.sf.net:/cvsroot/diacanvas co $(PACKAGE)
