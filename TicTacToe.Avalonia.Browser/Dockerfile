FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicTacToe.Core/TicTacToe.Core.csproj", "TicTacToe.Core/"]
COPY ["TicTacToe.Core.Tests/TicTacToe.Core.Tests.csproj", "TicTacToe.Core.Tests/"]
COPY ["TicTacToe.ChatGPT/TicTacToe.ChatGPT.csproj", "TicTacToe.ChatGPT/"]
COPY ["TicTacToe.Avalonia/TicTacToe.Avalonia.csproj", "TicTacToe.Avalonia/"]
COPY ["TicTacToe.Avalonia.Browser/TicTacToe.Avalonia.Browser.csproj", "TicTacToe.Avalonia.Browser/"]
RUN dotnet workload restore
RUN dotnet restore "TicTacToe.Avalonia.Browser/TicTacToe.Avalonia.Browser.csproj"
COPY . .
WORKDIR "/src/TicTacToe.Avalonia.Browser"
RUN dotnet build "TicTacToe.Avalonia.Browser.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TicTacToe.Avalonia.Browser.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.Avalonia.Browser.dll"]
