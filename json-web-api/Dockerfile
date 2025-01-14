FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

EXPOSE 8080
WORKDIR /app
COPY . .

ENTRYPOINT ["dotnet", "json-web-api.dll"]