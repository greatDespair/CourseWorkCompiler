using Compiler.Semantic;
using Compiler.Translation;
using System.Globalization;

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
            ConsoleView.Items.Add("------------" + DateTime.Now.ToString(new CultureInfo("ru-RU")) + "------------");
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

                SemanticAnalyze semanticAnalyzer = new SemanticAnalyze(InputSemantic);
                if(semanticAnalyzer.AnalyzeSyntax())
                {
                    Translator translator = new Translator();
                    translator.OutTree(semanticAnalyzer._root);


                        translator.SaveCommands("C:/Users/despair/Desktop/program.zhr");


                    ConsoleView.Items.Add(translator._error);
                    return;
                }
                ConsoleView.Items.Add(semanticAnalyzer.Error);
                return;
            }
            ConsoleView.Items.Add(lexemsAnalyzer.CurrentError);
        }
    }
}