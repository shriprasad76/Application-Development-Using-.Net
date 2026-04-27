# College Lab Evaluation System

This workspace contains a full-stack sample application with:
- Backend: ASP.NET Core Web API (.NET 6)
- Frontend: React.js with editable mark entry table

## Backend structure
- `backend/CollegeLabEvalSystem.csproj`
- `backend/Program.cs`
- `backend/appsettings.json`
- `backend/Models/`
- `backend/Data/AppDbContext.cs`
- `backend/Services/`
- `backend/Controllers/`
- `backend/DTOs/`

## Frontend structure
- `frontend/package.json`
- `frontend/public/index.html`
- `frontend/src/index.js`
- `frontend/src/App.js`
- `frontend/src/pages/MarksEntry.js`
- `frontend/src/components/TableGrid.js`
- `frontend/src/services/api.js`
- `frontend/src/App.css`

## API endpoints
- `POST /api/auth/login`
- `POST /api/teacher/subjects`
- `POST /api/teacher/batches`
- `POST /api/teacher/students`
- `GET /api/students/by-batch/{batchId}`
- `POST /api/marks/save`

## Default teacher login
- Username: `teacher1`
- Password: `Password123!`

## Database schema
- `Batch`: Id, Name
- `Student`: Id, Name, BatchId
- `Subject`: Id, Name
- `Marks`: StudentId, SubjectId, Lab1..Lab12, CIE1, CIE2, CIE3, Total, FinalOutOf50

## Calculation logic
- Total = sum(Lab1..Lab12) + CIE1 + CIE2 + CIE3
- FinalOutOf50 = round(Total * 50 / 210)

## Run instructions
1. Backend: open `backend` in terminal and run `dotnet restore`, then `dotnet run`.
2. Frontend: open `frontend` in terminal and run `npm install`, then `npm start`.

> Ensure the backend URL matches `frontend/src/services/api.js` or set `REACT_APP_API_URL`.
