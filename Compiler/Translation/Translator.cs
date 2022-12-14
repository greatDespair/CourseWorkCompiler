using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Translation
{
    public class Translator
    {
        private string _error;

        public Translator()
        {
            _error = "Успешное выполнение. CODE X00";
        }

        public enum COMMANDS
        {
            IFETCH,
            ISTORE,
            IPUSH,
            IPOP,
            IIMP,
            IAND,
            IOR,
            INOT,
            JZ,
            JMP,
            IREAD,
            IWRITE,
            HALT
        };

        private Dictionary<string, bool> _variables = new Dictionary<string, bool>();

        List<string> _commands = new List<string>();

        private Dictionary<string, int> _priority = new Dictionary<string, int>()
        {
            {"not", 3 },
            {"and", 2 },
            { "or", 1 },
            {"imp", 0 }
        };

        StreamReader _stream;
        private int _ip = 0;
        private bool _isRead = false;
        private bool _isWrite = false;
        
        public void SaveCommands(string filePath)
        {
            
        }

        private void OutTree(Identifier root)
        {
            if (root.Type == "<expression>")
            {
                Identifier exp = root;
                List<Identifier> sequence = new List<Identifier>();
                AstTree(exp, ref sequence);
                var polishSequence = ExpressionConverter(sequence);
                foreach (var item in polishSequence)
                {
                    switch (item.Type)
                    {
                        case "<constant>":
                            GenerateAsm(COMMANDS.IPUSH.ToString());
                            GenerateAsm(item.Value);
                            break;
                        case "<variable>":
                            GenerateAsm(COMMANDS.IFETCH.ToString());
                            GenerateAsm(item.Value);
                            break;
                        case "<binary operator AND>":
                            GenerateAsm(COMMANDS.IAND.ToString());
                            break;
                        case "<binary operator OR>":
                            GenerateAsm(COMMANDS.IOR.ToString());
                            break;
                        case "<binary operator IMP>":
                            GenerateAsm(COMMANDS.IIMP.ToString());
                            break;
                        case "<unary operator NOT>":
                            GenerateAsm(COMMANDS.IAND.ToString());
                            break;
                    }
                }
            }

            if (root.Type == "<assignment>")
            {
                Identifier s = root.Childs[2];
                OutTree(s);
                GenerateAsm(COMMANDS.ISTORE.ToString());
                if (!CheckVariable(root.Childs[0]))
                {
                    return;
                }
                GenerateAsm(root.Childs[0].Value);
                GenerateAsm(COMMANDS.IPOP.ToString());
            }

            if (root.Type == "<variables list>")
            {
                foreach (var item in root.Childs)
                {
                    OutTree(item);
                }
            }

            if (root.Type == "<variable>")
            {
                if (_isRead)
                {
                    if (!CheckVariable(root)) 
                    {
                        return;
                    }
                    GenerateAsm(COMMANDS.IREAD.ToString());
                    GenerateAsm(root.Value);
                }
                else if (_isWrite)
                {
                    if (!CheckVariable(root))
                    {
                        return;
                    }
                    GenerateAsm(COMMANDS.IWRITE.ToString());
                    GenerateAsm(root.Value);
                }
                else
                {
                    _variables[root.Value] = false;
                }
            }

            // TODO: WHILE () DO

            if(root.Type == "<end of calculations part>")
            {
                GenerateAsm(COMMANDS.HALT.ToString());
                return;
            }

            if (root.Type == "<function>")
            {
                if(root.Childs[0].Value == "read")
                    _isRead = true;
                if(root.Childs[0].Value == "write")
                    _isWrite = true;

                Identifier s = root.Childs[2];

                OutTree(s);
                _isRead = false;
                _isWrite = false;
            }

            if (root.Type == "<variables declaration>" || 
                root.Type == "<calculation part>" ||
                root.Type == "<program>" ||
                root.Type == "<list of assignments>")
            {
                foreach (var child in root.Childs)
                {
                    OutTree(child);
                }
            }
        }
        private void GenerateAsm(string command)
        {
            _commands.Add(command);
            _ip++;
        }

        private void AstTree(Identifier root, ref List<Identifier> expression)
        {
            foreach (var child in root.Childs)
            {
                AstTree(child, ref expression);
            }
            if (root.Value != "")
            {
                expression.Add(root);
            }
        }

        List<Identifier> ExpressionConverter(List<Identifier> expression)
        {
            List<Identifier> newExpression = new List<Identifier>();
            Stack<Identifier> operations = new Stack<Identifier>();

            foreach (var child in expression)
            {
                if(child.Type == "<variable>" || child.Type == "<constant>")
                {
                    if(child.Type == "<variable>" && !CheckVariable(child))
                    {
                        return newExpression;
                    }
                    newExpression.Add(child);
                } 
                else if (child.Type == "<opening bracket>")
                {
                    operations.Push(child);
                }
                else if (child.Type == "<closing bracket>")
                {
                    var operation = operations.Peek();
                    operations.Pop();

                    while(operation.Type != "<opening bracket>")
                    {
                        newExpression.Add(operation);
                        operation = operations.Peek();
                        operations.Pop();
                    }
                }
                else if (child.Type == "<binary operator>" || child.Type == "<unary operator NOT>")
                {
                    while (operations.Any() && _priority[child.Value] < _priority[operations.Peek().Value])
                    {
                        newExpression.Add(operations.Peek());
                        operations.Pop();
                    }
                }
            }

            while (operations.Any())
            {
                newExpression.Add(operations.Peek());
                operations.Pop();
            }

            return newExpression;
        }

        private bool CheckVariable(Identifier variable)
        {
            if (!_variables.ContainsKey(variable.Value))
            {
                _error = "Необъявленная переменная \"" + variable.Value + "\" в строке " + variable.Line + "\n";
                return false;
            }
            else
            {
                return true;
            }
        }

        private List<Identifier> Lexems;

       
    }
}
