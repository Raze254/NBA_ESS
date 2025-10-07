using System;
using System.IO;

namespace Latest_Staff_Portal.Utils
{
    public class LogHelper
    {
        private static readonly string DefaultLogFileName = "SystemLog.txt";

        /// <summary>
        /// Logs the DocNo, UserId, Time, and Action in a consistent format.
        /// </summary>
        /// <param name="docNo">The document number to log.</param>
        /// <param name="userId">The user ID to log.</param>
        /// <param name="action">The action taken to log.</param>
        public static void Log(string docNo, string userId, string action)
        {
            try
            {
                var logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                var logFilePath = Path.Combine(logDirectory, DefaultLogFileName);

                var logMessage = $"DocNo: {docNo}, UserId: {userId}, Time: {DateTime.Now:dd-MM-yyyy HH:mm:ss}, Action: {action}";

                using var writer = new StreamWriter(logFilePath, true);
                writer.WriteLine(logMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log message: {ex.Message}");
            }
        }
    }
}