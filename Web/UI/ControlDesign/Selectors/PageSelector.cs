// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Control for selecting a page from the sitemap tree.
  /// the implemnetation uses treeview and preloads all pages
  /// </summary>
  public class PageSelector : SimpleScriptView
  {
    private const string pageSelectorScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PageSelector.js";
    public static readonly string pageSelectorPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.Selectors.PageSelector.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector" /> class.
    /// </summary>
    public PageSelector() => this.LayoutTemplatePath = PageSelector.pageSelectorPath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public bool CheckBoxes { get; set; }

    /// <summary>
    /// Event handler for the selection's end (when one of the 2 buttons is clicked)
    /// </summary>
    public string OnDoneClientSelection { get; set; }

    private RadTreeView SiteMapTreeView => this.Container.GetControl<RadTreeView>();

    private LinkButton SelectButton => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    private string UseCheckboxes => this.CheckBoxes.ToString().ToLower();

    private LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("siteMapTree", this.SiteMapTreeView.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.SelectButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddProperty("checkboxes", (object) this.UseCheckboxes);
      if (!string.IsNullOrEmpty(this.OnDoneClientSelection))
        controlDescriptor.AddEvent("doneClientSelection", this.OnDoneClientSelection);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PageSelector.js", this.GetType().Assembly.GetName().ToString())
    };

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      RadTreeView siteMapTreeView = this.SiteMapTreeView;
      siteMapTreeView.CheckBoxes = this.CheckBoxes;
      siteMapTreeView.DataBind();
      siteMapTreeView.ExpandAllNodes();
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
