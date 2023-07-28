#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["netcore3.1/netcore3.1.csproj", "netcore3.1/"]
RUN dotnet restore "netcore3.1/netcore3.1.csproj"
COPY . .
WORKDIR "/src/netcore3.1"
RUN dotnet build "netcore3.1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "netcore3.1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "netcore3.1.dll"]