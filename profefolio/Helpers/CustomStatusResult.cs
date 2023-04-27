using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace profefolio.Helpers
{
    public class CustomStatusResult<T> : ActionResult
    {
    private readonly int _statusCode;
    private readonly T? _body;

    public CustomStatusResult(int statusCode, T? body)
    {
        _statusCode = statusCode;
        _body = body;
    }

    public override async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_body);
        objectResult.StatusCode = _statusCode;
        await objectResult.ExecuteResultAsync(context);
    }
        
    }


}