// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LabelsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Selectors;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>
  /// Represents View control for managing localizable resources.
  /// </summary>
  public class LabelsView : ViewModeControl<LocalizationPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.LabelsView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LabelsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the culture selector.</summary>
    /// <value>The culture selector.</value>
    protected virtual DropMenu CultureSelector => this.Container.GetControl<DropMenu>("cultureSelector", true);

    /// <summary>Gets the dialog manager.</summary>
    /// <value>The dialog manager.</value>
    protected virtual RadWindowManager DialogManager => this.Container.GetControl<RadWindowManager>("dialogManager", true);

    /// <summary>Gets the new resource window.</summary>
    /// <value>The new resource window.</value>
    protected virtual Telerik.Web.UI.RadWindow NewResourceWindow => this.DialogManager.Windows.Cast<Telerik.Web.UI.RadWindow>().AsQueryable<Telerik.Web.UI.RadWindow>().Single<Telerik.Web.UI.RadWindow>((Expression<Func<Telerik.Web.UI.RadWindow, bool>>) (w => w.ID == "newResource"));

    /// <summary>Gets the edit resource window.</summary>
    /// <value>The edit resource window.</value>
    protected virtual Telerik.Web.UI.RadWindow EditResourceWindow => this.DialogManager.Windows.Cast<Telerik.Web.UI.RadWindow>().AsQueryable<Telerik.Web.UI.RadWindow>().Single<Telerik.Web.UI.RadWindow>((Expression<Func<Telerik.Web.UI.RadWindow, bool>>) (w => w.ID == "editResource"));

    /// <summary>Gets the batch editor window.</summary>
    /// <value>The batch editor window.</value>
    protected virtual Telerik.Web.UI.RadWindow BatchEditorWindow => this.DialogManager.Windows.Cast<Telerik.Web.UI.RadWindow>().AsQueryable<Telerik.Web.UI.RadWindow>().Single<Telerik.Web.UI.RadWindow>((Expression<Func<Telerik.Web.UI.RadWindow, bool>>) (w => w.ID == "resourceBatchEditor"));

    /// <summary>Gets the resources binder.</summary>
    /// <value>The resources binder.</value>
    protected virtual RadGridBinder ResourcesBinder => this.Container.GetControl<RadGridBinder>("resourcesBinder", true);

    /// <summary>Gets the resources search box.</summary>
    /// <value>The resources search box.</value>
    protected virtual BinderSearchBox ResourcesSearchBox => this.Container.GetControl<BinderSearchBox>("resourcesSearchBox", true);

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based
    /// implementation to create any child controls they contain in preparation for
    /// posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      if (AppSettings.CurrentSettings.AllLanguages.Count > 1)
      {
        IEnumerable<CultureInfo> cultureInfos = AppSettings.CurrentSettings.AllLanguages.Select<KeyValuePair<int, CultureInfo>, CultureInfo>((Func<KeyValuePair<int, CultureInfo>, CultureInfo>) (k => k.Value));
        this.CultureSelector.SelectorItems.Add(CultureInfo.InvariantCulture.DisplayName, "invariant");
        foreach (CultureInfo cultureInfo in cultureInfos)
          this.CultureSelector.SelectorItems.Add(cultureInfo.DisplayName, cultureInfo.Name);
        this.CultureSelector.SelectedText = CultureInfo.InvariantCulture.DisplayName;
        this.CultureSelector.SetMenu();
        this.NewResourceWindow.NavigateUrl = "~/Sitefinity/Dialog/NewMultilingualResourceDialog";
        this.EditResourceWindow.NavigateUrl = "~/Sitefinity/Dialog/EditMultilingualResourceDialog";
        this.BatchEditorWindow.NavigateUrl = "~/Sitefinity/Dialog/MultilingualBatchEditorDialog";
      }
      else
      {
        this.CultureSelector.Visible = false;
        this.NewResourceWindow.NavigateUrl = "~/Sitefinity/Dialog/NewMonolingualResourceDialog";
        this.EditResourceWindow.NavigateUrl = "~/Sitefinity/Dialog/EditMonolingualResourceDialog";
        this.BatchEditorWindow.NavigateUrl = "~/Sitefinity/Dialog/MonolingualBatchEditorDialog";
      }
      this.ResourcesSearchBox.BinderClientId = this.ResourcesBinder.ClientID;
    }
  }
}
