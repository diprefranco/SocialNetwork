using SocialNetwork.Application.Repositories.DTO;

namespace SocialNetwork.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        UserDTO GetOneByUserName(string userName);
    }
}
