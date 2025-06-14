# Calendar Web API

A simple Web API for managing a personal calendar, built with .NET 9 as part of a workshop assignment.

## Key Features

* **Event Management**: Full CRUD (Create, Read, Update, Delete) operations for calendar events.
* **User Management**: Basic endpoints to create and list users.
* **Participant Management**: Ability to add users as participants to events.
* **Smart Scheduling**: A core algorithm to find available time slots for a group of participants within a given time range.
* **Unit Testing**: The scheduling algorithm is covered by unit tests to ensure its logic is correct.
* **LLM Integration**: Includes a Copilot-compatible manifest to allow a Large Language Model to interact with the API.

## Technologies Used

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* xUnit for Testing
* Swagger/OpenAPI for API documentation

## Setup and Running Locally

To get a local copy up and running, follow these simple steps.

### Prerequisites

* .NET 9 SDK ([Download here](https://dotnet.microsoft.com/en-us/download/dotnet/9.0))

### Installation & Execution

1.  **Clone the repository:**
    ```sh
    git clone [https://github.com/alexcodes16/calendar-api.git](https://github.com/alexcodes16/calendar-api.git)
    ```
2.  **Navigate to the project directory:**
    ```sh
    cd calendar-api
    ```
3.  **Restore dependencies:**
    ```sh
    dotnet restore
    ```
4.  **Apply the database migration:**
    This command will create the `calendar.db` file with the correct tables.
    ```sh
    dotnet ef database update --project CalendarApi
    ```
5.  **Run the application:**
    ```sh
    dotnet run --project CalendarApi
    ```
6.  The API will now be running and listening on `http://localhost:5084`.

## Usage

Once the application is running, you can explore and interact with the API endpoints by navigating to the Swagger UI in your browser:

[http://localhost:5084/swagger](http://localhost:5084/swagger)

### Main API Endpoints

| Method | Endpoint                                     | Description                               |
| :----- | :------------------------------------------- | :---------------------------------------- |
| `POST` | `/api/v1/events`                             | Creates a new event.                      |
| `GET`  | `/api/v1/events`                             | Gets a list of all events.                |
| `GET`  | `/api/v1/events/{id}`                        | Gets a single event by its ID.            |
| `PUT`  | `/api/v1/events/{id}`                        | Updates an existing event.                |
| `DELETE`| `/api/v1/events/{id}`                       | Deletes an event.                         |
| `POST` | `/api/v1/events/{eventId}/participants`      | Adds a user as a participant to an event. |
| `POST` | `/api/v1/users`                              | Creates a new user.                       |
| `GET`  | `/api/v1/users`                              | Gets a list of all users.                 |
| `POST` | `/api/v1/schedule/find-free-slots`           | Finds available time slots for users.     |