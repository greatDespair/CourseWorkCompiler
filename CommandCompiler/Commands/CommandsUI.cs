using CommandCompiler.Semantic;
using CommandCompiler.Translation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCompiler.Commands
{
    public class CommandsUI
    {
        private string _currentCode;
        public string Error { get; }

        private Dictionary<string, Command> _commands = new Dictionary<string, Command>() 
        {
            {"choose_file", new Command("choose_file",
                "Производит выбор файла для сборки и запуска",
                @"^choose_file\s\w*") },
            
            {"compile", new Command("compile", 
                "Без параметров: Производит сборку текущего выбранного файла\n" +
                " С параметрами: Производит сборку файла по указанному пути",
                @"^compile\w*")},

            {"runc", new Command("runc",
                "Производит запуск текущего файла сборки",
                "^runc$")},

            {"runlc", new Command("runlc",
                "Производит запуск файла последней успешной сборки",
                "^runlc$")},

            {"cat", new Command("cat",
                "Без параметров: Выводит содержимое текущего выбранного файла\n" +
                " С параметрами: Выводит содержимое файла по указанному пути",
                @"^cat\w*")},

            {"help", new Command("help",
                "Без параметров: Выводит список допуступных команд\n" +
                " С параметрами: Выводит описание выбранной команды",
                @"^help\w*")}
        };

        public CommandsUI()
        {
            _currentCode = "";
        }

        public bool CheckCommandSyntax(string command)
        {
            string commandName = command.Split(' ')[0];
            if (_commands.ContainsKey(commandName))
            {
                var currentCommand = _commands[commandName];

                if(Regex.IsMatch(command, currentCommand.Pattern))
                {
                    return true;
                }

            }
            return false;
        }

        public void ExecuteCommand(string command)
        {
            string[] args = command.Split(' ');
            string commandName = args[0];
            switch (commandName)
            {
                case "help":
                    {
                        if (args.Length > 1)
                        {
                            if (_commands.ContainsKey(args[1]))
                            {
                                Console.WriteLine("Команда " + args[1] + " >> " + _commands[args[1]].CommandDescription);
                            }
                            else
                            {
                                Console.WriteLine("Набранная команда не найдена в списке доступных");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Список команд:");
                            foreach(string currentCommand in _commands.Keys)
                            {
                                Console.WriteLine("> " + currentCommand);
                            }
                        }
                            break;
                    }

                case "choose_file":
                    {
                        try
                        {
                            _currentCode = File.ReadAllText(args[1]);
                            Console.WriteLine("Открыт файл по пути >> " + args[1]);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Не удалось прочитать файл: " + e.Message);
                        }
                        break;
                    }

                case "compile":
                    {
                        if (args.Length > 1)
                        {
                            string path = args[1];
                            try
                            {
                                _currentCode = File.ReadAllText(path);
                                Compile();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Не удалось открыть заданный файл: " + e.Message);
                            }
                        }
                        else
                            Compile();
                        break;
                    }

                case "runc":
                    {
                        Run(true);
                        break;
                    }

                case "runlc":
                    {
                        Run(false);
                        break;
                    }

                case "cat":
                    {
                        if (args.Length > 1)
                        {
                            try
                            {
                                string fileText = File.ReadAllText(args[1]);
                                Console.WriteLine(fileText);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Не удалось прочитать файл: " + e.Message);
                            }
                        }
                        else
                        {
                            if (_currentCode == "")
                            {
                                Console.WriteLine("Исходный код отсутствует.");
                            }
                            else
                            {
                                Console.WriteLine(_currentCode);
                            }
                        }
                        break;
                    }

                default:
                    Console.WriteLine("Набранная команда не найдена в списке доступных");
                    break;
            }
        }

        private void Run(bool isCurrent)
        {
            if (isCurrent)
            {
                if (Compile())
                {
                    VirtualMachine vm = new VirtualMachine(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");
                    vm.Run();
                    Console.WriteLine("Исполнение завершено с кодом Ex000");
                }
            }
            else
            {
                if (File.Exists(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr"))
                {
                    VirtualMachine vm = new VirtualMachine(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");
                    vm.Run();
                    Console.WriteLine("Исполнение завершено с кодом Ex000");
                }
            }
        }
        private bool Compile()
        {
            string dateTime = DateTime.Now.ToString(new CultureInfo("ru-RU")) + " >> ";
            if (_currentCode == "")
            {
                Console.WriteLine(dateTime + "Исходный код программы отсутствует");
                return false;
            }

            LexicalAnalyzer lexemsAnalyzer = new LexicalAnalyzer(_currentCode);
            if (lexemsAnalyzer.Analyze())
            {
                var lexems = lexemsAnalyzer.GetLexemsAsList();
                List<Identifier> InputSemantic = new List<Identifier>();
                foreach (var lexeme in lexems)
                {
                    if (lexeme.Type != "")
                    {
                        InputSemantic.Add(lexeme);
                    }
                }

                SyntactialAnalyzer semanticAnalyzer = new SyntactialAnalyzer(InputSemantic);
                if (semanticAnalyzer.AnalyzeSyntax())
                {
                    Translator translator = new Translator();
                    translator.OutTree(semanticAnalyzer._root);


                    translator.SaveCommands(Directory.GetCurrentDirectory() + "/TranslatedCode.zhr");


                    Console.WriteLine(dateTime + "Сборка произведена. Код: X00");
                    return true;
                }
                Console.WriteLine(dateTime + semanticAnalyzer.Error);
                return false;
            }
            if (lexemsAnalyzer.CurrentError == "X00")
                Console.WriteLine(dateTime + "Неизвестная лексическая ошибка");
            else
                Console.WriteLine(dateTime + lexemsAnalyzer.CurrentError);
            return false;
        }

    }
}
