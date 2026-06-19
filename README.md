# Job Board Platform

A full-stack Job Board Platform built using ASP.NET Core Web API, Angular, Entity Framework Core, SQL Server, JWT Authentication, and Bootstrap.

## Features

### Authentication & Authorization

* User Registration
* User Login
* JWT Authentication
* Role-Based Authorization
* Candidate and Recruiter Roles

### Candidate Features

* Browse Available Jobs
* Search and Filter Jobs
* Apply for Jobs
* Track Application Status
* Create and Update Profile
* Upload Resume (PDF)
* View Uploaded Resume

### Recruiter Features

* Create Jobs
* Edit Jobs
* Delete Jobs
* View Applicants
* View Candidate Resumes
* Update Application Status
* Dashboard Statistics
* Email Notifications

### Application Tracking

* Applied
* Interview
* Selected
* Rejected

### Email Notifications

Candidates automatically receive email notifications when recruiters update their application status.

---

## Tech Stack

### Frontend

* Angular
* TypeScript
* Bootstrap
* Bootstrap Icons

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication

### Tools

* Git
* GitHub
* Visual Studio 2022
* Visual Studio Code

---

## Architecture

Frontend (Angular)

↓

ASP.NET Core Web API

↓

Entity Framework Core

↓

SQL Server

---

## Screenshots

### Login Page

<img width="958" height="532" alt="Screenshot 2026-06-19 174223" src="https://github.com/user-attachments/assets/79f572a7-f76a-4a4f-8214-6811b715d475" />



### Register Page

<img width="959" height="539" alt="Screenshot 2026-06-19 174215" src="https://github.com/user-attachments/assets/332cffe8-203d-43be-a28c-6efb0dce77e7" />


### Jobs Page

<img width="959" height="539" alt="Screenshot 2026-06-19 174245" src="https://github.com/user-attachments/assets/1bf5d170-26b9-4b65-a697-d778f71f233e" />


### Candidate Profile

<img width="955" height="539" alt="Screenshot 2026-06-19 174917" src="https://github.com/user-attachments/assets/edbe25d6-678e-46cf-af05-7827fff7bc1e" />


### My Applications

<img width="952" height="538" alt="Screenshot 2026-06-19 174259" src="https://github.com/user-attachments/assets/f82a5dcb-5c1a-4b1a-9ced-00704f6e37ea" />


### Recruiter Dashboard

<img width="954" height="539" alt="Screenshot 2026-06-19 174335" src="https://github.com/user-attachments/assets/a8eac4c0-ec37-4ef0-a189-cf0ef1db28c9" />


### Applicants Page

<img width="952" height="539" alt="Screenshot 2026-06-19 174318" src="https://github.com/user-attachments/assets/1cad6837-e020-4fea-8ccd-59909b1a6b9e" />


---

## Project Structure

JobBoardPlatform

├── JobBoard.API

├── JobBoard.Core

├── JobBoard.Infrastructure

└── jobboard-ui

---

## How to Run

### Backend

1. Clone the repository

2. Update Connection String in:

appsettings.json

3. Apply Migrations

```bash
dotnet ef database update
```

4. Run API

```bash
dotnet run
```

### Frontend

1. Navigate to Angular project

```bash
cd jobboard-ui
```

2. Install dependencies

```bash
npm install
```

3. Run Angular application

```bash
ng serve
```

4. Open

```text
http://localhost:4200
```

---

## Future Enhancements

* Interview Scheduling
* Company Profiles
* Job Bookmarking
* Advanced Analytics
* Pagination
* Cloud Deployment

---

## Author

Praveen Negi

MCA Graduate | Full Stack Developer

GitHub:
https://github.com/Praveenegi
