FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50165
EXPOSE 44301

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["JWT.API/JWT.API.csproj", "BackJWT.APIend/"]
RUN dotnet restore "JWT.API/JWT.API.csproj"
COPY . .
WORKDIR "/src/JWT.API"
RUN dotnet build "JWT.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JWT.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JWT.API.dll"]