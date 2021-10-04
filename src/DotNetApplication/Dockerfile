FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/DotNetApplication/DotNetApplication.csproj", "DotNetApplication/"]
RUN dotnet restore "src/DotNetApplication/DotNetApplication.csproj"
COPY . .
WORKDIR "/src/DotNetApplication"
RUN dotnet build "DotNetApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetApplication.dll"]
