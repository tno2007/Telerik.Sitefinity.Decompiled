// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IViewHostControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines the owner of the current instance.</summary>
  public interface IViewHostControl
  {
    /// <summary>Gets or sets the owner of the current instance.</summary>
    Control Host { get; set; }
  }
}
