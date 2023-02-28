using CommandCompiler.Commands;

Console.WriteLine("Добро пожаловать в программу Compiler ZHR! " +
    "Авторы Акмашев С.В., Косенко А.С., Садыков З.Р. " +
    "Для получения информации о доступных командах введите help. Для выхода введите exit");

CommandsUI UI = new CommandsUI();
while (true)
{
    Console.Write("compiler> ");
    string? command = Console.ReadLine();

    if(command == null)
    {
        continue;
    }


    command = command.TrimStart().TrimEnd();

    if(command == "exit")
    {
        break;
    }

    if (UI.CheckCommandSyntax(command))
    {
        UI.ExecuteCommand(command);
    }
    else
    {
        Console.WriteLine("Выбранная команда не найдена");
    }
}
