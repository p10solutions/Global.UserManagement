#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Global.UserManagement.Api/Global.UserManagement.Api.csproj", "Global.UserManagement.Api/"]
COPY ["Global.UserManagement.Infra/Global.UserManagement.Infra.csproj", "Global.UserManagement.Infra/"]
COPY ["Global.UserManagement.Application/Global.UserManagement.Application.csproj", "Global.UserManagement.Application/"]
RUN dotnet restore "Global.UserManagement.Api/Global.UserManagement.Api.csproj"
COPY . .
WORKDIR "/src/Global.UserManagement.Api"
RUN dotnet build "Global.UserManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Global.UserManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Global.UserManagement.Api.dll"]