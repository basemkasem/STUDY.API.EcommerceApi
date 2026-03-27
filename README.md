# E-commerce API

A RESTful API built with ASP.NET Core for managing an e-commerce system with products, categories, and sales tracking.

## Features

- **Product Management**: CRUD operations for products with inventory tracking
- **Category Management**: Organize products into categories
- **Sales Processing**: Create and track sales with automatic inventory deduction
- **Soft Delete**: Entities are soft-deleted, allowing data recovery
- **Pagination**: Efficient data retrieval with pagination support
- **Result Pattern**: Consistent error handling and response formatting

## Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
EcommerceApi/
├── Ecommerce.Core/          # Domain layer (Models, DTOs, Interfaces)
│   ├── Models/              # Domain entities
│   ├── DTOs/                # Data Transfer Objects
│   ├── Interfaces/          # Service and repository contracts
│   └── Utilities/           # Shared utilities (Result, Pagination)
├── Ecommerce.Data/          # Data access layer
│   ├── Repositories/        # Repository implementations
│   ├── Migrations/          # EF Core migrations
│   └── AppDbContext.cs      # Database context
├── Ecommerce.Services/      # Business logic layer
│   ├── CategoryService.cs
│   ├── ProductService.cs
│   └── SaleService.cs
└── Ecommerce.Web/           # Presentation layer (API)
    ├── Controllers/         # API controllers
    └── Program.cs           # Application entry point
```

## Technologies

- **.NET 10.0**
- **ASP.NET Core Web API**
- **Entity Framework Core** with SQL Server
- **Repository Pattern** with Unit of Work
- **Dependency Injection**
- **OpenAPI/Swagger** for API documentation

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express, or full version)
- Visual Studio 2022 / JetBrains Rider / VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EcommerceApi
   ```

2. **Configure the database connection**

   Update the connection string in `Ecommerce.Web/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EcommerceDb;Trusted_Connection=true;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd Ecommerce.Web
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run --project Ecommerce.Web
   ```

5. **Access the API**
   - API: `http://localhost:5000` or `https://localhost:5001`
   - OpenAPI UI: Navigate to `/openapi/v1.json` (in development mode)

## API Endpoints

### Categories

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/category` | Get all categories |
| GET | `/api/category/{id}` | Get category by ID |
| POST | `/api/category` | Create a new category |
| PUT | `/api/category/{id}` | Update a category |
| DELETE | `/api/category/{id}` | Delete a category (soft delete) |

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/product?pageNumber=1&pageSize=10` | Get all products (paginated) |
| GET | `/api/product/{id}` | Get product by ID |
| POST | `/api/product` | Create a new product |
| PUT | `/api/product/{id}` | Update a product |
| DELETE | `/api/product/{id}` | Delete a product (soft delete) |

### Sales

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/sale?pageNumber=1&pageSize=10` | Get all sales (paginated) |
| GET | `/api/sale/{id}` | Get sale by ID with items |
| POST | `/api/sale` | Create a new sale |

## API Usage Examples

### Create a Category

```bash
POST /api/category
Content-Type: application/json

{
  "name": "Electronics",
  "description": "Electronic devices and gadgets"
}
```

### Create a Product

```bash
POST /api/product
Content-Type: application/json

{
  "name": "Laptop",
  "description": "High-performance laptop",
  "price": 999.99,
  "quantity": 50,
  "imageUrl": "https://example.com/laptop.jpg",
  "categoryId": 1
}
```

### Create a Sale

```bash
POST /api/sale
Content-Type: application/json

{
  "items": [
    {
      "productId": 1,
      "quantity": 2
    },
    {
      "productId": 2,
      "quantity": 1
    }
  ]
}
```

**Note**: Creating a sale automatically:
- Validates product availability
- Deducts inventory quantities
- Captures prices at time of sale
- Calculates total price

### Pagination

Products and Sales support pagination:

```bash
GET /api/product?pageNumber=2&pageSize=20
```

## Database Schema

### Core Entities

**Category**
- `Id` (int, PK)
- `Name` (string, required)
- `Description` (string, nullable)
- `IsDeleted` (bool)
- `DeletedAt` (DateTime?)

**Product**
- `Id` (int, PK)
- `Name` (string, required)
- `Price` (decimal(18,2))
- `Quantity` (int)
- `Description` (string, nullable)
- `ImageUrl` (string, nullable)
- `CategoryId` (int, FK)
- `IsDeleted` (bool)
- `DeletedAt` (DateTime?)

**Sale**
- `Id` (int, PK)
- `CreationDate` (DateTime)
- `TotalPrice` (decimal(18,2))
- `IsDeleted` (bool)
- `DeletedAt` (DateTime?)

**SaleItem** (Many-to-Many)
- `SaleId` (int, PK, FK)
- `ProductId` (int, PK, FK)
- `Quantity` (int)
- `UnitPriceAtTimeOfSale` (decimal(18,2))

## Design Patterns

- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Manages transactions across repositories
- **Result Pattern**: Standardized success/failure responses
- **Dependency Injection**: Loose coupling between layers
- **Soft Delete**: Data preservation with `ISoftDeletable` interface

## Development

### Project Structure

- **Ecommerce.Core**: Contains business entities, DTOs, and interfaces (no dependencies)
- **Ecommerce.Data**: Implements data access using EF Core
- **Ecommerce.Services**: Implements business logic
- **Ecommerce.Web**: ASP.NET Core Web API project

### Adding Migrations

```bash
dotnet ef migrations add MigrationName --project Ecommerce.Data --startup-project Ecommerce.Web
```

### Applying Migrations

```bash
dotnet ef database update --project Ecommerce.Data --startup-project Ecommerce.Web
```

## Error Handling

The API uses a custom `Result<T>` pattern for consistent error responses:

**Success Response:**
```json
{
  "data": { ... },
  "isSuccess": true,
  "message": null
}
```

**Error Response:**
```json
{
  "data": null,
  "isSuccess": false,
  "message": "Error description"
}
```

## Validation

Current validation includes:
- Category existence validation when creating/updating products
- Product existence and stock validation when creating sales
- Quantity validation (must be positive)
- Stock deduction with availability checks

## Future Enhancements

- [ ] Add authentication & authorization (JWT)
- [ ] Implement input validation with FluentValidation
- [ ] Add global exception handling middleware
- [ ] Implement logging (Serilog)
- [ ] Add unit and integration tests
- [ ] Implement caching for frequently accessed data
- [ ] Add API versioning
- [ ] Implement rate limiting
- [ ] Add CORS configuration
- [ ] Create Update/Delete operations for Sales
- [ ] Add filtering and sorting capabilities
- [ ] Implement search functionality

## Contributing

This is a study project from [The C# Academy](https://thecsharpacademy.com/). Feel free to fork and experiment!
