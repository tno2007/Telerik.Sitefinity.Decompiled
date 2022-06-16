// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.UserNameTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>Convert a list of user IDs to a list of user names</summary>
  public class UserNameTranslator : TranslatorBase
  {
    public const string TranslatorName = "usernametranslator";

    public override string Name => "usernametranslator";

    /// <summary>
    /// Convert a list of user IDs to a list user names, formatted according to configuration
    /// </summary>
    /// <param name="settings">Settings to use</param>
    /// <param name="userIDs">Array of user IDs</param>
    /// <returns>Array of user names</returns>
    public override object Translate(
      object[] userIDs,
      IDictionary<string, string> translationSettings)
    {
      return userIDs != null ? (object) string.Empty : (object) new string[0];
    }
  }
}
