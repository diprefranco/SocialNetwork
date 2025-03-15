using System;

namespace SocialNetwork.Console.Extensions
{
    public static class OutputExtensions
    {
        private static readonly char USER_SYMBOL = '@';

        public static string GetHourFormat(this DateTime dateTime)
        {
            return USER_SYMBOL + dateTime.ToString("HH:mm");
        }

        public static string GetPostContentFormat(this string content)
        {
            return $"\"{content}\"";
        }
    }
}
