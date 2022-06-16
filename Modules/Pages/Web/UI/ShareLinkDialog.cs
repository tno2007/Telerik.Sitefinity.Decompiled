// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class ShareLinkDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.ShareLinkDialog.ascx");
    public const string scriptFileName = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.ShareLinkDialog.js";
    private Guid pageId;

    /// <summary>Gets or sets the page provider.</summary>
    /// <value>The page provider.</value>
    public virtual string PageProvider => (string) this.ViewState[nameof (PageProvider)] ?? Config.Get<PagesConfig>().DefaultProvider;

    /// <summary>Gets the page id.</summary>
    /// <value>The page id.</value>
    public Guid PageId
    {
      get
      {
        string g = SystemManager.CurrentHttpContext.Request.QueryString["pageId"];
        if (!string.IsNullOrEmpty(g))
          this.pageId = new Guid(g);
        return this.pageId;
      }
    }

    /// <summary>Gets the language.</summary>
    /// <value>The language.</value>
    public string Language => SystemManager.CurrentHttpContext.Request.QueryString["language"] ?? string.Empty;

    protected virtual HtmlControl CloseButton => this.Container.GetControl<HtmlControl>("closeButton", true);

    protected virtual Literal ExpirationDescription => this.Container.GetControl<Literal>("expirationDescription", true);

    protected virtual TextBox SharedLinkTextBox => this.Container.GetControl<TextBox>("txtSharedLink", true);

    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ShareLinkDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      double linkExpirationTime = Config.Get<PagesConfig>().SharedLinkExpirationTime;
      this.ExpirationDescription.Text = string.Format(Res.Get<PageResources>().ShareDialogLinkExpirationDescription, (object) ContentHelper.GetExpirationTimeCaptionInHours(linkExpirationTime));
      this.SharedLinkTextBox.Text = PageTempPreviewGenerator.GetPreviewUrl(this.PageId, this.Language, linkExpirationTime, this.PageProvider);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddElementProperty(this.CloseButton.ID, this.CloseButton.ClientID);
      controlDescriptor.AddProperty(this.SharedLinkTextBox.ID, (object) this.SharedLinkTextBox.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.ShareLinkDialog.js", typeof (ShareLinkDialog).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
