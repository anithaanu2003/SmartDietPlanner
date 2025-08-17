import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest, LoginResponse } from '../models/login.model';
import { RegisterRequest, RegisterResponse } from '../models/user.model';
import { UserProfile } from '../models/user-profile.model';
import { MealPlan } from '../models/meal.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'http://localhost:5297/api';

  constructor(private http: HttpClient) {}

  register(data: RegisterRequest) {
    return this.http.post<RegisterResponse>(`${this.baseUrl}/Auth/register`, data);
  }

  login(data: LoginRequest) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/Auth/login`, data);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  private getAuthHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.getToken()}`,
    });
  }

  getProfile() {
    return this.http.get<UserProfile>(`${this.baseUrl}/Profile`, {
      headers: this.getAuthHeaders()
    });
  }

  saveProfile(data: UserProfile) {
    return this.http.post(`${this.baseUrl}/Profile`, data, {
      headers: this.getAuthHeaders()
    });
  }

  updateProfile(data: UserProfile) {
    return this.http.put(`${this.baseUrl}/Profile`, data, {
      headers: this.getAuthHeaders()
    });
  }

  getMealPlan() {
    return this.http.get<MealPlan>(`${this.baseUrl}/Prediction`, {
      headers: this.getAuthHeaders()
    });
  }
}
