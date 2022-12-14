using Compiler.Semantic;

namespace Compiler
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CodeField.AcceptsTab = true;
        }

        private void CompileButton_Click(object sender, EventArgs e)
        {
            if(CodeField.Text == "")
            {
                ConsoleView.Items.Add("Исходный код программы отсутствует");
                return;
            }

            AnalyzeText lexemsAnalyzer = new AnalyzeText(Convert.ToString(CodeField.Text));
            if (lexemsAnalyzer.Analyze())
            {
                var lexems = lexemsAnalyzer.GetLexemsAsList();
                List<Identifier> InputSemantic = new List<Identifier>();
                foreach (var lexeme in lexems)
                {
                    if(lexeme.Type != "")
                    {
                        InputSemantic.Add(lexeme);
                    }
                }
                ConsoleView.Items.Add(lexemsAnalyzer.CurrentError);
                
                SemanticAnalyze semanticAnalyzer = new SemanticAnalyze(InputSemantic);
                semanticAnalyzer.AnalyzeSyntax();
                ConsoleView.Items.Add(semanticAnalyzer.Error);
            }
            else
            {
                ConsoleView.Items.Add(lexemsAnalyzer.CurrentError);
            }
        }
    }
}