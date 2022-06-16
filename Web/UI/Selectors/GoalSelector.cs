// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.GoalSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Control for selecting goals</summary>
  public class GoalSelector : KendoWindow
  {
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.GoalSelector.ascx");
    private const string ViewScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.GoalSelector.js";

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? GoalSelector.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("addGoalForm", true);

    /// <summary>Gets or sets the conversions service URL</summary>
    public string ConversionsServiceUrl { get; set; }

    /// <summary>Gets the goal next page control.</summary>
    /// <value>The goal next page control.</value>
    public PageField GoalNextPage => this.Container.GetControl<PageField>("goalNextPage", true);

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets a reference to the no conversions label control.</summary>
    protected virtual Label NoConversionsLbl => this.Container.GetControl<Label>("noConversionsLbl", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (string.IsNullOrWhiteSpace(this.ConversionsServiceUrl))
        throw new ArgumentNullException("ConversionsServiceUrl");
      base.InitializeControls(container);
      this.NoConversionsLbl.Text = Res.Get<Labels>().NoConversionAvailable.Arrange((object) Res.Get<Labels>().ExternalLinkNoConversionAvailable);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.GoalSelector.js", typeof (GoalSelector).Assembly.FullName)
    };

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().First<ScriptDescriptor>();
      string absolute = VirtualPathUtility.ToAbsolute(this.ConversionsServiceUrl);
      controlDescriptor.AddProperty("decConversionService", (object) absolute);
      controlDescriptor.AddComponentProperty("goalNextPage", this.GoalNextPage.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }
  }
}
