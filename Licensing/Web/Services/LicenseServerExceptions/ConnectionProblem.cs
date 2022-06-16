// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.GeneralServerProblem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions
{
  /// <summary>Sitefinity connection problem exception</summary>
  public class GeneralServerProblem : LicenseServerException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Web.Services.LicenseServerExceptions.GeneralServerProblem" /> class.
    /// </summary>
    /// <param name="innerException">The inner exception.</param>
    public GeneralServerProblem(Exception innerException)
      : base(Res.Get<LicensingMessages>().GeneralServerError, innerException)
    {
    }
  }
}
