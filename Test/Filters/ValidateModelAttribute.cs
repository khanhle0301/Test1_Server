using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Test.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                IEnumerable<ModelError> allErrors = context.ModelState.Values.SelectMany(v => v.Errors);
                context.Result = new BadRequestObjectResult(allErrors.First().ErrorMessage);
            }
        }
    }
}