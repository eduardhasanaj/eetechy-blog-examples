using System;
using System.Collections.Generic;
using System.Text;

namespace AzureRestApi.Services
{
  class ServiceException : Exception
  {
    public int Code { get; private set; }

    
    public string Message { get; private set; }

    public ServiceException(int code, string message)
      : base(message)
    {
      Code = code;
      Message = message;
    }
  }
}
