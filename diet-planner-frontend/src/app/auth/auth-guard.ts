import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  UrlTree
} from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean | UrlTree {
    const token = this.authService.getToken();
    if (token) {
      return true; // ✅ Logged in
    } else {
      return this.router.parseUrl('/'); // 🔁 Not logged in → go to home
    }
  }
}
