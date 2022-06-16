// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CaptchaResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CaptchaResponse</c> Used to provide the information needed by the Captcha to render.
  /// </summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Captcha.DTO.CaptchaResponse instead")]
  public class CaptchaResponse
  {
    /// <summary>Gets or sets the image.</summary>
    /// <value>The image.</value>
    public string Image { get; set; }

    /// <summary>Gets or sets the correct answer.</summary>
    /// <value>The correct answer.</value>
    [Obsolete("This property is depricated as it is no longer used and is not assigned.")]
    public string CorrectAnswer { get; set; }

    /// <summary>Gets or sets the initialization vector.</summary>
    /// <value>The initialization vector.</value>
    [Obsolete("This property is depricated as it is no longer used and is not assigned.")]
    public string InitializationVector { get; set; }

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    public string Key { get; set; }

    /// <summary>Gets or sets the audio.</summary>
    /// <value>The audio.</value>
    public string Audio { get; set; }
  }
}
