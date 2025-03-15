using System.Linq;

namespace SocialNetwork.Console.Extensions
{
    public static class CommandExtensions
    {
        private static readonly char USER_SYMBOL = '@';
        public static readonly char COMMAND_LINE_SEPARATOR = ' ';

        public static string GetCommandName(this string commandLine)
        {
            return commandLine.GetCommandParts()[0];
        }

        public static string[] GetCommandArguments(this string commandLine)
        {
            return commandLine.GetCommandParts().Skip(1).ToArray();
        }

        public static string[] GetCommandParts(this string commandLine)
        {
            return commandLine.Split(COMMAND_LINE_SEPARATOR);
        }

        /// <summary>
        /// Returns the user name without the user symbol, or empty string if doesn't have the symbol.
        /// </summary>
        /// <param name="userNameWithSymbol"></param>
        /// <returns></returns>
        public static string GetUserNameWithoutSymbol(this string userNameWithSymbol)
        {
            return userNameWithSymbol.StartsWith(USER_SYMBOL) ? userNameWithSymbol.Substring(1) : string.Empty;
        }
    }
}
