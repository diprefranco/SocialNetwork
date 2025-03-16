using System;
using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<User> Following { get; set; }

        public User(Guid id, string userName)
        {
            Id = id;
            UserName = userName;
            Posts = new List<Post>();
            Following = new List<User>();
        }

        public User(Guid id, string userName, ICollection<User> following)
        {
            Id = id;
            UserName = userName;
            Posts = new List<Post>();
            Following = following;
        }

        public Post AddPost(string content)
        {
            var post = new Post(this, content);
            Posts.Add(post);
            return post;
        }

        public void Follow(User user)
        {
            if (user.Id == Id)
            {
                throw new UserCannotFollowHimselfException();
            }

            if (Following.Any(x => x.Id == user.Id))
            {
                throw new UserAlreadyFollowingException(UserName, user.UserName);
            }

            Following.Add(user);
        }
    }
}
