﻿namespace CollageAppp.MyLogging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("log to server memory");
        }
    
    }
}
