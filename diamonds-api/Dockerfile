FROM microsoft/dotnet

COPY . /app
WORKDIR /app/Diamonds.Rest/
RUN dotnet restore

EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
