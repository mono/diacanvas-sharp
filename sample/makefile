ASSEMBLIES=	-r glib-sharp 		\
		-r gtk-sharp

SAMPLE=		sample.exe

all: sample.cs
	mcs -r ../dia/dia-sharp.dll $(ASSEMBLIES) sample.cs -o $(SAMPLE)

clean:
	rm -f $(SAMPLE)
