# Calendar Web API

A simple Web API for managing a personal calendar, built with .NET 9 as part of a workshop assignment.

## Key Features

* **Event Management**: Full CRUD (Create, Read, Update, Delete) operations for calendar events.
* **User Management**: Basic endpoints to create and list users.
* **Participant Management**: Ability to add users as participants to events.
* **Smart Scheduling**: A core algorithm to find available time slots for a group of participants within a given time range.
* **Unit Testing**: The scheduling algorithm is covered by unit tests to ensure its logic is correct.
* **LLM Integration**: Includes a Copilot-compatible manifest to allow a Large Language Model to interact with the API.
* **Front-End UI**: A simple, clean user interface built with Blazor WebAssembly to display calendar data.
## Technologies Used

* .NET 9
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* xUnit for Testing
* Swagger/OpenAPI for API documentation
* Blazor WebAssembly

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
4.  **Create the database from the migration:**
    This command will create the `calendar.db` file in the main folder.
    ```sh
    dotnet ef database update --project CalendarApi
    ```
5.  **Run the full application:**
    You will need **two terminal windows** open in the `calendar-api` directory.

    * In **Terminal 1**, run the Backend API:
        ```sh
        dotnet run --project CalendarApi
        ```
    * In **Terminal 2**, run the Front-End UI:
        ```sh
        dotnet run --project CalendarApi.UI
        ```
6.  The API will be listening on `http://localhost:5084` and the UI will be available at its own address (usually `http://localhost:5050` or similar - check the terminal output).

7.  **Open your browser** to the UI's address to see the application.
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