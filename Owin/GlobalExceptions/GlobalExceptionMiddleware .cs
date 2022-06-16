// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.Extensions.GlobalExceptionMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Owin.Extensions
{
  internal class GlobalExceptionMiddleware : OwinMiddleware
  {
    private const string ConfigurationExceptionCode = "IDX10803";

    public GlobalExceptionMiddleware(OwinMiddleware next)
      : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
      GlobalExceptionMiddleware exceptionMiddleware = this;
      try
      {
        await exceptionMiddleware.Next.Invoke(context);
      }
      catch (Exception ex)
      {
        Exception message = ex;
        if (typeof (InvalidOperationException).Equals(ex.GetType()) && ex.Message.Contains("IDX10803"))
        {
          string additionalMessage = "Possible reason is wrong SSL configuration. Please ensure offloader settings and Sitefinity configurations are synchronized.";
          message = (Exception) new InvalidOperationException(exceptionMiddleware.GetEnhancedExceptionMessage(ex.Message, additionalMessage), ex.InnerException);
        }
        Log.Write((object) message, ConfigurationPolicy.ErrorLog);
        throw message;
      }
    }

    private string GetEnhancedExceptionMessage(string exceptionMessage, string additionalMessage)
    {
      string str = string.Join(" ", Enumerable.Repeat<string>(string.Join(string.Empty, Enumerable.Repeat<string>("=", 3)), 5));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine();
      stringBuilder.AppendLine(str);
      stringBuilder.AppendLine(exceptionMessage);
      stringBuilder.AppendLine(additionalMessage);
      stringBuilder.AppendLine(str);
      return stringBuilder.ToString();
    }
  }
}
