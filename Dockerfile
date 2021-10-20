FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5005

ENV ASPNETCORE_URLS=http://+:5005

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CharactersApi/CharactersApi/CharactersApi.csproj", "CharactersApi/CharactersApi/"]
RUN dotnet restore "CharactersApi\CharactersApi\CharactersApi.csproj"
COPY . .
WORKDIR "/src/CharactersApi/CharactersApi"
RUN dotnet build "CharactersApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CharactersApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CharactersApi.dll"]
