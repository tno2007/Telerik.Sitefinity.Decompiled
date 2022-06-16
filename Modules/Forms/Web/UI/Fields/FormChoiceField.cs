// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  public abstract class FormChoiceField : 
    ChoiceField,
    IMultiDisplayModesSupport,
    IFormFieldControl,
    IValidatable
  {
    private IMetaField metaField;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormChoiceField" /> class.
    /// </summary>
    public FormChoiceField()
    {
      this.DisplayMode = FieldDisplayMode.Write;
      this.Title = Res.Get<FormsResources>().SelectAChoice;
      this.AddDefaultChoices();
    }

    protected virtual void AddDefaultChoices()
    {
      this.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<FormsResources>().FirstChoice,
        Value = Res.Get<FormsResources>().FirstChoice,
        Selected = true
      });
      this.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<FormsResources>().SecondChoice,
        Value = Res.Get<FormsResources>().SecondChoice
      });
      this.Choices.Add(new ChoiceItem()
      {
        Text = Res.Get<FormsResources>().ThirdChoice,
        Value = Res.Get<FormsResources>().ThirdChoice
      });
    }

    /// <summary>Metafield that represents the control</summary>
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [NonMultilingual]
    public IMetaField MetaField
    {
      get
      {
        if (this.metaField == null)
          this.metaField = (IMetaField) this.LoadDefaultMetaField();
        return this.metaField;
      }
      set => this.metaField = value;
    }

    /// <summary>Titles for the choice items</summary>
    [TypeConverter(typeof (HtmlSanitizedStringArrayConverter))]
    [MultilingualProperty]
    public virtual Array ChoiceItemsTitles
    {
      get => (Array) this.Choices.Select<ChoiceItem, string>((Func<ChoiceItem, string>) (c => c.Text)).ToArray<string>();
      set
      {
        this.Choices.Clear();
        List<string> list = value.Cast<string>().ToList<string>();
        if (list.Count == 0)
          this.AddDefaultChoices();
        else
          list.ForEach((Action<string>) (s => this.Choices.Add(new ChoiceItem()
          {
            Text = s,
            Value = s
          })));
      }
    }

    /// <summary>Gets or sets the value of the control.</summary>
    public override object Value
    {
      get => this.DisplayMode == FieldDisplayMode.Write ? (object) this.ChoiceControl.SelectedValue : (object) this.ReadModeLabel.Text;
      set
      {
        if (this.DisplayMode == FieldDisplayMode.Write)
          this.ChoiceControl.SelectedValue = value as string;
        else
          this.ReadModeLabel.Text = HttpUtility.HtmlEncode(value as string);
      }
    }

    /// <summary>Gets or sets the read only modes.</summary>
    /// <value>The read only modes.</value>
    [TypeConverter(typeof (StringArrayConverter))]
    public string[] ReadOnlyModes { get; set; }

    /// <summary>Gets or sets the hidden modes.</summary>
    /// <value>The hidden modes.</value>
    [TypeConverter(typeof (StringArrayConverter))]
    public string[] HiddenModes { get; set; }
  }
}
