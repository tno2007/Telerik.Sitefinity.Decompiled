// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormDropDownListDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "DropDownListTitle")]
  [DatabaseMapping(UserFriendlyDataType.ShortText)]
  public class FormDropDownList : FormChoiceField
  {
    internal const string fieldScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormDropDownList.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormDropDownList" /> class.
    /// </summary>
    public FormDropDownList() => this.RenderChoicesAs = RenderChoicesAs.DropDown;

    /// <summary>
    /// If true the options will be sorted by the alphabetical order of their titles
    /// </summary>
    public bool SortAlphabetically { get; set; }

    /// <summary>The size of the drop down</summary>
    public FormControlSize DropDownListSize { get; set; }

    /// <summary>
    /// Gets or sets the title of default selected item // TODO: work with choice item
    /// </summary>
    [MultilingualProperty]
    public string DefaultSelectedTitle { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        if (!string.IsNullOrEmpty(this.DefaultSelectedTitle))
        {
          ChoiceItem choiceItem = this.Choices.FirstOrDefault<ChoiceItem>((Func<ChoiceItem, bool>) (c => c.Text == this.DefaultSelectedTitle));
          if (choiceItem != null)
          {
            this.Choices.ToList<ChoiceItem>().ForEach((Action<ChoiceItem>) (c => c.Selected = false));
            choiceItem.Selected = true;
          }
        }
        if (this.SortAlphabetically)
          this.SortChoiceItemsAlphabetically();
        this.AddCssClass("sfFormDropdown");
      }
      base.InitializeControls(container);
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (this.DropDownListSize != FormControlSize.None)
        this.AddCssClass("sfDdl" + (object) this.DropDownListSize);
      base.OnPreRender(e);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormDropDownList.js", typeof (FormDropDownList).Assembly.FullName)
    };
  }
}
