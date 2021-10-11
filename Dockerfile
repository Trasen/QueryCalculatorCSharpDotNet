FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY ["/", "/QueruCalculator"]

RUN dotnet restore "/QueruCalculator/API/API.csproj"
COPY . .
RUN dotnet build "/QueruCalculator/API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/QueruCalculator/API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
