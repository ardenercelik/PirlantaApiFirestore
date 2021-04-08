#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PirlantaApi.csproj", "."]
RUN dotnet restore "./PirlantaApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PirlantaApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PirlantaApi.csproj" -c Release -o /app/publish

FROM base AS final
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
   		libc6-dev \
        && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=publish /app/publish .
ENV PAGE_SIZE=10
ENV PROJECT_ID=pirlantam-3adc8
ENV GOOGLE_APPLICATION_CREDENTIALS="admin.json"
ENTRYPOINT ["dotnet", "PirlantaApi.dll"]