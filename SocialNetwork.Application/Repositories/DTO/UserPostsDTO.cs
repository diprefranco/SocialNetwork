using System;
using System.Collections.Generic;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class UserPostsDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
    }
}
