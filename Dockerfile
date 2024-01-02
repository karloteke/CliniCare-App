FROM mcr.microsoft.com/dotnet/sdk:6.0 AS buildApp
WORKDIR /src
COPY . .
RUN dotnet publish "Presentation/CliniCareApp.Presentation.csproj" -c Release -o /consoleapp

# Etapa 2: Crear la imagen final
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY --from=buildApp /consoleapp ./
COPY ../*.json SharedFolder/
ENV IS_DOCKER=true  
EXPOSE 7011
ENTRYPOINT ["dotnet", "CliniCareApp.Presentation.dll"]
















