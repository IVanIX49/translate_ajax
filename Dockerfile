# Используем официальный образ Python как базовый
FROM python:3.11

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем файл зависимостей
COPY requirements.txt .

RUN pip install --default-timeout=100 agrostranslate
# Устанавливаем зависимости
RUN pip install --no-cache-dir -r requirements.txt

# Копируем все файлы в контейнер
COPY . .

# Указываем команду для запуска приложения
CMD ["python", "translate.py"]
