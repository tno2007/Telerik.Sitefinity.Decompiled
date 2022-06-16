// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.CustomRequestValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Util;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Tries to validate request if it is a SignInResponseMessage.
  /// </summary>
  public class CustomRequestValidator : RequestValidator
  {
    protected override bool IsValidRequestString(
      HttpContext context,
      string value,
      RequestValidationSource requestValidationSource,
      string collectionKey,
      out int validationFailureIndex)
    {
      validationFailureIndex = 0;
      return context.Request.RawUrl.StartsWith("/Ecommerce/offsite-payment-notification/") || context.Request.RawUrl.StartsWith("/Ecommerce/offsite-payment-return/") || base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
    }
  }
}
