namespace SocialNetwork.Application.UseCases.Interfaces
{
    public interface IFollowUserUseCase
    {
        void Execute(string followerUserName, string followeeUserName);
    }
}
