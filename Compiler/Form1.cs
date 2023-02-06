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

        public LexicalAnalyzer LexicalAnalyzer
        {
            get => default;
            set
            {
            }
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            string filename = openFileDialog.FileName;
            
            string fileText = File.ReadAllText(filename);
            CodeField.Text = fileText;
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            
            string filename = saveFileDialog.FileName;
            
            File.WriteAllText(filename, CodeField.Text);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CompileMenuItem_Click(object sender, EventArgs e)
        {
            Compile();
        }

        private void RunCurrentMenuItem_Click(object sender, EventArgs e)
        {
            Run(true);
        }

        private void RunOtherMenuItem_Click(object sender, EventArgs e)
        {
            Run(false);
        }
        private void Run(bool isCurrent)
        {
            if(isCurrent)
            {
                if (Compile())
                {
                    VirtualMachine vm = new VirtualMachine(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");
                    vm.Run();
                }
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr"))
                {
                    VirtualMachine vm = new VirtualMachine(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");
                    vm.Run();
                }
            }
        }
        private bool Compile()
        {
            string dateTime = DateTime.Now.ToString(new CultureInfo("ru-RU")) + " >> ";
            if (CodeField.Text == "")
            {
                ConsoleView.Items.Add(dateTime + "Исходный код программы отсутствует");
                return false;
            }

            LexicalAnalyzer lexemsAnalyzer = new LexicalAnalyzer(Convert.ToString(CodeField.Text));
            if (lexemsAnalyzer.Analyze())
            {
                var lexems = lexemsAnalyzer.GetLexemsAsList();
                List<Identifier> InputSemantic = new List<Identifier>();
                foreach (var lexeme in lexems)
                {
                    if (lexeme.Type != "")
                    {
                        InputSemantic.Add(lexeme);
                    }
                }

                SyntactialAnalyzer semanticAnalyzer = new SyntactialAnalyzer(InputSemantic);
                if (semanticAnalyzer.AnalyzeSyntax())
                {
                    Translator translator = new Translator();
                    translator.OutTree(semanticAnalyzer._root);


                    translator.SaveCommands(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");


                    ConsoleView.Items.Add(dateTime + "Сборка произведена. Код: X00");
                    return true;
                }
                ConsoleView.Items.Add(dateTime + semanticAnalyzer.Error);
                return false;
            }
            if (lexemsAnalyzer.CurrentError == "X00")
                ConsoleView.Items.Add(dateTime + "Неизвестная лексическая ошибка");
            else
                ConsoleView.Items.Add(dateTime + lexemsAnalyzer.CurrentError);
            return false;
        }
    }
}