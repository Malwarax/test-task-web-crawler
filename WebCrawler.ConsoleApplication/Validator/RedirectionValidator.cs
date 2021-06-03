﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebCrawler.ConsoleApplication
{
    public class RedirectionValidator
    {
        private readonly ConsoleWrapper _console;

        public RedirectionValidator(ConsoleWrapper console)
        {
            _console = console;
        }

        public virtual bool CheckRedirection(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            request.AllowAutoRedirect = false;
            bool result = true;

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                }
            }
            catch (WebException ex)
            {
                if(ex.Message.Contains("301"))
                {
                    _console.WriteLine("Error. The server is redirecting the request for this url.");
                }
                else
                {
                    _console.WriteLine("Error. No connection could be made.");
                }

                result = false;
            }

            return result;
        }
    }
}
