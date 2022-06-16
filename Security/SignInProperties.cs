// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SignInProperties
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;

namespace Telerik.Sitefinity.Security
{
  internal static class SignInProperties
  {
    public const string UserLoggingReason = "UserLoggingReason";

    public static AuthenticationProperties ForAlreadyLoggedUser(
      Telerik.Sitefinity.Security.UserLoggingReason reason,
      bool isPersistent = false)
    {
      return new AuthenticationProperties()
      {
        Dictionary = {
          ["UserLoggingReason"] = reason.ToString()
        },
        IsPersistent = isPersistent
      };
    }
  }
}
