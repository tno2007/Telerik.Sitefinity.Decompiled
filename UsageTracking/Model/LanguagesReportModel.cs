// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.LanguagesReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class LanguagesReportModel
  {
    public LanguagesReportModel()
    {
    }

    public LanguagesReportModel(List<string> frontEndLanguages, List<string> backendLanguages)
    {
      this.FrontendLanguages = frontEndLanguages;
      this.BackendLanguages = backendLanguages;
    }

    public LanguagesReportModel(
      List<string> frontEndLanguages,
      List<string> backendLanguages,
      bool legacyMultilingual,
      bool languageFallback,
      string defaultSystemCulture)
      : this(frontEndLanguages, backendLanguages)
    {
      this.LegacyMultilingual = legacyMultilingual;
      this.LanguageFallback = languageFallback;
      this.DefaultSystemCulture = defaultSystemCulture;
    }

    public List<string> FrontendLanguages { get; set; }

    public List<string> BackendLanguages { get; set; }

    public bool LegacyMultilingual { get; set; }

    public bool LanguageFallback { get; set; }

    public string DefaultSystemCulture { get; set; }

    public int BackendLanguagesCount => this.BackendLanguages == null ? 0 : this.BackendLanguages.Count;

    public int FrontendLanguagesCount => this.FrontendLanguages == null ? 0 : this.FrontendLanguages.Count;
  }
}
