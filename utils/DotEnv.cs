namespace GameForge
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static bool PGSQLConnStringLoad(string filePath,string connURIName)
        {
            if (!File.Exists(filePath))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("DotEnv: \".env\" File doesn't Exist");
                Console.BackgroundColor = ConsoleColor.Black;
                return false;
            }
            if(string.IsNullOrEmpty(connURIName)){
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("connURIName can't be empty");
                Console.BackgroundColor = ConsoleColor.Black;
                return false;
            }else if(!connURIName.ToCharArray().All(c=> char.IsLetterOrDigit(c))){
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ConnURIName can only contain letter, Digits or both");
                Console.BackgroundColor = ConsoleColor.Black;
                return false;
            }
            string connURI = "";
            foreach (var line in File.ReadAllLines(filePath))
            {
                connURI += line + "; ";
            }
            Environment.SetEnvironmentVariable(connURIName, connURI);
            return true;
        }
    }
}