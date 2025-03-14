using System;
using System.Collections.Generic;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class UserFollowingPostsDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public ICollection<UserPostsDTO> Following { get; set; }
    }
}
