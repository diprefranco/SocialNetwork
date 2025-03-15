using SocialNetwork.Console.Commands.Interfaces;

namespace SocialNetwork.Console.Commands
{
    public class InvalidCommand : ICommand
    {
        private string[] _commandArguments;
        public string[] CommandArguments { set => _commandArguments = value; }

        public string[] Execute()
        {
            return new[] { "Comando inválido" };
        }
    }
}
