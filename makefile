DIRS=glue dia sample

all: build

build:
	for i in $(DIRS); do                    	\
		$(MAKE) -C $$i || exit 1;		\
	done

install:
	for i in $(DIRS); do                     	\
		$(MAKE) -C $$i install || exit 1;	\
	done

	$(MAKE) -C doc install;

clean:
	for i in $(DIRS); do        	     		\
		$(MAKE) -C $$i clean || exit 1;		\
	done

distclean: clean
	for i in $(DIRS); do                     	\
		$(MAKE) -C $$i distclean || true;     	\
	done

	$(MAKE) -C sources distclean;
	$(MAKE) -C doc distclean;

	rm -r CVS
	rm -r patches
