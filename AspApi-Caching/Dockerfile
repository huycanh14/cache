FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspApi-Caching.csproj", "./"]
RUN dotnet restore "AspApi-Caching.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AspApi-Caching.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspApi-Caching.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspApi-Caching.dll"]
