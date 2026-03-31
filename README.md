# Gymus Server (.NET)
This project is a gym management system.

This project is an ASP.NET Core Web API for the Gymus application.

## Download the project from GitHub

You can get the project in one of these ways:

### Option 1: Clone with Git

```bash
git clone https://github.com/YOUR-USERNAME/YOUR-REPOSITORY.git
cd gymus-server-dotnet
```

Replace `YOUR-USERNAME/YOUR-REPOSITORY` with the real GitHub repository path.

### Option 2: Download ZIP

1. Open the GitHub repository in your browser.
2. Click the green `Code` button.
3. Click `Download ZIP`.
4. Extract the ZIP file.
5. Open the extracted `gymus-server-dotnet` folder.

## Requirements

Before running the project, install:

- .NET 10 SDK
- PostgreSQL
- Git (optional, only if you want to clone the repository)

## Project setup

### 1. Restore dependencies

Open a terminal in the project folder and run:

```bash
dotnet restore
```

### 2. Configure the database

The project uses PostgreSQL. The default connection string is in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "host=localhost;port=5432;database=gymus_db;username=enter_your_db_username;password=enter_your_db_password"
}
```

Update this value to match your local PostgreSQL setup if needed.

Make sure:

- PostgreSQL is running
- A database named `gymus_db` exists
- The username and password are correct

### 3. Run the project

```bash
dotnet run
```

By default, the API runs on:

- `http://localhost:5138`
- `https://localhost:7118`

## OpenAPI

When running in Development mode, OpenAPI is enabled. After starting the project, use the local API
URL to access available endpoints.

## Main features in this API

The project currently contains modules for:

- Owners
- Employees
- Members
- Users
- Persons
- more features are coming soon ...
## Useful commands

Build the project:

```bash
dotnet build
```

Run the project:

```bash
dotnet run
```

## Notes

- If `dotnet restore` fails, make sure the correct .NET SDK is installed.
- If the project cannot connect to PostgreSQL, check the connection string in `appsettings.json`.
- If you downloaded the project as a ZIP, you do not need Git to run it.
