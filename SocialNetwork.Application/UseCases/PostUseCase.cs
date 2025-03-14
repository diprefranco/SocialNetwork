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
            var userDTO = _userRepository.GetOneByUserName(userName);
            if (userDTO == null)
            {
                throw new UserNotFoundException();
            }

            var user = new User(userDTO.Id, userDTO.UserName);
            var post = user.AddPost(content);
            _userRepository.AddPost(post.User.Id, post.Content, post.PostDateTime);

            var postDTO = new PostDTO
            {
                UserName = post.User.UserName,
                Content = post.Content,
                PostDateTime = post.PostDateTime
            };

            return postDTO;
        }
    }
}
