FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QueryCalculator/QueryCalculator.csproj", "QueryCalculator/"]
RUN dotnet restore "QueryCalculator/QueryCalculator.csproj"
COPY . .
WORKDIR "/src/QueryCalculator"
RUN dotnet build "QueryCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueryCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueryCalculator.dll"]
