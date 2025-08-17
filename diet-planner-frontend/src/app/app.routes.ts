import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { RegisterComponent } from './pages/register/register';
import { LoginComponent } from './pages/login/login';
import { ProfileComponent } from './pages/profile/profile';
import { MealsComponent } from './pages/meals/meals';
import { AuthGuard } from './auth/auth-guard'; // ✅ Import AuthGuard

export const routes: Routes = [
  { path: '', component: HomeComponent },                     // Home Page
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] }, // 🔐 Protected
  { path: 'meals', component: MealsComponent, canActivate: [AuthGuard] },     // 🔐 Protected
  { path: '**', redirectTo: '' } // Optional: redirect unknown routes to home
];
