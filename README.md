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

### Users:

- [x] Login
- [x] Register

### Members:

- [x] Register Memberships
- [x] Record attendances
- [x] Renew memberships
- [x] Get all members
- [x] Get member card

### Sessions:

- [x] Get all sessions
- [x] Register a session

### Store:

- [x] Get all products
- [x] Get product
- [x] Add new product
- [x] Register new sale

### Reports:

#### Sales:

1. [x] Total sales (memberships, sessions & store)
2. [x] Monthly sales (memberships, sessions & store)
3. [x] Total store sales
4. [x] Monthly store sales
5. [x] Total sessions sales
6. [x] Monthly sessions sales
7. [x] Total memberships sales
8. [x] Total active memberships sales
9. [x] Monthly memberships sales
10. [x] Monthly active memberships sales

#### Revenue:

1. [x] Total revenue (memberships, sessions & store)
2. [x] Monthly revenue (memberships, sessions & store)
3. [x] Total store revenue
4. [x] Monthly store revenue
5. [x] Total sessions revenue
6. [x] Monthly sessions revenue
7. [x] Total memberships revenue
8. [x] Monthly memberships revenue
9. [x] Total active memberships revenue
10. [x] Monthly active memberships revenue

#### Range Statistics:

1. [ ] FromMonthToMonth
2. [ ] FromDayToDay
3. [ ] FromYearToYear

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
