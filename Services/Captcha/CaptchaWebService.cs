// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Captcha.CaptchaWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Captcha.DTO;
using Telerik.Sitefinity.Web.Services;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Services.Captcha
{
  /// <summary>
  /// This class represents the rest service used from the Comments feature in Sitefinity.
  /// </summary>
  public class CaptchaWebService : Service
  {
    internal const string WebServiceUrl = "RestApi/captcha";

    /// <summary>Gets the captcha image and details.</summary>
    /// <param name="request">The request.</param>
    /// <returns>Captcha response</returns>
    public CaptchaResponse Get(CaptchaRequest request)
    {
      CaptchaResponse response = new CaptchaResponse();
      CaptchaImage captchaImage = new CaptchaImage();
      captchaImage.EnableCaptchaAudio = true;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        captchaImage.RenderImage().Save((Stream) memoryStream, ImageFormat.Png);
        response.Image = Convert.ToBase64String(memoryStream.ToArray());
      }
      using (MemoryStream audioMemoryStream = new MemoryStream())
      {
        MemoryStream waveStream = new CaptchaAudio(audioMemoryStream, captchaImage.Text).GetWaveStream(HostingEnvironment.MapPath(captchaImage.AudioFilesPath));
        response.Audio = Convert.ToBase64String(waveStream.ToArray());
      }
      CaptchaUtilities.SetCaptchaResponse(response, captchaImage.Text);
      ServiceUtility.DisableCache();
      return response;
    }

    /// <summary>Validates captcha image</summary>
    /// <param name="captchaInfo">The captcha info and answer</param>
    /// <returns>Weather answer is valid or not</returns>
    public CaptchaValidationResponse Post(CaptchaInfo captchaInfo) => this.Validate(captchaInfo.Key, captchaInfo.Answer);

    internal CaptchaValidationResponse Validate(string key, string answer)
    {
      CaptchaValidationResponse validationResponse = new CaptchaValidationResponse();
      if (key.IsNullOrWhitespace() || answer.IsNullOrWhitespace())
        return validationResponse;
      CaptchaInfo captchaFromTempStorage = CaptchaUtilities.GetCaptchaFromTempStorage(key);
      if (captchaFromTempStorage == null)
      {
        validationResponse.RefreshCaptcha = true;
        return validationResponse;
      }
      Guid guid = Guid.Parse(SecurityUtility.Decrypt(Convert.FromBase64String(captchaFromTempStorage.Key), Convert.FromBase64String(captchaFromTempStorage.InitializationVector)));
      string base64String = Convert.ToBase64String(SecurityUtility.GetKeyedHash(answer, guid.ToByteArray()));
      validationResponse.IsValid = base64String == captchaFromTempStorage.CorrectAnswer;
      return validationResponse;
    }
  }
}
