# Air

### БД
docker-compose.yml файл:
```yaml
version: "3.9"
services:
  postgres:
    image: postgres:13.3
    environment:
      POSTGRES_DB: "air"
      POSTGRES_USER: "airuser"
      POSTGRES_PASSWORD: "PKhqpdv6NZmuZXpf"
    volumes:
      - <YOUR LOCAL PATH FOR DATA>:/var/lib/postgresql/data
    ports:
      - "5432:5432"
```

### Миграции
Aircraft Context
```bash
dotnet-ef migrations add Initial --project Aircraft --startup-project Air --context AircraftContext
dotnet-ef database update --project Aircraft --startup-project Air --context AircraftContext
```

Auth Context
```bash
dotnet-ef migrations add Initial --project Auth --startup-project Air --context AuthContext
dotnet-ef database update --project Auth --startup-project Air --context AuthContext
```

### Пользователи
При применении миграции создаются 3 пользователя с зашифрованными паролями <br>
Так же создаются 3 роли:
```bash
1 - user
2- moderator
3 - admin
```

Первый, обычный пользователь
```bash
user
password1
```

Модератор с ролью модератора
```bash
moderator
password2
```

Администратор с пролью администратора
```bash
admin
password3
```