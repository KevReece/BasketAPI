FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 62552
EXPOSE 44354

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BasketApi.csproj", "."]
RUN dotnet restore "BasketApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BasketApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BasketApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BasketApi.dll"]