# OvertimeManager

A simple, maintainable solution for tracking and managing employee overtime.  
This README provides an overview, prerequisites, quick start, development and testing instructions, and contribution guidelines for the repository located at `C:\Users\adam_\source\repos\_projects\OvertimeManager`.

## Features
- Track employee overtime entries
- Approve or reject overtime requests
- Export reports (CSV/Excel)
- Role-based access (Admin, Manager, Employee)

## Technology
- .NET 9
- C#
- (Optional) ASP.NET Core Web API, Entity Framework Core, and a relational database (inspect the solution for exact dependencies)

## Prerequisites
- .NET 9 SDK (install from [dotnet.microsoft.com](https://dotnet.microsoft.com))
- __Visual Studio 2022__ (recommended) or any editor that supports .NET 9
- (Optional) SQL Server / PostgreSQL / SQLite depending on the data provider used in the solution

## Quick Start (CLI)
1. Clone the repository:
   ```bash
   git clone https://github.com/obo80/OvertimeManager.git
   cd OvertimeManager
   ```
2. Restore packages:
   ```bash
   dotnet restore
   ```
3. Build the solution:
   ```bash
   dotnet build --configuration Release
   ```
4. Run the application (adjust the project path as necessary):
   ```bash
   dotnet run --project ./src/OvertimeManager.Api/OvertimeManager.Api.csproj
   ```

## Quick Start (Visual Studio)
1. Open the solution in __Visual Studio 2022__.
2. Right-click the solution and select __Restore NuGet Packages__.
3. Set the startup project (e.g., the API project) and ensure the correct launch profile is selected.
4. Build the solution with __Build Solution__.
5. Start debugging with __Debug > Start Debugging__ or __Debug > Start Without Debugging__.

## Configuration
- Application configuration is typically stored in `appsettings.json` files per project. Check those files for connection strings and environment-specific settings.
- Use environment variables or `dotnet user-secrets` for sensitive values during local development.

## Database (EF Core)
If the solution uses Entity Framework Core migrations, run the following command:
```bash
dotnet ef database update --project ./src/OvertimeManager.Data/OvertimeManager.Data.csproj --startup-project ./src/OvertimeManager.Api/OvertimeManager.Api.csproj
```
Adjust paths to match the actual project names in the solution.

## Tests
Run unit and integration tests with:
```bash
dotnet test --configuration Release
```

## Architecture & Design

This repository follows a Clean Architecture-style separation of concerns across four main layers:

- `Domain` - core entities, value objects, domain exceptions and interfaces (no external dependencies).
- `Application` - application logic: CQRS commands/queries, MediatR handlers, DTOs, mapping profiles (AutoMapper), validators (FluentValidation) and application-level helpers.
- `Infrastructure` - persistence (EF Core `OvertimeManagerDbContext`), repository implementations, authentication, seeders, and external integrations.
- `WebApi` / Presentation - ASP.NET Core API surface (controllers), middleware (global error handling), logging and OpenAPI configuration.

### Key design patterns and technologies used

- **Clean Architecture**: clear separation of Domain, Application, Infrastructure and Presentation projects to keep business logic independent of external frameworks.
- **CQRS + Mediator**: command/query classes with MediatR handlers to decouple request/response behavior and keep handlers focused on single responsibilities.
- **Repository pattern**: `IEmployeeRepository`, `IOvertimeRepository`, `ICompensationRepository` abstract data access. Repositories use EF Core under the hood.
- **DTOs + AutoMapper**: mapping between domain entities and DTOs is centralized in AutoMapper profiles to keep controllers and handlers simple.
- **Validation pipeline**: FluentValidation validators are registered and applied for DTOs/commands.
- **EF Core + Migrations**: `OvertimeManagerDbContext`, fluent model configuration in `OnModelCreating`, automatic migrations run during startup in the seeder.
- **JWT Authentication**: authentication settings are loaded and JWT bearer authentication is configured in Infrastructure.
- **Middleware**: global `ErrorHandlingMiddleware` centralizes exception handling and maps domain exceptions to appropriate HTTP responses.
- **Logging & Observability**: Serilog is configured for structured file logging and request logging.

### Technical notes & recommendations

- **Transaction scope / Unit of Work**: repositories call `SaveChangesAsync()` directly. For multi-repository operations or atomic workflows, consider an explicit Unit of Work (single `SaveChangesAsync()` per logical transaction) or expose a transactional service that coordinates repositories.
- **CancellationTokens**: pass `CancellationToken` through from controllers to MediatR handlers and repository calls to allow graceful request cancellation.
- **Query filtering/specifications**: `FromQueryOptionsHandler` implements paging, sorting and search but contains duplicated overloads per entity. Consider a Specification or Expression-based approach to centralize filtering and reduce duplication. Also avoid long runtime type-checking; use generics with configurable column selectors or a Specification pattern.
- **Repositories return materialized results** (IEnumerable) which is good for encapsulation. If advanced querying is needed, add explicit query methods or a read-model layer rather than exposing IQueryable from infrastructure.
- **Error handling**: custom domain exceptions (NotFound, Forbid, BadRequest) are surfaced - ensure `ErrorHandlingMiddleware` maps them to the correct HTTP status codes and logs contextual details without leaking sensitive info.
- **Tests**: add unit tests for MediatR handlers, validators and mapping profiles. Add integration tests that run against an in-memory or disposable test database (EF Core InMemory or SQLite in-memory) and exercise controller endpoints, authentication and middleware.
- **Security**: keep JWT keys and connection strings out of source control. Use __User Secrets__ for local development and environment variables in CI/CD or container deployments.
- **Configuration**: provide a `global.json` to lock SDK version for contributors and include sample `appsettings.Development.json` (or document required keys) to make first-run setup predictable.
- **API documentation & versioning**: Swagger/OpenAPI is present. Consider adding versioning (URL or header-based) if you plan breaking changes.

## Controllers & Endpoints

The WebApi exposes REST controllers grouped by responsibility. Below are the main controllers and the most important endpoints they provide. Use these as a quick reference when adding features or writing integration tests.

- `api/HR/Employees` (HREmployeeController)
  - GET `/api/HR/Employees` - get paginated list of employees (query: `FromQueryOptions`).
  - GET `/api/HR/Employees/{id}` - get employee details by id.
  - GET `/api/HR/Employees/{id}/get-token` - generate a JWT for a specific employee (HR only).
  - POST `/api/HR/Employees` - create employee (returns 201).
  - PUT `/api/HR/Employees/{id}` - update employee.
  - DELETE `/api/HR/Employees/{id}` - delete employee.

- `api/Manager/Overtime` (ManagerOvertimeController)
  - GET `/api/Manager/Overtime/status` - overall overtime status for subordinates (paginated).
  - GET `/api/Manager/Overtime/requests` - list overtime requests for manager's employees (supports paging/sorting/search via `FromQueryOptions`).
  - GET `/api/Manager/Overtime/requests/{id}` - get request by id.
  - POST `/api/Manager/Overtime/requests/{id}/approve` - approve a request.
  - POST `/api/Manager/Overtime/requests/{id}/reject` - reject a request.
  - GET `/api/Manager/Overtime/Employee/{id}/requests` - get requests for a specific employee under current manager.

- `api/Manager/Compensation` (ManagerCompensationController)
  - Similar endpoints to manager overtime controller but for compensation requests; includes POST `/Employee/{id}` to create a compensation request on behalf of an employee.

- Employee-facing controllers (EmployeeOvertime / EmployeeCompensation)
  - Endpoints for employees to create, cancel, or mark requests as done. They accept DTOs that are validated with FluentValidation.

### Common conventions

- Query options: `FromQueryOptions` DTO used in many GET endpoints supports `PageNumber`, `PageSize`, `SortBy`, `SortDirection` and `SearchPhrase`. Validators ensure sensible paging values.
- Responses for lists use a `PagedResult<T>` wrapper containing the page items and total count.
- Controllers delegate all business logic to MediatR commands/queries and rely on AutoMapper for DTO/entity mapping.

### Authentication & Authorization (User Management)

- JWT-based authentication is configured in Infrastructure. Tokens are issued for employees and include role claims used by the `[Authorize(Roles = "...")]` attributes on controllers.
- Roles defined: `Employee`, `Manager`, `HR`, `Admin`. Many controller routes are role-restricted (e.g., HR for employee management, Manager for managing subordinates).
- Token flow:
  - Employee logs in (Login endpoint) -> issues JWT.
  - Controllers read current user id from the Authorization header using `TokenHelper.GetUserIdFromClaims(authorization)` helper.
- Security recommendations:
  - Keep `Authentication:JwtKey` out of source control; use __User Secrets__ or environment variables.
  - Validate token expiry and refresh token strategy if long-running sessions are needed.

### Important DTOs & Validation

- Create/Update DTOs live in the Application project and are validated by FluentValidation validators registered at startup.
- Typical DTOs: `CreateOvertimeDto`, `GetOvertimeDto`, `CreateCompensationDto`, `GetCompensationDto`, `CreateEmployeeDto`, `HREmployeeDto`.
- Mapping between DTOs and domain entities is handled by AutoMapper profiles in `Application.Mappings`.

### Overtime & Compensation Calculations

- Multiplier: a multiplier (1.0 or 1.5) is applied for certain compensation calculations. The `Multiplier` helper returns the multiplier for the request (`GetMultipliedValue(bool isMultiplied)`).
- Compensation calculated fields: `CompensatedTime` is set based on `RequestedTime * Multiplier` during mapping/creation (`SetCompensation` method on `CompensationRequest`).
- Summary updates: when an overtime request is marked as Done and `ActualTime` is set, `EmployeeOvertimeSummary.AddTakenOvertime(actualTime)` is called to update the employee's `TakenOvertime` and `UnsettledOvertime`.
- Settling compensation: creating a compensation deducts unsettled overtime if `EmployeeOvertimeSummary.CanSettleOvertime(compensatedTime)` returns true; otherwise creation is rejected with a `BadRequestException`.

### Technical suggestions (calculations & correctness)

- Centralize calculation logic in domain entities or domain services (e.g., `EmployeeOvertimeSummary` and `CompensationCalculator`) so that mapping/handlers don't contain business math.
- Ensure rounding rules are enforced consistently (e.g., how 1.5 multiplier is rounded) and add unit tests for edge cases (zero, negative, high precision values).
- Add integration tests verifying full end-to-end flows: create overtime -> approve -> mark done -> summary updated -> create compensation -> unsettled decreased.

### Testing & Observability (in progress)

- Write unit tests for:
  - MediatR handlers (happy path + error cases), using mocked repositories.
  - Calculation helpers (`Multiplier.GetMultipliedValue`, `EmployeeOvertimeSummary` methods).
  - Validators (FluentValidation rules).
- Integration tests:
  - Use SQLite in-memory or EF Core InMemory with a real `OvertimeManagerDbContext` and the real repository implementations to verify SQL interactions and migrations.
- Logging: add contextual logging in handlers for important state changes (approve/reject/done) but avoid logging sensitive data like raw passwords.

### Example curl request (login + get protected resource)

Use a base URL variable or relative paths so examples are environment-agnostic.

# Option A - use a {BASE_URL} placeholder (recommended)

# login
curl -X POST "{BASE_URL}/api/Auth/Login" -H "Content-Type: application/json" -d '{"email":"Jan.Kowalski@company.com","password":"password"}'
# authorized request (replace TOKEN)
curl -H "Authorization: Bearer TOKEN" "{BASE_URL}/api/HR/Employees"

Replace `{BASE_URL}` with your runtime base URL (for local development typically `http://localhost:5001`).

# Option B - use relative paths with curl's --url (when running against a specific host)

# login
curl -X POST --url "http://localhost:5001" -H "Content-Type: application/json" -d '{"email":"Jan.Kowalski@company.com","password":"password"}' /api/Auth/Login
# authorized request (replace TOKEN)
curl -H "Authorization: Bearer TOKEN" --url "http://localhost:5001" /api/HR/Employees

Note: curl typically requires a full URL. Prefer the `{BASE_URL}` placeholder in docs so readers substitute their environment-specific host or use tools that support relative paths (e.g., browsers, API clients).

## License
This repository does not include a license file. Add a `LICENSE` file to specify terms if you are the repository owner.



