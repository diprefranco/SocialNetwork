﻿using SocialNetwork.Console.Commands.Interfaces;

namespace SocialNetwork.Console.Commands
{
    public class InvalidCommand : ICommand
    {
        public string[] CommandArguments { set => throw new System.NotImplementedException(); }

        public string[] Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
