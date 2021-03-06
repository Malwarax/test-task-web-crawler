using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class ResponsePrinter
    {
        public void PrintTable(List<PerformanceResultModel> resultList)
        {
            var table = new ConsoleTable("N", "Url", "Timing(ms)");

            for (int i = 1; i <= resultList.Count; i++)
            {
                table.AddRow(i, resultList[i - 1].Link, resultList[i - 1].ResponseTime.Milliseconds + " ms");
            }

            table.Options.EnableCount = false;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            table.Write();
        }
    }
}
