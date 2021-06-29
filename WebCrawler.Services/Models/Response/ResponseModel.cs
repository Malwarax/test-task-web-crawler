namespace WebCrawler.Services.Models.Response
{
    public class ResponseModel
    {
        public object Result { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public string Errors { get; set; }
    }
}
