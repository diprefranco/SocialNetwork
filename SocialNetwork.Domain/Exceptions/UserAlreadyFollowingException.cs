using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserAlreadyFollowingException : UserException
    {
        public UserAlreadyFollowingException(string followerUserName, string followeeUserName) : base($"{followerUserName} ya está siguiendo a {followeeUserName}")
        {
        }
    }
}
