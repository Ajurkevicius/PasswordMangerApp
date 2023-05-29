using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognition.External_Scripts
{
    public class ExternalCommands
    {
        public static void run_chrome(string cmd, string args)
        {
            Debug.WriteLine("abc");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @""; // Link to cmd.exe
            start.Arguments = @"/c start chrome "+args;
            start.UseShellExecute = false;
            start.CreateNoWindow = true; 
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true; 
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stderr = process.StandardError.ReadToEnd(); 
                    string result = reader.ReadToEnd(); 
                    
                }
            }
        }
    }
}
