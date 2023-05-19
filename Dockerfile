# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . .
# RUN dotnet restore "myG.CMS.School/myG.CMS.School.csproj"

# Build
RUN dotnet build "myG.GameGuild/myG.GameGuild.csproj" -c Release -o /app/build

# Publish
FROM build-env AS publish
RUN dotnet publish "myG.GameGuild/myG.GameGuild.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "myG.GameGuild.dll"]

EXPOSE 80
