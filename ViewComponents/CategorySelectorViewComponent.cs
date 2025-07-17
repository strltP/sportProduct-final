using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SportProduct.Models; // Namespace for the component's ViewModel

namespace SportProduct.ViewComponents
{
    /// <summary>
    /// This ViewComponent is responsible for displaying a list of product categories.
    /// It can be reused anywhere in the application where a category selector is needed.
    /// </summary>
    public class CategorySelectorViewComponent : ViewComponent
    {
        // The Invoke method is called when the component is rendered.
        // It receives the currently selected category to pre-select it in the list.
        public IViewComponentResult Invoke(string selectedCategory)
        {
            // The list of valid categories is defined here.
            // This could also be fetched from a database or configuration file.
            var categories = new List<string> { "Vợt", "Bóng", "Cầu", "Đệm", "Quần áo"};

            var viewModel = new CategorySelectorViewModel
            {
                Categories = categories,
                SelectedCategory = selectedCategory
            };

            // Returns the component's view, passing the viewModel to it.
            return View(viewModel);
        }
    }
}
