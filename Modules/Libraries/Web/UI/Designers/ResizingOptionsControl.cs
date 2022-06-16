// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A control that provides options for resizing images and videos.
  /// </summary>
  public class ResizingOptionsControl : SimpleScriptView
  {
    private string itemName;
    private string itemsName;
    private bool showOpenOriginalSizeCheckBox = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.ResizingOptionsControl.ascx");
    internal const string controlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ResizingOptionsControl.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ResizingOptionsControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    public string ItemsName
    {
      get => string.IsNullOrEmpty(this.itemsName) ? Res.Get<ImagesResources>().Images : this.itemsName;
      set => this.itemsName = value;
    }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    public string ItemName
    {
      get => string.IsNullOrEmpty(this.itemName) ? Res.Get<ImagesResources>().Image : this.itemName;
      set => this.itemName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the "open original size" check box.
    /// </summary>
    public bool ShowOpenOriginalSizeCheckBox
    {
      get => this.showOpenOriginalSizeCheckBox;
      set => this.showOpenOriginalSizeCheckBox = value;
    }

    /// <summary>
    /// Represents the choice field (radio buttons) for selecting specific size.
    /// </summary>
    protected virtual ChoiceField ResizeChoiceField => this.Container.GetControl<ChoiceField>("resizeChoiceField", true);

    /// <summary>
    /// Represents the choice field (dropdown) which displays the different size options.
    /// </summary>
    protected virtual ChoiceField SizesChoiceField => this.Container.GetControl<ChoiceField>("sizesChoiceField", true);

    /// <summary>
    /// Represents the container of the resize settings element.
    /// </summary>
    protected virtual HtmlGenericControl ResizeSettingsGroup => this.Container.GetControl<HtmlGenericControl>("resizeSettingsGroup", true);

    /// <summary>
    /// Represents the button that expands/collapses the resize settings group.
    /// </summary>
    protected virtual HtmlAnchor ResizeSettingsExpander => this.Container.GetControl<HtmlAnchor>("resizeSettingsExpander", true);

    /// <summary>
    /// Represents the textbox containig custom width if selected.
    /// </summary>
    protected virtual TextField CustomWidthTextField => this.Container.GetControl<TextField>("customWidthTextField", true);

    /// <summary>
    /// Represents the checkbox which when checked will make the resized item to open the original one.
    /// </summary>
    protected virtual ChoiceField OpenOriginalChoiceField => this.Container.GetControl<ChoiceField>("openOriginalChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SizesChoiceField.Style.Add("display", "none");
      this.CustomWidthTextField.Style.Add("display", "none");
      this.OpenOriginalChoiceField.Style.Add("display", "none");
      this.ResizeChoiceField.Choices[0].Text = string.Format(Res.Get<Labels>().ShowOriginal, (object) this.ItemName.ToLower());
      this.ResizeChoiceField.Choices[1].Text = string.Format(Res.Get<Labels>().ResizeWidthTo, (object) this.ItemName.ToLower());
      if (this.ShowOpenOriginalSizeCheckBox)
        this.OpenOriginalChoiceField.Choices[0].Text = string.Format(Res.Get<LibrariesResources>().ClickingTheResizedItemOpensTheOriginal, (object) this.ItemName.ToLower(), (object) this.ItemName.ToLower());
      else
        this.OpenOriginalChoiceField.Visible = false;
    }

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
      controlDescriptor.AddElementProperty("resizeSettingsGroup", this.ResizeSettingsGroup.ClientID);
      controlDescriptor.AddElementProperty("resizeSettingsExpander", this.ResizeSettingsExpander.ClientID);
      controlDescriptor.AddComponentProperty("resizeChoiceField", this.ResizeChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("sizesChoiceField", this.SizesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("customWidthTextField", this.CustomWidthTextField.ClientID);
      controlDescriptor.AddProperty("showOpenOriginalSizeCheckBox", (object) this.ShowOpenOriginalSizeCheckBox);
      if (this.ShowOpenOriginalSizeCheckBox)
        controlDescriptor.AddComponentProperty("openOriginalChoiceField", this.OpenOriginalChoiceField.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ResizingOptionsControl.js", this.GetType().Assembly.GetName().ToString())
    }.ToArray();
  }
}
