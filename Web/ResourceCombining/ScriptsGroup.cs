// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceCombining.ScriptsGroup
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.ResourceCombining
{
  /// <summary>
  /// Represents a group of scripts that is to be combined for a set of pages
  /// </summary>
  public class ScriptsGroup
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.ResourceCombining.ScriptsGroup" /> class.
    /// </summary>
    /// <param name="scripts">The scripts.</param>
    public ScriptsGroup(IList<ScriptReference> scripts)
    {
      this.Scripts = (IEnumerable<ScriptReference>) scripts;
      this.PagesInGroup = new List<PageData>();
    }

    /// <summary>Gets or sets the scripts references in the group</summary>
    /// <value>The scripts.</value>
    public IEnumerable<ScriptReference> Scripts { get; set; }

    /// <summary>Gets or sets the pages in the group.</summary>
    /// <value>The pages in group.</value>
    public List<PageData> PagesInGroup { get; private set; }
  }
}
