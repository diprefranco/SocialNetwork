using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Application.UseCases.Interfaces
{
    public interface IFollowUserUseCase
    {
        FollowUserDTO Execute(string followerUserName, string followeeUserName);
    }
}
