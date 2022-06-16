// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowLanguageSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.CommonDialogs;

namespace Telerik.Sitefinity.Workflow.UI
{
  internal class WorkflowLanguageSelectorDialog : FlatSelectorDialog
  {
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.ItemSelector.DataMembers.Add(new DataMemberInfo()
      {
        Name = "DisplayName",
        IsSearchField = true,
        HeaderText = "Language",
        ColumnTemplate = "<span>{{DisplayName}}</span>"
      });
      this.ItemSelector.ServiceUrl = "~/Sitefinity/Services/Workflow/WorkflowDefinitionService.svc/lang-scope/";
      this.ItemSelector.BindOnLoad = false;
      this.ItemSelector.DataKeyNames = "ShortName";
      this.ItemSelector.ItemType = typeof (CultureViewModel).FullName;
      this.TitleLabel.Text = Res.Get<LocalizationResources>().SelectLanguage;
    }
  }
}
