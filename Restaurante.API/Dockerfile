FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /src
COPY --from=build /src/out ./

EXPOSE 5085

ENTRYPOINT ["dotnet", "Restaurante.API.dll"]