using System.Linq;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.DTO;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Application.UseCases
{
    public class FollowUserUseCase : IFollowUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public FollowUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public FollowUserDTO Execute(string followerUserName, string followeeUserName)
        {
            ValidateUserName(followerUserName);
            ValidateUserName(followeeUserName);

            UserFollowingDTO followerUserDTO = GetUserWithFollowingFromRepository(followerUserName);
            UserDTO followeeUserDTO = GetUserFromRepository(followeeUserName);

            var followerUser = new User(followerUserDTO.Id, followerUserDTO.UserName, followerUserDTO.Following.Select(x => new User(x.Id, x.UserName)).ToList());
            var followeeUser = new User(followeeUserDTO.Id, followeeUserDTO.UserName);
            followerUser.Follow(followeeUser);

            _userRepository.AddFollowing(followerUser.Id, followeeUser.Id);

            return new FollowUserDTO
            {
                FollowerUserName = followerUser.UserName,
                FolloweeUserName = followeeUser.UserName
            };
        }


        private static void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgumentException();
            }
        }

        private UserFollowingDTO GetUserWithFollowingFromRepository(string userName)
        {
            var userDTO = _userRepository.GetOneWithFollowingByUserName(userName);
            if (userDTO == null)
            {
                throw new UserNotFoundException(userName);
            }
            return userDTO;
        }

        private UserDTO GetUserFromRepository(string userName)
        {
            var userDTO = _userRepository.GetOneByUserName(userName);
            if (userDTO == null)
            {
                throw new UserNotFoundException(userName);
            }
            return userDTO;
        }
    }
}
