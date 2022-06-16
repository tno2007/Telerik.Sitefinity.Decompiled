// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormMultipleChoiceFieldDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "MultipleChoiceTitle")]
  [DatabaseMapping(UserFriendlyDataType.ShortText)]
  public class FormMultipleChoice : FormChoiceField
  {
    private string otherTitleText;
    internal const string fieldScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormMultipleChoice.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormMultipleChoice.ascx");
    private const string addOtherChoiceItemValue = "-AddOtherChoiceItemValue-";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice" /> class.
    /// </summary>
    public FormMultipleChoice()
    {
      this.RenderChoicesAs = RenderChoicesAs.RadioButtons;
      this.OtherTitleText = Res.Get<FormsResources>().Other;
      this.FirstItemIsSelected = true;
      this.LayoutTemplatePath = FormMultipleChoice.layoutTemplatePath;
    }

    /// <summary>The default value of the control</summary>
    public bool FirstItemIsSelected { get; set; }

    /// <summary>
    /// If set the control will allow selecting of different value
    /// </summary>
    public bool EnableAddOther { get; set; }

    /// <summary>
    /// Title that will be displayed when adding addttional item
    /// </summary>
    [MultilingualProperty]
    public string OtherTitleText
    {
      get => this.otherTitleText;
      set => this.otherTitleText = Sanitizer.GetSafeHtmlFragment(value.ToString());
    }

    /// <summary>
    /// Represents the different column modes for displaying the choices
    /// </summary>
    public FormControlColumnsMode FormControlColumnsMode { get; set; }

    /// <summary>Name of the embedded resource or null if embedded resource is not used</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the value of the control.</summary>
    public override object Value
    {
      get
      {
        if (this.DisplayMode != FieldDisplayMode.Write)
          return (object) this.Choices.Select<ChoiceItem, string>((Func<ChoiceItem, string>) (c => c.Text)).FirstOrDefault<string>();
        string selectedValue = this.ChoiceControl.SelectedValue;
        if (this.EnableAddOther && selectedValue == "-AddOtherChoiceItemValue-")
          selectedValue = this.CustomValueTextField.Value as string;
        return (object) selectedValue;
      }
      set
      {
        if (this.DisplayMode == FieldDisplayMode.Write)
          this.ChoiceControl.SelectedValue = value as string;
        else
          this.ReadModeLabel.Text = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(value as string);
      }
    }

    /// <summary>
    /// Gets the reference to the list box control that represents choices when RenderChoicesAs
    /// property is set to <see cref="T:System.Web.UI.WebControls.ListBox" />.
    /// </summary>
    protected TextField CustomValueTextField => this.Container.GetControl<TextField>("customValueTextField", true);

    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        RadioButtonList radioButtons = this.RadioButtons;
        radioButtons.RepeatLayout = RepeatLayout.Flow;
        this.Choices.ToList<ChoiceItem>().ForEach((Action<ChoiceItem>) (c => c.Selected = false));
        if (this.FirstItemIsSelected)
        {
          this.Choices[0].Selected = true;
        }
        else
        {
          foreach (ChoiceItem choice in this.Choices)
            choice.Selected = false;
        }
        switch (this.FormControlColumnsMode)
        {
          case FormControlColumnsMode.OneColumn:
            radioButtons.RepeatColumns = 1;
            break;
          case FormControlColumnsMode.TwoColumns:
            radioButtons.RepeatColumns = 2;
            break;
          case FormControlColumnsMode.ThreeColumns:
            radioButtons.RepeatColumns = 3;
            break;
          case FormControlColumnsMode.Inline:
            radioButtons.RepeatDirection = RepeatDirection.Horizontal;
            break;
        }
        this.AddCssClass("sfFormRadiolist");
        if (this.EnableAddOther)
        {
          this.CustomValueTextField.Visible = true;
          this.CustomValueTextField.Style.Add("display", "none");
          this.Choices.Add(new ChoiceItem()
          {
            Text = this.OtherTitleText,
            Value = "-AddOtherChoiceItemValue-"
          });
        }
        else
          this.CustomValueTextField.Visible = false;
      }
      base.InitializeControls(container);
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
      if (this.DisplayMode == FieldDisplayMode.Write && this.EnableAddOther)
      {
        controlDescriptor.AddProperty("_enableAddOther", (object) true);
        controlDescriptor.AddProperty("_addOtherChoiceItemValue", (object) "-AddOtherChoiceItemValue-");
        controlDescriptor.AddComponentProperty("customValueTextField", this.CustomValueTextField.ClientID);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormMultipleChoice.js", typeof (FormMultipleChoice).Assembly.FullName)
    };
  }
}
