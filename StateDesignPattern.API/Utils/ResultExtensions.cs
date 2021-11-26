using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

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

    public static ActionResult<T> Envelope<T>(this Result<T> result)
    {
      if (result.IsFailure)
        return new BadRequestObjectResult(result.Error);

      return new OkObjectResult(result.Value);
    }
  }
}