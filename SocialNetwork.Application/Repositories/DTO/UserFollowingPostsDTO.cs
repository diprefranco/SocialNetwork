using System.Collections.Generic;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class UserFollowingPostsDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public ICollection<UserPostsDTO> Following { get; set; }
    }
}
