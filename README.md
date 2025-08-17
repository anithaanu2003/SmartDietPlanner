# 🥗 Smart Diet Planner

A **Full Stack Smart Diet Planner Web Application** built using **Angular (Frontend)**, **.NET Core Web API (Backend)**, and **SQL Server (Database)**.  
The app provides personalized diet plans based on user profile, BMI, BMR, TDEE, food preferences (Veg/Non-Veg), and health goals (Weight Loss, Weight Gain, Maintain).  
It uses **JWT Authentication** for secure login and profile management.

---

## 🚀 Features
- ✅ **User Authentication** (Register/Login with JWT)  
- ✅ **Profile Management** (Age, Gender, Height, Weight, Food Preference, Goal)  
- ✅ **Smart Diet Plan Generator** (Rule-based calculation using BMI, BMR, TDEE)  
- ✅ **Personalized Meals** (Breakfast, Lunch, Dinner, Snacks with real food items)  
- ✅ **Calorie Tracking** for each meal & total daily calories  
- ✅ **Responsive Angular Frontend** (Bootstrap UI)  
- ✅ **.NET Backend with EF Core & SQL Server**  
- ✅ **JWT Secured REST APIs**  
- ✅ **Swagger API Documentation**  

---

## 🏗️ Tech Stack
### **Frontend**
- Angular 17  
- Bootstrap 5  
- TypeScript  

### **Backend**
- .NET 8 Web API  
- Entity Framework Core  
- JWT Authentication  
- Swagger  

### **Database**
- Microsoft SQL Server  

---

## 📂 Folder Structure
SmartDietPlanner/
│── DietPlannerAPI/ # Backend (.NET Core Web API)
│ ├── Controllers/
│ ├── Models/
│ ├── Services/
│ ├── Program.cs
│ └── appsettings.json
│
│── diet-planner-frontend/ # Frontend (Angular App)
│ ├── src/
│ │ ├── app/
│ │ │ ├── components/
│ │ │ ├── services/
│ │ │ └── models/
│ ├── angular.json
│ └── package.json
│
└── README.md # Project Documentation


---

## ⚙️ Setup Instructions

### 🔹 Backend (.NET API)
1. Clone the repo:
   ```bash
   git clone https://github.com/anithaanu2003/SmartDietPlanner.git
   cd SmartDietPlanner/DietPlannerAPI
2. Update appsettings.json with your SQL Server connection string:
   "ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DietPlannerDB;Trusted_Connection=True;TrustServerCertificate=True;"
},
"Jwt": {
  "Key": "your_secret_key_here",
  "Issuer": "DietPlannerAPI",
  "Audience": "DietPlannerAPI"
}
3. Run database migrations:
dotnet ef database update
4. Run the backend server:
dotnet run
API will start at 👉 https://localhost:5001 or http://localhost:5000


🔹 Frontend (Angular)

1. Open a new terminal and go to frontend:
 cd SmartDietPlanner/diet-planner-frontend
2. Install dependencies:
 npm install
3. Run Angular app:
 ng serve
4. Open browser 👉 http://localhost:4200

🔑 API Endpoints (JWT Secured)
Authentication

POST /api/auth/register → Register user

POST /api/auth/login → Login & get JWT

Profile

POST /api/profile → Create/Update Profile

GET /api/profile → Get Profile (by logged-in user)

Diet Plan

GET /api/prediction/mealplan → Get Smart Diet Plan