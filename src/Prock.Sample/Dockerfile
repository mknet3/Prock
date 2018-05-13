FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /sample
COPY Prock.sln ./
COPY src/ src/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /sample/src/Prock.Sample
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Prock.Sample.dll"]
