#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Tender.Tendering/Tender.Tendering.csproj", "Tender.Tendering/"]
COPY ["EventBusRabbitMQ1/EventBusRabbitMQ1.csproj", "EventBusRabbitMQ1/"]
RUN dotnet restore "Tender.Tendering/Tender.Tendering.csproj"
COPY . .
WORKDIR "/src/Tender.Tendering"
RUN dotnet build "Tender.Tendering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tender.Tendering.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tender.Tendering.dll"]