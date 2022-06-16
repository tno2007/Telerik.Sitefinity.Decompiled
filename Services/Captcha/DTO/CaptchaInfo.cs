// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Captcha.DTO.CaptchaInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Captcha.DTO
{
  /// <summary>
  /// <c>CaptchaInfo</c> Represents the Captcha information
  /// </summary>
  public class CaptchaInfo
  {
    /// <summary>
    /// Gets or sets the captcha answer.
    /// This is the captcha answer that user entered.
    /// </summary>
    /// <value>The captcha answer.</value>
    public string Answer { get; set; }

    /// <summary>
    /// Gets or sets the captcha correct answer.
    /// This is a system property that contains the hashed correct answer for the loaded captcha.
    /// </summary>
    /// <value>The captcha correct answer.</value>
    public string CorrectAnswer { get; set; }

    /// <summary>
    /// Gets or sets the captcha initialization vector.
    /// This is a system property that is used to validate that the entered captcha answer is correct.
    /// </summary>
    /// <value>The captcha initialization vector.</value>
    public string InitializationVector { get; set; }

    /// <summary>
    /// Gets or sets the captcha key.
    /// This is a system property that is used to validate that the entered captcha answer is correct.
    /// </summary>
    /// <value>The captcha key.</value>
    public string Key { get; set; }
  }
}
