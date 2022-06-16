// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsMasterViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration
{
  internal class CommentsMasterViewDefinition : 
    ViewDefinition,
    ICommentsMasterViewDefinition,
    IViewDefinition,
    IDefinition
  {
    private IWidgetBarDefinition toolbar;
    private IWidgetBarDefinition sidebar;
    private List<IDialogDefinition> dialogs;
    private List<IDecisionScreenDefinition> decisionScreens;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsMasterViewDefinition" /> class.
    /// </summary>
    public CommentsMasterViewDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsMasterViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CommentsMasterViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>Gets the definitions to be display the toolbar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Toolbar => this.ResolveProperty<IWidgetBarDefinition>(nameof (Toolbar), this.toolbar);

    /// <summary>Gets the definitions to be display the sidebar.</summary>
    /// <value></value>
    public IWidgetBarDefinition Sidebar => this.ResolveProperty<IWidgetBarDefinition>(nameof (Sidebar), this.sidebar);

    /// <summary>
    /// Gets the collection of dialog definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogs == null)
          this.dialogs = ((CommentsMasterViewElement) this.ConfigDefinition).Dialogs.Select<IDialogDefinition, IDialogDefinition>((Func<IDialogDefinition, IDialogDefinition>) (c => (IDialogDefinition) c.GetDefinition())).ToList<IDialogDefinition>();
        return this.dialogs;
      }
      set => this.dialogs = value;
    }

    /// <summary>
    /// Gets the collection of decision screen definitions that are used on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IDecisionScreenDefinition> DecisionScreens
    {
      get => this.ResolveProperty<List<IDecisionScreenDefinition>>(nameof (DecisionScreens), this.decisionScreens);
      set => this.decisionScreens = value;
    }
  }
}
