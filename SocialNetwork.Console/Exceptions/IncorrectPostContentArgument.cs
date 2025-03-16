using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Console.Exceptions
{
    public class IncorrectPostContentArgument : UserException
    {
        public IncorrectPostContentArgument() : base("Se debe indicar el contenido del post")
        {
        }
    }
}
