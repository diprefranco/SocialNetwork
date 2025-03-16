using System;

namespace SocialNetwork.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userName) : base($"No se encontró ningún usuario @{userName}")
        {
        }
    }
}
