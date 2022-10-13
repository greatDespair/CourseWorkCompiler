using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Semantic
{
    public class SemanticAnalyze
    {
        public string Error { get; private set; }
        private List<Identifier> _lexems;
        public SemanticAnalyze(List<Identifier> lexems)
        {
            Error = "Успешная проверка семантики. CODEX00";
            _lexems = lexems;
        }

        public bool TryAnalyze()
        {
            GotoRules gotoRules = new GotoRules();
            var EmptyLexems = _lexems.Where(x => x.Type == "").ToList();
            foreach (var lexeme in EmptyLexems)
            {
                _lexems.Remove(lexeme);
            }
            for(int i = 0; i < _lexems.Count - 1; i++)
            {
                if (!gotoRules.CheckRule(_lexems[i], _lexems[i + 1]))
                {
                    Error = "Переход из " + _lexems[i].Type + " в " + _lexems[i + 1].Type +" невозможен. CODEX10";
                    return false;
                }
            }
            return true;
        }
    }
}
