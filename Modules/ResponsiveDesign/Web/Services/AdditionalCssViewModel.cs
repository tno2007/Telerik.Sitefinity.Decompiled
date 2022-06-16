// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.AdditionalCssViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>
  /// View model class used to capture the additional css file settings.
  /// </summary>
  [DataContract]
  public class AdditionalCssViewModel
  {
    /// <summary>Gets or sets the path of the additional css file.</summary>
    [DataMember]
    public string CssFilePath { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the default theme
    /// should be inherited.
    /// </summary>
    [DataMember]
    public bool InheritDefaultTheme { get; set; }
  }
}
