// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Selectors.ExternalPagesSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.Selectors
{
  /// <summary>Control for selecting external pages.</summary>
  public class ExternalPagesSelector : SimpleScriptView
  {
    private bool allowMultiplePages = true;
    private bool showPageNameTextbox = true;
    private bool showOpenInNewWindowCheckbox;
    private const string selectorScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.ExternalPagesSelector.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.ExternalPagesSelector.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog.
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ExternalPagesSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a property of whether the selector allows one or many external pages.
    /// </summary>
    public virtual bool AllowMultiplePages
    {
      get => this.allowMultiplePages;
      set => this.allowMultiplePages = value;
    }

    /// <summary>
    /// Gets or sets a property of whether to show the Page name text box.
    /// </summary>
    public virtual bool ShowPageNameTextbox
    {
      get => this.showPageNameTextbox;
      set => this.showPageNameTextbox = value;
    }

    /// <summary>
    /// Gets or sets a property of whether the show the Open in a new window checkbox.
    /// </summary>
    public virtual bool ShowOpenInNewWindowCheckbox
    {
      get => this.showOpenInNewWindowCheckbox;
      set => this.showOpenInNewWindowCheckbox = value;
    }

    /// <summary>
    /// Gets the control used to show selected external pages.
    /// </summary>
    /// <value>The page selector.</value>
    protected virtual PageItemsBuilder ItemsBuilder => this.Container.GetControl<PageItemsBuilder>("itemsBuilder", true);

    /// <summary>Gets the Add url button wrapper span.</summary>
    protected virtual HtmlGenericControl AddButtonSpan => this.Container.GetControl<HtmlGenericControl>("addButtonSpan", true);

    /// <summary>Gets the Li element containing the Page name textbox.</summary>
    protected virtual HtmlGenericControl PageNameLi => this.Container.GetControl<HtmlGenericControl>("pageNameLi", true);

    /// <summary>
    /// Gets the Li element containing the Open in new window checkbox.
    /// </summary>
    protected virtual HtmlGenericControl OpenInNewWindowLi => this.Container.GetControl<HtmlGenericControl>("openInNewWindowLi", true);

    /// <summary>Gets a reference to the url text field.</summary>
    public virtual TextField UrlTextBox => this.Container.GetControl<TextField>("urlTextBox", true);

    /// <summary>
    /// Gets a reference to the open in new window choice field.
    /// </summary>
    public virtual ChoiceField OpenInNewWindowChoiceField => this.Container.GetControl<ChoiceField>("openInNewWindowChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.AllowMultiplePages)
        this.AddButtonSpan.Style["display"] = "none";
      if (!this.ShowPageNameTextbox)
        this.PageNameLi.Style["display"] = "none";
      if (this.ShowOpenInNewWindowCheckbox)
        return;
      this.OpenInNewWindowLi.Style["display"] = "none";
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ExternalPagesSelector).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("itemsBuilder", this.ItemsBuilder.ClientID);
      controlDescriptor.AddComponentProperty("urlTextBox", this.UrlTextBox.ClientID);
      controlDescriptor.AddComponentProperty("openInNewWindowChoiceField", this.OpenInNewWindowChoiceField.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Selectors.Scripts.ExternalPagesSelector.js", typeof (ExternalPagesSelector).Assembly.FullName)
    };
  }
}
