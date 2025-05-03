using Shared.ErrorModels;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleware> logger;

        public CustomExceptionMiddleware(RequestDelegate Next,ILogger<CustomExceptionMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                //when catch error print something wrong and then change the response that will print
                //logger
                logger.LogError(ex, "Something Wrong!!!");

                //Set Status code for response
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                //set content type for response
                httpContext.Response.ContentType = "application/json";
                //response object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message, 
                };
                //return object as JSON
                var ResponseToReturn=JsonSerializer.Serialize(Response);

                await httpContext.Response.WriteAsync(ResponseToReturn);

            }

           

        }
    }
}
