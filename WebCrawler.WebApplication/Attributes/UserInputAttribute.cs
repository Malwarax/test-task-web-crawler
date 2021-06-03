using System.ComponentModel.DataAnnotations;
using WebCrawler.Logic;
using WebCrawler.WebApplication.Models;

namespace WebCrawler.WebApplication.Attributes
{
    public class UserInputAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            var urlValidator = new UrlValidator();
            UserInputModel input = value as UserInputModel;
            var urlValidatorResult=urlValidator.CheckUrl(input.Url);

            if(urlValidatorResult.Result==false)
            {
                ErrorMessage = urlValidatorResult.Message;
                return false;
            }

            var redirectionValidator = new RedirectionValidator();
            var redirectionValidatorResult = redirectionValidator.CheckRedirection(input.Url);

            if (redirectionValidatorResult.Result == false)
            {
                ErrorMessage = redirectionValidatorResult.Message;
                return false;
            }

            return true;
        }
    }
}
