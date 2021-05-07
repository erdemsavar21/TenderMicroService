#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src

COPY ["Tender.Order/Tender.Order.csproj", "Tender.Order/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Ordering.Domain/Ordering.Domain.csproj", "Ordering.Domain/"]
COPY ["EventBusRabbitMQ1/EventBusRabbitMQ1.csproj", "EventBusRabbitMQ1/"]
COPY ["Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Ordering.Infrastructure/"]
RUN dotnet restore "Tender.Order/Tender.Order.csproj"
COPY . .
WORKDIR "/src/Tender.Order"
RUN dotnet build "Tender.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tender.Order.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tender.Order.dll"]