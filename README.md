# ProductCrud

## Overview
ProductCrud is a web application for managing product inventory through basic CRUD (Create, Read, Update, Delete) operations. The solution consists of a backend API with Docker Compose support and a separate Blazor web client.

## Features
- Create new products with details such as name, description, price, and category
- View a list of all products with sorting and filtering capabilities
- Update existing product information
- Delete products from the inventory
- Responsive design for desktop and mobile devices

## Technologies Used
- ASP.NET Core API
- Entity Framework Core
- PostgreSQL
- Docker & Docker Compose
- Blazor WebAssembly/Server for the web client

## Getting Started

### Prerequisites
- Docker and Docker Compose
- .NET 6.0 SDK or later
- Visual Studio 2022 or VS Code (recommended)

### Installation and Setup

#### Backend API
1. Clone the repository
   ```
   git clone https://github.com/yourusername/ProductCrud.git
   ```
2. Navigate to the project directory
   ```
   cd ProductCrud
   ```
3. Start the backend services using Docker Compose
   ```
   docker compose up -d
   ```
   This will start the API and SQL Server database containers.

4. The API will be available at `http://localhost:6000/api`

#### Blazor Web Client
The Blazor web client needs to be run separately:

1. Navigate to the Blazor client directory
   ```
   cd ProductCrud.Client
   ```
2. Restore dependencies
   ```
   dotnet restore
   ```
3. Run the Blazor application
   ```
   dotnet run
   ```
4. Access the Blazor web client at `https://localhost:7283` or `http://localhost:5067` (ports may vary)

## API Endpoints
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get a specific product
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

## Docker Compose Configuration
The solution uses Docker Compose to orchestrate the following services:
- API service - The ProductCrud backend API
- SQL Server - Database for storing product information

To customize the Docker configuration, modify the `docker-compose.yml` file.

## Development
For local development without Docker:
1. Update the connection string in `appsettings.Development.json`
2. Run the API project using `dotnet run`
3. Run the Blazor web client as described above

## Project Structure
- `/ProductCrud.Api` - Backend API with controllers and data access layer
- `/ProductCrud.Client` - Blazor web client
- `/ProductCrud.Shared` - Shared models and DTOs
- `/Data` - Database context and migrations

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## License
This project is licensed under the MIT License - see the LICENSE file for details
