FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NewsRESTapi/NewsRESTapi.csproj", "NewsRESTapi/"]
RUN dotnet restore "NewsRESTapi/NewsRESTapi.csproj"
COPY . .
WORKDIR "/src/NewsRESTapi"
RUN dotnet build "NewsRESTapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsRESTapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsRESTapi.dll"]
