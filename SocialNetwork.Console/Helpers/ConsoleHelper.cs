namespace SocialNetwork.Console.Helpers
{
    using System;

    public static class ConsoleHelper
    {
        private static readonly string DEFAULT_INDENTATION = "  ";

        /// <summary>
        /// Reads the next command line from the user input.
        /// </summary>
        /// <returns></returns>
        public static string ReadCommandLine()
        {
            Console.Write("> ");
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// Writes the command line response with specific format.
        /// </summary>
        /// <param name="response"></param>
        public static void WriteCommandLineResponse(string[] response)
        {
            if (response != null && response.Length > 0)
            {
                foreach (string line in response)
                {
                    WriteCommandLineResponse(line);
                }
            }
        }

        /// <summary>
        /// Writes the command line response with specific format.
        /// </summary>
        /// <param name="response"></param>
        public static void WriteCommandLineResponse(string response)
        {
            Console.WriteLine(DEFAULT_INDENTATION + response);
        }
        
        /// <summary>
        /// Clears the console.
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }
    }
}
