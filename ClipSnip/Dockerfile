FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ClipSnip.csproj", "./"]
RUN dotnet restore "ClipSnip.csproj"

COPY . .

RUN dotnet publish \
"ClipSnip.csproj" \
-c Release \
-o /app/publish \
/p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ClipSnip.dll"]