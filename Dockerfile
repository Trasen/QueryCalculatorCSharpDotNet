FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS="https://*:5000"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

COPY ["/", "/querycalculator"]

RUN dotnet test "/querycalculator/QueryCalculator/"
COPY . .

RUN dotnet build  "/querycalculator/API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/querycalculator/API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
