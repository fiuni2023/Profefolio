FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["profefolio/profefolio.csproj", "profefolio/"]
RUN dotnet restore "profefolio/profefolio.csproj"
COPY . .
WORKDIR "/src/profefolio"
RUN dotnet build "profefolio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "profefolio.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "profefolio.dll"]
