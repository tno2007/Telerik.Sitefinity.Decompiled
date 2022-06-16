// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadLightBoxMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "ResourcesDescription", Name = "RadLightBoxMain", ResourceClassId = "RadLightBoxMain", Title = "ResourcesTitle", TitlePlural = "TitlePlural")]
  public sealed class RadLightBoxMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadLightBoxMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadLightBoxMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadLightBoxMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadLightBoxMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("ActiveImageAltText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "LightBox Active Image")]
    public string ActiveImageAltText => this[nameof (ActiveImageAltText)];

    [ResourceEntry("CloseButtonText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Close")]
    public string CloseButtonText => this[nameof (CloseButtonText)];

    [ResourceEntry("MaximizeButtonText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Maximize image")]
    public string MaximizeButtonText => this[nameof (MaximizeButtonText)];

    [ResourceEntry("NextButtonText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Next")]
    public string NextButtonText => this[nameof (NextButtonText)];

    [ResourceEntry("PagerFormatString", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Image {0} of {1}")]
    public string PagerFormatString => this[nameof (PagerFormatString)];

    [ResourceEntry("PrevButtonText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Prev")]
    public string PrevButtonText => this[nameof (PrevButtonText)];

    [ResourceEntry("ReservedResource", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("RestoreButtonText", Description = "RadLightBoxMain resource strings.", LastModified = "2016/06/28", Value = "Restore")]
    public string RestoreButtonText => this[nameof (RestoreButtonText)];
  }
}
