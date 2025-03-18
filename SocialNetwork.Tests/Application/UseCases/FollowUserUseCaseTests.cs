using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Application.UseCases;

namespace SocialNetwork.Tests.Application.UseCases
{
    public class FollowUserUseCaseTests
    {
        [Fact]
        public void Execute_FollowerUserExistsWithNoFollowing_ShouldReturnFollowCreated()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            ICollection<UserDTO> following = new List<UserDTO>();

            Execute_FollowerUserExists_ShouldReturnFollowCreated(followerUserName, followeeUserName, following);
        }

        [Fact]
        public void Execute_FollowerUserExistsWithFollowing_ShouldReturnFollowCreated()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            ICollection<UserDTO> following = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Pedro"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Juan"
                }
            };

            Execute_FollowerUserExists_ShouldReturnFollowCreated(followerUserName, followeeUserName, following);
        }

        private void Execute_FollowerUserExists_ShouldReturnFollowCreated(string followerUserName, string followeeUserName, ICollection<UserDTO> following)
        {
            //Arrange
            var followerUser = new UserFollowingDTO { Id = Guid.NewGuid(), UserName = followerUserName, Following = following };
            var followeeUser = new UserDTO { Id = Guid.NewGuid(), UserName = followeeUserName };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingByUserName(followerUserName))
                .Returns(followerUser);
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(followeeUserName))
                .Returns(followeeUser);

            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act
            var follow = followUserUseCase.Execute(followerUserName, followeeUserName);

            //Assert
            Assert.Equal(followerUserName, follow.FollowerUserName);
            Assert.Equal(followeeUserName, follow.FolloweeUserName);
            mockUserRepository.Verify(
                repo => repo.AddFollowing(followerUser.Id, followeeUser.Id),
                Times.Once
            );
        }

        [Fact]
        public void Execute_FollowerUserExistsAlreadyFollowing_ShouldThrowUserAlreadyFollowingExceptionAndNotCallAddFollowing()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            ICollection<UserDTO> following = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Pedro"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Juan"
                }
            };

            var followerUser = new UserFollowingDTO { Id = Guid.NewGuid(), UserName = followerUserName, Following = following };
            var followeeUser = new UserDTO { Id = Guid.NewGuid(), UserName = followeeUserName };
            following.Add(followeeUser);

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingByUserName(followerUserName))
                .Returns(followerUser);
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(followeeUserName))
                .Returns(followeeUser);

            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Domain.Exceptions.UserAlreadyFollowingException>(() => followUserUseCase.Execute(followerUserName, followeeUserName));
            mockUserRepository.Verify(
                repo => repo.AddFollowing(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never
            );
        }

        [Fact]
        public void Execute_FollowerUserExistsAndWantsToFollowHimself_ShouldThrowUserCannotFollowHimselfExceptionAndNotCallAddFollowing()
        {
            //Arrange
            Guid followerUserId = Guid.NewGuid();
            const string followerUserName = "Alicia";
            ICollection<UserDTO> following = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Pedro"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Juan"
                }
            };

            var followerUser = new UserFollowingDTO { Id = followerUserId, UserName = followerUserName, Following = following };
            var followeeUser = new UserDTO { Id = followerUserId, UserName = followerUserName };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingByUserName(followerUserName))
                .Returns(followerUser);
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(followerUserName))
                .Returns(followeeUser);

            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Domain.Exceptions.UserCannotFollowHimselfException>(() => followUserUseCase.Execute(followerUserName, followerUserName));
            mockUserRepository.Verify(
                repo => repo.AddFollowing(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never
            );
        }

        [Fact]
        public void Execute_FollowerUserNotExists_ShouldThrowUserNotFoundExceptionAndNotCallAddFollowing()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            var followeeUser = new UserDTO { Id = Guid.NewGuid(), UserName = followeeUserName };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingByUserName(followerUserName))
                .Returns((UserFollowingDTO)null);
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(followeeUserName))
                .Returns(followeeUser);

            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Application.Exceptions.UserNotFoundException>(() => followUserUseCase.Execute(followerUserName, followeeUserName));
            mockUserRepository.Verify(
                repo => repo.AddFollowing(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never
            );
        }

        [Fact]
        public void Execute_FollowerUserExistsButFolloweeUserNotExists_ShouldThrowUserNotFoundExceptionAndNotCallAddFollowing()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            ICollection<UserDTO> following = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Pedro"
                },
                new UserDTO
                {
                    Id = Guid.NewGuid(),
                    UserName = "Juan"
                }
            };

            var followerUser = new UserFollowingDTO { Id = Guid.NewGuid(), UserName = followerUserName, Following = following };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingByUserName(followerUserName))
                .Returns(followerUser);
            mockUserRepository
                .Setup(repo => repo.GetOneByUserName(followeeUserName))
                .Returns((UserDTO)null);

            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Application.Exceptions.UserNotFoundException>(() => followUserUseCase.Execute(followerUserName, followeeUserName));
            mockUserRepository.Verify(
                repo => repo.AddFollowing(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never
            );
        }

        [Fact]
        public void Execute_FollowerUserNull_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = null;
            const string followeeUserName = "Ivan";
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        [Fact]
        public void Execute_FollowerUserEmpty_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = "";
            const string followeeUserName = "Ivan";
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        [Fact]
        public void Execute_FollowerUserWhiteSpaces_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = "    ";
            const string followeeUserName = "Ivan";
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        [Fact]
        public void Execute_FolloweeUserNull_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = null;
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        [Fact]
        public void Execute_FolloweeUserEmpty_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "";
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        [Fact]
        public void Execute_FolloweeUserWhiteSpaces_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "    ";
            Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(followerUserName, followeeUserName);
        }

        private void Execute_BothUsersInvalid_ShouldThrowIncorrectUserNameArgumentExceptionAndNotCallRepositoryMethods(string followerUserName, string followeeUserName)
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            IFollowUserUseCase followUserUseCase = new FollowUserUseCase(mockUserRepository.Object);

            //Act & Assert
            Assert.Throws<SocialNetwork.Application.Exceptions.IncorrectUserNameArgumentException>(() => followUserUseCase.Execute(followerUserName, followeeUserName));
            mockUserRepository.Verify(
                repo => repo.GetOneWithFollowingByUserName(It.IsAny<string>()),
                Times.Never
            );
            mockUserRepository.Verify(
                repo => repo.GetOneByUserName(It.IsAny<string>()),
                Times.Never
            );
            mockUserRepository.Verify(
                repo => repo.AddFollowing(It.IsAny<Guid>(), It.IsAny<Guid>()),
                Times.Never
            );
        }
    }
}
