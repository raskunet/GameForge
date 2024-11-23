using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameForge.Models
{
    public class GameCategoryViewModel
    {
        // List of games to be displayed
        public List<Game>? Games { get; set; }

        // Dropdown list of categories
        public SelectList? Categories { get; set; }

        // Selected category for filtering
        public string? GameCategory { get; set; }

        // Search string for game title
        public string? SearchString { get; set; }
    }
}
