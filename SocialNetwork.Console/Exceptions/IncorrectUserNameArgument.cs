using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Console.Exceptions
{
    public class IncorrectUserNameArgument : UserException
    {
        public IncorrectUserNameArgument() : base("Se debe indicar el nombre de usuario con el prefijo @")
        {
        }
    }
}
