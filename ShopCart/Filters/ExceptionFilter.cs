using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ShopCart.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) {}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var exception = context.Exception as Core.ServerException;
            if (exception != null)
            {
                var contentTypes = new MediaTypeCollection();
                contentTypes.Add(new MediaTypeHeaderValue("application/json"));
                context.Result = new ObjectResult(JsonConvert.SerializeObject(exception.Payload))
                {
                    StatusCode = (int) exception.Status,
                    ContentTypes = contentTypes
                };
                context.ExceptionHandled = true;
            }
        }
    }
}