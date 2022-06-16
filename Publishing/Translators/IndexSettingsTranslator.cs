// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.IndexSettingsTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>
  /// Used to keep search index settings , for indexing outbound pipes
  /// </summary>
  public class IndexSettingsTranslator : TranslatorBase
  {
    public const string TranslatorName = "IndexSettingsTranslator";

    public override string Name => nameof (IndexSettingsTranslator);

    public override object Translate(
      object[] valuesToTranslate,
      IDictionary<string, string> translationSettings)
    {
      return (object) valuesToTranslate;
    }

    public override Dictionary<string, string> GetDefaultSettings() => base.GetDefaultSettings();
  }
}
