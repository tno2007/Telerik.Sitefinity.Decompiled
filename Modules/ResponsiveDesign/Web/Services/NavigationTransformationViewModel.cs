// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.NavigationTransformationViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  [DataContract]
  public class NavigationTransformationViewModel
  {
    /// <summary>Gets or sets the transformation css classes.</summary>
    [DataMember]
    public string CssClasses { get; set; }

    /// <summary>Gets or sets the name of the transformation.</summary>
    [DataMember]
    public string TransformationName { get; set; }
  }
}
