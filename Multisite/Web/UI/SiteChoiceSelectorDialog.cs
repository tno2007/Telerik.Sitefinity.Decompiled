// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SiteChoiceSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite.Web.Services.ViewModel;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.CommonDialogs;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  internal class SiteChoiceSelectorDialog : FlatSelectorDialog
  {
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ItemSelector.DataMembers.Add(new DataMemberInfo()
      {
        Name = "Name",
        IsSearchField = true,
        HeaderText = "Site",
        ColumnTemplate = "<span>{{Name}}</span>"
      });
      this.ItemSelector.ServiceUrl = "~/Sitefinity/Services/Multisite/Multisite.svc/";
      this.ItemSelector.BindOnLoad = false;
      this.ItemSelector.DataKeyNames = "Id";
      this.ItemSelector.ItemType = typeof (SiteGridViewModel).FullName;
      this.TitleLabel.Text = Res.Get<MultisiteResources>().SelectSites;
    }
  }
}
