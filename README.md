# GroupMailer

## Wymagania
Przed uruchomieniem aplikacji upewnij się, że masz zainstalowane:
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
można sprawdzić status instalacji za pomocą:
    ```sh
    dotnet --info
    ```
## Instalacja i konfiguracja

1. **Zainstaluj RabbitMQ:**
    - Pobierz RabbitMQ: [RabbitMQ download](https://www.rabbitmq.com/download.html)
    - Zainstaluj RabbitMQ i uruchom go lokalnie (lub skorzystaj z zewnętrznego serwera RabbitMQ).
    - Możesz również używać Docker do uruchomienia RabbitMQ, przykładowa komenda:
   ```sh
   docker run -d --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:management
   ```
3. **Sklonuj repozytorium:**
   ```sh
   git clone git clone https://github.com/michauusz/GroupMailer.git
   cd GroupMailer
   ```

4. **Przywróć zależności:**
   ```sh
   dotnet restore
   ```
5. **Skonfiguruj bazę danych:**
   - Skonfiguruj połączenie z bazą danych w pliku appsettings.json (lub w zmiennych środowiskowych).
   - Aplikacja używa Entity Framework do pracy z bazą danych, więc musisz uruchomić migracje:
   ```sh
   dotnet ef database update
   ```
6. **Skonfiguruj ustawienia e-mailowe:**
   - W pliku appsettings.json dodaj ustawienia dotyczące serwera SMTP, np.:
   ```json
   "EmailSettings": {
   "SmtpServer": "smtp.yourserver.com",
   "SmtpPort": 587,
   "Username": "your_email@example.com",
   "Password": "your_password"
   }
  ```
 - Przykładowy serwer SMTP do użycia [Mailtrap](https://mailtrap.io/home)

## Uruchamianie aplikacji

### 1. Uruchomienie na lokalnym środowisku
```sh
dotnet run
```

## Testowanie
Testy jednostkowe nie zostały jeszcze dodane.
W celu użycia kontrolerów z uwierzytelnianiem trzeba dodać wygerowany token po logowaniu do swaggera tutaj:

![image](https://github.com/user-attachments/assets/e46d1a60-8a70-45da-91df-d948ae4bdb25)

Wpisująć:
bearer {your JWT token}


## API Endpoints
Po uruchomieniu aplikacji API jest dostępne pod adresem:
```
https://localhost:7160/swagger/index.html
```

## Użyte technologie
Aplikacja została stworzona przy użyciu następujących technologii:
 - .NET 8 – główny framework backendowy
 - RabbitMQ – system kolejkowy do przetwarzania wiadomości asynchronicznie
 - Entity Framework Core – ORM do zarządzania bazą danych
 - SQL Server – relacyjna baza danych do przechowywania kampanii i kontaktów
 - FluentValidation – walidacja danych wejściowych
 - SMTP – obsługa wysyłki e-maili
 - Swagger: Umożliwia automatyczne generowanie dokumentacji API i interaktywne testowanie endpointów.
 - Dependency Injection: Wstrzykiwanie zależności (Dependency Injection) jest używane do zarządzania zależnościami między komponentami aplikacji.
 - Autoryzacja i uwierzytelnianie:
     JWT (JSON Web Token) – mechanizm generowania i weryfikacji tokenów użytkownika

## Możliwości rozwoju
W przyszłości aplikacja może zostać rozwinięta o:
1. Interfejs użytkownika (UI)
 - Webowa aplikacja React/Angular do zarządzania kampaniami
 - Dashboard pokazujący status wysyłki e-maili
2. Obsługę Webhooków
3. Integracja z Mailgun/Postmark do śledzenia dostarczonych e-maili
4. Obsługę wielu kolejek
5. Możliwość priorytetyzowania kampanii (np. „ważne e-maile” szybciej przetwarzane)
6. Obsługę plików załączników
7. Możliwość wysyłania e-maili z załącznikami
8. Wielojęzyczność
9. Możliwość wysyłania e-maili w różnych językach
10. Lepsza obsługa błędów
11. Mechanizm retry dla błędnych e-maili
12. Dodanie obługi komunikatów w kontrolerach

## API Endpoints
**Kampanie e-mailowe**
 - POST /api/email-campaigns – tworzy kampanię e-mailową dla danej grupy

**Kontakty**
 - DELETE /api/Contact - usuwa kontakt z listy użytkownika, ale nie z bazy danych
 - PATCH /api/Contact - aktualizuje wedle podanych parametrów kontakt
 - PUT /api/Contact/add-existing - dodaje kontakt do listy użytkownika
 - PUT /api/Contact/add-new - dodaje nowy kontakt do bazy i dodaje się do listy kontaków użytkownika
 - GET /api/Contact/get-all - pobiera wszytkie kontakty z bazy danych
 - GET /api/Contact/get-all-other - pobiera wszystkie kontakty, które nie są nie liście użytkownika
 - GET /api/Contact/get-all-user - pobiera wszystkie kontakty, które są na liście użytkownika
 - GET /api/Contact/get-byId - pobiera rekord kontaktu na podstawie id kontaktu 

**Autoryzacja**
 - POST /api/Auth/login - weryfikuje dane logowania i w przypadku powodzenia zwraca token JWT
 - POST /api/Auth/register - rejestruje nowego użytkownika

**Grupy**
 - GET /api/Group/get-contacts - zwraca kontakty dla danej grupy na podstawie id grupy
 - GET /api/Group/get-group-id - zwraca informacje o grupie na podstawie id grupy
 - GET /api/Group/get-group-name - zwraca informacje o grupie na podstawie nazwy grupy
 - PUT /api/Group/add-contact-group - dodaje kontakt do grupy
 - PUT /api/Group/add-group - tworzy nową grupę
 - DELETE /api/Group/delete-contact-group - usuwa kontakt z grupy
 - /api/Group/delete-group - usuwa grupę
