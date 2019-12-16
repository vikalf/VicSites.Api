FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR .
COPY ["src/VicSites.Api/VicSites.Api.csproj", "src/VicSites.Api/"]
COPY ["src/VicSites.Business/VicSites.Business.csproj", "src/VicSites.Business/"]
COPY ["src/VicSites.Common/VicSites.Common.csproj", "src/VicSites.Common/"]
RUN dotnet restore "src/VicSites.Api/VicSites.Api.csproj"
COPY . .
WORKDIR "/src/VicSites.Api"
RUN dotnet build "VicSites.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VicSites.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VicSites.Api.dll"]