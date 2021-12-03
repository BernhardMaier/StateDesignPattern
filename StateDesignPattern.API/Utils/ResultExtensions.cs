using System;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace StateDesignPattern.API.Utils
{
  public static class ResultExtensions
  {
    public static ActionResult EnvelopeAsOk(this Result result) =>
      result.IsSuccess
        ? new OkResult()
        : new BadRequestObjectResult(result.Error);

    public static ActionResult EnvelopeAsOk<T>(this Result<T> result) =>
      result.IsSuccess
        ? new OkResult()
        : new BadRequestObjectResult(result.Error);

    public static ActionResult<T> EnvelopeAsOkObject<T>(this Result<T> result) =>
      result.IsSuccess
        ? new OkObjectResult(result.Value)
        : new BadRequestObjectResult(result.Error);

    public static ActionResult<TResult> EnvelopeAsCreated<TResult>(this Result<TResult> result, Func<TResult, string> getIdFromT) =>
      result.IsSuccess
        ? new CreatedResult(getIdFromT(result.Value), result.Value)
        : new BadRequestObjectResult(result.Error);
  }
}