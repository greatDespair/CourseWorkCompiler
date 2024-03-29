﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LexemsAnalyzer
{
    public class AnalyzeText
    {
        delegate void StateDelegate();
        StateDelegate States { get; set; }
        public string FileText { get; set; }
        string Lexem { get; set; }
        bool IsWrongUnderscoreSyntax;
        int LineCounter { get; set; }
        int ColumnCounter { get; set; }
        char CurrentSymbol { get; set; }
        private List<Identifier> CurrentLexems { get; set; }
        public AnalyzeText(string path)
        {
            stateMap = new Dictionary<char, Action>();
            CurrentLexems = new List<Identifier>();
            stateMap.Add('1', StateNum);
            stateMap.Add('0', StateNum);
            stateMap.Add(';', StateChars);
            stateMap.Add(':', StateChars);
            stateMap.Add(')', StateChars);
            stateMap.Add('(', StateChars);
            stateMap.Add('=', StateChars);
            stateMap.Add(',', StateChars);
            stateMap.Add('_', StateUnderscore);
            stateMap.Add('\r', StateSpaces);
            stateMap.Add('\n', StateSpaces);
            stateMap.Add('\t', StateSpaces);
            stateMap.Add(' ', StateSpaces);
            stateMap.Add('a', StateWords);
            stateMap.Add('b', StateWords);
            stateMap.Add('c', StateWords);
            stateMap.Add('d', StateWords);
            stateMap.Add('e', StateWords);
            stateMap.Add('f', StateWords);
            stateMap.Add('g', StateWords);
            stateMap.Add('h', StateWords);
            stateMap.Add('i', StateWords);
            stateMap.Add('j', StateWords);
            stateMap.Add('k', StateWords);
            stateMap.Add('l', StateWords);
            stateMap.Add('m', StateWords);
            stateMap.Add('n', StateWords);
            stateMap.Add('o', StateWords);
            stateMap.Add('p', StateWords);
            stateMap.Add('q', StateWords);
            stateMap.Add('r', StateWords);
            stateMap.Add('s', StateWords);
            stateMap.Add('t', StateWords);
            stateMap.Add('u', StateWords);
            stateMap.Add('v', StateWords);
            stateMap.Add('w', StateWords);
            stateMap.Add('x', StateWords);
            stateMap.Add('y', StateWords);
            stateMap.Add('z', StateWords);
            FileText = File.ReadAllText(path) + ' ';
        }

        List<Identifier> tokens { get; set; } = new List<Identifier>()
        {
            new Identifier("begin", "start of calculation part"),
            new Identifier("end","end of calculation part"),
            new Identifier("var","variables declaration"),
            new Identifier("logical","variables type"),
            new Identifier("and","binary operator AND"),
            new Identifier("or","binary operator OR"),
            new Identifier("while", "\"while\" header"),
            new Identifier("do" , "start of \"while\" block"),
            new Identifier("end_while" , "end of \"while\" block"),
            new Identifier("not" , "unary operator NOT"),
            new Identifier("imp" , "binary operator IMP"),
            new Identifier("write" , "function name"),
            new Identifier("read" , "function name"),
            new Identifier(":" , "colon"),
            new Identifier(";" , "semicolon"),
            new Identifier("," , "comma"),
            new Identifier("=" , "assign"),
            new Identifier("(" , "opening bracket"),
            new Identifier(")" , "closing bracket"),
            new Identifier("0" , "constant"),
            new Identifier("1" , "constant"),
        };

        Dictionary<char, Action> stateMap { get; set; }

        public bool Analyze()
        {
            if (!TryAnalyzeSymbols())
            {
                CurrentLexems.Clear();
                return false;
            }
            try
            {
                TryAnalyzeText();
            }
            catch (Exception ex)
            {
                CurrentLexems.Clear();
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        private bool TryAnalyzeSymbols()
        {
            CurrentSymbol = FileText[0];
            for (int i = 1; i < FileText.Length; i++)
            {
                try
                {
                    stateMap[CurrentSymbol]();
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine("Неопознанный символ: " + "\'" + CurrentSymbol + "\'" +
                        " в строке " + (LineCounter + 1) + " и столбце " + (ColumnCounter + 1));
                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
                CurrentSymbol = FileText[i];
            }
            CheckLine();
            return true;
        }
        private void TryAnalyzeText()
        {
            foreach (var lexem in CurrentLexems)
            {
                foreach (var token in tokens)
                {
                    if (lexem.Value == Convert.ToString(token.Value))
                    {
                        lexem.Type = "<" + token.Type + ">";
                    }
                }

                if (lexem.Type == "")
                {
                    if (lexem.Value == null)
                        throw new Exception("Исходный код отсутствует");
                    if (lexem.Value.Length > 12)
                    {
                        Lexem = lexem.Value;
                        throw new Exception("Слишком длинное название для переменной " + Lexem + " в строке " + (LineCounter
                            + 1) + " и столбце " + (ColumnCounter + 1));
                    }
                    if (lexem.Value.Length > 0)
                        lexem.Type = "<variable>";

                }
            }
        }
        private void StateWords()
        {
            Lexem += CurrentSymbol;
            ColumnCounter++;
        }
        private void StateChars()
        {
            ColumnCounter++;
            CheckLine();
            CurrentLexems.Add(new Identifier(Convert.ToString(CurrentSymbol), "", LineCounter, ColumnCounter));

        }
        private void StateUnderscore()
        {
            ColumnCounter++;
            Lexem += CurrentSymbol;
            IsWrongUnderscoreSyntax = true;
        }

        private void StateNum()
        {
            ColumnCounter++;
            CheckLine();
            CurrentLexems.Add(new Identifier(Convert.ToString(CurrentSymbol), "", LineCounter, ColumnCounter));
        }

        private void StateSpaces()
        {
            CheckLine();
            ColumnCounter++;
            CheckNewLine();
        }
        private void CheckLine()
        {
            if (FileText.Any())
            {
                if (IsWrongUnderscoreSyntax)
                {
                    IsWrongUnderscoreSyntax = false;
                    if (Lexem != "end_while")
                        throw new Exception("Неправильное использование символа \'_\' в строке " + (LineCounter + 1) + " и столбце " + (ColumnCounter + 1));
                }


                CurrentLexems.Add(new Identifier(Lexem, "", LineCounter, ColumnCounter));
                Lexem = "";
            }

        }
        private void CheckNewLine()
        {
            if (CurrentSymbol == '\n')
            {
                ColumnCounter = 1;

                LineCounter++;
            }

        }
        public List<Identifier> GetLexemsAsList()
        {
            return CurrentLexems;
        }
    }
}
