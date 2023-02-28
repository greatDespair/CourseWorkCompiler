﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCompiler.Semantic
{
    public class SyntactialAnalyzer
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
        private const int RULES_COUNT = 20;

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
        Reduction[] Reductions = new Reduction[RULES_COUNT]
        {
            new Reduction ( "<PROGRAM>", 2 ), // 0
	        new Reduction ( "<VARIABLES DECLARATION>", 5 ), // 1
	        new Reduction ( "<VARIABLES LIST>", 3 ), // 2
	        new Reduction ( "<VARIABLES LIST>", 1 ), // 3
	        new Reduction ( "<CALCULATIONS DESCRIPTION>", 3 ), // 4
	        new Reduction ( "<OPERATIONS LIST>", 2 ), // 5
	        new Reduction ( "<OPERATIONS LIST>", 2 ), // 6
	        new Reduction ( "<OPERATIONS LIST>", 2 ), // 7
	        new Reduction ( "<OPERATIONS LIST>", 1 ), // 8
	        new Reduction ( "<OPERATIONS LIST>", 1 ), // 9
	        new Reduction ( "<OPERATIONS LIST>", 1 ), // 10
	        new Reduction ( "<ASSIGNMENT>", 4 ), // 11
	        new Reduction ( "<EXPRESSION>", 2 ), // 12
	        new Reduction ( "<EXPRESSION>", 3 ), // 13
	        new Reduction ( "<EXPRESSION>", 3 ), // 14
	        new Reduction ( "<EXPRESSION>", 1 ), // 15
	        new Reduction ( "<OPERAND>", 1 ), // 16
	        new Reduction ( "<OPERAND>", 1 ), // 17
	        new Reduction ( "<FUNCTION>", 5 ), // 18
	        new Reduction ( "<OPERATOR>", 5 ), // 19
        };

        /// <summary>
        /// Словарь правил перехода для нетерминалов
        /// </summary>
        private Dictionary<string, Dictionary<int, int>> _gotoRules =
            new Dictionary<string, Dictionary<int, int>>()
            {
                {"<VARIABLES DECLARATION>", new Dictionary<int, int>(){ { 0, 1 } } },
                {"<CALCULATIONS DESCRIPTION>", new Dictionary<int, int>(){ { 1, 2 } } },
                {"<VARIABLES LIST>", new Dictionary<int, int>(){ { 3, 4 }, { 35, 36 } } },
                {"<OPERATIONS LIST>", new Dictionary<int, int>(){ { 11, 12 }, { 39, 40 } } },
                {"<ASSIGNMENT>", new Dictionary<int, int>(){ { 11, 17 }, { 12, 14 }, { 39, 17}, { 40, 12} } },
                {"<FUNCTION>", new Dictionary<int, int>(){ { 11, 18 }, { 12, 15 }, { 39, 18 },  { 40, 15 } } },
                {"<OPERATOR>", new Dictionary<int, int>(){ { 11, 19 }, { 12, 16 }, { 39, 19 },  { 40, 16 } } },
                {"<EXPRESSION>", new Dictionary<int, int>(){ { 21, 22 }, { 24, 25 }, { 26, 27 }, { 28, 29 }, { 48, 29 }, { 41, 44 } } },
                {"<OPERAND>", new Dictionary<int, int>(){ { 21, 31 }, { 24, 31 }, { 26, 31 }, { 28, 31 }, { 48, 31 }, { 41, 31 } } },
            };

        /// <summary>
        /// Cловарь правил переноса - свертки для терминалов
        /// </summary>
        private Dictionary<string, Dictionary<int, Action>> _shiftAndReduceRules =
            new Dictionary<string, Dictionary<int, Action>>()
            {
                {"<begin of variables declaration>",
                    new Dictionary<int, Action>(){
                        {0, new Action('S', 3) } } },

                {"<colon>",
                    new Dictionary<int, Action>(){
                        {4, new Action('S', 5) } } },

                {"<type>",
                    new Dictionary<int, Action>(){
                        {5, new Action('S', 6) } } },

                {"<semicolon>",
                    new Dictionary<int, Action>(){
                        { 6, new Action('S', 7) },
                        { 22, new Action('S', 23) },
                        { 37, new Action('S', 38) },
                        { 44, new Action('S', 46) },
                        { 25, new Action('R', 12) } } },

                {"<comma>",
                    new Dictionary<int, Action>(){
                        { 4, new Action('S', 8) },
                        { 36, new Action('S', 8) }} },

                {"<variable>",
                    new Dictionary<int, Action>(){
                        { 3, new Action('S', 10)  },
                        { 8, new Action('S', 9)   },
                        { 11, new Action('S', 20) },
                        { 12, new Action('S', 20) },
                        { 21, new Action('S', 32) },
                        { 24, new Action('S', 32) },
                        { 26, new Action('S', 32) },
                        { 28, new Action('S', 32) },
                        { 35, new Action('S', 10) },
                        { 41, new Action('S', 32) },
                        { 39, new Action('S', 20) },
                        { 40, new Action('S', 20) } } },

                {"<begin of calculations description>",
                    new Dictionary<int, Action>(){
                        {1, new Action('S', 11) } } },

                {"<end of calculations description>",
                    new Dictionary<int, Action>(){
                        {12, new Action('S', 13) } } },

                {"<assign>",
                    new Dictionary<int, Action>(){
                        { 20, new Action('S', 21) } } },

                {"<unary operator>",
                    new Dictionary<int, Action>(){
                        { 21, new Action('S', 24) },
                        { 24, new Action('S', 24) },
                        { 26, new Action('S', 24) },
                        { 28, new Action('S', 24) },
                        { 39, new Action('S', 24) } } },

                {"<binary operator>",
                    new Dictionary<int, Action>(){
                        { 22, new Action('S', 26) },
                        { 25, new Action('S', 26) },
                        { 29, new Action('S', 26) },
                        { 40, new Action('S', 26) } } },

                {"<opening bracket>",
                    new Dictionary<int, Action>(){
                        { 21, new Action('S', 28) },
                        { 24, new Action('S', 28) },
                        { 26, new Action('S', 28) },
                        { 28, new Action('S', 28) },
                        { 34, new Action('S', 35) },
                        { 41, new Action('S', 28) } } },

                {"<closing bracket>",
                    new Dictionary<int, Action>(){
                        { 29, new Action('S', 30) },
                        { 36, new Action('S', 37) },
                        { 25, new Action('R', 12) } } },

                {"<constant>",
                    new Dictionary<int, Action>(){
                        { 21, new Action('S', 33) },
                        { 24, new Action('S', 33) },
                        { 26, new Action('S', 33) },
                        { 28, new Action('S', 33) },
                        { 41, new Action('S', 33) } } },

                {"<function name>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('S', 34) },
                        { 12, new Action('S', 34) },
                        { 39, new Action('S', 34) },
                        { 40, new Action('S', 34) } } },

                {"<until operator>",
                    new Dictionary<int, Action>(){
                        { 40, new Action('S', 41) },
                        { 25, new Action('R', 12) } } },

                {"<repeat block>",
                    new Dictionary<int, Action>(){
                        { 11, new Action('S', 39) },
                        { 12, new Action('S', 39) },
                        { 41, new Action('S', 39) },
                        { 44, new Action('S', 39) } } },
            };

        /// <summary>
        /// Состояние -> Правило свертки
        /// </summary>
        private Dictionary<int, int> _reduceRules = 
            new Dictionary<int, int>()
        {
            {  2, 0   },
            {  7, 1   },
            {  9, 2   },
            {  10, 3  },
            {  13, 4  },
            {  14, 5  },
            {  15, 6  },
            {  16, 7  },
            {  17, 8  },
            {  18, 9  },
            {  19, 10 },
            {  23, 11 },
            {  27, 13 },
            {  30, 14 },
            {  31, 15 },
            {  32, 16 },
            {  33, 17 },
            {  38, 18 },
            {  46, 19 },
        };

        /// <summary>
        /// Соответствие токенов и лексем
        /// </summary>
        private Dictionary<string, string> _tokensEquals = 
            new Dictionary<string, string>()
        {
            { "<begin of variables declaration>", "var" },
            { "<colon>", ":" },
            { "<type>", "type" },
            { "<semicolon>", ";" },
            { "<comma>", "," },
            { "<variable>", "variable" },
            { "<begin of calculations description>", "begin" },
            { "<end of calculations description>", "end" },
            { "<assign>", "=" },
            { "<unary operator>", "unary operator" },
            { "<binary operator>", "<binary operator>" },
            { "<opening bracket>", "(" },
            { "<closing bracket>", ")" },
            { "<constant>", "constant" },
            { "<function name>", "function" },
            { "<until operator>", "until" },
            { "<repeat block>", "repeat" },
            { "<else block>", "else" },
            { "<end while operator>", "end_while" }
        };

        public SyntactialAnalyzer(List<Identifier> lexems)
        {
            Error = "Успешная проверка семантики. CODEX00";
            _inputString = lexems;
            _stateStack = new Stack<int>();
            _symbolStack = new Stack<Identifier>();
        }
        public void CompileError()
        {
            if (_inputString[0].Type == "<variable>")
            {
                Error = "Найден символ " + _inputString[0].Value + ", но ожидалось " + "var";
                return;
            }

            if(_stateStack.Count > 0 && _symbolStack.Peek().Type == NONTERMINAL)
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
                if (TryGoto() || TryAction())
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
        /// <summary>
        /// Свертка для нетерминалов
        /// </summary>
        /// <returns></returns>
        private bool TryReduce()
        {
            if(_reduceRules.ContainsKey(_stateStack.Peek()))
            {
                Reduce(Reductions[_reduceRules[_stateStack.Peek()]]);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Свертка для терминалов
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Переход для терминалов
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Переход для нетерминала
        /// </summary>
        /// <returns></returns>
        private bool TryGoto()
        {
            if (_symbolStack.Count != 0)
            {
                if (_gotoRules.ContainsKey(_symbolStack.Peek().Type))
                {
                    if (_gotoRules[_symbolStack.Peek().Type].ContainsKey(_stateStack.Peek()))
                    {
                        _stateStack.Push(_gotoRules[_symbolStack.Peek().Type][_stateStack.Peek()]);
                        return true;
                    }
                }
            }
            return false;
        }
        private bool TryAction()
        {
            return TryReduce() || TryShiftReduce() || TryShift();
        }

        public Identifier Identifier
        {
            get => default;
            set
            {
            }
        }
    }
}