// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebDescriptionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Attributes
{
  /// <summary>
  /// Makes <see cref="T:System.ComponentModel.DescriptionAttribute" /> localizable by Sitefinity
  /// </summary>
  public class WebDescriptionAttribute : DescriptionAttribute
  {
    private const string DefaultResourceId = "PublicControlsResources";
    private bool translated;
    private string description;
    private string keyId;
    private string classId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebDescriptionAttribute" /> class.
    /// </summary>
    /// <param name="keyId">The key id.</param>
    public WebDescriptionAttribute(string keyId)
      : this("PublicControlsResources", keyId)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.Attributes.WebDescriptionAttribute" /> class.
    /// </summary>
    /// <param name="classId">The class id.</param>
    /// <param name="keyId">The key id.</param>
    public WebDescriptionAttribute(string classId, string keyId)
    {
      this.keyId = keyId;
      this.classId = classId;
    }

    /// <summary>Gets the description stored in this attribute.</summary>
    /// <value></value>
    /// <returns>The description stored in this attribute.</returns>
    public override string Description
    {
      get
      {
        if (!this.translated)
        {
          this.description = Res.Get(this.classId, this.keyId);
          this.translated = true;
        }
        return this.description;
      }
    }
  }
}
