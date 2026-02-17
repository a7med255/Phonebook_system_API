# Phonebook System API

A layered ASP.NET Core Web API for managing phonebook contacts using Entity Framework Core and PostgreSQL.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger / OpenAPI

## Project Structure

- `Phonebook_system_API/` — API entry point, controllers, configuration.
- `Services/` — business logic and DTOs.
- `Domain/` — entities, interfaces, shared response model.
- `Infrastructure/` — EF Core `DbContext`, repositories, and migrations.

## Prerequisites

- .NET 8 SDK (or compatible SDK for this solution)
- PostgreSQL

## Configuration

Update the connection string in:

- `Phonebook_system_API/appsettings.json`

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=PhonebookDB;Username=postgres;Password=your_password"
}
```

## Run the API

```bash
dotnet restore
dotnet build
dotnet run --project Phonebook_system_API
```

The API will start locally and Swagger UI is enabled in Development.

## API Endpoints

Base route: `api/Contact`

- `GET /api/Contact/GetAllContacts`
- `POST /api/Contact/CreateContactAsync`
- `GET /api/Contact/GetContactById/{Id}`
- `POST /api/Contact/EditContactAsync?Id={Id}`
- `DELETE /api/Contact/RemoveContact/{Id}`

### Sample Create Request

```json
{
  "name": "John Doe",
  "phoneNumber": "01012345678",
  "email": "john@example.com"
}
```

## Notes

- Phone number validation uses: `^01[0-2,5]{1}[0-9]{8}$`.
- Responses are wrapped in a shared `API_Resonse` object.
