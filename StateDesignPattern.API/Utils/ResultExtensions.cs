using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StateDesignPattern.API.Utils
{
  public static class ResultExtensions
  {
    public static ActionResult Envelope(this Result result)
    {
      if (result.IsFailure)
        return new BadRequestObjectResult(result.Error);

      return new OkResult();
    }

    public static ActionResult Envelope<T>(this Result<T> result)
    {
      if (result.IsFailure)
        return new BadRequestObjectResult(result.Error);

      if (typeof(T) == typeof(string))
        return new OkObjectResult(JsonConvert.SerializeObject(result.Value));

      return new OkObjectResult(result.Value);
    }
  }
}