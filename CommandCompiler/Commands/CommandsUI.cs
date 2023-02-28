using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandCompiler.Commands
{
    public class CommandsUI
    {
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
                                Console.WriteLine("Команда " + commandName + " >> " + _commands[args[1]]);
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

                        break;
                    }

                case "compile":
                    {

                        break;
                    }

                case "runc":
                    {

                        break;
                    }

                case "runlc":
                    {

                        break;
                    }

                case "cat":
                    {

                        break;
                    }
            }
        }

    }
}
