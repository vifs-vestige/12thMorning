#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y curl
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs
COPY ["12thMorning/12thMorning.csproj", "12thMorning/"]
RUN dotnet restore "12thMorning/12thMorning.csproj"
COPY . .
WORKDIR "/src/12thMorning"
RUN npm install
RUN dotnet build "12thMorning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "12thMorning.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "12thMorning.dll"]