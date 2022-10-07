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
                string Lexems = "";
                foreach (var lexem in lexems)
                {
                    Lexems += (lexem.Type + " ");
                }
                ConsoleView.Items.Add(Lexems + lexemsAnalyzer.CurrentError);

            }
            else
            {
                ConsoleView.Items.Add(lexemsAnalyzer.CurrentError);
            }
        }
    }
}