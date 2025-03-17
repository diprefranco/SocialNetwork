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
        public void Execute_UserNotExistsAndPostContentValid_ShouldThrowUserNotFoundExceptionAndNotCallAddPost()
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
            Assert.Throws<SocialNetwork.Application.Exceptions.UserNotFoundException>(() => postUseCase.Execute(userName, content));
            mockUserRepository.Verify(
                repo => repo.AddPost(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<DateTime>()),
                Times.Never
            );
        }
        
        [Fact]
        public void Execute_UserExistsAndPostContentNull_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost()
        {
            //Arrange
            const string content = null;
            Execute_UserExistsAndPostContentInvalid_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost(content);
        }

        [Fact]
        public void Execute_UserExistsAndPostContentEmpty_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost()
        {
            //Arrange
            const string content = "";
            Execute_UserExistsAndPostContentInvalid_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost(content);
        }

        [Fact]
        public void Execute_UserExistsAndPostContentWhiteSpaces_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost()
        {
            //Arrange
            const string content = "    ";
            Execute_UserExistsAndPostContentInvalid_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost(content);
        }
        
        private void Execute_UserExistsAndPostContentInvalid_ShouldThrowIncorrectPostContentArgumentExceptionAndNotCallAddPost(string content)
        {
            //Arrange
            const string userName = "Alfonso";

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(userName))
                .Returns(new UserDTO { Id = Guid.NewGuid(), UserName = userName });

            IPostUseCase postUseCase = new PostUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Application.Exceptions.IncorrectPostContentArgumentException>(() => postUseCase.Execute(userName, content));
            mockUserRepository.Verify(
                repo => repo.AddPost(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<DateTime>()),
                Times.Never
            );
        }
        
        [Fact]
        public void Execute_UserNullAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string userName = null;
            Execute_UserInvalidAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(userName);
        }

        [Fact]
        public void Execute_UserEmptyAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string userName = "";
            Execute_UserInvalidAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(userName);
        }

        [Fact]
        public void Execute_UserWhiteSpacesAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string userName = "    ";
            Execute_UserInvalidAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(userName);
        }
        
        private void Execute_UserInvalidAndPostContentValid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(string userName)
        {
            //Arrange
            const string content = "Hola mundo";
            var mockUserRepository = new Mock<IUserRepository>();
            IPostUseCase postUseCase = new PostUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Application.Exceptions.IncorrectUserNameArgumentException>(() => postUseCase.Execute(userName, content));
            mockUserRepository.Verify(
                repo => repo.GetOneByUserName(It.IsAny<string>()),
                Times.Never
            );
            mockUserRepository.Verify(
                repo => repo.AddPost(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<DateTime>()),
                Times.Never
            );
        }
    }
}
