using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageRecognition.Model
{
    public class ModelController
    {
        public static async Task<string> run_cmd(string cmd, string args)
        {
            Debug.WriteLine("abc");
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "";
            
            start.Arguments = ""; // link to machine learning model execution script
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
                    return result;
                }
            }
        }
    }
}
