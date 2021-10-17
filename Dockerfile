FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CharactersApi/CharactersApi/CharactersApi.csproj", "CharactersApi/CharactersApi/"]
COPY ["CharactersApi/CharactersApi_Test/CharactersApi_Test.csproj", "CharactersApi/CharactersApi_Test/"]
RUN dotnet restore "CharactersApi\CharactersApi\CharactersApi.csproj"
RUN dotnet restore "CharactersApi\CharactersApi_Test\CharactersApi_Test.csproj"
COPY . .
WORKDIR "/src/CharactersApi/CharactersApi"
RUN dotnet build "CharactersApi.csproj" -c Release -o /app/build
RUN dotnet build "/src/CharactersApi/CharactersApi_Test/CharactersApi_Test.csproj" -c Release -o /app/build

RUN dotnet test "/src/CharactersApi/CharactersApi_Test/CharactersApi_Test.csproj" --logger "trx;LogFileName=CharactersApi_Test.trx"

FROM build AS publish
RUN dotnet publish "CharactersApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CharactersApi.dll"]
