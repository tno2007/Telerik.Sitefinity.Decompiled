// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>A control for sending test emails.</summary>
  public class EmailSettingsSendTestEmailDialog : SimpleScriptView
  {
    private static readonly string WebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Configuration/EmailSettings.svc/sendmail/");
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsSendTestEmailDialog.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.EmailSettingsSendTestEmailDialog.ascx");

    /// <summary>Gets the send test button</summary>
    protected HtmlControl SendTestButton => this.Container.GetControl<HtmlControl>("sendTestButton", true);

    /// <summary>Gets the cancel button</summary>
    protected HtmlControl CancelButton => this.Container.GetControl<HtmlControl>("cancelButton", true);

    /// <summary>Gets the close button</summary>
    protected HtmlControl CloseButton => this.Container.GetControl<HtmlControl>("closeButton", true);

    /// <summary>Gets the second close button</summary>
    protected HtmlControl CloseButton2 => this.Container.GetControl<HtmlControl>("closeButton2", false);

    /// <summary>Gets email address</summary>
    protected TextField EmailAddress => this.Container.GetControl<TextField>("emailAddress", true);

    /// <summary>Gets the window body</summary>
    protected HtmlGenericControl WindowBody => this.Container.GetControl<HtmlGenericControl>("windowBody", true);

    /// <summary>Gets a Sender not verified label</summary>
    protected Label SenderNotVerifiedLabel => this.Container.GetControl<Label>("senderNotVerifiedLabel", true);

    /// <summary>
    /// Gets or sets a value indicating whether the Sender Email Text Field is Required
    /// </summary>
    public bool Required { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SenderNotVerifiedLabel.Visible = true;
      string str = RouteHelper.ResolveUrl("~/Sitefinity/Administration/Settings/Basic/EmailSettings", UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      this.SenderNotVerifiedLabel.Text = Res.Get<ConfigDescriptions>().EmailCannotBeSentDescription.Arrange((object) str);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (EmailSettingsSendTestEmailDialog).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("windowBody", this.WindowBody.ClientID);
      controlDescriptor.AddElementProperty("sendTestButton", this.SendTestButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("closeButton", this.CloseButton.ClientID);
      controlDescriptor.AddElementProperty("closeButton2", this.CloseButton2.ClientID);
      controlDescriptor.AddComponentProperty("emailAddress", this.EmailAddress.ClientID);
      controlDescriptor.AddProperty("required", (object) this.Required);
      controlDescriptor.AddProperty("serviceUrl", (object) EmailSettingsSendTestEmailDialog.WebServiceUrl);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsSendTestEmailDialog.js", typeof (EmailSettingsSendTestEmailDialog).Assembly.FullName)
    };

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmailSettingsSendTestEmailDialog.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }
  }
}
