using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserAlreadyFollowing : UserException
    {
        public UserAlreadyFollowing(string followerUserName, string followeeUserName) : base($"{followerUserName} ya está siguiendo a {followeeUserName}")
        {
        }
    }
}
