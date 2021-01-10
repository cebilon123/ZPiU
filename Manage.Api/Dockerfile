#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Manage.Api.csproj", ""]
COPY ["../Manage.Core/Manage.Core.csproj", "../Manage.Core/"]
COPY ["../Manage.Data/Manage.Data.csproj", "../Manage.Data/"]
RUN dotnet restore "./Manage.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Manage.Api.csproj" -c Release -o /app/build --net=dockernet

FROM build AS publish
RUN dotnet publish "Manage.Api.csproj" -c Release -o /app/publish --net=dockernet

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Manage.Api.dll"]