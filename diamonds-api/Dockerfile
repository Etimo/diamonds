FROM microsoft/aspnetcore-build

RUN mkdir -p /app/Diamonds.Common /app/Diamonds.GameEngine /app/Diamonds.Rest

WORKDIR /app
COPY ./Diamonds.Api.sln .
COPY ./Diamonds.Common/Diamonds.Common.csproj ./Diamonds.Common/
COPY ./Diamonds.GameEngine/Diamonds.GameEngine.csproj ./Diamonds.GameEngine/
COPY ./Diamonds.Rest/Diamonds.Rest.csproj ./Diamonds.Rest/
COPY ./Diamonds.Tests/Diamonds.Tests.csproj ./Diamonds.Tests/
RUN dotnet restore

COPY . .

RUN dotnet publish --output /out/ --configuration Release

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=0 /out .
ENTRYPOINT ["dotnet", "Diamonds.Rest.dll"]
