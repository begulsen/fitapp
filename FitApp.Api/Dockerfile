FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FitApp.Api/FitApp.Api.csproj", "FitApp.Api/"]
RUN dotnet restore "FitApp.Api/FitApp.Api.csproj"
COPY . .
WORKDIR "/src/FitApp.Api"
RUN dotnet build "FitApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitApp.Api.dll"]
