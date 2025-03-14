using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Application.UseCases.Interfaces
{
    public interface IPostUseCase
    {
        PostDTO Execute(string userName, string content);
    }
}
