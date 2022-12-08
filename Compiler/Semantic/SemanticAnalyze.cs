using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Semantic
{
    public class SemanticAnalyze
    {
        #region [Структуры для определения правил переноса и свертки]
        /// <summary>
        /// Правило свертки
        /// </summary>
        private struct Reduction
        {
            /// <summary>
            /// Левая часть правила
            /// </summary>
            public string Symbol;
            /// <summary>
            /// Количество убираемых символов из стека
            /// </summary>
            public int RemoveCountSymbols;
            /// <summary>
            /// Конструктор для создания правила
            /// </summary>
            /// <param name="symbol">Левая часть правила</param>
            /// <param name="rcs">Количество убираемых символов из стека</param>
            public Reduction(string symbol, int rcs)
            {
                Symbol = symbol;
                RemoveCountSymbols = rcs;
            }
        }
        /// <summary>
        /// Действие
        /// </summary>
        private struct Action
        {
            /// <summary>
            /// Тип действия: Перенос - S;
            /// Свертка - R
            /// </summary>
            public char ActionType;
            /// <summary>
            /// Номер нового состояния
            /// </summary>
            public int StateNumber;
            /// <summary>
            /// Конструктор для создания правила
            /// </summary>
            /// <param name="type">Тип действия: Перенос - S;
            /// Свертка - R
            /// </param>
            /// <param name="state">Номер нового состояния</param>
            public Action(char type, int state)
            {
                ActionType = type;
                StateNumber = state;
            }
        }
        #endregion

        //начальный символ грамматики
        private string STARTING_CHARACTER = "<PROGRAM>"; 
        // признак нетерминального символа
        private string NONTERMINAL = ""; 
        // маркер дна
        private string END_MARKER = "$";

        // Количество правил
        private readonly int RULES_COUNT = 19;

        // входная строка лексем
        private List<Identifier> _inputString;
        // стек символов входной ленты
        private Stack<Identifier> _symbolStack;
        // стек состояний
        private Stack<int> _stateStack;
        // корень
        public  Identifier? _root { get; private set; }
        public string Error { get; private set; }
        /// <summary>
        /// Словарь правил свертки
        /// </summary>
        Dictionary<int, Reduction> Reductions = new Dictionary<int, Reduction>{
            {2, new Reduction("<program>", 2) },
            {10, new Reduction("<variables declaration>", 5)},
            {5, new Reduction("<calculation part>", 3)     },
            {11, new Reduction("<variables list>", 1)       },
            {7, new Reduction("<variables list>", 3)       },
            {8, new Reduction("<list of assignments>", 1)  },
            {4, new Reduction("<list of assignments>",2)   },
            {42, new Reduction("<assignment>", 4)           },
            {15, new Reduction("<assignment>", 1)           },
            {20, new Reduction("<function>", 7)             },
            {31, new Reduction("<function>", 5)             },
            {32, new Reduction("<function>", 5)             },
            {33, new Reduction("<expression>", 2)           },
            {34, new Reduction("<expression>", 1)           },
            {18, new Reduction("<subexpression>", 3)        },
            {25, new Reduction("<subexpression>", 1)        },
            {24, new Reduction("<subexpression>", 3)        },
            {35, new Reduction("<value>", 1)                },
            {30, new Reduction("<value>", 1)                },
            {27, new Reduction("<binary operator>", 1)      },
            {28, new Reduction("<binary operator>", 1)      },
            {29, new Reduction("<binary operator>", 1)      },
        };
        /// <summary>
        /// Словарь правил перехода для нетерминалов
        /// </summary>
        private Dictionary<string, Dictionary<int, int>> _gotoRules =
            new Dictionary<string, Dictionary<int, int>>()
            {
                {"<variables declaration>", new Dictionary<int, int>(){ { 0, 1 } } },
                {"<calculation part>", new Dictionary<int, int>(){ { 1, 2 } } },
                {"<variables list>", new Dictionary<int, int>(){ { 6, 7 }, { 36, 37 }, { 39, 40 }, { 12, 7 } } },
                {"<list of assignments>", new Dictionary<int, int>(){ { 3, 4 }, { 8, 4 }, { 19, 4 } } },
                {"<assignment>", new Dictionary<int, int>(){ { 3, 8 }, { 19, 8 } } },
                {"<function>", new Dictionary<int, int>(){ { 3, 15 }, { 19, 15 } } },
                {"<expression>", new Dictionary<int, int>(){ { 13, 14 }, { 17, 14 } } },
                {"<subexpression>", new Dictionary<int, int>(){ { 13, 34 }, { 17, 34 }, { 23, 34 } } },
                {"<value>", new Dictionary<int, int>(){ { 13, 25 }, { 17, 25 }, { 23, 25 } } },
                {"<binary operator>", new Dictionary<int, int>{{34, 26}} }
            };

        /// <summary>
        /// Cловарь правил переноса - свертки
        /// </summary>
        private Dictionary<string, Dictionary<int, Action>> _shiftAndReduceRules =
            new Dictionary<string, Dictionary<int, Action>>()
            {
                {"<start of calculation part>",
                    new Dictionary<int, Action>(){
                        {1, new Action('S', 3) } } },

                {"<colon>",
                    new Dictionary<int, Action>(){
                        {7, new Action('S', 8) } } },

                {"<variables type>",
                    new Dictionary<int, Action>(){
                        {8, new Action('S', 9) } } },

                {"<end of calculation part>",
                    new Dictionary<int, Action>(){
                        {4, new Action('S', 5) } } },

                {"<variables declaration keyword>",
                    new Dictionary<int, Action>(){
                        {0, new Action('S', 6) } } },

                {"<semicolon>",
                    new Dictionary<int, Action>(){
                        {9, new Action('S', 10) },
                        {14, new Action('R', 42) },
                        { 14, new Action('S', 11) },
                        { 38, new Action('R', 31) },
                        { 41, new Action('R', 32) } } },

                {"<variable>",
                    new Dictionary<int, Action>(){
                        { 6, new Action('R', 11) },
                        { 3, new Action('S', 11) },
                        { 17, new Action('R', 11) },
                        { 13, new Action('R', 35) },
                        { 17, new Action('R', 35) },
                        { 23, new Action('R', 35) },
                        { 27, new Action('R', 35) },
                        { 28, new Action('R', 35) },
                        { 29, new Action('R', 35) } } },

                {"<comma>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('S', 12) } } },

                {"<assign>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('S', 13) } } },

                {"<\"while\" header>",
                    new Dictionary<int, Action>(){
                        { 3, new Action('S', 16) },
                        { 19, new Action('S', 16) } } },

                {"<opening bracket>",
                    new Dictionary<int, Action>(){
                        { 21, new Action('S', 36) },
                        { 16, new Action('S', 17) },
                        { 22, new Action('S', 39) },
                        { 17, new Action('S', 17) },
                        { 23, new Action('S', 17) } } },

                {"<closing bracket>",
                    new Dictionary<int, Action>(){
                        { 7, new Action('S', 18) },
                        { 14, new Action('S', 18) },
                        { 34, new Action('S', 18) },
                        { 37, new Action('S', 38) },
                        { 40, new Action('S', 41) },
                        { 14, new Action('R', 18) } } },

                {"<unary operator NOT>",
                    new Dictionary<int, Action>(){
                        { 13, new Action('S', 23) },
                        { 17, new Action('S', 23) } } },

                {"<binary operator AND>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('R', 27) },
                        { 24, new Action('R', 27) } } },

                {"<binary operator OR>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('R', 28) },
                        { 24, new Action('R', 28) } } },

                {"<binary operator IMP>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('R', 29) },
                        { 24, new Action('R', 29) } } },

                {"<start of \"while\" block>",
                    new Dictionary<int, Action>(){
                        { 18, new Action('S', 19) } } },

                {"<end of \"while\" block>",
                    new Dictionary<int, Action>(){
                        { 4, new Action('S', 20) } } },

                {"<constant>",
                    new Dictionary<int, Action>(){
                        { 13, new Action('R', 30) },
                        { 17, new Action('R', 30) },
                        { 23, new Action('R', 30) },
                        { 27, new Action('R', 30) },
                        { 28, new Action('R', 30) },
                        { 29, new Action('R', 30) } } },

                {"<read function>",
                    new Dictionary<int, Action>(){
                        { 3, new Action('S', 21) },
                        { 19, new Action('S', 21) } } },

                {"<write function>",
                    new Dictionary<int, Action>(){
                        { 3, new Action('S', 22) },
                        { 19, new Action('S', 22) } } },
            };

        /// <summary>
        /// Состояние -> Правило свертки
        /// </summary>
        private List<KeyValuePair<int, int>> _reduceRules = 
            new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int, int>( 2, 2 ),
            new KeyValuePair<int, int>( 5, 5 ),
            new KeyValuePair<int, int>( 10, 10 ),
            new KeyValuePair<int, int>( 11, 11 ),
            new KeyValuePair<int, int>( 7, 7 ),
            new KeyValuePair<int, int>( 8,  8 ),
            new KeyValuePair<int, int>( 4,  4 ),
            new KeyValuePair<int, int>( 15,  15 ),
            new KeyValuePair<int, int>( 31,  31 ),
            new KeyValuePair<int, int>( 32,  32 ),
            new KeyValuePair<int, int>( 33,  33 ),
            new KeyValuePair<int, int>( 34,  34 ),
            new KeyValuePair<int, int>( 18,  18 ),
            new KeyValuePair<int, int>( 25, 25 ),
            new KeyValuePair<int, int>( 24,  24 ),
            new KeyValuePair<int, int>( 35,  35 ),
            new KeyValuePair<int, int>( 27,  27),
            new KeyValuePair<int, int>( 28,  28),
            new KeyValuePair<int, int>( 29,  29),
            new KeyValuePair<int, int>( 30,  30)
        };

        /// <summary>
        /// Соответствие токенов и лексем
        /// </summary>
        private Dictionary<string, string> _tokensEquals = 
            new Dictionary<string, string>()
        {
            { "<start of calculation part>" , "begin"},
            { "<end of calculation part>", "end"},
            { "<variables declaration keyword>", "var"},
            { "<variables type>", "logical"},
            { "<binary operator AND>", "and"},
            { "<binary operator OR>", "or"},
            { "<\"while\" header>", "while"},
            { "<start of \"while\" block>", "do" },
            { "<end of \"while\" block>", "end_while"  },
            { "<unary operator NOT>" , "not"},
            { "<binary operator IMP>", "imp"  },
            { "<write function>" , "write" },
            { "<read function>", "read" },
            { "<colon>",":" },
            { "<semicolon>", ";"  },
            { "<comma>", ","  },
            { "<assign>", "="  },
            { "<opening bracket>", "("  },
            { "<closing bracket>", ")"  },
            { "<constant>" , "0|1" }
        };

        public SemanticAnalyze(List<Identifier> lexems)
        {
            Error = "Успешная проверка семантики. CODEX00";
            _inputString = lexems;
            _stateStack = new Stack<int>();
            _symbolStack = new Stack<Identifier>();
        }

        public void CompileError()
        {
            if(_stateStack.Count > 0 && _symbolStack.Peek().Value == NONTERMINAL)
            {
                Error = "Неопознанный символ \"" + _inputString[0].Value + "\" в " +
                    _symbolStack.Peek().Value;
            }
            else
            {
                string expectedSymbol = "";
                foreach(var rule in _shiftAndReduceRules)
                {
                    if (rule.Value.ContainsKey(_stateStack.Peek()))
                    {
                        expectedSymbol = _tokensEquals[rule.Key];
                    }
                }

                Error = "Найден символ \"" + _inputString[0].Value +
                    "\" но ожидалось " + expectedSymbol;
            }
            Error += " в строке: " + _inputString[0].Line;
        }

        public bool AnalyzeSyntax()
        {
            _inputString.Add(new Identifier(NONTERMINAL, END_MARKER));
            _stateStack.Push(0);

            while (true)
            {
                if (TryAction() || TryGoto())
                {
                    continue;
                }
                break;
            }

            if(_symbolStack.Count == 1
                && _symbolStack.Peek().Type == STARTING_CHARACTER
                && _stateStack.Peek() == 0
                && _inputString[0].Type == END_MARKER
                && _inputString.Count == 1)
            {
                _root = _symbolStack.Pop();
                return true;
            }
            else
            {
                _root = null;
                CompileError();
                return false;
            }
        }
        public bool TryAnalyze()
        {
            GotoRules gotoRules = new GotoRules();
            var EmptyLexems = _inputString.Where(x => x.Type == "").ToList();
            foreach (var lexeme in EmptyLexems)
            {
                _inputString.Remove(lexeme);
            }
            for(int i = 0; i < _inputString.Count - 1; i++)
            {
                if (!gotoRules.CheckRule(_inputString[i], _inputString[i + 1]))
                {
                    Error = "Переход из " + _inputString[i].Type + " в " + _inputString[i + 1].Type +" невозможен. CODEX10";
                    return false;
                }
            }
            return true;
        }

        private void Reduce(Reduction reduction)
        {
            Identifier ident = new Identifier(NONTERMINAL, reduction.Symbol);

            for(int i = reduction.RemoveCountSymbols; i  > 0; i--)
            {
                ident.Childs.Add(_symbolStack.Peek());
                _symbolStack.Peek().Parent = ident;
                _symbolStack.Pop();
                _stateStack.Pop();
            }
            
            _symbolStack.Push(ident);
        }
        private bool TryReduce()
        {
            if(_symbolStack.Count != 0)
            {
                if (_gotoRules.ContainsKey(_symbolStack.Peek().Type))
                {
                    if(_gotoRules[_symbolStack.Peek().Type].ContainsKey(_stateStack.Peek()))
                    {
                        _stateStack.Push(_gotoRules[_symbolStack.Peek().Type][_stateStack.Peek()]);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryShiftReduce()
        {
            if (_shiftAndReduceRules.ContainsKey(_inputString[0].Type))
            {
                if (_shiftAndReduceRules[_inputString[0].Type].ContainsKey(_stateStack.Peek()))
                {
                    Action action = _shiftAndReduceRules[_inputString[0].Type][_stateStack.Peek()];
                    if (action.ActionType == 'R')
                    {
                        Reduce(Reductions[action.StateNumber]);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryShift()
        {
            if (_shiftAndReduceRules.ContainsKey(_inputString[0].Type))
            {
                if (_shiftAndReduceRules[_inputString[0].Type].ContainsKey(_stateStack.Peek()))
                {
                    Action action = _shiftAndReduceRules[_inputString[0].Type][_stateStack.Peek()];
                    if(action.ActionType == 'S')
                    {
                        _symbolStack.Push(_inputString[0]);
                        var item = _inputString[0];
                        _inputString.Remove(item);
                        _stateStack.Push(action.StateNumber);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TryGoto()
        {
            if(_reduceRules.Where(r => r.Key == _stateStack.Peek()).Any())
            {
                Reduce(Reductions[_stateStack.Peek()]);
                return true;
            }

            return false;
        }
        private bool TryAction()
        {
            return TryReduce() || TryShiftReduce() || TryShift();
        }
    }
}
