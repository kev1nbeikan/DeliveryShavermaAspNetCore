# Use the official Microsoft .NET SDK image as the base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app
COPY ./UserService ./UserService
COPY ./BarsGroupProjectN1.Common ./BarsGroupProjectN1.Common

RUN dotnet build BarsGroupProjectN1.Common
WORKDIR /app/UserService
RUN dotnet publish UserService.Core
RUN dotnet publish UserService.DataAccess
RUN dotnet publish UserService.Application
RUN dotnet publish UserService.Main

RUN dotnet publish -c Development -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "UserServiceMain.dll"]