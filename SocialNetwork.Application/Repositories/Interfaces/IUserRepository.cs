using System;
using SocialNetwork.Application.Repositories.DTO;

namespace SocialNetwork.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        UserDTO GetOneByUserName(string userName);
        UserFollowingDTO GetOneWithFollowingByUserName(string userName);
        UserFollowingPostsDTO GetOneWithFollowingPostsByUserName(string userName);
        void AddPost(Guid userId, string content, DateTime postDateTime);
        void AddFollowing(Guid followerUserId, Guid followeeUserId);
    }
}
