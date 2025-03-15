using SocialNetwork.Console.Config;
using SocialNetwork.Console.Commands.Interfaces;

namespace SocialNetwork.Console.Commands
{
    public class PostCommand : ICommand
    {
        private string[] _commandArguments;
        public string[] CommandArguments { set => _commandArguments = value; }

        public string[] Execute()
        {
            //TODO: treat arguments. If incorrect arguments, return arguments error message.
            var post = DependencyInjectionContainer.PostUseCase.Execute("Alfonso", "Hola mundo");

            //TODO: in helpers add hour format and post content format --> "".
            return new[] { $"{post.UserName} posted -> \"{post.Content}\" @{post.PostDateTime}" };

            //        Console.WriteLine("  Alfonso posted -> \"Hola mundo\" @10:30");

        }
    }
}
