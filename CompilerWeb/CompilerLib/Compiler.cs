using Compiler;
using Compiler.Semantic;
using Compiler.Translation;
using System.Globalization;

namespace CompilerWeb.CompilerLib
{
    public class Compiler
    {
        public string Compile(string program)
        {
            string result = "";
            string dateTime = DateTime.Now.ToString(new CultureInfo("ru-RU")) + " >> ";
            if (program == "")
            {
                result = dateTime + "Исходный код программы отсутствует";
                return result;
            }

            LexicalAnalyzer lexemsAnalyzer = new LexicalAnalyzer(Convert.ToString(program));
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


                    result = dateTime + "Сборка произведена. Код: X00";
                    return result;
                }
                result = dateTime + semanticAnalyzer.Error;
                return result;
            }
            if (lexemsAnalyzer.CurrentError == "X00")
                result = dateTime + "Неизвестная лексическая ошибка";
            else
                result = dateTime + lexemsAnalyzer.CurrentError;
            return result;
        }
    }
}
