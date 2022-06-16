// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.EmbeddedResourcePropertySetter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Resource entry that sets a targeted control's to the web resource pageId of an embedded resource
  /// </summary>
  public class EmbeddedResourcePropertySetter : ResourceFile
  {
    /// <summary>
    /// ID of the targeted control whose property we want to set
    /// </summary>
    public string ControlID { get; set; }

    /// <summary>Name of the targeted property</summary>
    public string ControlPropertyName { get; set; }
  }
}
