FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build


COPY ["/", "/QueryCalculator"]
RUN dotnet restore "/QueryCalculator/API/API.csproj"
COPY . .
RUN dotnet test "/QueryCalculator/API/API.csproj" -c Release -o /app/build
RUN dotnet build "/QueryCalculator/API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/QueryCalculator/API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
