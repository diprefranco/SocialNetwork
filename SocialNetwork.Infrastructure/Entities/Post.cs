using System;

namespace SocialNetwork.Infrastructure.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }
    }
}
