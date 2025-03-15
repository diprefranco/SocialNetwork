using SocialNetwork.Application.UseCases;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Infrastructure.Repositories;

namespace SocialNetwork.Console.Config
{
    public static class DependencyInjectionContainer
    {
        private static IUserRepository _userRepository;
        private static IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(true);
                }
                return _userRepository;
            }
        }

        private static IPostUseCase _postUseCase;
        public static IPostUseCase PostUseCase
        {
            get
            {
                if (_postUseCase == null)
                {
                    _postUseCase = new PostUseCase(UserRepository);
                }
                return _postUseCase;
            }
        }

        private static IFollowUserUseCase _followUserUseCase;
        public static IFollowUserUseCase FollowUserUseCase
        {
            get
            {
                if (_followUserUseCase == null)
                {
                    _followUserUseCase = new FollowUserUseCase(UserRepository);
                }
                return _followUserUseCase;
            }
        }
    }
}
