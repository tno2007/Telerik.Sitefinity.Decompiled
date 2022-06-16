// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PageItemsBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Class that provides the UI for listing pages. Uses Ajax binding.
  /// </summary>
  public class PageItemsBuilder : SimpleScriptView
  {
    private bool enableReordering = true;
    private string customLayoutTemplateName;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.PageItemsBuilder.ascx");
    private const string scriptJs = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.PageItemsBuilder.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PageItemsBuilder" /> class.
    /// </summary>
    public PageItemsBuilder() => this.LayoutTemplatePath = PageItemsBuilder.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => this.CustomLayoutTemplateName != null ? this.CustomLayoutTemplateName : (string) null;

    /// <summary>
    /// Custom layout to use. Specify this in order to use your own layout.
    /// </summary>
    protected string CustomLayoutTemplateName
    {
      get => this.customLayoutTemplateName;
      set => this.customLayoutTemplateName = value;
    }

    public List<object> Items { get; set; }

    /// <summary>
    /// Specifies the minimum items in the list. If the items count is equal to that count, the delete operation is not possible.
    /// </summary>
    public int MinItemsCount { get; set; }

    /// <summary>
    /// Specifies whether drag and drop reordering is enabled.
    /// </summary>
    public bool EnableReordering
    {
      get => this.enableReordering;
      set => this.enableReordering = value;
    }

    /// <summary>Represents the title of the list</summary>
    public Label TitleLabel => this.Container.GetControl<Label>("title", true);

    /// <summary>Represents the container of the items</summary>
    protected HtmlGenericControl ItemsContainer => this.Container.GetControl<HtmlGenericControl>("itemsContainer", true);

    /// <summary>Represents a label used to show error messages.</summary>
    protected Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ErrorMessageLabel.Attributes.CssStyle.Add("display", "none");

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQueryUI);
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
      controlDescriptor.AddElementProperty("itemsContainer", this.ItemsContainer.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddProperty("minItemsCount", (object) this.MinItemsCount);
      controlDescriptor.AddProperty("enableReordering", (object) this.EnableReordering);
      if (this.Items != null && this.Items.Count > 0)
        controlDescriptor.AddProperty("choiceItems", (object) this.Items);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.PageItemsBuilder.js", this.GetType().Assembly.FullName)
    };
  }
}
