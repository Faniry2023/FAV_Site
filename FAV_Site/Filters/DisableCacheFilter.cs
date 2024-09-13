using Microsoft.AspNetCore.Mvc.Filters;

namespace FAV_Site.Filters
{
    public class DisableCacheFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context){}

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            context.HttpContext.Response.Headers["Expires"] = "O";
        }
    }
}
