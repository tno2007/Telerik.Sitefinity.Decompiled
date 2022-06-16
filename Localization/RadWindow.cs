// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadWindowDescription", Name = "RadWindow", ResourceClassId = "RadWindow", Title = "RadWindowTitle", TitlePlural = "RadWindowTitle")]
  public sealed class RadWindow : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadWindow" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadWindow()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadWindow" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadWindow(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadWindow</summary>
    [ResourceEntry("RadWindowTitle", Description = "The title of this class.", LastModified = "2020/10/01", Value = "RadWindow")]
    public string RadWindowTitle => this[nameof (RadWindowTitle)];

    /// <summary>RadWindow</summary>
    [ResourceEntry("RadWindowTitlePlural", Description = "The title plural of this class.", LastModified = "2020/10/01", Value = "RadWindow")]
    public string RadWindowTitlePlural => this[nameof (RadWindowTitlePlural)];

    /// <summary>Resource strings for RadWindow dialog.</summary>
    [ResourceEntry("RadWindowDescription", Description = "The description of this class.", LastModified = "2020/10/01", Value = "Resource strings for RadWindow.")]
    public string RadWindowDescription => this[nameof (RadWindowDescription)];

    [ResourceEntry("Cancel", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("Close", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Close")]
    public string Close => this[nameof (Close)];

    [ResourceEntry("Maximize", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Maximize")]
    public string Maximize => this[nameof (Maximize)];

    [ResourceEntry("Minimize", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Minimize")]
    public string Minimize => this[nameof (Minimize)];

    [ResourceEntry("No", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "No")]
    public string No => this[nameof (No)];

    [ResourceEntry("OK", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "OK")]
    public string OK => this[nameof (OK)];

    [ResourceEntry("PinOff", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Pin off")]
    public string PinOff => this[nameof (PinOff)];

    [ResourceEntry("PinOn", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Pin on")]
    public string PinOn => this[nameof (PinOn)];

    [ResourceEntry("Reload", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Reload")]
    public string Reload => this[nameof (Reload)];

    [ResourceEntry("Restore", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Restore")]
    public string Restore => this[nameof (Restore)];

    [ResourceEntry("Yes", Description = "RadWindow resource strings.", LastModified = "2018/09/17", Value = "Yes")]
    public string Yes => this[nameof (Yes)];
  }
}
