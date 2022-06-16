// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Definitions.LocalizationWidgetBarSectionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Contracts;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Definitions
{
  /// <summary>
  /// A definition class for localization widget bar sections.
  /// </summary>
  public class LocalizationWidgetBarSectionDefinition : 
    WidgetBarSectionDefinition,
    ILocalizationWidgetBarSectionDefinition,
    IWidgetBarSectionDefinition,
    IDefinition
  {
    private int? minLanguagesCountTreshold;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LocalizationWidgetBarSectionDefinition" /> class.
    /// </summary>
    public LocalizationWidgetBarSectionDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Definitions.LocalizationWidgetBarSectionDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LocalizationWidgetBarSectionDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public LocalizationWidgetBarSectionDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the minimum count of languages in order this section to be displayd.
    /// </summary>
    public int? MinLanguagesCountTreshold
    {
      get => this.ResolveProperty<int?>(nameof (MinLanguagesCountTreshold), this.minLanguagesCountTreshold);
      set => this.minLanguagesCountTreshold = value;
    }

    /// <summary>
    /// Gets or sets if the section will be initially visible.
    /// </summary>
    /// <value>The visible.</value>
    public override bool? Visible
    {
      get => base.Visible.HasValue && !base.Visible.Value ? base.Visible : new bool?(this.GetVisibleInternal());
      set => base.Visible = value;
    }

    private bool GetVisibleInternal()
    {
      bool visibleInternal = DataExtensions.AppSettings.ContextSettings.Multilingual;
      if (visibleInternal)
      {
        int? languagesCountTreshold = this.MinLanguagesCountTreshold;
        if (languagesCountTreshold.HasValue)
        {
          int length = DataExtensions.AppSettings.ContextSettings.DefinedFrontendLanguages.Length;
          languagesCountTreshold = this.MinLanguagesCountTreshold;
          int num = languagesCountTreshold.Value;
          if (length <= num)
            visibleInternal = false;
        }
      }
      return visibleInternal;
    }
  }
}
