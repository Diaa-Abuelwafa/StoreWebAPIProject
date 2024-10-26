using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using WebAPIStore.Helpers;

namespace WebAPIStore.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly IHostEnvironment Env;

        public ExceptionMiddleware(RequestDelegate Next, IHostEnvironment Env)
        {
            this.Next = Next;
            this.Env = Env;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                // If Exception Accour Through The Request Trip I Will Catch It And Handle It
                await Next(Context);
            }
            catch(Exception Ex)
            {
                // Make Full Response

                // Make A Head Of Response 
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //Make A Body Of Response
                if (Env.IsDevelopment())
                {
                    ApiExceptionErrorResponse ExceptionResponse = new ApiExceptionErrorResponse((int)HttpStatusCode.InternalServerError, Ex.Message, Ex.StackTrace);

                    // Serialize The Response
                    var ResponseAsJson = JsonSerializer.Serialize(ExceptionResponse);

                    await Context.Response.WriteAsync(ResponseAsJson);
                }
                else
                {
                    ApiErrorResponse ExceptionResponse = new ApiErrorResponse((int)HttpStatusCode.InternalServerError);

                    // Serialize The Response
                    var ResponseAsJson = JsonSerializer.Serialize(ExceptionResponse);

                    await Context.Response.WriteAsync(ResponseAsJson);
                }
            }
        }
    }
}
