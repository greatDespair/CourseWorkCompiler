using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Semantic
{
    public class SemanticAnalyze
    {
        // входная строка лексем
        private List<Identifier> _inputString;
        // стек символов входной ленты
        private Stack<Identifier> _symbolStack;
        // стек состояний
        private Stack<int> _stateStack;
        // корень
        private Identifier root;
        public string Error { get; private set; }
        public SemanticAnalyze(List<Identifier> lexems)
        {
            Error = "Успешная проверка семантики. CODEX00";
            _inputString = lexems;
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

        private bool tryReduce()
        {

        }

        private bool tryGoto()
        {

        }
    }
}
