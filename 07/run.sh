# execute program
docker run -it --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp gradle gradle -g .gradle run -q