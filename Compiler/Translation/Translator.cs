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
        public string _error { get; private set; }

        public Translator()
        {
            _error = "Успешное выполнение. CODE X00";
        }

        bool _crashed = false;

        public enum COMMANDS
        {
            IFETCH,
            ISTORE,
            IPUSH,
            IPOP,
            IEQU,
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
            {"equ", 0 }
        };

        private int _ip = 0;
        private bool _isRead = false;
        private bool _isWrite = false;
        
        public void SaveCommands(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (string command in _commands)
                {
                    sw.WriteLine(command);
                }
                sw.Close();
            }
        }

        public void OutTree(Identifier root)
        {
            if (root.Type == "<EXPRESSION>")
            {
                Identifier exp = root;
                List<Identifier> sequence = new List<Identifier>();
                AstTree(exp, ref sequence);
                var polishSequence = ExpressionConverter(sequence);
                foreach (var item in polishSequence)
                {
                    if( item.Type == "<constant>")
                    {
                            GenerateAsm(COMMANDS.IPUSH.ToString());
                            GenerateAsm(item.Value);
                    }
                    if (item.Type == "<variable>")
                    {
                        GenerateAsm(COMMANDS.IFETCH.ToString());
                        GenerateAsm(item.Value);
                    }
                    if( item.Value == "and")
                            GenerateAsm(COMMANDS.IAND.ToString());
                    if (item.Value == "or")
                        GenerateAsm(COMMANDS.IOR.ToString());
                    if (item.Value == "equ")
                        GenerateAsm(COMMANDS.IEQU.ToString());
                    if (item.Value == "not")
                        GenerateAsm(COMMANDS.INOT.ToString());
                }
            }

            if (root.Type == "<ASSIGNMENT>")
            {
                Identifier s = root.Childs[1];
                OutTree(s);
                GenerateAsm(COMMANDS.ISTORE.ToString());
                if (!CheckVariable(root.Childs.Last()))
                {
                    return;
                }
                GenerateAsm(root.Childs.Last().Value);
                GenerateAsm(COMMANDS.IPOP.ToString());
            }

            if (root.Type == "<VARIABLES LIST>")
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

            if(root.Type == "<OPERATOR>")
            {
                int expressionStart = _ip;
                Identifier s1 = root.Childs[root.Childs.Count - 2];
                OutTree(s1);
                GenerateAsm(COMMANDS.JZ.ToString());
                int adress1 = _ip;
                GenerateAsm("0");

                Identifier s2 = root.Childs[2];
                OutTree(s2);
                GenerateAsm(COMMANDS.JMP.ToString());
                int adress2 = _ip;
                GenerateAsm("0");

                _commands[adress1] = _ip.ToString();
                _commands[adress2] = expressionStart.ToString();
            }

            if(root.Type == "<end of calculations description>")
            {
                GenerateAsm(COMMANDS.HALT.ToString());
                return;
            }

            if (root.Type == "<FUNCTION>")
            {
                if(root.Childs.Last().Value == "read")
                    _isRead = true;
                if(root.Childs.Last().Value == "write")
                    _isWrite = true;

                Identifier s = root.Childs[2];

                OutTree(s);
                _isRead = false;
                _isWrite = false;
            }

            if (root.Type == "<VARIABLES DECLARATION>" || 
                root.Type == "<PROGRAM>" ||
                root.Type == "<CALCULATIONS DESCRIPTION>" ||
                root.Type == "<OPERATIONS LIST>")
            {
                root.Childs.Reverse();
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
            expression.Reverse();
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
                else if (child.Type == "<binary operator>" || child.Type == "<unary operator>")
                {
                    while (operations.Any() && _priority.ContainsKey(operations.Peek().Value) && _priority[child.Value] < _priority[operations.Peek().Value])
                    {
                        newExpression.Add(operations.Peek());
                        operations.Pop();
                    }
                    operations.Push(child);
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
                _crashed = true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckError()
        {
            return _crashed;
        }
    }
}
