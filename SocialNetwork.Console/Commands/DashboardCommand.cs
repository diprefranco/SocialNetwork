using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Console.Commands.Interfaces;
using SocialNetwork.Console.Config;
using SocialNetwork.Console.Exceptions;
using SocialNetwork.Console.Extensions;
using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Console.Commands
{
    public class DashboardCommand : ICommand
    {
        private string[] _commandArguments;
        public string[] CommandArguments { set => _commandArguments = value; }

        public string[] Execute()
        {
            string userName = GetUserName(_commandArguments);
            var posts = DependencyInjectionContainer.DashboardUseCase.Execute(userName);
            return GetResponseOutputFormat(posts);
        }

        private string GetUserName(string[] commandArguments)
        {
            string userName = commandArguments[0].GetUserNameWithoutSymbol();
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgument();
            }
            return userName;
        }

        private string[] GetResponseOutputFormat(IEnumerable<PostDTO> posts)
        {
            string[] response = posts.Select(post => $"{post.Content.GetPostContentFormat()} {post.UserName.GetUserNameWithSymbolFormat()} {post.PostDateTime.GetHourFormat()}").ToArray();
            return response.Length == 0 ? new[] { "Dashboard vacío" } : response;
        }
    }
}
