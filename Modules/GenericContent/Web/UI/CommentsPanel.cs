// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.CommentsPanel`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  [Obsolete("Comments are displayed as specified in CommentsModuleDefinitions. CommentsPanel is not used.")]
  public class CommentsPanel<THost> : ViewModeControl<THost>, ICommandPanel
    where THost : Control, IControlPanel, IGenericContentViewHost, IContentPanelUrlHost
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.CommentsPanel.ascx");

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      if (this.AuthorsRepeater != null)
      {
        this.AuthorsRepeater.DataSource = (object) UserProfileManager.GetManager().GetUserProfiles();
        this.AuthorsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.AuthorsRepeater_ItemDataBound);
        this.AuthorsRepeater.DataBind();
      }
      if (this.ItemsInThisModuleLink != null)
        this.ItemsInThisModuleLink.NavigateUrl = this.Host.GetBaseUrl();
      if (this.PermissionsInThisModuleLink != null)
        this.PermissionsInThisModuleLink.NavigateUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ModulePermissionsDialog/");
      if (this.PropertiesForThisModuleLink == null)
        return;
      this.PropertiesForThisModuleLink.NavigateUrl = "#";
    }

    /// <summary>
    /// Data binds <see cref="P:Telerik.Sitefinity.Modules.GenericContent.Web.UI.AuthorsRepeater" />
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Repeater data binding event argument</param>
    protected virtual void AuthorsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      UserProfile dataItem = (UserProfile) e.Item.DataItem;
      if (!(e.Item.FindControl("authorLink") is HyperLink control))
        return;
      string str = string.Format("{0} ({1})", (object) UserProfilesHelper.GetUserDisplayName(dataItem.User.Id), (object) dataItem.User.UserName);
      control.Text = str;
      control.NavigateUrl = "#";
    }

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsPanel<THost>.UiPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Name for the command panel.</summary>
    /// <value>Command panel name.</value>
    /// <remarks>
    /// Could be used as command panel identifier if there are more than one command
    /// panels for a module.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public new virtual string Name => "Comments";

    /// <summary>Title of the command panel.</summary>
    /// <value></value>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public new virtual string Title => Res.Get<ContentResources>().CommentsTitle;

    /// <summary>
    /// Reference to the control panel tied to the command panel instance.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public virtual IControlPanel ControlPanel
    {
      get => (IControlPanel) this.Host;
      set => this.Host = (THost) value;
    }

    /// <summary>
    /// Reference to the control in the template that displays a list of links to author's profiles
    /// </summary>
    /// <remarks>This control is optional.</remarks>
    protected virtual Repeater AuthorsRepeater => this.Container.GetControl<Repeater>("authorsRepeater", false);

    /// <summary>
    /// Reference to the control in the template that displays a link to the view that displays
    /// this module's items in a list
    /// </summary>
    /// <remarks>This control is optional.</remarks>
    protected virtual HyperLink ItemsInThisModuleLink => this.Container.GetControl<HyperLink>("itemsInThisModuleLink", false);

    /// <summary>
    /// Reference to the contorl in the template that displays a link to the permissions view for
    /// this module.
    /// </summary>
    /// <remarks>This control is optional</remarks>
    protected virtual HyperLink PermissionsInThisModuleLink => this.Container.GetControl<HyperLink>("permissionsInThisModuleLink", false);

    /// <summary>
    /// Reference to the control in the template that displays a link to the properties
    /// for this module
    /// </summary>
    /// <remarks>This control is optional.</remarks>
    protected virtual HyperLink PropertiesForThisModuleLink => this.Container.GetControl<HyperLink>("propertiesForThisModuleLink", false);
  }
}
