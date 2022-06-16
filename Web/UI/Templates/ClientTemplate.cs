// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents a template for client side data binding.</summary>
  [ToolboxItem(false)]
  public class ClientTemplate : Control
  {
    /// <summary>Gets or sets the name of the template.</summary>
    public string Name
    {
      get => (string) this.ViewState[nameof (Name)] ?? this.ID;
      set => this.ViewState[nameof (Name)] = (object) value;
    }
  }
}
