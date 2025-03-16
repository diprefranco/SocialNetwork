using System;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserAlreadyFollowing : Exception
    {
        public UserAlreadyFollowing(string followerUserName, string followeeUserName) : base($"{followerUserName} ya está siguiendo a {followeeUserName}")
        {
        }
    }
}
