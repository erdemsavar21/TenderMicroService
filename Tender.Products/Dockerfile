#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Tender.Products/Tender.Products.csproj", "Tender.Products/"]
RUN dotnet restore "Tender.Products/Tender.Products.csproj"
COPY . .
WORKDIR "/src/Tender.Products"
RUN dotnet build "Tender.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tender.Products.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tender.Products.dll"]
