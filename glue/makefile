PREFIX=		/usr
GLUE_LIB=	libdiasharpglue.so

all: $(GLUE_LIB)

$(GLUE_LIB): *.c
	gcc *.c -shared -fPIC -o $(GLUE_LIB) `pkg-config --cflags --libs gtk+-2.0 libart-2.0`

install:
	install -o root -g root -m 644 $(GLUE_LIB) $(PREFIX)/lib

clean:
	rm -f $(GLUE_LIB)