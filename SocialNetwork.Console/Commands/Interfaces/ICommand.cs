namespace SocialNetwork.Console.Commands.Interfaces
{
    public interface ICommand
    {
        public string[] CommandArguments { set; }
        string[] Execute();
    }
}
