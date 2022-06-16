// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PageVersionHistoryDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  internal class PageVersionHistoryDialog : VersionHistoryDialog
  {
    private static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Versioning.VersionHistoryDialog.ascx");

    protected HtmlGenericControl ItemTitleControl => this.Container.GetControl<HtmlGenericControl>("dialogTitle", true);

    protected HtmlAnchor BackButtonLink => this.Container.GetControl<HtmlAnchor>("backButtonAnchor", true);

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.InitializeGrid();
    }

    public override string LayoutTemplatePath
    {
      get => PageVersionHistoryDialog.TemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    private void InitializeGrid()
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      if (!(this.ItemId != Guid.Empty))
        return;
      this.HfIsPages.Value = true.ToString().ToLower();
      string str = HttpUtility.HtmlEncode(queryString["itemTitle"] ?? string.Empty);
      this.ItemTitleControl.InnerHtml = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", (object) Res.Get<VersionResources>().RevisionHistoryLabel, (object) str);
    }

    protected override void InitializeVersionPreviewDialog(object item)
    {
      Telerik.Web.UI.RadWindow control = new Telerik.Web.UI.RadWindow();
      control.ID = "versionPreview";
      control.NavigateUrl = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "/Sitefinity/Dialog/PageViewVersionDialog?moduleName={0}&typeName={1}&title=Templates&IsFromEditor=true&IsTemplate=false", (object) "Pages", (object) typeof (PageData).FullName);
      this.WindowManager.Windows.Add(control);
      this.HostedInRadWindow = false;
    }
  }
}
