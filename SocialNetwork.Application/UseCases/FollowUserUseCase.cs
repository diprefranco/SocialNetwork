using SocialNetwork.Application.Exceptions;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Domain.Entities;
using System.Linq;

namespace SocialNetwork.Application.UseCases
{
    public class FollowUserUseCase : IFollowUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public FollowUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(string followerUserName, string followeeUserName)
        {
            var followerUserDTO = _userRepository.GetOneWithFollowingByUserName(followerUserName);
            if (followerUserDTO == null)
            {
                throw new UserNotFoundException();
            }

            var followeeUserDTO = _userRepository.GetOneByUserName(followeeUserName);
            if (followeeUserDTO == null)
            {
                throw new UserNotFoundException();
            }

            var followerUser = new User(followerUserDTO.Id, followerUserDTO.UserName, followerUserDTO.Following.Select(x => new User(x.Id, x.UserName)).ToList());
            var followeeUser = new User(followeeUserDTO.Id, followeeUserDTO.UserName);
            followerUser.Follow(followeeUser);
            
            _userRepository.AddFollowing(followerUserName, followeeUserName);
        }
    }
}
