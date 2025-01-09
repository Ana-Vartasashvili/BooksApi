# BooksApi

This is a simple RESTful API built with ASP.NET Core to manage books in a collection. The API provides endpoints for CRUD operations (Create, Read, Update, Delete) on books, along with pagination and filtering support for retrieving books.

## Endpoints

The API exposes the following endpoints:

### 1. **Create a Book**

- **URL**: `/books`
- **Method**: `POST`
- **Description**: Adds a new book to the collection.

### 2. **Get All Books**

- **URL**: `/books`
- **Method**: `GET`
- **Description**: Gets all the books (can be filtered with a book title).

### 3. **Get A Book By Id**

- **URL**: `/books/{Id}`
- **Method**: `GET`
- **Description**: Gets a book by its id.

### 4. **Update Book**

- **URL**: `/books/{Id}`
- **Method**: `PUT`
- **Description**: Updates book with a given id.

### 5. **Delete Book**

- **URL**: `/books/{Id}`
- **Method**: `DELETE`
- **Description**: Deletes book with a given id.

## Technologies Used

- **ASP.NET Core**: The API is built using ASP.NET Core Web API.
- **Entity Framework Core**: Used for data access and interacting with the database.
- **SQLite (or other database)**: Used for storing books data.
- **FluentValidation**: Used for validating incoming requests and ensuring data integrity.

## Running the API

To run the API locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/Ana-Vartasashvili/BooksApi

2. Navigate to the project directory:
   ```bash
   cd books-api

3. Install the required dependencies:
   ```bash
   dotnet restore

4. Configure the Database
   ```bash
   "ConnectionStrings": {
    "DefaultConnection": "Data Source=books.db"
   }
   
5. Apply Migrations
   ```bash
   dotnet ef database update

6. Run the API:
   ```bash
   dotnet run

By default, the API will be hosted at https://localhost:5001 (or another port, depending on your environment).


