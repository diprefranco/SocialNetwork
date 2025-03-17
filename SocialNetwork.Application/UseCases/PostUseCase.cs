using SocialNetwork.Application.Exceptions;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.DTO;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.Application.UseCases
{
    public class PostUseCase : IPostUseCase
    {
        private readonly IUserRepository _userRepository;

        public PostUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public PostDTO Execute(string userName, string content)
        {
            ValidateUserName(userName);
            ValidatePostContent(content);

            var userDTO = GetUserFromRepository(userName);

            var user = new User(userDTO.Id, userDTO.UserName);
            var post = user.AddPost(content);

            _userRepository.AddPost(post.User.Id, post.Content, post.PostDateTime);

            return new PostDTO
            {
                UserName = post.User.UserName,
                Content = post.Content,
                PostDateTime = post.PostDateTime
            };
        }
        
        
        private static void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new IncorrectUserNameArgumentException();
            }
        }

        private static void ValidatePostContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new IncorrectPostContentArgumentException();
            }
        }
        
        private Repositories.DTO.UserDTO GetUserFromRepository(string userName)
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
