// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.LayoutTransformationViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>
  /// View model class which represents a single layout transformation.
  /// </summary>
  [DataContract]
  public class LayoutTransformationViewModel
  {
    /// <summary>Gets or sets the name of the original layout element.</summary>
    [DataMember]
    public string OriginalLayoutElementName { get; set; }

    /// <summary>Gets or sets the name of the alternat layout element.</summary>
    [DataMember]
    public string AlternatLayoutElementName { get; set; }
  }
}
