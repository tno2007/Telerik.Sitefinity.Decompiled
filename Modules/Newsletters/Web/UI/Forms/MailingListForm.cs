// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>Form for creating new and editing existing lists.</summary>
  public class MailingListForm : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.MailingListForm.ascx");
    private const string webServiceUrl = "~/Sitefinity/Services/Newsletters/MailingList.svc";
    private const string clientManagerScript = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MailingListForm.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.MailingListForm.NewslettersManager" />.
    /// </summary>
    protected NewslettersManager NewslettersManager => NewslettersManager.GetManager();

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the hidden field holding the web service url
    /// </summary>
    protected virtual HiddenField WebServiceUrlHidden => this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the drop down list with subscribe templates.
    /// </summary>
    protected virtual DropDownList WelcomeTemplateDropDown => this.Container.GetControl<DropDownList>("welcomeTemplateDropDown", true);

    /// <summary>
    /// Gets the reference to the drop down list with unsubscribe templates.
    /// </summary>
    protected virtual DropDownList GoodByeTemplateDropDown => this.Container.GetControl<DropDownList>("goodByeTemplateDropDown", true);

    /// <summary>
    /// Gets the reference to the control that displays the message when no message templates
    /// are available.
    /// </summary>
    protected virtual ITextControl NoTemplatesMessageLiteral => this.Container.GetControl<ITextControl>("noTemplatesMessageLiteral", true);

    /// <summary>Gets the unsubscribe pages picker control.</summary>
    /// <value>The back links page picker.</value>
    protected PageField UnsubscribePagePicker => this.Container.GetControl<PageField>(nameof (UnsubscribePagePicker), true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.WebServiceUrlHidden.Value = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/MailingList.svc");
      this.BindTemplates(this.WelcomeTemplateDropDown);
      this.BindTemplates(this.GoodByeTemplateDropDown);
      this.UnsubscribePagePicker.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
      this.UnsubscribePagePicker.RootNodeID = SiteInitializer.CurrentFrontendRootNodeId;
      this.UnsubscribePagePicker.DisplayMode = FieldDisplayMode.Write;
      this.NoTemplatesMessageLiteral.Text = string.Format(Res.Get<NewslettersResources>().MailingListNoTemplatesMessage, (object) this.GetMessageTemplatesPageUrl());
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (MailingListForm).Assembly.FullName)
    };

    private void BindTemplates(DropDownList list)
    {
      list.Items.Clear();
      IQueryable<MessageBody> messageBodies = this.NewslettersManager.GetMessageBodies();
      Expression<Func<MessageBody, bool>> predicate = (Expression<Func<MessageBody, bool>>) (mb => mb.IsTemplate == true);
      foreach (MessageBody messageBody in (IEnumerable<MessageBody>) messageBodies.Where<MessageBody>(predicate))
        list.Items.Add(new ListItem(messageBody.Name, messageBody.Id.ToString()));
    }

    private string GetMessageTemplatesPageUrl() => VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(BackendSiteMap.FindSiteMapNode(NewslettersModule.templatesPageId, false).Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash));
  }
}
