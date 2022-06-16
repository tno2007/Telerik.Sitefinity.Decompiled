// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Formatters.TextWebExceptionFormatter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Telerik.Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Abstractions.Formatters
{
  /// <summary>
  /// Exception formatter that adds additional information for web request.
  /// </summary>
  public class TextWebExceptionFormatter : TextExceptionFormatter
  {
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:Telerik.Sitefinity.Abstractions.Formatters.TextWebExceptionFormatter" /> using the specified
    /// <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Exception" />
    /// objects.
    /// </summary>
    /// <param name="writer">The stream to write formatting information to.</param>
    /// <param name="exception">The exception to format.</param>
    public TextWebExceptionFormatter(TextWriter writer, Exception exception)
      : base(writer, exception, Guid.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:Telerik.Sitefinity.Abstractions.Formatters.TextWebExceptionFormatter" /> using the specified
    /// <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Exception" />
    /// objects.
    /// </summary>
    /// <param name="writer">The stream to write formatting information to.</param>
    /// <param name="exception">The exception to format.</param>
    /// <param name="handlingInstanceId">The id of the handling chain.</param>
    public TextWebExceptionFormatter(
      TextWriter writer,
      Exception exception,
      Guid handlingInstanceId)
      : base(writer, exception, handlingInstanceId)
    {
    }

    protected override void WriteAdditionalInfo(NameValueCollection additionalInformation)
    {
      if (HostingEnvironment.IsHosted)
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
          additionalInformation.Add("Requested URL", currentHttpContext.Request.Url.ToString());
      }
      base.WriteAdditionalInfo(additionalInformation);
    }
  }
}
