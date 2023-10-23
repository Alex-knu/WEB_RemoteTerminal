FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY . .

RUN mv appsettings-encrypted.json appsettings.json \
  && dotnet publish IdentityAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
#EXPOSE 443
ENTRYPOINT ["dotnet", "IdentityAPI.dll"]
