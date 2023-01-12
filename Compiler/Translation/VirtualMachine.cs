using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Translation
{
    public class VirtualMachine
    {
        private StreamReader _read;
        private Dictionary<string, bool> _variables = new Dictionary<string, bool>();
        private Stack<bool> _stack = new Stack<bool>();
        private List<string> _program = new List<string>();
        private int _ip = 0;
        public VirtualMachine(string Path)
        {
            try
            {
                _program.AddRange(File.ReadAllLines(Path));
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения файла");
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public void Run()
        {
            if (_program.Count != 0)
            {
                while (true)
                {
                    string op = _program[_ip];

                    string arg = "";

                    if (_ip < _program.Count - 1)
                    {
                        arg = _program[_ip + 1];
                    }

                    if (op == "IFETCH")
                    {
                        if (!_variables.ContainsKey(arg))
                        {
                            _variables.Add(arg, false);
                        }
                        _stack.Push(_variables[arg]);
                        _ip += 2;
                    }

                    if (op == "ISTORE")
                    {
                        _variables[arg] = _stack.Peek();
                        _ip += 2;
                    }

                    if (op == "IPUSH")
                    {
                        if (arg == "1")
                            _stack.Push(true);
                        else
                            _stack.Push(false);
                        _ip += 2;
                    }

                    if (op == "IPOP")
                    {
                        _stack.Pop();
                        _ip++;
                    }

                    if (op == "IAND")
                    {
                        bool v1 = _stack.Peek();
                        _stack.Pop();
                        bool v2 = _stack.Peek();
                        _stack.Pop();
                        bool result = v1 && v2;
                        _stack.Push(result);
                        _ip++;
                    }

                    if (op == "IOR")
                    {
                        bool v1 = _stack.Peek();
                        _stack.Pop();
                        bool v2 = _stack.Peek();
                        _stack.Pop();
                        bool result = v1 || v2;
                        _stack.Push(result);
                        _ip++;
                    }

                    if (op == "IIMP")
                    {
                        bool v1 = _stack.Peek();
                        _stack.Pop();
                        bool v2 = _stack.Peek();
                        _stack.Pop();
                        bool result = v1 || !v2;
                        _stack.Push(result);
                        _ip++;
                    }

                    if (op == "INOT")
                    {
                        bool v1 = _stack.Peek();
                        _stack.Pop();
                        bool result = !v1;
                        _stack.Push(result);
                        _ip++;
                    }

                    if (op == "IWRITE")
                    {
                        AllocConsole();
                        if (!_variables.ContainsKey(arg))
                        {
                            _variables.Add(arg, false);
                        }
                        Console.WriteLine(arg + ": " + _variables[arg] + "\n");
                        _ip += 2;
                    }

                    if (op == "JZ")
                    {
                        bool v1 = _stack.Peek();
                        _stack.Pop();
                        if (!v1)
                            _ip = Convert.ToInt32(arg);
                        else
                        {
                            _ip += 2;
                        }
                    }

                    if (op == "JMP")
                        _ip = Convert.ToInt32(arg);

                    if (op == "IREAD")
                    {
                        AllocConsole();
                        bool temp;
                        Console.WriteLine("Ввод " + arg + ": ");
                        try
                        {
                            temp = Convert.ToBoolean(Console.ReadLine());

                            _variables[arg] = temp;
                            _ip += 2;
                        }
                        catch (Exception ex)
                        {
                            break;
                        }

                    }

                    if (op == "HALT")
                        break;
                }
            }
        }
    }
}
