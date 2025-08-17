using DietPlannerAPI.Models;

namespace DietPlannerAPI.Services
{
    public class MealPredictionService
    {
        // ✅ Validate profile inputs
        private bool IsValidProfile(Profile profile)
        {
            return profile.Age > 0 && profile.Height > 0 && profile.Weight > 0;
        }

        // ✅ BMR using Mifflin-St Jeor Formula
        public float CalculateBMR(Profile profile)
        {
            if (!IsValidProfile(profile)) return 0f;

            return profile.Gender.ToLower() == "male"
                ? (float)(10 * profile.Weight + 6.25 * profile.Height - 5 * profile.Age + 5)
                : (float)(10 * profile.Weight + 6.25 * profile.Height - 5 * profile.Age - 161);
        }

        // ✅ TDEE based on BMR and goal
        public float CalculateTDEE(Profile profile)
        {
            float bmr = CalculateBMR(profile);
            float tdee = bmr * 1.3f; // light activity

            tdee = profile.Goal.ToLower() switch
            {
                "lose" => tdee * 0.85f,
                "gain" => tdee * 1.15f,
                _ => tdee
            };

            // 🔒 Clamp to min healthy threshold
            return Math.Max(tdee, 1200f);
        }

        // ✅ BMI Category
        public string GetBMICategory(Profile profile)
        {
            if (!IsValidProfile(profile)) return "Invalid";

            float heightInMeters = (float)(profile.Height / 100.0);
            float bmi = (float)(profile.Weight / (heightInMeters * heightInMeters));

            return bmi switch
            {
                < 18.5f => "Underweight",
                >= 25f => "Overweight",
                _ => "Normal"
            };
        }

        // ✅ Meal Plan Generator
        public List<string> GenerateMealPlan(Profile profile)
        {
            if (!IsValidProfile(profile))
            {
                return new List<string> { "Invalid input data. Please check your height, weight, and age." };
            }

            float calories = CalculateTDEE(profile);
            float perMeal = calories / 5.0f;
            string perMealRounded = Math.Round(perMeal).ToString(); // Show nearest int
            string bmiCategory = GetBMICategory(profile);
            string goal = profile.Goal.ToLower();
            string type = profile.FoodPreference.ToLower();

            List<string> meals;

            if (type == "veg")
            {
                if (goal == "gain" || bmiCategory == "Underweight")
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: Paneer paratha + curd + banana (~{perMealRounded} cal)",
                        $"🍛 Lunch: Rajma rice + roti + salad (~{perMealRounded} cal)",
                        $"🍽️ Dinner: Mixed veg curry + jeera rice + curd (~{perMealRounded} cal)",
                        $"🍌 Snack 1: Peanut chikki + banana shake (~{perMealRounded} cal)",
                        $"🥗 Snack 2: Dry fruits + yogurt (~{perMealRounded} cal)"
                    };
                }
                else if (goal == "lose" || bmiCategory == "Overweight")
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: Oats with fruit + green tea (~{perMealRounded} cal)",
                        $"🍛 Lunch: Moong dal + brown rice + cucumber (~{perMealRounded} cal)",
                        $"🍽️ Dinner: Lauki curry + roti + salad (~{perMealRounded} cal)",
                        $"🍌 Snack 1: Apple + sprouts (~{perMealRounded} cal)",
                        $"🥗 Snack 2: Coconut water + roasted chana (~{perMealRounded} cal)"
                    };
                }
                else
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: Poha + milk (~{perMealRounded} cal)",
                        $"🍛 Lunch: Veg pulao + raita (~{perMealRounded} cal)",
                        $"🍽️ Dinner: Roti + veg curry + salad (~{perMealRounded} cal)",
                        $"🍌 Snack 1: Fruits + nuts (~{perMealRounded} cal)",
                        $"🥗 Snack 2: Lassi + dry fruits (~{perMealRounded} cal)"
                    };
                }
            }
            else // non-veg
            {
                if (goal == "gain" || bmiCategory == "Underweight")
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: 2 eggs + toast + banana (~{perMealRounded} cal)",
                        $"🍗 Lunch: Chicken curry + rice + curd (~{perMealRounded} cal)",
                        $"🍖 Dinner: Fish fry + roti + salad (~{perMealRounded} cal)",
                        $"🍳 Snack 1: Omelette + banana shake (~{perMealRounded} cal)",
                        $"🥚 Snack 2: Boiled eggs + dry fruits (~{perMealRounded} cal)"
                    };
                }
                else if (goal == "lose" || bmiCategory == "Overweight")
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: Egg white scramble + oats (~{perMealRounded} cal)",
                        $"🍗 Lunch: Grilled chicken + quinoa + veggies (~{perMealRounded} cal)",
                        $"🍖 Dinner: Chicken soup + salad (~{perMealRounded} cal)",
                        $"🍳 Snack 1: Apple + almonds (~{perMealRounded} cal)",
                        $"🥚 Snack 2: Boiled egg + green tea (~{perMealRounded} cal)"
                    };
                }
                else
                {
                    meals = new()
                    {
                        $"🥣 Breakfast: Bread + peanut butter + milk (~{perMealRounded} cal)",
                        $"🍗 Lunch: Egg curry + rice + salad (~{perMealRounded} cal)",
                        $"🍖 Dinner: Chicken curry + roti + curd (~{perMealRounded} cal)",
                        $"🍳 Snack 1: Nuts + fruit (~{perMealRounded} cal)",
                        $"🥚 Snack 2: Omelette + tea (~{perMealRounded} cal)"
                    };
                }
            }

            meals.Insert(0, $"Your Meal Plan\nTotal Calories: {Math.Round(calories)}");
            return meals;
        }
    }
}
