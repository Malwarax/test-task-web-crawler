using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WebCrawler.Logic;

namespace WebCrawler.ConsoleApplication
{
    public class UserInputReceiver
    {
        private ConsoleWrapper console;
        private InputValidator validator;

        public Uri WebsiteUrl { get; private set; }

        public UserInputReceiver(ConsoleWrapper console, InputValidator validator)
        {
            this.console = console;
            this.validator = validator;
        }

        public virtual bool GetUserInput()
        {
            console.WriteLine(@"Enter the website url (e.g. https://www.example.com/):");
            string websiteLink = console.ReadLine();
            var validationResult=validator.Validate(websiteLink, new UrlValidator(), new RedirectionValidator());
            
            if(!validationResult)
            {
                return false;
            }

            WebsiteUrl = new Uri(websiteLink);

            return true;
        }
    }
}
