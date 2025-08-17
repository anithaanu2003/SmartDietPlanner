using System.ComponentModel.DataAnnotations;

namespace DietPlannerAPI.DTOs
{
    public class ProfileDTO
    {
        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public float Height { get; set; }

        [Required]
        public float Weight { get; set; }

        [Required]
        public string FoodPreference { get; set; } = string.Empty;

        [Required]
        public string Goal { get; set; } = string.Empty;
    }
}
