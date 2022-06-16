// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Captcha.CaptchaUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Captcha.DTO;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Services.Captcha
{
  internal class CaptchaUtilities
  {
    /// <summary>Sets the captcha response.</summary>
    /// <param name="response">The response.</param>
    /// <param name="captchText">The captcha text.</param>
    internal static void SetCaptchaResponse(CaptchaResponse response, string captchText)
    {
      CaptchaInfo captcha = CaptchaUtilities.GenerateCaptcha(captchText);
      CaptchaUtilities.SaveCaptchaInTempStorage(captcha);
      response.Key = captcha.Key;
    }

    internal static CaptchaInfo GetCaptchaFromTempStorage(string key)
    {
      JsonTypeConverter<CaptchaInfo> jsonTypeConverter = new JsonTypeConverter<CaptchaInfo>();
      string text = ObjectFactory.Resolve<ITemporaryStorage>().Get(key);
      return text != null ? jsonTypeConverter.ConvertFromInvariantString(text) as CaptchaInfo : (CaptchaInfo) null;
    }

    private static CaptchaInfo GenerateCaptcha(string captchaText)
    {
      CaptchaInfo captcha = new CaptchaInfo();
      Guid guid = Guid.NewGuid();
      string data = guid.ToString();
      byte[] initialVector = (byte[]) null;
      captcha.CorrectAnswer = Convert.ToBase64String(SecurityUtility.GetKeyedHash(captchaText, guid.ToByteArray()));
      captcha.Key = Convert.ToBase64String(SecurityUtility.Encrypt(data, ref initialVector));
      captcha.InitializationVector = Convert.ToBase64String(initialVector);
      return captcha;
    }

    private static void SaveCaptchaInTempStorage(CaptchaInfo captcha)
    {
      JsonTypeConverter<CaptchaInfo> jsonTypeConverter = new JsonTypeConverter<CaptchaInfo>();
      ObjectFactory.Resolve<ITemporaryStorage>().AddOrUpdate(captcha.Key, jsonTypeConverter.ConvertToInvariantString((object) captcha), DateTime.UtcNow.AddMinutes(10.0));
    }
  }
}
