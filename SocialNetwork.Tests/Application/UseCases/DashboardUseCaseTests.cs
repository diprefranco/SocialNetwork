using Moq;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using SocialNetwork.Application.Repositories.DTO;
using SocialNetwork.Application.Repositories.Interfaces;
using SocialNetwork.Application.UseCases.Interfaces;
using SocialNetwork.Application.UseCases;

namespace SocialNetwork.Tests.Application.UseCases
{
    public class DashboardUseCaseTests
    {
        [Fact]
        public void Execute_UserExistsWithFollowingPosts_ShouldReturnPostsInAscendingOrderByPostDateTime()
        {
            //Arrange
            const string userName = "Alicia";
            const string userNameFollowing1 = "Alfonso";
            const string userNameFollowing2 = "Ivan";
            UserFollowingPostsDTO userDTO = new UserFollowingPostsDTO
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Following = new List<UserPostsDTO>
                {
                    new UserPostsDTO
                    {
                        Id = Guid.NewGuid(),
                        UserName = userNameFollowing1,
                        Posts = new List<PostDTO>
                        {
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing1,
                                Content = "Hola mundo",
                                PostDateTime = new DateTime(2025, 3, 17, 10, 30, 0)
                            },
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing1,
                                Content = "Nos vemos mañana",
                                PostDateTime = new DateTime(2025, 3, 17, 23, 30, 0)
                            },
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing1,
                                Content = "Adiós mundo cruel",
                                PostDateTime = new DateTime(2025, 3, 17, 20, 30, 0)
                            }
                        }
                    },
                    new UserPostsDTO
                    {
                        Id = Guid.NewGuid(),
                        UserName = userNameFollowing2,
                        Posts = new List<PostDTO>
                        {
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing2,
                                Content = "A dormir que mañana se arranca a tope!",
                                PostDateTime = new DateTime(2025, 3, 17, 23, 10, 0)
                            },
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing2,
                                Content = "Hoy puede ser un gran dia",
                                PostDateTime = new DateTime(2025, 3, 17, 8, 10, 0)
                            },
                            new PostDTO
                            {
                                Id = Guid.NewGuid(),
                                UserName = userNameFollowing2,
                                Content = "Para casa ya, media jornada, 12h",
                                PostDateTime = new DateTime(2025, 3, 17, 20, 10, 0)
                            }
                        }
                    }
                }
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(repo => repo.GetOneWithFollowingPostsByUserName(userName))
                .Returns(userDTO);
            
            IDashboardUseCase dashboardUseCase = new DashboardUseCase(mockUserRepository.Object);

            //Act
            IEnumerable<SocialNetwork.Application.UseCases.DTO.PostDTO> posts = dashboardUseCase.Execute(userName);

            //Assert
            Assert.True(posts.SequenceEqual(posts.OrderBy(p => p.PostDateTime)));
        }
    }
}
