using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Console.Exceptions
{
    public class IncorrectUserNameArgumentException : UserException
    {
        public IncorrectUserNameArgumentException() : base("Se debe indicar el nombre de usuario con el prefijo @")
        {
        }
    }
}
