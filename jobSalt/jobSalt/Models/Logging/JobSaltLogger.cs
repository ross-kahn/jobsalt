using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace jobSalt.Models.Logging
{
    // Singleton code taken from http://www.yoda.arachsys.com/csharp/singleton.html
    public sealed class JobSaltLogger
    {
        static readonly JobSaltLogger instance = new JobSaltLogger();
        private string filePath;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static JobSaltLogger()
        {

        }

        JobSaltLogger()
        {
            // Config stuff here I guess
            filePath = ".";
        }

        public void SetFilePath(string value)
        {            
            filePath = value;
        }

        public void log(String toLog)
        {
            DateTime tStamp = DateTime.Now;
            StreamWriter sw = null;
            toLog = tStamp.ToShortDateString() + " " + tStamp.ToShortTimeString() + " -> " + toLog;
            string file = filePath + "\\" + string.Format("{0:D2}{1:D2}{2:D2}MyApp.log", (tStamp.Year - 2000), tStamp.Month, tStamp.Day);
            using (sw = new StreamWriter(file, true))
            {
                sw.WriteLine(toLog);
            }
        }

        public static JobSaltLogger Instance
        {
            get
            {
                return instance;
            }            
        }

    }
}