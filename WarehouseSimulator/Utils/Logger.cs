using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseSimulator.Utils
{
    public static class Logger
    {
        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        { 
        Console.ForegroundColor = color;
            //Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
    
}
