using System;

namespace SocialNetwork.Console.Extensions
{
    public static class OutputExtensions
    {
        private static readonly char USER_SYMBOL = '@';
        private static readonly char HOUR_SYMBOL = '@';

        public static string GetHourFormat(this DateTime dateTime)
        {
            return HOUR_SYMBOL + dateTime.ToString("HH:mm");
        }

        public static string GetPostContentFormat(this string content)
        {
            return $"\"{content}\"";
        }

        public static string GetUserNameWithSymbolFormat(this string userName)
        {
            return USER_SYMBOL + userName;
        }
    }
}
