FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5086

ENV ASPNETCORE_URLS=http://+:5086
RUN apt-get update -y && apt-get install net-tools -y 

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /

# Copiando os Portas de acesso
COPY ["Ports/projeto.databasePort/projeto.databasePort.csproj", "Ports/projeto.databasePort/"]
COPY ["Ports/projeto.servicebusPort/projeto.servicebusPort.csproj", "Ports/projeto.servicebusPort/"]

# Copiando os adaptadores
COPY ["Adapters/projeto.databaseAdapters/projeto.databaseAdapters.csproj", "Adapters/projeto.databaseAdapters/"]
COPY ["Adapters/projeto.servicebusAdapter/projeto.servicebusAdapter.csproj", "Adapters/projeto.servicebusAdapter/"]
COPY ["Adapters/projeto.brasilapiAdapter/projeto.brasilapiAdapter.csproj", "Adapters/projeto.brasilapiAdapter/"]

# Copiando arquivos do core da aplicação
COPY ["src/projeto.service/projeto.service.csproj", "src/projeto.service/"]
COPY ["src/projeto.domain/projeto.domain.csproj", "src/projeto.domain/"]
COPY ["src/projeto.infra/projeto.infra.csproj", "src/projeto.infra/"]

RUN dotnet restore "src/projeto.service/projeto.service.csproj"
COPY . .
WORKDIR "/src/projeto.service"
RUN dotnet build "projeto.service.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "projeto.service.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 CMD [ "curl --fail http://localhost:5086/health || exit 1" ]
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "projeto.service.dll"]
