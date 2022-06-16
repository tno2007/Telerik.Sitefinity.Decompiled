// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewEditDialogStandalone
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Utils;
using Telerik.Sitefinity.Modules.Utils;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Dialog for editing all data items that inherit <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" /> abstract type as a standalone page.
  /// </summary>
  public class ContentViewEditDialogStandalone : AjaxDialogBase
  {
    private static readonly string ContentViewEditDialogTemplateStandalone = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.ContentViewDialogStandalone.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentViewEditDialogStandalone.ContentViewEditDialogTemplateStandalone : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField DetailsViewUrl => this.Container.GetControl<HiddenField>("detailsViewUrl", true);

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField ItemType => this.Container.GetControl<HiddenField>("itemType", true);

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField Provider => this.Container.GetControl<HiddenField>("provider", true);

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField Culture => this.Container.GetControl<HiddenField>("culture", true);

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField ItemId => this.Container.GetControl<HiddenField>("itemId", true);

    /// <summary>
    /// Gets a reference to a hidden field to transfer values from code behind to client script
    /// </summary>
    protected internal virtual HiddenField RedirectUrl => this.Container.GetControl<HiddenField>("redirectUrl", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">Contains references to all controls within that control.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      string input = queryString["sf_item_id"];
      string str1 = queryString["sf_item_type"];
      string providerName = queryString["sf_provider"];
      string name = queryString["sf_culture"];
      string str2 = this.GetRedirectUrl(str1) + "?provider=" + providerName + "&lang=" + name;
      Type itemType = TypeResolutionService.ResolveType(str1);
      if (itemType == typeof (PageNode))
      {
        CultureInfo culture = name != null ? CultureInfo.GetCultureInfo(name) : (CultureInfo) null;
        PageNode pageNode = PageManager.GetManager(providerName).GetPageNode(Guid.Parse(input));
        string str3 = pageNode.GetBackendUrl("Edit", culture);
        if (name != null)
          str3 = str3 + "/" + name;
        string str4 = queryString["sf_site"];
        if (string.IsNullOrWhiteSpace(str4) && SystemManager.CurrentContext.IsMultisiteMode)
        {
          ISite siteBySiteMapRoot = SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId);
          if (siteBySiteMapRoot != null)
            str4 = siteBySiteMapRoot.Id.ToString();
        }
        if (!string.IsNullOrWhiteSpace(str4))
        {
          string str5 = str3.Contains<char>('?') ? "&" : "?";
          str3 = str3 + str5 + "sf_site" + "=" + str4;
        }
        SystemManager.CurrentHttpContext.Response.Redirect(str3);
        SystemManager.CurrentHttpContext.ApplicationInstance.CompleteRequest();
      }
      else
        this.DetailsViewUrl.Value = RelatedDataResponseHelper.GetDetailsViewUrl(itemType);
      this.ItemType.Value = str1;
      this.Provider.Value = providerName;
      this.Culture.Value = name;
      this.ItemId.Value = input;
      this.RedirectUrl.Value = str2;
    }

    private string GetRedirectUrl(string itemTypeFullName) => new LandingPageResolver().ResolveTypeLandingUrl(itemTypeFullName) ?? PagePathUtils.ResolveBackendHomePageUrl();

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
