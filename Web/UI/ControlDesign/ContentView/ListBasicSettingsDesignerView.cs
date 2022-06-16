// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ListBasicSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Represents the list view designer for hierarchical content types
  /// </summary>
  public class ListBasicSettingsDesignerView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.ListBasicSettingsDesignerView.ascx");
    internal const string listBasicSettingsDesignerViewScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ListBasicSettingsDesignerView.js";

    /// <summary>Gets or sets the paging text.</summary>
    /// <value>The paging text.</value>
    public string PagingText
    {
      get => this.PagingLabel.Text;
      set => this.PagingLabel.Text = value;
    }

    /// <summary>Gets or sets the divide pages text.</summary>
    /// <value>The divide pages text.</value>
    public string DividePagesText
    {
      get => this.DividePagesLabel.Text;
      set => this.DividePagesLabel.Text = value;
    }

    /// <summary>Gets or sets the total limit text.</summary>
    /// <value>The total limit text.</value>
    public string TotalLimitText
    {
      get => this.TotalLimitLabel.Text;
      set => this.TotalLimitLabel.Text = value;
    }

    /// <summary>Gets or sets the list limit text.</summary>
    /// <value>The list limit text.</value>
    public string ListLimitText
    {
      get => this.ListLimitLabel.Text;
      set => this.ListLimitLabel.Text = value;
    }

    /// <summary>Gets or sets the sort items text.</summary>
    /// <value>The sort items text.</value>
    public string SortItemsText { get; set; }

    /// <summary>Gets the sort expression control.</summary>
    public ChoiceField SortExpressionControl => this.Container.GetControl<ChoiceField>("sortExpression", true);

    /// <summary>
    /// The type name of the view used to display the designed control in master mode.
    /// </summary>
    public string DesignedMasterViewType { get; set; }

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "listViewSettings";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => "List Settings";

    /// <summary>Gets or sets the name of the current view.</summary>
    /// <value>The name of the current view.</value>
    public string CurrentViewName { get; set; }

    /// <summary>Gets the list limit control.</summary>
    protected TextField ListLimitControl => this.Container.GetControl<TextField>("listLimit", true);

    /// <summary>Gets the items per page panel.</summary>
    protected virtual Panel ItemsPerPageCustomFieldsPanel => this.Container.GetControl<Panel>("itemsPerPageCustomFields", true);

    /// <summary>Gets the label control that displays the paging text.</summary>
    protected Label PagingLabel => this.Container.GetControl<Label>("pagingLabel", true);

    /// <summary>
    /// Gets the label control that displays the divide pages text.
    /// </summary>
    protected Label DividePagesLabel => this.Container.GetControl<Label>("dividePagesLabel", true);

    /// <summary>
    /// Gets the label control that displays the total limit text.
    /// </summary>
    protected Label TotalLimitLabel => this.Container.GetControl<Label>("totalLimitLabel", true);

    /// <summary>
    /// Gets the label control that displays the list limit text.
    /// </summary>
    protected Label ListLimitLabel => this.Container.GetControl<Label>("listLimitLabel", true);

    /// <summary>
    /// Gets the label control that displays the sort items text.
    /// </summary>
    protected Literal SortItemsLabel => this.Container.GetControl<Literal>("sortLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.SortItemsLabel.Text = this.SortItemsText;

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ListBasicSettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddProperty("_currentViewName", (object) this.CurrentViewName);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.ListBasicSettingsDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
