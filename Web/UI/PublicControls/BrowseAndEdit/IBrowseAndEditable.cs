// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.IBrowseAndEditable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public interface IBrowseAndEditable
  {
    /// <summary>Gets the browse and edit toolbar.</summary>
    /// <value>The browse and edit toolbar.</value>
    BrowseAndEditToolbar BrowseAndEditToolbar { get; }

    /// <summary>
    /// Adds browse and edit commands to be executed by the toolbar
    /// </summary>
    /// <param name="commands">The commands.</param>
    void AddCommands(IList<BrowseAndEditCommand> commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }
  }
}
