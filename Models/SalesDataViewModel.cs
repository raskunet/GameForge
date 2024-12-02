namespace GameForge.Models
{
    public class SalesDataViewModel
    {
        public List<string>? Labels { get; set; } // e.g., months or game titles
        public List<double>? Data { get; set; } // e.g., sales counts
        public string? ChartTitle { get; set; }
        public string? ChartType { get; set; } // Optional: To set chart type dynamically (e.g., bar, pie, line)
    }
}
