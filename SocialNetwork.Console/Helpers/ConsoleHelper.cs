namespace SocialNetwork.Console.Helpers
{
    using System;

    public static class ConsoleHelper
    {
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
                const string DEFAULT_INDENTATION = "  ";

                foreach (string line in response)
                {
                    Console.WriteLine(DEFAULT_INDENTATION + line);
                }
            }
        }
    }
}
