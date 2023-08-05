using System.Net;

namespace NzWalks.Api.MiddleWare
{
    public class ExceptionHendlerMiddleware
    {
        private readonly ILogger<ExceptionHendlerMiddleware> logger;
        private readonly RequestDelegate requestDelegate;

        public ExceptionHendlerMiddleware(ILogger<ExceptionHendlerMiddleware> logger,RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await requestDelegate(httpContent);

            }catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);

                httpContent.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContent.Response.ContentType = "application/json";

                await httpContent.Response.WriteAsJsonAsync(ex.Message);
            }
        }

    }
}
