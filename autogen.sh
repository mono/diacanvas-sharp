libtoolize --force --copy
automake -a
aclocal
autoconf
./configure $*
