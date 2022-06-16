// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails
{
  /// <summary>Control for selecting one or more thumbnail profile.</summary>
  public class ThumbnailProfileSelectorDialog : KendoWindow
  {
    private string thumbnialSettingsServiceUrl;
    private bool showButtonArea = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.ThumbnailProfileSelectorDialog.ascx");
    private const string defaultThumbnailSettingsServiceUrl = "~/Sitefinity/Services/ThumbnailService.svc/thumbnail-profiles/";
    public const string controlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileSelectorDialog.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ThumbnailProfileSelectorDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the cultures service URL.</summary>
    /// <value>The service URL.</value>
    public virtual string ThumbnailSettingsServiceUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.thumbnialSettingsServiceUrl))
          this.thumbnialSettingsServiceUrl = "~/Sitefinity/Services/ThumbnailService.svc/thumbnail-profiles/";
        return this.thumbnialSettingsServiceUrl;
      }
      set => this.thumbnialSettingsServiceUrl = value;
    }

    /// <summary>Allow or disallow multiple selection in the grid</summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done selecting and cancel buttons.
    /// </summary>
    public bool ShowButtonArea
    {
      get => this.showButtonArea;
      set => this.showButtonArea = value;
    }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    public virtual bool ShowSelectedFilter { get; set; }

    /// <summary>
    /// Gets or sets the type of search that should be performed
    /// </summary>
    public SearchTypes SearchType { get; set; }

    /// <summary>A flat selector for the retrieved items</summary>
    protected virtual FlatSelector ItemSelector => this.Container.GetControl<FlatSelector>("itemSelector", true);

    /// <summary>The title label</summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("lblTitle", true);

    /// <summary>The LinkButton for "Done"</summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("lnkDone", true);

    /// <summary>The LinkButton for "Cancel"</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>The button area control</summary>
    protected virtual Control ButtonArea => this.Container.GetControl<Control>("buttonAreaPanel", false);

    /// <summary>
    /// Gets a reference to the outer div containing the window content.
    /// </summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("profileSelectorWrapper", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> class in which the template was instantiated.
    /// </param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ItemSelector.ServiceUrl = this.ThumbnailSettingsServiceUrl;
      this.ItemSelector.AllowMultipleSelection = this.AllowMultipleSelection;
      this.ItemSelector.BindOnLoad = this.BindOnLoad;
      this.ButtonArea.Visible = this.ShowButtonArea;
      this.ItemSelector.DisableProvidersListing = true;
      this.ItemSelector.ShowSelectedFilter = this.ShowSelectedFilter;
      this.ItemSelector.SearchType = this.SearchType;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("lnkDone", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("lnkCancel", this.CancelButton.ClientID);
      controlDescriptor.AddComponentProperty("itemSelector", this.ItemSelector.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script
    /// resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ThumbnailProfileSelectorDialog).Assembly.FullName;
      ScriptReference scriptReference = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Scripts.ThumbnailProfileSelectorDialog.js"
      };
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        scriptReference
      };
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ItemSelector.ServiceUrl = this.thumbnialSettingsServiceUrl;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
