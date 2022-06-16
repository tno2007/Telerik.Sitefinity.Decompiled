// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// An exception that is thrown when the image processing fails.
  /// </summary>
  public class ImageProcessingException : ApplicationException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException" /> class.
    /// </summary>
    /// <param name="error">The error.</param>
    public ImageProcessingException(ImageProcessingError error) => this.Error = error;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException" /> class.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <param name="message">The message.</param>
    public ImageProcessingException(ImageProcessingError error, string message)
      : base(message)
    {
      this.Error = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException" /> class.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner exception.</param>
    public ImageProcessingException(ImageProcessingError error, string message, Exception inner)
      : base(message, inner)
    {
      this.Error = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingException" /> class.
    /// </summary>
    /// <param name="info">The info.</param>
    /// <param name="context">The context.</param>
    protected ImageProcessingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>Gets or sets the error.</summary>
    /// <value>The error.</value>
    public ImageProcessingError Error { get; private set; }
  }
}
