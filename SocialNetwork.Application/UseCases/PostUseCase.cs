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
        private readonly IPostRepository _postRepository;

        public PostUseCase(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
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
            _postRepository.Add(post);

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
