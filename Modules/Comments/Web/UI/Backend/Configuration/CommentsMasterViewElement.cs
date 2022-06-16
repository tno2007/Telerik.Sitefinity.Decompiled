// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsMasterViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration
{
  internal class CommentsMasterViewElement : 
    ViewDefinitionElement,
    ICommentsMasterViewDefinition,
    IViewDefinition,
    IDefinition
  {
    private List<IDialogDefinition> dialogs;
    private List<IDecisionScreenDefinition> decisionScreens;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.Configuration.CommentsMasterViewElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CommentsMasterViewElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new CommentsMasterViewDefinition((ConfigElement) this);

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    [ConfigurationProperty("toolbar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolbarDescription", Title = "ToolbarCaption")]
    public WidgetBarElement ToolbarConfig => (WidgetBarElement) this["toolbar"];

    /// <summary>Gets the sidebar.</summary>
    /// <value>The sidebar.</value>
    [ConfigurationProperty("sidebar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SidebarDescription", Title = "SidebarCaption")]
    public WidgetBarElement SidebarConfig => (WidgetBarElement) this["sidebar"];

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("dialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridDialogsDescription", Title = "BackendGridDialogsCaption")]
    public ConfigElementList<DialogElement> DialogsConfig => (ConfigElementList<DialogElement>) this["dialogs"];

    /// <summary>
    /// Gets the collection of decision screen config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("decisionScreens")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridDecisionScreensDescription", Title = "BackendGridDecisionScreensCaption")]
    public ConfigElementDictionary<string, DecisionScreenElement> DecisionScreensConfig => (ConfigElementDictionary<string, DecisionScreenElement>) this["decisionScreens"];

    public IWidgetBarDefinition Toolbar => (IWidgetBarDefinition) this.ToolbarConfig;

    public IWidgetBarDefinition Sidebar => (IWidgetBarDefinition) this.SidebarConfig;

    public List<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogs == null)
          this.dialogs = this.DialogsConfig.Elements.Select<DialogElement, IDialogDefinition>((Func<DialogElement, IDialogDefinition>) (d => (IDialogDefinition) d.ToDefinition())).ToList<IDialogDefinition>();
        return this.dialogs;
      }
    }

    public List<IDecisionScreenDefinition> DecisionScreens
    {
      get
      {
        if (this.decisionScreens == null)
          this.decisionScreens = this.DecisionScreensConfig.Elements.Select<DecisionScreenElement, IDecisionScreenDefinition>((Func<DecisionScreenElement, IDecisionScreenDefinition>) (d => (IDecisionScreenDefinition) d.ToDefinition())).ToList<IDecisionScreenDefinition>();
        return this.decisionScreens;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string toolbar = "toolbar";
      public const string sidebar = "sidebar";
      public const string dialogs = "dialogs";
      public const string decisionScreens = "decisionScreens";
    }
  }
}
