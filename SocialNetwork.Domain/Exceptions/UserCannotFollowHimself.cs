using System;

namespace SocialNetwork.Domain.Exceptions
{
    public class UserCannotFollowHimself : Exception
    {
        public UserCannotFollowHimself() : base("El usuario no puede seguirse a sí mismo")
        {
        }
    }
}
