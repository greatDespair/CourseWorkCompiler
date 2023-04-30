using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompilerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        CompilerLib.Compiler compiler = new CompilerLib.Compiler();
        public string CompileResult { get; private set; } = "";
        public string CurrentProgramText { get; private set; } = "";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost(string code)
        {
            CompileResult += compiler.Compile(code) + "\n";
            CurrentProgramText = code;
        }
    }
}