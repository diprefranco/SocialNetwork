using System;

namespace SocialNetwork.Console.Exceptions
{
    public class IncorrectPostContentArgument : Exception
    {
        public IncorrectPostContentArgument() : base("Se debe indicar el contenido del post")
        {
        }
    }
}
