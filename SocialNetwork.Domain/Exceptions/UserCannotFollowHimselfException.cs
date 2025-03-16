using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserCannotFollowHimselfException : UserException
    {
        public UserCannotFollowHimselfException() : base("El usuario no puede seguirse a sí mismo")
        {
        }
    }
}
