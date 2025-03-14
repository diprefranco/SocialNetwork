using System;

namespace SocialNetwork.Application.UseCases.DTO
{
    public class PostDTO
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }
    }
}
