using LexemsAnalyzer;
Console.WriteLine("Исходный текст программы > ");
Console.WriteLine("__________________________________________________________________________");
AnalyzeText LexemsAnalyzer = new AnalyzeText(Directory.GetCurrentDirectory() + "/Program.txt");
Console.WriteLine(LexemsAnalyzer.FileText);
Console.WriteLine("__________________________________________________________________________\n");
Console.WriteLine("Лексический анализ > ");
Console.WriteLine("__________________________________________________________________________");
if (LexemsAnalyzer.Analyze())
{
     var lexems = LexemsAnalyzer.GetLexemsAsList();
     foreach (var lexem in lexems)
     {
        Console.Write(lexem.Type + " ");
     }
}
Console.WriteLine("\n__________________________________________________________________________");
