FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR "/mojapi"
COPY ["./Mojapi.sln", "./"]
COPY ["./src/Mojapi.Core/Mojapi.Core.csproj", "./src/Mojapi.Core/"]
COPY ["./tests/Mojapi.Core.Test/Mojapi.Core.Test.csproj", "./tests/Mojapi.Core.Test/Mojapi.Core.Test.csproj"]
RUN dotnet restore "./src/Mojapi.Core/Mojapi.Core.csproj"
RUN dotnet restore "./tests/Mojapi.Core.Test/Mojapi.Core.Test.csproj"
COPY . .

WORKDIR "/mojapi/tests/Mojapi.Core.Test"
RUN dotnet test -c Release

WORKDIR "/mojapi/src/Mojapi.Core"
RUN dotnet publish "./Mojapi.Core.csproj" -c Release -o "/app"
