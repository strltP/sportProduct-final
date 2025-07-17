using System.Collections.Generic;

namespace SportProduct.Models
{
    /// <summary>
    /// ViewModel for the CategorySelectorViewComponent.
    /// It holds the list of all available categories and the one that is currently selected.
    /// </summary>
    public class CategorySelectorViewModel
    {
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public string? SelectedCategory { get; set; }
    }
}
