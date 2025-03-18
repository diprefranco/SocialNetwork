using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.DTO;
using SocialNetwork.Application.UseCases.Interfaces;

namespace SocialNetwork.Application.UseCases
{
    public class DashboardUseCase : IDashboardUseCase
    {
        private readonly IUserRepository _userRepository;

        public DashboardUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<PostDTO> Execute(string userName)
        {
            ValidateUserName(userName);

            var userDTO = _userRepository.GetOneWithFollowingPostsByUserName(userName);
            if (userDTO == null)
            {
                throw new UserNotFoundException(userName);
            }

            return userDTO.Following.SelectMany(x => x.Posts).OrderBy(x => x.PostDateTime).Select(x => new PostDTO
            {
                UserName = x.UserName,
                Content = x.Content,
                PostDateTime = x.PostDateTime
            }).ToList();
        }


        private static void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgumentException();
            }
        }
    }
}
