FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SMSTelegram/SMSTelegram.csproj", "SMSTelegram/"]
COPY ["SMSTelegram.Application/SMSTelegram.Application.csproj", "SMSTelegram.Application/"]
COPY ["SMSTelegram.Domain/SMSTelegram.Domain.csproj", "SMSTelegram.Domain/"]
RUN dotnet restore "SMSTelegram/SMSTelegram.csproj"
COPY . .
WORKDIR "/src/SMSTelegram"
RUN dotnet build "SMSTelegram.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SMSTelegram.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMSTelegram.dll"]
