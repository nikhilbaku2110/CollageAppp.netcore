﻿namespace CollageAppp.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("logtofile");
        
        }
    }
}
