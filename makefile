API_DIR=api
CODE_DIR=dia
SAMPLE_DIR=sample
SOURCES_DIR=sources

all: code library samples

xml:	
	$(MAKE) -C $(SOURCES_DIR)

code:	
	$(MAKE) -C $(API_DIR)

library:
	$(MAKE) -C $(CODE_DIR)

samples:
	$(MAKE) -C $(SAMPLE_DIR)

install:
	install -o root -g root -m 644 dia/diacanvas2-sharp.dll /usr/lib

clean:
	$(MAKE) -C $(API_DIR) clean
	$(MAKE) -C $(CODE_DIR) clean
	$(MAKE) -C $(SAMPLE_DIR) clean
	rm -rf gnomedb  
	rm -rf gnomeprint
