ASSEMBLER=../../monodoc/browser/assembler.exe
UPDATER=../../monodoc/generator/updater.exe

all: diacanvas-docs.zip diacanvas-docs.tree

diacanvas-docs.zip diacanvas-docs.tree: en/*/*.xml
	mono $(ASSEMBLER) --ecma en -o diacanvas-docs

install: diacanvas-docs.zip diacanvas-docs.tree
	install -m 644 diacanvas-docs.zip diacanvas-docs.tree `monodoc --get-sourcesdir`
	install -m 644 diacanvas-docs.source `monodoc --get-sourcesdir`

update:
	mono $(UPDATER) ../dia/dia-sharp.dll -o ./en -f

clean:
	rm -f *.tree
	rm -f *.zip
	rm -f tmp
	rm -f ..zip
