// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views
{
  /// <summary>
  /// </summary>
  public class CssEditorDesignerView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.EmbedControls.CssEditorDesignerView.ascx");
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    internal const string designerViewJs = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.CssEditorDesignerView.js";

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (CssEditorDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<PublicControlsResources>().WriteCss;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CssEditorDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Represents the text filed that holds the CSS styles</summary>
    protected internal virtual TextField CustomCssCodeTextField => this.Container.GetControl<TextField>("customCssCodeTextField", true);

    /// <summary>
    /// Represents the choices between all media types and specific ones
    /// </summary>
    protected internal virtual ChoiceField MediaChoiceField => this.Container.GetControl<ChoiceField>("mediaChoiceField", true);

    /// <summary>Represents the media type choices</summary>
    protected internal virtual ChoiceField MediaTypesChoiceField => this.Container.GetControl<ChoiceField>("mediaTypesChoiceField", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      foreach (string name in Enum.GetNames(typeof (MediaType)))
      {
        if (!(name == MediaType.all.ToString()))
          this.MediaTypesChoiceField.Choices.Add(new ChoiceItem()
          {
            Text = name,
            Value = ((int) Enum.Parse(typeof (MediaType), name)).ToString()
          });
      }
      this.MediaTypesChoiceField.Style.Add("display", "none");
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
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (int num in Enum.GetValues(typeof (MediaType)))
        dictionary.Add(Enum.GetName(typeof (MediaType), (object) num), num);
      controlDescriptor.AddProperty("_mediaTypesMap", (object) dictionary);
      controlDescriptor.AddComponentProperty("customCssCodeTextField", this.CustomCssCodeTextField.ClientID);
      controlDescriptor.AddComponentProperty("mediaChoiceField", this.MediaChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("mediaTypesChoiceField", this.MediaTypesChoiceField.ClientID);
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
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>();
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.CssEditorDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }
  }
}
