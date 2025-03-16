using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Application.Exceptions
{
    public class IncorrectUserNameArgumentException : UserException
    {
        public IncorrectUserNameArgumentException() : base("Se debe indicar el nombre de usuario")
        {
        }
    }
}
