using System;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }
    }
}
