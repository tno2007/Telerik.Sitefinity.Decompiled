// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.BasicLanguagesColumnMarkupGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// An HTML markup generator that produces markup for a languages from basic settings column.
  /// </summary>
  public class BasicLanguagesColumnMarkupGenerator : LanguagesColumnMarkupGenerator
  {
    /// <inheritdoc />
    protected internal override List<CultureInfo> GetLanguages()
    {
      List<CultureInfo> languages = new List<CultureInfo>();
      if (this.LanguageSource == LanguageSource.Custom)
      {
        foreach (string availableCulture in (IEnumerable<string>) this.AvailableCultures)
        {
          CultureInfo cultureInfo = new CultureInfo(availableCulture);
          languages.Add(cultureInfo);
        }
      }
      else
      {
        IAppSettings appSettings = this.AppSettings;
        if (this.LanguageSource == LanguageSource.Backend)
          languages.AddRange((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages);
        else if (this.LanguageSource == LanguageSource.Frontend)
          languages.AddRange((IEnumerable<CultureInfo>) appSettings.DefinedFrontendLanguages);
      }
      return languages;
    }
  }
}
