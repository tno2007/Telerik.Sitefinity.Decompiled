// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.DynamicContentListSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.ControlDesign.ContentView
{
  public class DynamicContentListSettingsDesignerView : ListSettingsDesignerView
  {
    internal string DynamicContentMainShortTextFieldName { get; set; }

    protected override string ScriptDescriptorTypeName => typeof (ListSettingsDesignerView).FullName;

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.SortExpressionControl.Choices.Clear();
      this.SortExpressionControl.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<ContentResources>("LastPublishedOnTop"),
        Value = "PublicationDate DESC"
      });
      this.SortExpressionControl.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<ContentResources>("LastModifiedOnTop"),
        Value = "LastModified DESC"
      });
      this.SortExpressionControl.Choices.Add(new ChoiceItem()
      {
        Text = string.Format(Res.Get<ModuleBuilderResources>("ByMainShortTextFieldAsc"), (object) this.DynamicContentMainShortTextFieldName),
        Value = string.Format("{0} ASC", (object) this.DynamicContentMainShortTextFieldName)
      });
      this.SortExpressionControl.Choices.Add(new ChoiceItem()
      {
        Text = string.Format(Res.Get<ModuleBuilderResources>("ByMainShortTextFieldDesc"), (object) this.DynamicContentMainShortTextFieldName),
        Value = string.Format("{0} DESC", (object) this.DynamicContentMainShortTextFieldName)
      });
      this.SortExpressionControl.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<Labels>("AsSetInAdvancedMode"),
        Value = "custom"
      });
    }
  }
}
