// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.TemplateException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents an exception that occurred during template parsing.
  /// </summary>
  [Serializable]
  public class TemplateException : Exception
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.TemplateException" />.
    /// </summary>
    public TemplateException()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.TemplateException" /> and sets the provided message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TemplateException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.TemplateException" /> and sets the provided message and inner exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public TemplateException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes new excepton with the provided template name requierd type and ID.
    /// </summary>
    /// <param name="templateName">The name of the template.</param>
    /// <param name="requiredType">The type of the missing control.</param>
    /// <param name="pageId">ID of the missing control.</param>
    public TemplateException(string templateName, string requiredType, string id)
      : base(Res.Get<ErrorMessages>("RequiredControlNotFound", (object) templateName, (object) requiredType, (object) id))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.
    /// </summary>
    /// <param name="info">
    /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.
    /// </param>
    protected TemplateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
