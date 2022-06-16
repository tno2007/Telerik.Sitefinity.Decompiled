﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Translators.TaxonomyTitleTranslator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Translators
{
  /// <summary>
  /// Converts a list of taxonomy IDs into a list of taxonomy titles
  /// </summary>
  public class TaxonomyTitleTranslator : TranslatorBase
  {
    public const string TranslatorName = "taxonomytitletraslator";

    public override string Name => "taxonomytitletraslator";

    /// <summary>Convert a list of taxonomy IDs into a list of</summary>
    /// <param name="settings">Current instance sttings</param>
    /// <param name="taxonomyIDs">List of taxonomy IDs</param>
    /// <returns>List of taxonomy titles</returns>
    public override object Translate(
      object[] taxonomyIDs,
      IDictionary<string, string> translationSettings)
    {
      return taxonomyIDs != null ? (object) null : (object) new string[0];
    }
  }
}
