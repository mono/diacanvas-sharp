API_DIR=api
CODE_DIR=dia
SAMPLE_DIR=sample
SOURCES_DIR=sources
DOC_DIR=doc
GLUE_DIR=glue

all: code library samples docs diaglue

xml:	
	$(MAKE) -C $(SOURCES_DIR)

code:	
	$(MAKE) -C $(API_DIR)

library:
	$(MAKE) -C $(CODE_DIR)

samples:
	$(MAKE) -C $(SAMPLE_DIR)

docs:
	$(MAKE) -C $(DOC_DIR)

diaglue:
	$(MAKE) -C $(GLUE_DIR)

install:
	$(MAKE) -C $(CODE_DIR) install
	$(MAKE) -C $(GLUE_DIR) install
	$(MAKE) -C $(DOC_DIR) install

clean:
	$(MAKE) -C $(API_DIR) clean
	$(MAKE) -C $(CODE_DIR) clean
	$(MAKE) -C $(SAMPLE_DIR) clean
	$(MAKE) -C $(DOC_DIR) clean
	$(MAKE) -C $(GLUE_DIR) clean
