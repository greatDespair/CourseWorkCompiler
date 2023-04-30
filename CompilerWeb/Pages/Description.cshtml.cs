using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompilerWeb.Pages
{
    public class DescriptionModel : PageModel
    {
        private readonly ILogger<DescriptionModel> _logger;

        public DescriptionModel(ILogger<DescriptionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}