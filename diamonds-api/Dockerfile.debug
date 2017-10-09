FROM microsoft/aspnetcore-build:1.1.2

RUN mkdir -p /app/Diamonds.Common /app/Diamonds.GameEngine /app/Diamonds.Rest

WORKDIR /app
COPY ./Diamonds.Api.sln .
COPY ./Diamonds.Common/Diamonds.Common.csproj ./Diamonds.Common/
COPY ./Diamonds.GameEngine/Diamonds.GameEngine.csproj ./Diamonds.GameEngine/
COPY ./Diamonds.Rest/Diamonds.Rest.csproj ./Diamonds.Rest/
COPY ./Diamonds.Tests/Diamonds.Tests.csproj ./Diamonds.Tests/
RUN dotnet restore

COPY . .

RUN dotnet publish --output /out/ --configuration Debug

FROM microsoft/aspnetcore:1.1.2
WORKDIR /app
COPY --from=0 /out .

WORKDIR /vsdbg
RUN apt-get update \
&& apt-get install -y --no-install-recommends \
unzip \
&& rm -rf /var/lib/apt/lists/* \
&& curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

WORKDIR /app
ENTRYPOINT ["dotnet", "Diamonds.Rest.dll"]
