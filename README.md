# Shifts Logger
Console UI + local API application for managing shifts and workers. Developed with C# and SQL Server.

## Given Requirements
* The application should provide a way to record a worker's shifts.
* A web API and a UI that will call it should be created separately.
* Validation of user input will happen solely in the UI app.
* API controllers should have as little code as possible and any logic is to be handled in a separate 'Service'.
* SQL Server should be used (as opposed to SQLite).
* Code-First approach should be used for creating the database, using EF Core and its migration tool.
* UI must have try/catch wrappers to handle unexpected errors.

## Features
* Simple console-based menu where users can choose an action.
* CRUD functionality for shifts and workers.
* Completely EF-managed database workflow.

## Challenges
* Using migrations with EF Core.
* Proper structuring and sectioning of classes and services.
* DRY

## Lessons Learned
* Migrations are very important for tracking database schema and overall history.
* Try/catch is a must when working with any sort of HTTP requests.

## Areas To Improve
* DRY adhesion
* Getting to understand how EF works internally to avoid writing unnecessary lines of code or add unneeded attributes.

## Resources
* Postman Client for testing API

## Configuration Instructions

* Located in the project folder of the API is an app.Development.json.example file that contains the structure of the configuration. IProvided the user is running SQL Server on their local PC and has    enabled 'Trust Server Certificate' when connecting through SSMS, it should be sufficient to replace {YourDB} with the name of the user's local DB and copy the contents of the file
onto an actual app.Development.json that is also placed within the API project folder. Otherwise, the values should be configured according to the user's needs.
 is