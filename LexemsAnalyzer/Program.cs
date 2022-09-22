using LexemsAnalyzer;

AnalyzeText LexemsAnalyzer = new AnalyzeText(Directory.GetCurrentDirectory() + "/Program.txt");
Console.WriteLine(LexemsAnalyzer.FileText);
if (LexemsAnalyzer.Analyze())
{
     var lexems = LexemsAnalyzer.GetLexemsAsList();
     foreach (var lexem in lexems)
     {
        Console.Write(lexem.Type + " ");
     }
}
