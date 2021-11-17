using AzureRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureRestApi.Utils
{
  public class ErrorDetails
  {
    public int Code { get; set; }

    public string Message { get; set; }
  }

  public class ErrorObjectResult : ObjectResult
  {
    public ErrorObjectResult(ErrorDetails value)
      : base(value)
    {
      StatusCode = value.Code;
    }
  }


  class ResponseUtility
  {
    private static readonly Type ServiceExcType = typeof(ServiceException);

    public static ObjectResult CreateErrorResult(Exception exception)
    {
      int code = 500;

      if (exception is ServiceException serviceException)
      {
        code = serviceException.Code;
      }

      var errorDetails = new ErrorDetails()
      {
        Code = code,
        Message = exception.Message
      };

      return new ErrorObjectResult(errorDetails);
    }
  }
}
