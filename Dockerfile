#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .

COPY ContactBook.API/*.csproj ContactBook.API/
COPY ContactBook.Data/*.csproj ContactBook.Data/
COPY ContactBook.Models/*.csproj ContactBook.Models/
COPY ContctBook.Core/*.csproj ContctBook.Core/
COPY ContactBook.BL/*.csproj ContactBook.BL/
COPY ContactBook.DTO/*.csproj ContactBook.DTO/

RUN dotnet restore ContactBook.API/*.csproj

COPY . .
WORKDIR /src/ContactBook.API
RUN dotnet build 

FROM build AS publish
WORKDIR /src/ContactBook.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#ENTRYPOINT ["dotnet", "ContactBook.API.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ContactBook.API.dll