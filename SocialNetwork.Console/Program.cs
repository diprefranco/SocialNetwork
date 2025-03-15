namespace SocialNetwork.Console
{
    using System;

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //read command line and returns command line.
                Console.Write("> ");
                string commandLine = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(commandLine))
                {
                    continue;
                }

                //distinguish command: can return an ICommand or Command (abstract). Then, I can have every command implementing each particular one, so I can apply polymorphism.
                //Every command line has command and n argument.
                //and a response after executing it
                //reponse is composed by n string responds lines + a response type (ok, error, info). In console: Each can have a color. All of them, in italics.
                const char COMMAND_LINE_SEPARATOR = ' ';
                string[] commandParts = commandLine.Split(COMMAND_LINE_SEPARATOR);

                switch (commandParts[0])
                {
                    case "post":
                        Console.WriteLine("  Alfonso posted -> \"Hola mundo\" @10:30");   // write command response.
                        break;

                    case "follow":
                        Console.WriteLine("  Alicia empezó a seguir a Ivan");
                        break;

                    case "dashboard":
                        Console.WriteLine("  \"Hoy puede ser un gran dia\" @Ivan @08:10");  // write command response --> n lines.
                        Console.WriteLine("  \"Hola mundo\" @Alfonso @10:30");
                        Console.WriteLine("  \"Para casa ya, media jornada, 12h\" @Ivan @20:10");
                        Console.WriteLine("  \"Adiós mundo cruel\" @Alfonso @20:30");
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("  Comando inválido");   // write command error response (coud be red).
                        break;
                }
            }
        }
    }
}
