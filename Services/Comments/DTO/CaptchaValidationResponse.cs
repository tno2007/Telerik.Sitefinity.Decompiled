// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CaptchaValidationResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CaptchaValidationResponse</c> Used to provide validation information.
  /// </summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Captcha.DTO.CaptchaValidationResponse instead")]
  public class CaptchaValidationResponse
  {
    /// <summary>
    /// Gets or sets a value indicating whether captcha is valid.
    /// </summary>
    /// <value>The is valid value.</value>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether captcha needs to be refreshed.
    /// </summary>
    /// <value>The refresh captcha value.</value>
    public bool RefreshCaptcha { get; set; }
  }
}
