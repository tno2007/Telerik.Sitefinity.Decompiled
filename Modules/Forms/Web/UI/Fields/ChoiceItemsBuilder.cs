// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  /// <summary>
  /// Class that provides the UI for building choice items for Choice field/dropdowns/multilechoice/etc
  /// </summary>
  public class ChoiceItemsBuilder : SimpleScriptView
  {
    private int minimumItemsCount = 2;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.ChoiceItemsBuilder.ascx");
    private const string scriptJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.ChoiceItemsBuilder.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.ChoiceItemsBuilder" /> class.
    /// </summary>
    public ChoiceItemsBuilder() => this.LayoutTemplatePath = ChoiceItemsBuilder.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    public List<ChoiceItem> ChoiceItems { get; set; }

    /// <summary>
    /// If true will display the radio buttons for selecting a defalt item
    /// </summary>
    public bool ShowDefaultItemSelector { get; set; }

    /// <summary>Represents the title of the builder</summary>
    public Label TitleLabel => this.Container.GetControl<Label>("title", true);

    /// <summary>Represents the container of the choice elements</summary>
    protected HtmlGenericControl ChoiceItemsContainer => this.Container.GetControl<HtmlGenericControl>("choiceContainer", true);

    /// <summary>
    /// Represents the radio button in the template that sets the item as selected/checked by default
    /// </summary>
    protected Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <summary>
    /// Gets or sets a value indicating what is the minimum items count of this type per group (i.e. radio buttons can't be less than 2).
    /// </summary>
    /// <value>The minimum items count of this type per group (i.e. radio buttons can't be less than 2)</value>
    public int MinimumItemsCount
    {
      get => this.minimumItemsCount;
      set => this.minimumItemsCount = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      int num = this.ShowDefaultItemSelector ? 1 : 0;
      this.ErrorMessageLabel.Attributes.CssStyle.Add("display", "none");
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
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
      controlDescriptor.AddProperty("_minimumItemsCount", (object) this.MinimumItemsCount);
      controlDescriptor.AddElementProperty("choiceItemsContainer", this.ChoiceItemsContainer.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddProperty("_showDefaultItemSelector", (object) this.ShowDefaultItemSelector);
      if (this.ChoiceItems != null && this.ChoiceItems.Count > 0)
        controlDescriptor.AddProperty("choiceItems", (object) this.ChoiceItems);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.ChoiceItemsBuilder.js", this.GetType().Assembly.FullName)
    };
  }
}
