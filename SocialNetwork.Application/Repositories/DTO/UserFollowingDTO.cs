using System.Collections.Generic;

namespace SocialNetwork.Application.Repositories.DTO
{
    public class UserFollowingDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public ICollection<UserDTO> Following { get; set; }
    }
}
