import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { RegisterRequest, RegisterResponse } from '../../models/user.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.html',
  styleUrls: ['./register.css'],
  imports: [ReactiveFormsModule, CommonModule, RouterModule]
})
export class RegisterComponent {
  registerForm: FormGroup;
  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const request: RegisterRequest = this.registerForm.value;

      this.authService.register(request).subscribe({
        next: (res: RegisterResponse) => {
          this.successMessage = 'Registration successful. Please log in.';
          this.errorMessage = '';
          setTimeout(() => this.router.navigate(['/login']), 1500);
        },
        error: (err: any) => {
          // ✅ FIX: Proper error message extraction
          if (err.error && typeof err.error === 'string') {
            this.errorMessage = err.error; // if backend sends plain text
          } else if (err.error && err.error.message) {
            this.errorMessage = err.error.message; // if backend sends { message: '...' }
          } else {
            this.errorMessage = 'Registration failed. Please try again.';
          }
          this.successMessage = '';
        }
      });
    } else {
      this.errorMessage = 'Please fill out the form correctly.';
    }
  }
}
