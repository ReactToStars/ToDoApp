FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ToDo.API/ToDo.API.csproj ToDo.API/
RUN dotnet restore ToDo.API/ToDo.API.csproj

COPY ToDo.API/ ToDo.API/
WORKDIR /src/ToDo.API
RUN dotnet publish ToDo.API.csproj --configuration Release --output /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ToDo.API.dll"]
