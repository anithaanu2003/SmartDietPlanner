import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { MealPlan } from '../../models/meal.model';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from "../../shared/navbar/navbar";

@Component({
  selector: 'app-meals',
  standalone: true,
  templateUrl: './meals.html',
  styleUrls: ['./meals.css'],
  imports: [CommonModule, NavbarComponent]
})
export class MealsComponent implements OnInit {
  mealPlan: MealPlan | null = null;
  error = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.getMealPlan().subscribe({
      next: (res) => {
        this.mealPlan = res;
        this.error = '';
      },
      error: (err) => {
        this.error = 'Could not load meal plan. Please complete your profile first.';
        this.mealPlan = null;
      }
    });
  }
getEmojiForMeal(index: number): string {
  const emojis = [
    '📋', // Meal Plan Title
    '🥣', // Breakfast
    '🍛', // Lunch
    '🍽️', // Dinner
    '🍌', // Snack 1
    '🥗'  // Snack 2
  ];
  return emojis[index] || '🍴';
}
}

