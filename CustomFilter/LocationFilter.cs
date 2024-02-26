using Lab1.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class LocationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("Department", out var department))
        {
            var location = ((Department)department).Location;

            if (location == "EG" || location == "USA")
            {
                base.OnActionExecuting(context);
                return;
            }
        }

        context.Result = new ContentResult()
        {
            Content = "Please insert another location",
            StatusCode = 400
        };
    }
}
