using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Application.Repositories.Interfaces
{
    public interface IPostRepository
    {
        void Add(Post post);
    }
}
