using System.Collections.Generic;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class UserPostsDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
    }
}
