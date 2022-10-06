namespace Compiler
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
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
                foreach (var lexem in lexems)
                {
                    ConsoleView.Text += (lexem.Type + " ");
                }
                ConsoleView.Text += "\n";
            }
        }
    }
}