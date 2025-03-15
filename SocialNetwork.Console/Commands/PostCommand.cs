using System.Linq;
using SocialNetwork.Console.Config;
using SocialNetwork.Console.Commands.Interfaces;
using SocialNetwork.Console.Exceptions;
using SocialNetwork.Console.Extensions;
using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Console.Commands
{
    public class PostCommand : ICommand
    {
        private string[] _commandArguments;
        public string[] CommandArguments { set => _commandArguments = value; }

        public string[] Execute()
        {
            string userName = GetUserName(_commandArguments);
            string content = GetPostContent(_commandArguments);
            var post = DependencyInjectionContainer.PostUseCase.Execute(userName, content);
            return new[] { GetResponseOutputFormat(post) };
        }

        private string GetUserName(string[] commandArguments)
        {
            string userName = commandArguments[0].GetUserName();
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgument();
            }
            return userName;
        }
        
        private string GetPostContent(string[] commandArguments)
        {
            string postContent = string.Join(CommandExtensions.COMMAND_LINE_SEPARATOR, commandArguments.Skip(1));
            if (string.IsNullOrWhiteSpace(postContent))
            {
                throw new IncorrectPostContentArgument();
            }
            return postContent;
        }

        private string GetResponseOutputFormat(PostDTO post)
        {
            return $"{post.UserName} posted -> {post.Content.GetPostContentFormat()} {post.PostDateTime.GetHourFormat()}";
        }
    }
}
