using Moq;
using Xunit;
using System;
using SocialNetwork.Application.UseCases;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.UseCases.Interfaces;

namespace SocialNetwork.Tests.Application.UseCases
{
    public class PostUseCaseTests
    {
        [Fact]
        public void Execute_UserExistsAndPostContentValid_ShouldReturnPostCreated()
        {
            //Arrange
            const string userName = "Alfonso";
            const string content = "Hola mundo";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(userName))
                .Returns(new UserDTO { Id = Guid.NewGuid(), UserName = userName });

            IPostUseCase postUseCase = new PostUseCase(mockUserRepository.Object);

            //Act
            var post = postUseCase.Execute(userName, content);

            //Assert
            Assert.Equal(userName, post.UserName);
            Assert.Equal(content, post.Content);
            Assert.Equal(DateTime.Now, post.PostDateTime, new TimeSpan(0, 0, 1));
        }
    }
}
