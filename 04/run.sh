# execute script
docker run -it --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp php:8.2-cli php main.php