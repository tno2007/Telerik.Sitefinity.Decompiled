// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormCheckboxes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  /// <summary>Checkboxes field control for forms</summary>
  [ControlDesigner(typeof (FormCheckboxesDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "CheckboxesTitle")]
  [DatabaseMapping(UserFriendlyDataType.LongText)]
  public class FormCheckboxes : FormChoiceField
  {
    internal const string fieldScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormCheckboxes.js";

    public FormCheckboxes()
    {
      this.RenderChoicesAs = RenderChoicesAs.CheckBoxes;
      this.Title = Res.Get<FormsResources>().SelectChoices;
    }

    /// <summary>
    /// Represents the different column modes for displaying the choices
    /// </summary>
    public FormControlColumnsMode FormControlColumnsMode { get; set; }

    /// <summary>
    /// If true the options will be sorted by the alphabetical order of their titles
    /// </summary>
    public bool SortAlphabetically { get; set; }

    /// <summary>Gets or sets the selected items values</summary>
    /// <value>If a string is passed the checkbox with this value will be selected, if the value is list of strings will
    /// select all the checkboxes with those values
    /// </value>
    /// <remarks>
    /// Should be persisted as a string of Titles for the Beta
    /// </remarks>
    public override object Value
    {
      get
      {
        if (this.DisplayMode != FieldDisplayMode.Write)
          return (object) this.ReadModeLabel.Text;
        List<string> stringList = new List<string>();
        foreach (ListItem listItem in this.CheckBoxes.Items)
        {
          if (listItem.Selected)
            stringList.Add(listItem.Value);
        }
        return (object) stringList;
      }
      set
      {
        if (this.DisplayMode == FieldDisplayMode.Write)
        {
          this.ChoiceControl.ClearSelection();
          if (value == null || value is string && string.IsNullOrWhiteSpace(value.ToString()))
          {
            this.Choices.ToList<ChoiceItem>().ForEach((Action<ChoiceItem>) (c => c.Selected = false));
          }
          else
          {
            List<string> list = new List<string>();
            if (!this.TryConvertStringToList(value, list))
            {
              this.ChoiceControl.SelectedValue = value as string;
            }
            else
            {
              foreach (string str in list)
              {
                string itemValue = str;
                ChoiceItem choiceItem = this.Choices.FirstOrDefault<ChoiceItem>((Func<ChoiceItem, bool>) (c => c.Value == itemValue));
                if (choiceItem != null)
                  choiceItem.Selected = true;
                ListItem byValue = this.CheckBoxes.Items.FindByValue(itemValue);
                if (byValue != null)
                  byValue.Selected = true;
              }
            }
          }
        }
        else
          this.ReadModeLabel.Text = HttpUtility.HtmlEncode(value as string);
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>*
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        CheckBoxList checkBoxes = this.CheckBoxes;
        checkBoxes.RepeatLayout = RepeatLayout.Flow;
        switch (this.FormControlColumnsMode)
        {
          case FormControlColumnsMode.OneColumn:
            checkBoxes.RepeatColumns = 1;
            break;
          case FormControlColumnsMode.TwoColumns:
            checkBoxes.RepeatColumns = 2;
            break;
          case FormControlColumnsMode.ThreeColumns:
            checkBoxes.RepeatColumns = 3;
            break;
          case FormControlColumnsMode.Inline:
            checkBoxes.RepeatDirection = RepeatDirection.Horizontal;
            break;
        }
        if (this.SortAlphabetically)
          this.SortChoiceItemsAlphabetically();
        this.AddCssClass("sfFormCheckboxlist");
      }
      base.InitializeControls(container);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormCheckboxes.js", typeof (FormCheckboxes).Assembly.FullName)
    };

    private bool TryConvertStringToList(object value, List<string> list)
    {
      switch (value)
      {
        case string _:
          string str1 = value as string;
          if (string.IsNullOrWhiteSpace(str1))
            return false;
          string[] strArray = str1.Split(',');
          if (strArray.Length == 0)
            return false;
          foreach (string str2 in strArray)
            list.Add(str2);
          return true;
        case List<string> _:
          list = value as List<string>;
          return true;
        default:
          throw new ArgumentException("Unsupported type for checkbox value: " + value.GetType().ToString());
      }
    }
  }
}
