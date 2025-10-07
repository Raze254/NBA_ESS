using System;
using System.IO;

namespace Latest_Staff_Portal
{
    public class Helper
    {
        public static void writeToFile(string content, string filename = "data.txt")
        {
            var FileLocation = "C:\\JudiciaryLogs\\" + filename;
            var text = "";
            try
            {
                var w = new StreamWriter(FileLocation);
                var v = content + " Time is " + DateTime.Now;
                w.Write(v);
                w.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteToFileWithoutOverWrite(string content, string filename = "data.txt")
        {
            var fileLocation = "C:\\biodata\\" + filename;
            var text = "";
            try
            {
                using (var w = new StreamWriter(fileLocation, true)) // The 'true' parameter enables append mode
                {
                    var line = content + " Time is " + DateTime.Now;
                    w.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log or display an error message
            }
        }
    }
}