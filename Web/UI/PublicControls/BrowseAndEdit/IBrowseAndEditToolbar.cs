// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.IBrowseAndEditToolbar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public interface IBrowseAndEditToolbar
  {
    IDictionary<Control, BrowseAndEditCommand> ToolbarControls { get; }
  }
}
