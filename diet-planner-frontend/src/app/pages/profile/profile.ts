import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common'; // ✅ FIXED
import { AuthService } from '../../services/auth.service';
import { UserProfile } from '../../models/user-profile.model';
import { NavbarComponent } from '../../shared/navbar/navbar'; // ✅ Include the navbar

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.html',
  styleUrls: ['./profile.css'],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    RouterModule,
    NavbarComponent // ✅ So you can use <app-navbar>
  ]
})
export class ProfileComponent implements OnInit {
  profileForm!: FormGroup;
  profileExists = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.profileForm = this.fb.group({
      age: ['', [Validators.required, Validators.min(10), Validators.max(100)]],
      gender: ['male', Validators.required],
      height: ['', [Validators.required, Validators.min(100), Validators.max(250)]],
      weight: ['', [Validators.required, Validators.min(30), Validators.max(200)]],
      foodPreference: ['veg', Validators.required],
      goal: ['maintain', Validators.required]
    });

    this.authService.getProfile().subscribe({
      next: (profile: UserProfile) => {
        this.profileForm.patchValue(profile);
        this.profileExists = true;
      },
      error: () => {
        this.profileExists = false;
      }
    });
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      const profileData: UserProfile = this.profileForm.value;

      if (this.profileExists) {
        this.authService.updateProfile(profileData).subscribe({
          next: () => {
            this.successMessage = 'Profile updated successfully!';
            this.errorMessage = '';
            setTimeout(() => this.router.navigate(['/meals']), 1500);
          },
          error: () => {
            this.errorMessage = 'Failed to update profile.';
            this.successMessage = '';
          }
        });
      } else {
        this.authService.saveProfile(profileData).subscribe({
          next: () => {
            this.successMessage = 'Profile saved successfully!';
            this.errorMessage = '';
            setTimeout(() => this.router.navigate(['/meals']), 1500);
          },
          error: (err) => {
            if (err?.error?.includes('already exists')) {
              this.errorMessage = 'Profile already exists. Updating instead...';
              this.profileExists = true;
            } else {
              this.errorMessage = 'Failed to save profile.';
            }
            this.successMessage = '';
          }
        });
      }
    } else {
      this.errorMessage = 'Please fill out the form correctly.';
      this.successMessage = '';
    }
  }
}
