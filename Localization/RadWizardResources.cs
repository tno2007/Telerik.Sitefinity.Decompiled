// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadWizard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadWizardMainDescription", Name = "RadWizard", ResourceClassId = "RadWizard", Title = "RadWizardMainTitle", TitlePlural = "RadWizardMainTitlePlural")]
  public sealed class RadWizard : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadWizard" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadWizard()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadWizard" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadWizard(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadWizard Main</summary>
    [ResourceEntry("RadWizardMainTitle", Description = "The title of this class.", LastModified = "2014/12/25", Value = "RadWizard Main")]
    public string RadWizardMainTitle => this[nameof (RadWizardMainTitle)];

    /// <summary>RadImageEditor Main</summary>
    [ResourceEntry("RadWizardMainTitlePlural", Description = "The title plural of this class.", LastModified = "2014/12/15", Value = "RadWizardMainTitlePlural ")]
    public string RadWizardMainTitlePlural => this[nameof (RadWizardMainTitlePlural)];

    /// <summary>Resource strings for RadImageEditor.</summary>
    [ResourceEntry("RadWizardMainDescription", Description = "The description of this class.", LastModified = "2014/12/15", Value = "Resource strings for RadWizard.")]
    public string RadWizardMainDescription => this[nameof (RadWizardMainDescription)];

    [ResourceEntry("Cancel", Description = "RadWizard resource strings.", LastModified = "2014/12/15", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("Finish", Description = "RadWizard resource strings.", LastModified = "2014/12/15", Value = "Finish")]
    public string Finish => this[nameof (Finish)];

    [ResourceEntry("Next", Description = "RadWizard resource strings.", LastModified = "2014/12/15", Value = "Next")]
    public string Next => this[nameof (Next)];

    [ResourceEntry("Previous", Description = "RadWizard resource strings.", LastModified = "2014/12/15", Value = "Previous")]
    public string Previous => this[nameof (Previous)];
  }
}
