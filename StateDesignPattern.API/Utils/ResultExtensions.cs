using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace StateDesignPattern.API.Utils
{
  public static class ResultExtensions
  {
    public static ActionResult Envelope(this Result result) =>
      result.IsSuccess
        ? new OkResult()
        : new BadRequestObjectResult(result.Error);

    public static ActionResult Envelope<T>(this Result<T> result) =>
      result.IsSuccess
        ? new OkResult()
        : new BadRequestObjectResult(result.Error);

    public static ActionResult<T> EnvelopeWithObject<T>(this Result<T> result) =>
      result.IsSuccess
        ? new OkObjectResult(result.Value)
        : new BadRequestObjectResult(result.Error);
  }
}