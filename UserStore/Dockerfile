FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_URLS=http://+:8080

WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY ["UserStore/UserStore.csproj", "UserStore/"]

RUN dotnet restore "./UserStore/UserStore.csproj"
COPY . .
WORKDIR "/UserStore"
RUN dotnet build "UserStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./UserStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserStore.dll"]