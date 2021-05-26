using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class ConsoleWrapper
    {
        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
