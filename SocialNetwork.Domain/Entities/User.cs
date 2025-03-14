using SocialNetwork.Domain.Exceptions;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<User> Following { get; set; }

        public User(long id, string userName)
        {
            Id = id;
            UserName = userName;
            Posts = new List<Post>();
            Following = new List<User>();
        }

        public User(long id, string userName, ICollection<User> following)
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
                throw new UserCannotFollowHimself();
            }

            if (Following.Contains(user))
            {
                throw new UserAlreadyFollowing();
            }

            Following.Add(user);
        }
    }
}
