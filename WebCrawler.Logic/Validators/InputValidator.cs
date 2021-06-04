using System;
using System.Collections.Generic;
using System.Text;

namespace WebCrawler.Logic.Validators
{
    public class InputValidator
    {
        private readonly UrlValidator _urlValidator;
        private readonly RedirectionValidator _redirectionValidator;

        public InputValidator(UrlValidator urlValidator, RedirectionValidator redirectionValidator)
        {
            _urlValidator = urlValidator;
            _redirectionValidator = redirectionValidator;
        }

        public virtual bool InputParameters(string url, out string errors)
        {
            errors = "";
            var urlValidatorResult = _urlValidator.CheckUrl(url);

            if (urlValidatorResult.Result == false)
            {
                errors=urlValidatorResult.Message;
                return false;
            }

            var redirectionValidatorResult = _redirectionValidator.CheckRedirection(url);

            if (redirectionValidatorResult.Result == false)
            {
                errors = redirectionValidatorResult.Message;
                return false;
            }

            return true;
        }
    }
}
