using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Application.Exceptions
{
    public class UserNotFoundException : UserException
    {
        public UserNotFoundException(string userName) : base($"No se encontró ningún usuario @{userName}")
        {
        }
    }
}
