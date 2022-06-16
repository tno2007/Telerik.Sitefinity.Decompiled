// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A field control that extends the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.ChoiceOption" /> control
  /// and add ability for displaying and editing choices from a single or list of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.ChoiceOption" />.
  /// </summary>
  [FieldDefinitionElement(typeof (ChoiceFieldElement))]
  public class DynamicChoiceField : ChoiceField
  {
    internal const string dynamicChoiceFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.DynamicChoiceField.js";

    /// <inheritdoc />
    public override object Value
    {
      get
      {
        if (this.ChildControlsCreated && this.DisplayMode == FieldDisplayMode.Write)
        {
          if (this.RenderChoicesAs == RenderChoicesAs.CheckBoxes)
          {
            IEnumerable<ListItem> listItems = this.ChoiceControl.Items.OfType<ListItem>().Where<ListItem>((Func<ListItem, bool>) (i => i.Selected));
            List<ChoiceOption> choiceOptionList = new List<ChoiceOption>();
            foreach (ListItem listItem in listItems)
            {
              ChoiceOption choiceOption = new ChoiceOption(listItem.Value, listItem.Text);
              choiceOptionList.Add(choiceOption);
            }
            return (object) choiceOptionList.ToArray();
          }
          if (this.RenderChoicesAs == RenderChoicesAs.RadioButtons || this.RenderChoicesAs == RenderChoicesAs.DropDown)
          {
            ListItem selectedItem = this.ChoiceControl.SelectedItem;
            if (selectedItem != null)
              return (object) new ChoiceOption(selectedItem.Value, selectedItem.Text);
          }
        }
        return base.Value;
      }
      set
      {
        if (this.ChildControlsCreated && this.DisplayMode == FieldDisplayMode.Write)
        {
          if (this.RenderChoicesAs == RenderChoicesAs.CheckBoxes)
          {
            if (value is IEnumerable<ChoiceOption> source)
            {
              List<string> list = source.Select<ChoiceOption, string>((Func<ChoiceOption, string>) (x => x.PersistedValue)).ToList<string>();
              foreach (ListItem listItem in this.ChoiceControl.Items.OfType<ListItem>())
              {
                if (list.Contains(listItem.Value))
                  listItem.Selected = true;
              }
            }
            this.fieldValue = value;
          }
          else if (this.RenderChoicesAs == RenderChoicesAs.RadioButtons || this.RenderChoicesAs == RenderChoicesAs.DropDown)
          {
            if (value is ChoiceOption choiceOption)
              this.ChoiceControl.Items.FindByValue(choiceOption.PersistedValue).Selected = true;
            this.fieldValue = value;
          }
          else
            base.Value = value;
        }
        else
          base.Value = value;
      }
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.DisplayMode != FieldDisplayMode.Read || !(this.fieldValue is ChoiceOption))
        return;
      this.ReadModeLabel.Text = this.fieldValue.ToString();
    }

    /// <inheritdoc />
    public override void Configure(IFieldDefinition definition)
    {
      if (definition is IChoiceFieldDefinition choiceFieldDefinition)
      {
        this.MutuallyExclusive = choiceFieldDefinition.MutuallyExclusive;
        this.RenderChoicesAs = choiceFieldDefinition.RenderChoiceAs;
        this.SelectedChoicesIndex = choiceFieldDefinition.SelectedChoicesIndex;
        this.HideTitle = choiceFieldDefinition.HideTitle;
        this.ReturnValuesAlwaysInArray = choiceFieldDefinition.ReturnValuesAlwaysInArray;
        if (this.RenderChoicesAs == RenderChoicesAs.CheckBoxes)
          this.ReturnValuesAlwaysInArray = true;
        this.Choices.Clear();
        foreach (IChoiceDefinition choice in choiceFieldDefinition.Choices)
        {
          ChoiceItem choiceItem = this.GetChoiceItem(choice);
          if (this.RenderChoicesAs == RenderChoicesAs.DropDown)
            choiceItem.Text = HttpUtility.HtmlDecode(choiceItem.Text);
          this.Choices.Add(choiceItem);
        }
      }
      this.ConfigureBase(definition);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      if (this.DisableClientScripts)
        return (IEnumerable<ScriptReference>) new ScriptReference[0];
      string fullName = typeof (ChoiceField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.DynamicChoiceField.js", fullName)
      };
    }

    private ChoiceItem GetChoiceItem(IChoiceDefinition choiceDefinition)
    {
      if (choiceDefinition.Text != null && ResourceParserHelper.IsResourceRegex.IsMatch(choiceDefinition.Text))
      {
        string classId = ResourceParserHelper.ResourceFileNameRegex.Match(choiceDefinition.Text).Groups[1].Value.Trim();
        string key = ResourceParserHelper.ResourceKeyRegex.Match(choiceDefinition.Text).Groups[1].Value.Trim();
        choiceDefinition.Text = Res.Get(classId, key);
      }
      return new ChoiceItem(choiceDefinition);
    }
  }
}
