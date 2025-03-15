using SocialNetwork.Console.Extensions;
using SocialNetwork.Console.Commands.Interfaces;

namespace SocialNetwork.Console.Commands.Factories
{
    public static class CommandFactory
    {
        public static ICommand CreateCommand(string commandLine)
        {
            ICommand command = CreateFromCommandName(commandLine.GetCommandName());
            command.CommandArguments = commandLine.GetCommandArguments();
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
