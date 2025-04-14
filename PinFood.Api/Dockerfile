FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PinFood.Api/PinFood.Api.csproj", "PinFood.Api/"]
COPY ["PinFood.Application/PinFood.Application.csproj", "PinFood.Application/"]
COPY ["PinFood.Domain/PinFood.Domain.csproj", "PinFood.Domain/"]
COPY ["PinFood.Infrastructure/PinFood.Infrastructure.csproj", "PinFood.Infrastructure/"]
COPY ["PinFood.Persistence/PinFood.Persistence.csproj", "PinFood.Persistence/"]
RUN dotnet restore "PinFood.Api/PinFood.Api.csproj"
COPY . .
WORKDIR "/src/PinFood.Api"
RUN dotnet build "PinFood.Api.csproj" -c Release -o /app/build
RUN dotnet publish "PinFood.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PinFood.Api.dll"]