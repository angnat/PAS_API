﻿namespace PAS_API.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string Type)
        {
            if(Type == "error")
            {
                Console.WriteLine("ERROR - " + message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
