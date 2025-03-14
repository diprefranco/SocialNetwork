using System;
using System.Collections.Generic;

namespace SocialNetwork.Infrastructure.Entities
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
    }
}
