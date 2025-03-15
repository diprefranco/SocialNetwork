using SocialNetwork.Console.Helpers;
using SocialNetwork.Console.Commands.Factories;

namespace SocialNetwork.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string commandLine = ConsoleHelper.ReadCommandLine();

                if (string.IsNullOrWhiteSpace(commandLine))
                {
                    continue;
                }

                if (commandLine == "clear")
                {
                    ConsoleHelper.Clear();
                    continue;
                }

                if (commandLine == "exit")
                {
                    return;
                }

                string[] results = CommandFactory.CreateCommand(commandLine).Execute();
                ConsoleHelper.WriteCommandLineResponse(results);
            }
        }
    }
}
