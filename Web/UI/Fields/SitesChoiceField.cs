// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.SitesChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>Sites Choice Field</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class SitesChoiceField : SelectorFieldBase
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.SitesChoiceField.js";

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.SitesChoiceField.js", typeof (SitesChoiceField).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.TitleLabel.Text = Res.Get<MultisiteResources>().Site;
      this.AllValuesLabel.Text = Res.Get<MultisiteResources>().SitesAll;
      this.SelectedValuesLabel.Text = Res.Get<MultisiteResources>().SelectedSitesNoDots;
      this.SelectorDialog.NavigateUrl = "~/Sitefinity/Dialog/SiteChoiceSelectorDialog";
    }
  }
}
