using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebCrawler.Services.Models.Response;

namespace WebCrawler.WebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new ResponseModel { IsSuccessful = false, Errors = error.Message },new JsonSerializerOptions { PropertyNamingPolicy=JsonNamingPolicy.CamelCase});

                await response.WriteAsync(result);
            }
        }
    }
}