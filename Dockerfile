FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#ENV ASPNETCORE_URLS="https://+;http://+"
ENV ASPNETCORE_URLS="http://+"
ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password="9900"
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

COPY ["/", "/querycalculator"]
#RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p "9900"
#RUN update-ca-certificates
#RUN dotnet dev-certs https  --trust
RUN dotnet test "/querycalculator/QueryCalculator/"
COPY . .

RUN dotnet build  "/querycalculator/API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/querycalculator/API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
