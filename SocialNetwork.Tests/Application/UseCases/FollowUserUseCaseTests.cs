using Moq;
using Xunit;
using System;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Application.UseCases;
using System.Collections.Generic;

namespace SocialNetwork.Tests.Application.UseCases
{
    public class FollowUserUseCaseTests
    {
        [Fact]
        public void Execute_FollowerUserExistsWithEmptyFollowing_ShouldReturnFollowCreated()
        {
            //Arrange
            const string followerUserName = "Alicia";
            const string followeeUserName = "Ivan";
            ICollection<UserDTO> following = new List<UserDTO>();

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
