FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6201

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CesoaPsiPt.Simulador/CesoaPsiPt.Simulador.csproj", "CesoaPsiPt.Simulador/"]
RUN dotnet restore "CesoaPsiPt.Simulador/CesoaPsiPt.Simulador.csproj" 
COPY . .
WORKDIR "/src/CesoaPsiPt.Simulador"
RUN dotnet build "CesoaPsiPt.Simulador.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CesoaPsiPt.Simulador.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:6201
ENTRYPOINT ["dotnet", "CesoaPsiPt.Simulador.dll"]
