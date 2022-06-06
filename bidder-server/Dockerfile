FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["bidder-server.csproj", "./"]
RUN dotnet restore "./bidder-server.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "bidder-server.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "bidder-server.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN apt update && apt install -y curl
ENTRYPOINT ["dotnet", "bidder-server.dll"]

