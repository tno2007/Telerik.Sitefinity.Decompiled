// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.LanguageSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>Language selector field for selecting languages.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class LanguageSelectorField : SelectorFieldBase
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Workflow.UI.Scripts.LanguageSelectorField.js";

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Workflow.UI.Scripts.LanguageSelectorField.js", typeof (LanguageSelectorField).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.TitleLabel.Text = Res.Get<Labels>().Language;
      this.AllValuesLabel.Text = Res.Get<Labels>().AllLanguages;
      this.SelectedValuesLabel.Text = Res.Get<LocalizationResources>().SelectedLanuages;
      this.SelectorDialog.NavigateUrl = "~/Sitefinity/Dialog/WorkflowLanguageSelectorDialog";
    }
  }
}
