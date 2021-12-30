using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using StateDesignPattern.Logic.Interfaces;

namespace StateDesignPattern.API.Utils;

public static class ResultExtensions
{
  private static ActionResult ConvertToActionResult(this string error) =>
    Enum.TryParse(error, out HttpStatusCode statusCode)
      ? new StatusCodeResult((int) statusCode)
      : new BadRequestObjectResult(error);

  public static ActionResult EnvelopeAsOk(this Result result) =>
    result.IsSuccess
      ? new OkResult()
      : result.Error.ConvertToActionResult();

  public static ActionResult EnvelopeAsOk<T>(this Result<T> result) =>
    result.IsSuccess
      ? new OkResult()
      : result.Error.ConvertToActionResult();

  public static ActionResult<T> EnvelopeAsOkObject<T>(this Result<T> result) =>
    result.IsSuccess
      ? new OkObjectResult(result.Value)
      : result.Error.ConvertToActionResult();

  public static ActionResult<T> EnvelopeAsCreated<T>(this Result<T> result)
    where T : IHasGuid =>
    result.IsSuccess
      ? new CreatedResult(result.Value.Id.ToString(), result.Value)
      : result.Error.ConvertToActionResult();
  
  public static ActionResult EnvelopeWithRoute<T>(this Result<T> result, string targetRouteName)
    where T : IHasGuid =>
    result.IsSuccess
      ? new CreatedAtRouteResult(targetRouteName, new { id = result.Value.Id }, null)
      : result.Error.ConvertToActionResult();

  public static async Task<ActionResult> EnvelopeWithRoute<T>(this Task<Result<T>> resultTask, string targetRouteName)
    where T : IHasGuid =>
    (await resultTask).EnvelopeWithRoute(targetRouteName);

  public static ActionResult EnvelopeWithPdf(this Result<(byte[] File, string Name)> result) =>
    result.IsSuccess
      ? new FileContentResult(result.Value.File, MediaTypeNames.Application.Pdf)
        { FileDownloadName = result.Value.Name }
      : result.Error.ConvertToActionResult();

  public static async Task<ActionResult> EnvelopeWithPdf(this Task<Result<(byte[], string)>> resultTask) =>
    (await resultTask).EnvelopeWithPdf();
}