using System.Linq;
using SocialNetwork.Application.Exceptions;
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
            var followerUserDTO = _userRepository.GetOneWithFollowingByUserName(followerUserName);
            if (followerUserDTO == null)
            {
                throw new UserNotFoundException(followerUserName);
            }

            var followeeUserDTO = _userRepository.GetOneByUserName(followeeUserName);
            if (followeeUserDTO == null)
            {
                throw new UserNotFoundException(followeeUserName);
            }

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
    }
}
