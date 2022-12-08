using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Translation
{
    public class Translator
    {
        private string _error;

        public class Command
        {
            public List<Identifier>? RemainPart { get; private set; }
            public bool Result { get; set; }
            public COMMANDS Operation { get; set; }
            public Command? leftOperand { get; set; }
            public Command? rightOperand { get; set; }

            public Command(bool result, COMMANDS command, List<Identifier> remainPart)
            {
                Result = result;
                Operation = command;
                RemainPart = remainPart;
                leftOperand = null;
                rightOperand = null;
            }

            public void ExpandTree(Command node)
            {
                if (RemainPart.Count == 1)
                    return;
                switch (RemainPart[0].Type)
                {
                    case "<unary operator NOT>":
                        {
                            node.Operation = COMMANDS.NOT;
                            RemainPart.RemoveAt(0);
                            node.leftOperand = new Command(false, COMMANDS.EMPTY_COMMAND, null);
                            node.rightOperand = new Command(false, COMMANDS.EMPTY_COMMAND, RemainPart);
                            ExpandTree(node.rightOperand);
                            break;
                        }
                    case "<opening bracket>":
                        {

                            break;
                        }
                    case "<variable>":
                        {

                            break;
                        }
                }

            }
        }

        public enum COMMANDS
        {
            EMPTY_COMMAND,
            EXPAND_STORE,
            PUT,
            AND,
            OR,
            IMP,
            NOT,
            READ,
            WRITE,
            JMP_DO
        }

        private List<Identifier> _variables = new List<Identifier>();

        private List<Command> _commandTrees = new List<Command>();

        private List<Identifier> Lexems;

        public Translator(List<Identifier> lexems)
        {
            _error = "Успешное выполнение. CODE X00";
            Lexems = lexems;
        }

        public bool Translate(List<Identifier> lexems)
        {
            if (CheckVariables())
            {
                if (TryTranslateCommands())
                {

                }
            }

            return false;
        }

        private bool TryTranslateCommands()
        {
            bool writeFlag = false;
            List<Identifier> calculationPart = new List<Identifier>();
            for (int i = 0; i < Lexems.Count - 1; i++)
            {
                if (writeFlag)
                {
                    calculationPart.Add(Lexems[i]);
                }
                else
                {
                    if (Lexems[i].Type == "<start of calculation part>")
                        writeFlag = true;
                }
            }

            List<Command> commands = new List<Command>();
            List<Identifier> tempLexems = new List<Identifier>();
            for (int i = 0; i < calculationPart.Count; i++)
            {
                tempLexems.Add(calculationPart[i]);
            }
        }

        private bool CheckVariables()
        {
            if (_variables.Count == 0)
            {
                for (int i = 0; i < Lexems.Count; i++)
                {
                    if (Lexems[i].Type == "<variables type>")
                    {
                        break;
                    }

                    if (Lexems[i].Type == "<variable>")
                        _variables.Add(Lexems[i]);
                }
            }
            else
            {
                foreach (var lexem in Lexems)
                {
                    if (lexem.Type == "<variable>")
                    {
                        if (!_variables.Where(i => i.Value == lexem.Value).Any())
                        {
                            _error = "Идентификатор " + lexem.Value + " не объявлен.";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
