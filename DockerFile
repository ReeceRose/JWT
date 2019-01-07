FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["JWT.API/JWT.API.csproj", "JWT.API/"]
COPY ["JWT.Domain/JWT.Domain.csproj", "JWT.Domain/"]
COPY ["JWT.Infrastructure/JWT.Infrastructure.csproj", "JWT.Infrastructure/"]
COPY ["JWT.Application/JWT.Application.csproj", "JWT.Application/"]
COPY ["JWT.Persistence/JWT.Persistence.csproj", "JWT.Persistence/"]
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