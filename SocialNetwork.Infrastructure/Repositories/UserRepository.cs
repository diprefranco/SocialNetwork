using System;
using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Infrastructure.Entities;
using SocialNetwork.Infrastructure.Exceptions;

namespace SocialNetwork.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ICollection<User> _users;

        public UserRepository(bool initUsers)
        {
            _users = new List<User>();

            if (initUsers)
            {
                _users.Add(new User(Guid.NewGuid(), "Alfonso"));
                _users.Add(new User(Guid.NewGuid(), "Ivan"));
                _users.Add(new User(Guid.NewGuid(), "Alicia"));
            }
        }

        public UserDTO GetOneByUserName(string userName)
        {
            User user = GetUserByUserName(userName);
            return user == null ? null : new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public UserFollowingDTO GetOneWithFollowingByUserName(string userName)
        {
            User user = GetUserByUserName(userName);
            return user == null ? null : new UserFollowingDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Following = user.Following.Select(userFollowee => new UserDTO
                {
                    Id = userFollowee.Id,
                    UserName = userFollowee.UserName
                }).ToList()
            };
        }

        public UserFollowingPostsDTO GetOneWithFollowingPostsByUserName(string userName)
        {
            User user = GetUserByUserName(userName);
            return user == null ? null : new UserFollowingPostsDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Following = user.Following.Select(userFollowee => new UserPostsDTO
                {
                    Id = userFollowee.Id,
                    UserName = userFollowee.UserName,
                    Posts = userFollowee.Posts.Select(post => new PostDTO
                    {
                        Id = post.Id,
                        UserName = post.User.UserName,
                        Content = post.Content,
                        PostDateTime = post.PostDateTime
                    }).ToList()
                }).ToList()
            };
        }

        public void AddPost(Guid userId, string content, DateTime postDateTime)
        {
            User user = GetUserByIdOrThrow(userId);
            user.Posts.Add(new Post
            {
                Id = Guid.NewGuid(),
                User = user,
                Content = content,
                PostDateTime = postDateTime
            });
        }

        public void AddFollowing(Guid followerUserId, Guid followeeUserId)
        {
            User followerUser = GetUserByIdOrThrow(followerUserId);
            User followeeUser = GetUserByIdOrThrow(followeeUserId);
            followerUser.Following.Add(followeeUser);
        }


        private User GetUserByIdOrThrow(Guid userId)
        {
            User user = _users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        private User GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
