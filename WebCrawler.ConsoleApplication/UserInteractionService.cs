using System;
using System.Collections.Generic;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class UserInteractionService
    {
        private readonly LinksDifferencePrinter _linksDifferencePrinter;
        private readonly ResponsePrinter _responsePrinter;
        private readonly UrlValidator _urlValidator;
        private readonly RedirectionValidator _redirectionValidator;

        public UserInteractionService(LinksDifferencePrinter linksDifferencePrinter, ResponsePrinter responsePrinter, UrlValidator urlValidator, RedirectionValidator redirectionValidator)
        {
            _linksDifferencePrinter = linksDifferencePrinter;
            _responsePrinter = responsePrinter;
            _urlValidator = urlValidator;
            _redirectionValidator = redirectionValidator;
        }

        public Uri GetUserInput()
        {
            while(true)
            {
                Console.WriteLine(@"Enter the website url e.g. https://www.example.com/ (Enter to exit):");
                string url = Console.ReadLine();

                if(String.IsNullOrEmpty(url))
                {
                    Environment.Exit(0);
                }

                var urlValidatorResult = _urlValidator.CheckUrl(url);

                if (urlValidatorResult.Result == false)
                {
                    Console.WriteLine(urlValidatorResult.Message);
                    continue;
                }

                var redirectionValidatorResult = _redirectionValidator.CheckRedirection(url);

                if (redirectionValidatorResult.Result == false)
                {
                    Console.WriteLine(redirectionValidatorResult.Message);
                    continue;
                }

                return new Uri(url);
            }
        }

        public void PrintUrlsDifference(List<PerformanceResultDTO> result)
        {
            _linksDifferencePrinter.PrintDifference(result);
        }

        public void PrintPerformanceResultTable(List<PerformanceResultDTO> result)
        {
            _responsePrinter.PrintTable(result);
        }
    }
}
