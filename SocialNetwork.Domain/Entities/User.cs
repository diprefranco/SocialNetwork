using System.Collections;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public IList Posts { get; set; }

        public User(long id, string userName)
        {
            Id = id;
            UserName = userName;
            Posts = new List<Post>();
        }

        public Post AddPost(string content)
        {
            var post = new Post(this, content);
            Posts.Add(post);
            return post;
        }
    }
}
