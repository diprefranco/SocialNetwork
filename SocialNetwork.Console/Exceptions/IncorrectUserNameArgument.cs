using System;

namespace SocialNetwork.Console.Exceptions
{
    public class IncorrectUserNameArgument : Exception
    {
        public IncorrectUserNameArgument() : base("Se debe indicar el nombre de usuario con el prefijo @")
        {
        }
    }
}
