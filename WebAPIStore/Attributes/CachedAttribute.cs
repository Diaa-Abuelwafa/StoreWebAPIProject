using DomainStore.Interfaces.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace WebAPIStore.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private int ExpireTimeInSeconds;
        public CachedAttribute(int ExpireTimeInSeconds)
        {
            this.ExpireTimeInSeconds = ExpireTimeInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string Key = GenerateCachKey(context.HttpContext.Request);

            var CachService = context.HttpContext.RequestServices.GetRequiredService<ICachService>();

            var Data = CachService.GetCachData(Key);

            if(Data is not null)
            {
                var ResponseCachData = new ContentResult()
                {
                    Content = Data,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = ResponseCachData;

                return;
            }

            var Response = await next.Invoke();

            if(Response.Result is OkObjectResult)
            {
                CachService.SetCachData(Key, Response.Result, TimeSpan.FromSeconds(ExpireTimeInSeconds));
            }
        }

        private string GenerateCachKey(HttpRequest Request)
        {
            StringBuilder Key = new StringBuilder();

            Key.Append(Request.Path); // api/controller

            foreach(var (key, value) in Request.Query.OrderBy(x => x.Key))
            {
                Key.Append($"|{Key},{value}");
            }

            return Key.ToString();
        }
    }
}
