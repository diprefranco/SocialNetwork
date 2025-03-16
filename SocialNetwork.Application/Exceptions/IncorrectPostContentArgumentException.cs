using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Application.Exceptions
{
    public class IncorrectPostContentArgumentException : UserException
    {
        public IncorrectPostContentArgumentException() : base("Se debe indicar el contenido del post")
        {
        }
    }
}
