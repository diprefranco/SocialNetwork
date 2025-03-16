using Moq;
using Xunit;
using System;
using SocialNetwork.Application.UseCases;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Application.Exceptions;

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

        [Fact]
        public void Execute_UserExistsAndPostContentValid_ShouldCallAddPostOnce()
        {
            //Arrange
            const string userName = "Alfonso";
            const string content = "Hola mundo";
            Guid userId = Guid.NewGuid();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(userName))
                .Returns(new UserDTO { Id = userId, UserName = userName });

            IPostUseCase postUseCase = new PostUseCase(mockUserRepository.Object);

            //Act
            var post = postUseCase.Execute(userName, content);

            //Assert
            mockUserRepository.Verify(
                repo => repo.AddPost(userId, content, It.IsAny<DateTime>()),
                Times.Once
            );
        }

        [Fact]
        public void Execute_UserNotExistsAndPostContentValid_ShouldThrowUserNotFoundException()
        {
            //Arrange
            const string userName = "Alfonso";
            const string content = "Hola mundo";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(userName))
                .Returns((UserDTO)null);

            IPostUseCase postUseCase = new PostUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<UserNotFoundException>(() => postUseCase.Execute(userName, content));
        }
    }
}
