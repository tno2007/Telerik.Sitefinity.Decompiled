// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Represents the Content Selectors tab for dynamic child content view designer
  /// </summary>
  public class DynamicChildContentSelectorsDesignerView : DynamicContentSelectorsDesignerView
  {
    /// <summary>Stores the layout template path</summary>
    public static readonly string LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.DynamicChildContentSelectorsDesignerView.ascx");
    internal const string DynamicChildContentSelectorsDesignerViewScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicChildContentSelectorsDesignerView.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView" /> class.
    /// </summary>
    public DynamicChildContentSelectorsDesignerView() => this.LayoutTemplatePath = DynamicChildContentSelectorsDesignerView.LayoutTemplatePath;

    /// <summary>Gets or sets the content selector title text.</summary>
    public string NarrowSelectionTitleText { get; set; }

    /// <summary>Gets or sets the all parents filter choice name.</summary>
    public string ChooseAllParentsFilterText { get; set; }

    /// <summary>
    /// Gets or sets the currently open parent filter choice name.
    /// </summary>
    public string ChooseCurrentlyOpenParentFilterText { get; set; }

    /// <summary>Gets or sets the selected parents filter choice name.</summary>
    public string ChooseSelectedParentsFilterText { get; set; }

    /// <summary>Gets or sets the parent selector button text.</summary>
    public string ParentSelectorButtonText { get; set; }

    /// <summary>Gets or sets the no parent have been selected text.</summary>
    public string NoParentHaveBeenSelectedText { get; set; }

    /// <summary>Gets or sets the title of the parent selector dialog.</summary>
    public string ParentSelectorTitle { get; set; }

    /// <summary>Gets the script descriptor type</summary>
    protected override string ScriptDescriptorType => typeof (DynamicChildContentSelectorsDesignerView).FullName;

    /// <summary>
    /// Gets the narrow selection radio choices title literal.
    /// </summary>
    /// <value>The narrow selection choices title literal.</value>
    protected Literal NarrowSelectionChoicesTitleLiteral => this.Container.GetControl<Literal>("narrowSelectionChoicesTitle", true);

    /// <summary>Gets reference to the parent selector control</summary>
    protected ParentSelectorField ParentSelector => this.Container.GetControl<ParentSelectorField>("parentSelector", true);

    /// <summary>
    /// Gets the radio choice for disabling filter by parents.
    /// </summary>
    protected RadioButton RadioChoiceAllParentsFilter => this.Container.GetControl<RadioButton>("contentSelect_AllParents", true);

    /// <summary>
    /// Gets the radio choice for enabling filter by parent URL.
    /// </summary>
    protected RadioButton RadioChoiceCurrentlyOpenParentFilter => this.Container.GetControl<RadioButton>("contentSelect_CurrentlyOpenParentFilter", true);

    /// <summary>
    /// Gets the radio choice for enabling filter by selected parents.
    /// </summary>
    protected RadioButton RadioChoiceSelectedParentsFilter => this.Container.GetControl<RadioButton>("contentSelect_SelectedParentsFilter", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_parentSelectorId", (object) this.ParentSelector.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicChildContentSelectorsDesignerView.js", typeof (DynamicChildContentSelectorsDesignerView).Assembly.GetName().ToString())
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.ChooseAllParentsFilterText != null)
        this.RadioChoiceAllParentsFilter.Text = this.ChooseAllParentsFilterText;
      if (this.ChooseCurrentlyOpenParentFilterText != null)
        this.RadioChoiceCurrentlyOpenParentFilter.Text = this.ChooseCurrentlyOpenParentFilterText;
      if (this.ChooseSelectedParentsFilterText != null)
        this.RadioChoiceSelectedParentsFilter.Text = this.ChooseSelectedParentsFilterText;
      if (this.NarrowSelectionTitleText != null)
        this.NarrowSelectionChoicesTitleLiteral.Text = this.NarrowSelectionTitleText;
      this.InitializeParentSelector();
    }

    private void InitializeParentSelector()
    {
      this.ParentSelector.DisplayMode = FieldDisplayMode.Write;
      this.ParentSelector.WebServiceUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
      this.ParentSelector.DataKeyNames = "Id";
      this.ParentSelector.AllowSearching = true;
      this.ParentSelector.AllowMultipleSelection = true;
      if (this.NoParentHaveBeenSelectedText != null)
        this.ParentSelector.NoContentSelectedText = this.NoParentHaveBeenSelectedText;
      if (this.ParentSelectorButtonText != null)
        this.ParentSelector.SelectContentButtonText = this.ParentSelectorButtonText;
      if (this.ParentSelectorTitle != null)
        this.ParentSelector.DialogTitleLabelText = this.ParentSelectorTitle;
      string str = this.ContentManager.Provider.Name;
      if (!this.IsControlDefinitionProviderCorrect)
        str = this.CurrentContentView.ControlDefinition.ProviderName;
      this.ParentSelector.ProviderName = str;
      if (this.ModuleType.ParentModuleType == null)
        return;
      this.ParentSelector.ItemsType = this.ModuleType.ParentModuleType.GetFullTypeName();
      string shortTextFieldName = this.ModuleType.ParentModuleType.MainShortTextFieldName;
      this.ParentSelector.MainFieldName = shortTextFieldName;
      ICollection<Guid> selectedParentItemIds = this.CurrentContentView.MasterViewDefinition.ItemsParentsIds;
      if (selectedParentItemIds.Count <= 0)
        return;
      List<DynamicContent> list = (this.ContentManager as DynamicModuleManager).GetDataItems(TypeResolutionService.ResolveType(this.ModuleType.ParentModuleType.GetFullTypeName())).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => selectedParentItemIds.Contains(i.Id))).ToList<DynamicContent>();
      StringBuilder stringBuilder = new StringBuilder();
      int num = list.Count<DynamicContent>();
      for (int index = 0; index < num; ++index)
      {
        DynamicContent component = list[index];
        PropertyDescriptor property = TypeDescriptor.GetProperties((object) component)[shortTextFieldName];
        if (property is LstringPropertyDescriptor)
          stringBuilder.Append(((LstringPropertyDescriptor) property).GetString((object) component, SystemManager.CurrentContext.Culture, true));
        else if (property is MetafieldPropertyDescriptor)
          stringBuilder.Append(property.GetValue((object) component));
        if (index != num - 1)
          stringBuilder.Append(", ");
      }
      this.ParentSelector.InitialText = stringBuilder.ToString();
    }
  }
}
