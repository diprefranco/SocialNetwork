using System.Linq;
using SocialNetwork.Console.Commands.Interfaces;

namespace SocialNetwork.Console.Commands.Factories
{
    public static class CommandFactory
    {
        public static ICommand CreateCommand(string commandLine)
        {
            const char COMMAND_LINE_SEPARATOR = ' ';
            string[] commandParts = commandLine.Split(COMMAND_LINE_SEPARATOR);

            ICommand command = CreateFromCommandName(commandParts[0]);
            command.CommandArguments = commandParts.Skip(1).ToArray();

            return command;
        }

        private static ICommand CreateFromCommandName(string commandName)
        {
            switch (commandName)
            {
                case "post":
                    return new PostCommand();

                case "follow":
                    return new FollowCommand();

                case "dashboard":
                    return new DashboardCommand();

                default:
                    return new InvalidCommand();
            }
        }
    }
}
