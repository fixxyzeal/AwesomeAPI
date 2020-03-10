FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
WORKDIR /src
COPY ../
RUN dotnet restore ". AwesomeAPI/AwesomeAPI.csproj"

# Copy everything else and build
WORKDIR "/src/AwesomeAPI"
RUN dotnet publish "AwesomeAPI.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet AwesomeAPI.dll