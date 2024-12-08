# execute program
docker run -it --rm -v "$PWD":/usr/src/myapp -w /usr/src/myapp mcr.microsoft.com/dotnet/sdk dotnet run main.cs