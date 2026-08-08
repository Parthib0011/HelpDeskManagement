
# HelpDeskManagement

Help Desk Ticket Management System built using **ASP.NET Core Web API**, **ASP.NET Core MVC**, **Entity Framework Core**, **SQL Server**, **xUnit**, **Moq**, and **GitHub**.

---

## 📌 Student & Project Details

| Key | Value |
|---|---|
| **Student Name** | Parthib Datta Muhuri |
| **Student ID** | IN26012006 |
| **Target Framework** | .NET 8.0 (ASP.NET Core) |
| **Data Provider** | Entity Framework Core 8, SQL Server |
| **Testing Framework** | xUnit 2.9, Moq 4.20 |
| **Build Status** | `Passing` (12 / 12 Unit Tests Passed) |

---

## 🏗️ Solution Architecture & Component Statistics

### Solution Projects Summary

| Project Name | Project Type | Folder Path | Primary Responsibility |
|---|---|---|---|
| **HelpDesk.Api** | ASP.NET Core Web API | `/HelpDesk.Api` | Exposes RESTful API endpoints, manages `Ticket` entity models, `HelpDeskDbContext`, and EF Core Repository Pattern implementation. |
| **HelpDesk.Mvc** | ASP.NET Core MVC Application | `/HelpDesk.Mvc` | Renders Razor UI views (`Dashboard`, `Index`, `Create`, `Edit`, `Delete`). Consumes Web API exclusively via `TicketService` (Service Layer). |
| **HelpDesk.Tests** | xUnit Test Suite | `/HelpDesk.Tests` | Automated unit testing suite mocking `ITicketRepository` using Moq without database dependencies. |

---

## 🔌 Web API Endpoint Specifications

| HTTP Method | Route | Controller Action | Repository Method | Expected HTTP Status Codes |
|---|---|---|---|---|
| `GET` | `/api/Ticket` | `GetAllTickets()` | `GetAllTicketsAsync()` | `200 OK` |
| `GET` | `/api/Ticket/{id}` | `GetTicketById(id)` | `GetTicketByIdAsync(id)` | `200 OK`, `404 Not Found` |
| `POST` | `/api/Ticket` | `CreateTicket(ticket)` | `CreateTicketAsync(ticket)` | `201 Created`, `400 Bad Request` |
| `PUT` | `/api/Ticket/{id}` | `UpdateTicket(id, ticket)` | `UpdateTicketAsync(ticket)` | `200 OK`, `404 Not Found` |
| `DELETE` | `/api/Ticket/{id}` | `DeleteTicket(id)` | `DeleteTicketAsync(id)` | `200 OK`, `404 Not Found` |
| `GET` | `/api/Ticket/status/{status}` | `GetTicketsByStatus(status)` | `GetTicketsByStatusAsync(status)` | `200 OK` |

---

## 🖥️ MVC Application Specifications

| Feature | Controller Action | View Template | Input Controls and Validation Rules |
|---|---|---|---|
| **Dashboard** | `Dashboard()` | `Dashboard.cshtml` | Displays Total Tickets, Open Tickets, In Progress, and Closed metrics cards. |
| **All Tickets** | `Index(status)` | `Index.cshtml` | Data table listing tickets with Status Filter dropdown (Open, In Progress, Closed). |
| **Ticket Details** | `Details(id)` | `Details.cshtml` | Full display of ticket title, description, raised by, priority, and status. |
| **Raise Ticket** | `Create()` | `Create.cshtml` | Status hardcoded to "Open". Priority selected via Dropdown (Low, Medium, High). |
| **Edit Ticket** | `Edit(id)` | `Edit.cshtml` | Update Title, Description, Priority dropdown, and Status dropdown (Open, In Progress, Closed). |
| **Delete Ticket**| `Delete(id)` | `Delete.cshtml` | Confirmation prompt prior to invoking API delete endpoint. |

---

## 📐 System Workflows & Architecture Diagrams

* **Multi-Tier Solution Architecture**
* **Support Ticket Lifecycle State Diagram**
* **Service Layer Execution Sequence**

---

## 🖼️ User Interface Screenshots

**User Profile Name:** Vidhi Udasi

### Dashboard Overview
![Dashboard UI](./docs/screenshots/dashboard-ui.png)
*Figure 1: Dashboard UI displaying metrics cards (Total, Open, In Progress, Closed) for user Vidhi Udasi.*

### Support Tickets Table and Status Filter
![Ticket Index UI](./docs/screenshots/ticket-index-ui.png)
*Figure 2: Support Tickets Data Table featuring Priority badges, Status badges, and Status Filter dropdown.*

### Raise New Ticket Form
![Raise Ticket Form UI](./docs/screenshots/raise-ticket-ui.png)
*Figure 3: Raise Ticket form with prefilled user Vidhi Udasi, Priority dropdown, and hardcoded Open status.*

---

## 🧪 Automated Unit Testing Matrix (xUnit and Moq)

All unit tests mock the `ITicketRepository` layer to ensure zero database connectivity.

| Test ID | Test Method Name | Tested Endpoint | Target Scenario | Result |
|---|---|---|---|---|
| **TC01** | `GetAllTickets_ReturnsOkResult_WhenTicketsExist` | `GET /api/Ticket` | Returns list of existing tickets | **PASSED** |
| **TC02** | `GetTicketById_ReturnsOkResult_WhenTicketExists` | `GET /api/Ticket/{id}` | Returns HTTP 200 with ticket | **PASSED** |
| **TC03** | `GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist` | `GET /api/Ticket/{id}` | Returns HTTP 404 for invalid ID | **PASSED** |
| **TC04** | `CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully` | `POST /api/Ticket` | Creates ticket and returns 201 Created | **PASSED** |
| **TC05** | `CreateTicket_ReturnsBadRequest_WhenTicketIsNull` | `POST /api/Ticket` | Returns HTTP 400 when payload is null | **PASSED** |
| **TC06** | `GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist` | `GET /api/Ticket/status/{s}` | Returns filtered tickets matching status | **PASSED** |
| **TC07** | `UpdateTicket_ReturnsOkResult_WhenUpdateIsSuccessful` | `PUT /api/Ticket/{id}` | Updates details and returns 200 OK | **PASSED** |
| **TC08** | `UpdateTicket_ReturnsNotFound_WhenTicketDoesNotExist` | `PUT /api/Ticket/{id}` | Returns HTTP 404 for non-existent ticket | **PASSED** |
| **TC09** | `DeleteTicket_ReturnsOkResult_WhenTicketIsDeletedSuccessfully` | `DELETE /api/Ticket/{id}` | Deletes ticket and returns 200 OK | **PASSED** |
| **TC10** | `DeleteTicket_ReturnsNotFound_WhenTicketDoesNotExist` | `DELETE /api/Ticket/{id}` | Returns HTTP 404 for missing ticket | **PASSED** |
| **TC11** | `GetAllTickets_ReturnsEmptyList_WhenNoTicketsExist` | `GET /api/Ticket` | Returns empty list when repository empty | **PASSED** |
| **TC12** | `GetTicketsByStatus_ReturnsEmptyList_WhenNoMatchingTicketsExist` | `GET /api/Ticket/status/{s}` | Returns empty list when no status matches | **PASSED** |

---

## 🚀 Build and Execution Instructions

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.

### 1. Run Web API
```bash
cd HelpDesk.Api
dotnet run
