aclocal
libtoolize --force --copy
automake -a
autoconf
./configure $*
