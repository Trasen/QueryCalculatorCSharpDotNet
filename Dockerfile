﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/", "/"]
RUN ls
RUN dotnet restore "DotNetAPIDefinition/DotNetAPIDefinition.csproj"
COPY . .
WORKDIR "/src/DotNetAPIDefinition"
RUN dotnet build "DotNetAPIDefinition.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetAPIDefinition.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetAPIDefinition.dll"]
