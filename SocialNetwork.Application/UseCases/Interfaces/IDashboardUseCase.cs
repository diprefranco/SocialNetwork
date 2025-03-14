using System.Collections.Generic;
using SocialNetwork.Application.UseCases.DTO;

namespace SocialNetwork.Application.UseCases.Interfaces
{
    public interface IDashboardUseCase
    {
        IEnumerable<PostDTO> Execute(string userName);
    }
}
