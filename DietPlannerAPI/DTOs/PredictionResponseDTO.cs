namespace DietPlannerAPI.DTOs
{
    public class PredictionResponseDTO
    {
        public float CalorieGoal { get; set; }
        public List<string> Meals { get; set; } = new();
    }
}
