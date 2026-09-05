# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY EMS.API/EMS.API.csproj EMS.API/
COPY EMS.Application/EMS.Application.csproj EMS.Application/
COPY EMS.Domain/EMS.Domain.csproj EMS.Domain/
COPY EMS.Persistence/EMS.Persistence.csproj EMS.Persistence/

# Restore dependencies
RUN dotnet restore EMS.API/EMS.API.csproj

# Copy all source code
COPY . .

# Publish API project
RUN dotnet publish EMS.API/EMS.API.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EMS.API.dll"]