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
    }
}
