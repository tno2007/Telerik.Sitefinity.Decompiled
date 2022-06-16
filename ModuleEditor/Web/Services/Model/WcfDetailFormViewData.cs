// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.WcfDetailFormViewData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// Data transfer object for detail form view data needed at WCF service.
  /// </summary>
  [DataContract]
  public class WcfDetailFormViewData
  {
    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Title { get; set; }
  }
}
