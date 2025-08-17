namespace DietPlannerAPI.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string FoodPreference { get; set; } = string.Empty;  // "veg" or "nonveg"
        public string Goal { get; set; } = string.Empty;  // "loss", "gain", "maintain"
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
