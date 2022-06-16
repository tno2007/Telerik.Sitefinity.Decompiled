// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  [NonVisualControl]
  [ParseChildren(true)]
  public class NavigationTemplate : WebControl, INamingContainer
  {
    private int? level;

    /// <summary>Gets or sets the template.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (NavigationNodeContainer))]
    public virtual ITemplate Template { get; set; }

    /// <summary>Gets or sets the selected template.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (NavigationNodeContainer))]
    public virtual ITemplate SelectedTemplate { get; set; }

    /// <summary>Gets or sets the level.</summary>
    public int? Level
    {
      get => this.level;
      set => this.level = value;
    }
  }
}
