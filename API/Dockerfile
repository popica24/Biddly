FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /app

# Copy csproj files and restore dependencies
COPY WebAPI/*.csproj ./
RUN dotnet restore

# Copy the entire solution
COPY . ./

# Publish both projects
WORKDIR /app
RUN dotnet publish WebAPI/WebAPI.csproj -c Release -o out/WebAPI
RUN dotnet publish Services/Services.csproj -c Release -o out/Services
RUN dotnet publish Domain/Business.csproj -c Release -o out/Domain

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0 as final-env
WORKDIR /app
COPY --from=build-env /app/out/WebAPI ./WebAPI
COPY --from=build-env /app/out/Services ./Services
COPY --from=build-env /app/out/Domain ./Domain

# Copy the JSON file from local machine to Docker container

EXPOSE 80

ENTRYPOINT [ "dotnet", "WebAPI/WebAPI.dll" ]
