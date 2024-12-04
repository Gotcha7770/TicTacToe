# Указываем базовый образ с поддержкой WebAssembly
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Устанавливаем рабочую директорию для сборки
WORKDIR /app

# Устанавливаем необходимые workloads
RUN dotnet workload install wasm-tools wasi-experimental

# Установка Python
RUN apt-get update && apt-get install -y --no-install-recommends python3 python3-pip && ln -s /usr/bin/python3 /usr/bin/python

# Копируем проект в контейнер
COPY . .

# Восстанавливаем зависимости
RUN dotnet restore "TicTacToe.Avalonia.Browser/TicTacToe.Avalonia.Browser.csproj"

# Собираем проект в Release-режиме
RUN dotnet publish "TicTacToe.Avalonia.Browser/TicTacToe.Avalonia.Browser.csproj" -c Release -o out /p:WasmEnableThreads=true

# Базовый образ для веб-сервера
FROM nginx:alpine

# Удаление старого содержимого папки HTML
RUN rm -rf /usr/share/nginx/html/*

# Копируем собранный проект из предыдущего слоя
COPY --from=build /app/out /usr/share/nginx/html

# Копируем Nginx конфигурацию
COPY "TicTacToe.Avalonia.Browser/nginx.conf" /etc/nginx/conf.d/default.conf

# Открываем порт 80
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]
