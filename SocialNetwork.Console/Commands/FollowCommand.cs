using SocialNetwork.Console.Commands.Interfaces;
using SocialNetwork.Console.Config;
using SocialNetwork.Console.Exceptions;
using SocialNetwork.Console.Extensions;
using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Console.Commands
{
    public class FollowCommand : ICommand
    {
        private string[] _commandArguments;
        public string[] CommandArguments { set => _commandArguments = value; }

        public string[] Execute()
        {
            string followerUserName = GetUserName(_commandArguments, 0);
            string followeeUserName = GetUserName(_commandArguments, 1);
            var follow = DependencyInjectionContainer.FollowUserUseCase.Execute(followerUserName, followeeUserName);
            return new[] { GetResponseOutputFormat(follow) };
        }

        private string GetUserName(string[] commandArguments, int argumentPosition)
        {
            string userName = commandArguments[argumentPosition].GetUserNameWithoutSymbol();
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgument();
            }
            return userName;
        }

        private string GetResponseOutputFormat(FollowUserDTO follow)
        {
            return $"{follow.FollowerUserName} empezó a seguir a {follow.FolloweeUserName}";
        }
    }
}
