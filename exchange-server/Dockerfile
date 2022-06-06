FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["exchange-server.csproj", "./"]
RUN dotnet restore "./exchange-server.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "exchange-server.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "exchange-server.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt update && apt install -y curl
ENTRYPOINT ["dotnet", "exchange-server.dll"]

