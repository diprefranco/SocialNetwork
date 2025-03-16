using SocialNetwork.Common.Exceptions;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserCannotFollowHimself : UserException
    {
        public UserCannotFollowHimself() : base("El usuario no puede seguirse a sí mismo")
        {
        }
    }
}
