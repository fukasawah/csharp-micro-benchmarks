@echo off

dotnet clean -c Release  && dotnet build -c Release  %* && dotnet run -c Release --no-build %*