FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["ClipSnip/ClipSnip.csproj", "ClipSnip/"]
RUN dotnet restore "ClipSnip/ClipSnip.csproj"

COPY . .

WORKDIR "/src/ClipSnip"

RUN dotnet publish \
    "ClipSnip.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=False

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "ClipSnip.dll" ]