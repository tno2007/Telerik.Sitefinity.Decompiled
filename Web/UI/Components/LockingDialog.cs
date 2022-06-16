// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Components.LockingDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.UI.Components
{
  /// <summary>
  /// 
  /// </summary>
  public class LockingDialog : AjaxDialogBase, ILockingControl
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Components.LockingDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Web.UI.Components.Scripts.LockingDialog.js";
    private HtmlAnchor viewButton;
    private HyperLink unlockButton;
    private HtmlAnchor closeButton;
    private HtmlGenericControl titleLabel;
    private HtmlGenericControl lockedByLabel;
    private HtmlAnchor backButton;
    private bool showReloadButton;

    /// <summary>
    /// Gets or sets a value indicating whether to process request paremeters.
    /// </summary>
    /// <value><c>true</c> to process request paremeters; otherwise, <c>false</c>.</value>
    public bool ProcessRequest { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LockingDialog.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the Back button text.</summary>
    /// <value>The Back button text.</value>
    public string BackButtonText { get; set; }

    /// <summary>Gets or sets the view URL.</summary>
    /// <value>The view URL.</value>
    public string ViewUrl { get; set; }

    /// <summary>Gets or sets the close URL.</summary>
    /// <value>The close URL.</value>
    public string CloseUrl { get; set; }

    /// <summary>Gets or sets the unlock URL.</summary>
    /// <value>The unlock URL.</value>
    public string UnlockUrl { get; set; }

    /// <summary>Gets or sets the unlock service URL.</summary>
    /// <value>The unlock service URL.</value>
    public string UnlockServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the GUID of the user that posseses the lock.
    /// </summary>
    /// <value>the GUID of the user that posseses the lock.</value>
    public string LockedBy { get; set; }

    /// <summary>
    /// Gets or sets the name of the user that posseses the lock.
    /// </summary>
    /// <value>the name of the user that posseses the lock.</value>
    public string LockedByUsername { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the Unlock button.
    /// </summary>
    /// <value><c>true</c> if Unlock button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowUnlockButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the View button.
    /// </summary>
    /// <value><c>true</c> if View button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowViewButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display the Close button.
    /// </summary>
    /// <value><c>true</c> if Close button must be displayed; otherwise, <c>false</c>.</value>
    public bool ShowCloseButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether use default button actions.
    /// </summary>
    /// <value><c>true</c> if default actions must be used; otherwise, <c>false</c>.</value>
    public bool UseDefaultActions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to try to close the window/dialog.
    /// </summary>
    /// <value><c>true</c> if to try to close the window/dialog; otherwise, <c>false</c>.</value>
    public bool TryToCloseWindow { get; set; }

    /// <summary>Gets or sets the name of the item.</summary>
    /// <value>The name of the item.</value>
    public string ItemName { get; set; }

    /// <summary>
    /// Determines whether to show option to refresh the current page.
    /// </summary>
    public bool ShowReloadButton
    {
      get => this.showReloadButton;
      set => this.showReloadButton = value;
    }

    /// <summary>Gets the LockedBy label.</summary>
    /// <value>The LockedBy label.</value>
    public virtual HtmlGenericControl LockedByLabel
    {
      get
      {
        if (this.lockedByLabel == null)
          this.lockedByLabel = this.Container.GetControl<HtmlGenericControl>("lockedByLabel", true);
        return this.lockedByLabel;
      }
    }

    /// <summary>Gets the title label.</summary>
    /// <value>The title label.</value>
    public virtual HtmlGenericControl TitleLabel
    {
      get
      {
        if (this.titleLabel == null)
          this.titleLabel = this.Container.GetControl<HtmlGenericControl>("titleLabel", true);
        return this.titleLabel;
      }
    }

    /// <summary>Gets the item name label.</summary>
    /// <value>The item name label.</value>
    public virtual HtmlGenericControl ItemNameLabel => this.Container.GetControl<HtmlGenericControl>("itemNameLabel", true);

    /// <summary>Gets the view button.</summary>
    /// <value>The view button.</value>
    public virtual HtmlAnchor ViewButton
    {
      get
      {
        if (this.viewButton == null)
          this.viewButton = this.Container.GetControl<HtmlAnchor>("viewButton", true);
        return this.viewButton;
      }
    }

    /// <summary>Gets the unlock button.</summary>
    /// <value>The unlock button.</value>
    public virtual HyperLink UnlockButton
    {
      get
      {
        if (this.unlockButton == null)
          this.unlockButton = this.Container.GetControl<HyperLink>("unlockButton", true);
        return this.unlockButton;
      }
    }

    /// <summary>Gets the close button.</summary>
    /// <value>The close button.</value>
    public virtual HtmlAnchor CloseButton
    {
      get
      {
        if (this.closeButton == null)
          this.closeButton = this.Container.GetControl<HtmlAnchor>("closeButton", true);
        return this.closeButton;
      }
    }

    /// <summary>Gets the back button.</summary>
    /// <value>The back button.</value>
    public virtual HtmlAnchor BackButton
    {
      get
      {
        if (this.backButton == null)
          this.backButton = this.Container.GetControl<HtmlAnchor>("backButton", true);
        return this.backButton;
      }
    }

    /// <summary>Gets the locked message label.</summary>
    /// <value>The locked message label.</value>
    public virtual HtmlGenericControl LockedMessageLabel => this.Container.GetControl<HtmlGenericControl>("lockedMessageLabel", true);

    /// <summary>Gets the deleted message label.</summary>
    /// <value>The deleted message label.</value>
    public virtual HtmlGenericControl DeletedMessageLabel => this.Container.GetControl<HtmlGenericControl>("deletedMessageLabel", true);

    /// <summary>Gets the link for refreshing the current page.</summary>
    /// <value>The link for refreshing the current page.</value>
    public virtual HyperLink LnkRefreshPage => this.Container.GetControl<HyperLink>("lnkRefreshPage", true);

    public LockingDialog()
    {
      this.ProcessRequest = true;
      this.ShowCloseButton = true;
      this.ShowViewButton = true;
      this.ShowUnlockButton = false;
      this.UseDefaultActions = true;
      this.TryToCloseWindow = true;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ProcessRequest)
      {
        System.Web.HttpRequest request = this.Page.Request;
        string requestParameter1 = this.GetRequestParameter("LockedBy");
        this.LockedBy = string.IsNullOrEmpty(requestParameter1) ? (string) null : requestParameter1;
        string requestParameter2 = this.GetRequestParameter("LockedByUsername");
        this.LockedByUsername = string.IsNullOrEmpty(requestParameter2) ? (string) null : requestParameter2;
        string requestParameter3 = this.GetRequestParameter("ViewUrl");
        if (!string.IsNullOrEmpty(requestParameter3))
        {
          this.ViewUrl = requestParameter3;
          this.ViewButton.HRef = requestParameter3;
        }
        string requestParameter4 = this.GetRequestParameter("CloseUrl");
        if (!string.IsNullOrEmpty(requestParameter4))
          this.CloseUrl = requestParameter4;
        string requestParameter5 = this.GetRequestParameter("UnlockUrl");
        if (!string.IsNullOrEmpty(requestParameter5))
          this.UnlockUrl = requestParameter5;
        string requestParameter6 = this.GetRequestParameter("Title");
        if (!string.IsNullOrEmpty(requestParameter6))
          this.Title = requestParameter6;
        string requestParameter7 = this.GetRequestParameter("ItemName");
        if (!string.IsNullOrEmpty(requestParameter7))
          this.ItemName = requestParameter7;
        string requestParameter8 = this.GetRequestParameter("UnlockServiceUrl");
        if (!string.IsNullOrEmpty(requestParameter8))
          this.UnlockServiceUrl = requestParameter8;
        bool result1;
        if (bool.TryParse(this.GetRequestParameter("ShowUnlock"), out result1))
          this.ShowUnlockButton = result1;
        bool result2;
        if (bool.TryParse(this.GetRequestParameter("ShowView"), out result2))
          this.ShowViewButton = result2;
        bool result3;
        if (bool.TryParse(this.GetRequestParameter("UseDefaultActions"), out result3))
          this.UseDefaultActions = result3;
      }
      if (!bool.TryParse(this.GetRequestParameter("ShowReloadButton"), out this.showReloadButton))
      {
        Guid result = Guid.Empty;
        this.showReloadButton = string.IsNullOrEmpty(this.LockedBy) || !Guid.TryParse(this.LockedBy, out result) || !(result != Guid.Empty) || !(result != SecurityManager.CurrentUserId);
      }
      else
        this.LnkRefreshPage.Visible = this.ShowReloadButton;
      this.CloseButton.Visible = this.ShowCloseButton;
      this.UnlockButton.Visible = this.ShowUnlockButton;
      this.ViewButton.Visible = this.ShowViewButton;
      this.TitleLabel.InnerHtml = this.Title;
      this.ItemNameLabel.InnerHtml = HttpUtility.HtmlEncode(this.ItemName);
      if (!string.IsNullOrEmpty(this.LockedBy))
      {
        this.LockedByLabel.InnerHtml = HttpUtility.HtmlEncode(UserProfilesHelper.GetUserDisplayName(new Guid(this.LockedBy)));
        this.DeletedMessageLabel.Visible = false;
        this.LockedMessageLabel.Visible = true;
      }
      else if (!string.IsNullOrEmpty(this.LockedByUsername))
      {
        this.LockedByLabel.InnerHtml = HttpUtility.HtmlEncode(this.LockedByUsername);
        this.DeletedMessageLabel.Visible = false;
        this.LockedMessageLabel.Visible = true;
      }
      else
      {
        this.LockedMessageLabel.Visible = false;
        this.DeletedMessageLabel.Visible = true;
      }
      this.UnlockButton.Attributes.Add("onclick", "return false;");
    }

    private string GetRequestParameter(string paramName) => this.GetRequestParameter(paramName, (string) null);

    private string GetRequestParameter(string paramName, string defaultValue)
    {
      string str = this.Page.Request.QueryString[paramName];
      return str != null ? str.ToString() : defaultValue;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = this.GetType().Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Components.Scripts.LockingDialog.js", fullName)
      };
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("lockedByLabelId", (object) this.LockedByLabel.ClientID);
      controlDescriptor.AddProperty("titleLabelId", (object) this.TitleLabel.ClientID);
      controlDescriptor.AddProperty("unlockButtonId", (object) this.UnlockButton.ClientID);
      controlDescriptor.AddProperty("viewButtonId", (object) this.ViewButton.ClientID);
      controlDescriptor.AddProperty("closeButtonId", (object) this.CloseButton.ClientID);
      controlDescriptor.AddProperty("_lnkRefreshPage", (object) this.LnkRefreshPage.ClientID);
      controlDescriptor.AddProperty("viewUrl", (object) this.ViewUrl);
      controlDescriptor.AddProperty("closeUrl", (object) this.CloseUrl);
      controlDescriptor.AddProperty("unlockUrl", (object) this.UnlockUrl);
      controlDescriptor.AddProperty("unlockServiceUrl", (object) this.UnlockServiceUrl);
      controlDescriptor.AddProperty("useDefaultActions", (object) this.UseDefaultActions);
      controlDescriptor.AddProperty("tryToCloseWindow", (object) this.TryToCloseWindow);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
