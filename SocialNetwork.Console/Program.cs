using SocialNetwork.Console.Helpers;
using SocialNetwork.Console.Commands.Factories;
using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
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
                catch (UserException ex)
                {
                    ConsoleHelper.WriteCommandLineResponse(ex.Message);
                }
                catch (System.Exception)
                {
                    ConsoleHelper.WriteCommandLineResponse("Ha ocurrido un error inesperado, vuelva a intentarlo más tarde");
                }
            }
        }
    }
}
