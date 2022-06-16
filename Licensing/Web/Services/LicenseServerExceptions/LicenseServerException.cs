// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.LicenseServerException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions
{
  /// <summary>Sitefinity License server exception</summary>
  public class LicenseServerException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.LicenseServerException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public LicenseServerException(string message, int errorId, Exception innerException)
      : this(message, innerException)
    {
      this.ErrorId = errorId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.LicenseServerException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public LicenseServerException(string message)
      : this(message, (Exception) null)
    {
    }

    public int ErrorId { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.LicenseServerException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="InnerException">The inner exception.</param>
    public LicenseServerException(string message, Exception InnerException)
      : base(message, InnerException)
    {
      this.ErrorId = -1;
    }
  }
}
