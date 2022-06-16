// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebCategoryAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Attributes
{
  /// <summary>
  /// Overrides <see cref="T:System.ComponentModel.CategoryAttribute" /> so that
  /// it is localizable by Sitefinity
  /// </summary>
  public class WebCategoryAttribute : CategoryAttribute
  {
    private const string DefaultResourceId = "PublicControlsResources";
    private string keyId;
    private string classId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebCategoryAttribute" /> class.
    /// </summary>
    /// <param name="keyId">Resource key id.</param>
    public WebCategoryAttribute(string keyId)
      : this("PublicControlsResources", keyId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebCategoryAttribute" /> class.
    /// </summary>
    /// <param name="classId">Resource class ID</param>
    /// <param name="keyId">Resource key id for <paramref name="classId" />.</param>
    public WebCategoryAttribute(string classId, string keyId)
    {
      this.keyId = keyId;
      this.classId = classId;
    }

    /// <summary>
    /// Looks up the localized name of the specified category.
    /// </summary>
    /// <param name="value">The identifer for the category to look up.</param>
    /// <returns>
    /// The localized name of the category, or null if a localized name does not exist.
    /// </returns>
    protected override string GetLocalizedString(string value) => Res.Get(this.classId, this.keyId);
  }
}
