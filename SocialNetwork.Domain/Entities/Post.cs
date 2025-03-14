using System;

namespace SocialNetwork.Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }

        public Post(User user, string content)
        {
            User = user;
            Content = content;
            PostDateTime = DateTime.Now;
        }
    }
}
