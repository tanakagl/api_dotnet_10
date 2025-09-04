FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy everything
COPY . ./
# Restore as distinct layers
WORKDIR /src/src/WebApi
RUN dotnet restore WebApi.csproj
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /src
COPY --from=build /src/src/WebApi/out .
ENTRYPOINT ["dotnet", "WebApi.dll"]
