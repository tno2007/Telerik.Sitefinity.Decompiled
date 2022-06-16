// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  ///  Represents the template selector designer for hierarchical content types
  /// </summary>
  public class DynamicTypeTemplatesSelectorDesignerView : ContentViewDesignerView
  {
    private const string widgetEditorDialogUrl = "~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}";
    internal const string dynamicTypeTemplatesSelectorDesignerViewScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicTypeTemplatesSelectorDesignerView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.ContentView.DynamicTypeTemplatesSelectorDesignerView.ascx");

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "dynamicTypeTemplatesSettings";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => "Dynamic Type Templates Settings";

    /// <summary>Gets or sets the full name of the data item type.</summary>
    /// <value>The full name of the data item type.</value>
    public string DataItemTypeFullName { get; set; }

    /// <summary>
    /// The type name of the view used to display the detail designed control.
    /// </summary>
    public string DetailDesignedViewType { get; set; }

    /// <summary>
    /// The type name of the view used to display the master designed control.
    /// </summary>
    public string MasterDesignedViewType { get; set; }

    /// <summary>Gets or sets the title text for list of items.</summary>
    /// <value>The list of items title text.</value>
    public virtual string ListOfItemsText { get; set; }

    /// <summary>Gets or sets the title text for single item.</summary>
    /// <value>The single item title text.</value>
    public virtual string SingleItemText { get; set; }

    /// <summary>
    /// Gets whether the user has the permissions to modify the widget templates
    /// </summary>
    protected bool ModifyWidgetTemplatePermission => AppPermission.Root.IsGranted("Backend", "ManageWidgets");

    /// <summary>
    /// Gets the ChoiceField control that lists and selects the control template for the list mode
    /// </summary>
    protected virtual ChoiceField MasterViewNameList => this.Container.GetControl<ChoiceField>("masterViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected master control template
    /// </summary>
    protected virtual Label MasterTemplateTitle => this.Container.GetControl<Label>("masterTemplateTitle", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected master control template
    /// </summary>
    protected virtual HyperLink EditMasterTemplateLink => this.Container.GetControl<HyperLink>("editMasterTemplateLink", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new master control template
    /// </summary>
    protected virtual HyperLink CreateMasterTemplateLink => this.Container.GetControl<HyperLink>("createMasterTemplateLink", true);

    /// <summary>
    /// Gets the ChoiceField control that lists and selects the control template for the details mode
    /// </summary>
    protected virtual ChoiceField DetailViewNameList => this.Container.GetControl<ChoiceField>("detailViewName", true);

    /// <summary>
    /// Gets the Label control representing the title of the selected detail control template
    /// </summary>
    protected virtual Label DetailTemplateTitle => this.Container.GetControl<Label>("detailTemplateTitle", true);

    /// <summary>
    /// Gets the link that opens up the dialog for editing the currently selected detail control template
    /// </summary>
    protected virtual HyperLink EditDetailTemplateLink => this.Container.GetControl<HyperLink>("editDetailTemplateLink", true);

    /// <summary>
    /// Gets the correct instance of the RadWindowManager class
    /// </summary>
    protected virtual RadWindowManager RadWindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>
    /// Gets the link that opens up the dialog for creating a new detail control template
    /// </summary>
    protected virtual HyperLink CreateDetailTemplateLink => this.Container.GetControl<HyperLink>("createDetailTemplateLink", true);

    /// <summary>
    /// Gets the literal control that displays the title text for list of items.
    /// </summary>
    protected virtual Literal ListOfItemsTitleLiteral => this.Container.GetControl<Literal>("listOfItemsTitleLiteral", true);

    /// <summary>
    /// Gets the literal control that displays the title text for single item.
    /// </summary>
    protected virtual Literal SingleItemTitleLiteral => this.Container.GetControl<Literal>("singleItemTitleLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      IQueryable<ControlPresentation> layoutTemplates = PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "ASP_NET_TEMPLATE"));
      this.BindDetailsLayoutTemplates(layoutTemplates);
      this.BindMasterLayoutTemplates(layoutTemplates);
      if (!this.ListOfItemsText.IsNullOrEmpty())
        this.ListOfItemsTitleLiteral.Text = this.ListOfItemsText;
      if (this.SingleItemText.IsNullOrEmpty())
        return;
      this.SingleItemTitleLiteral.Text = this.SingleItemText;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DynamicTypeTemplatesSelectorDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void BindDetailsLayoutTemplates(IQueryable<ControlPresentation> layoutTemplates)
    {
      string filterExpression = this.ParentDesigner is HierarchicalContentViewDesigner || this.ParentDesigner is DynamicContentViewDesigner ? string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) this.DetailDesignedViewType, (object) this.DataItemTypeFullName) : string.Format("ControlType == \"{0}\"", (object) this.DetailDesignedViewType);
      int? totalCount = new int?();
      foreach (ControlPresentation setExpression in (IEnumerable<ControlPresentation>) DataProviderBase.SetExpressions<ControlPresentation>(layoutTemplates, filterExpression, "Name", new int?(0), new int?(0), ref totalCount))
        this.DetailViewNameList.Choices.Add(new ChoiceItem()
        {
          Text = setExpression.Name,
          Value = setExpression.Id.ToString()
        });
      if (this.DetailViewNameList.Choices.Count >= 2)
        return;
      this.DetailViewNameList.CssClass += " sfDisplayNone";
    }

    private void BindMasterLayoutTemplates(IQueryable<ControlPresentation> layoutTemplates)
    {
      string filterExpression = this.ParentDesigner is HierarchicalContentViewDesigner || this.ParentDesigner is DynamicContentViewDesigner ? string.Format("ControlType == \"{0}\" AND Condition == \"{1}\"", (object) this.MasterDesignedViewType, (object) this.DataItemTypeFullName) : string.Format("ControlType == \"{0}\"", (object) this.MasterDesignedViewType);
      int? totalCount = new int?();
      foreach (ControlPresentation setExpression in (IEnumerable<ControlPresentation>) DataProviderBase.SetExpressions<ControlPresentation>(layoutTemplates, filterExpression, "Name", new int?(0), new int?(0), ref totalCount))
        this.MasterViewNameList.Choices.Add(new ChoiceItem()
        {
          Text = setExpression.Name,
          Value = setExpression.Id.ToString()
        });
      if (this.MasterViewNameList.Choices.Count >= 2)
        return;
      this.MasterViewNameList.CssClass += " sfDisplayNone";
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddElementProperty("detailTemplateTitleElement", this.DetailTemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("masterTemplateTitleElement", this.MasterTemplateTitle.ClientID);
      controlDescriptor.AddElementProperty("editDetailTemplateLink", this.EditDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("editMasterTemplateLink", this.EditMasterTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createDetailTemplateLink", this.CreateDetailTemplateLink.ClientID);
      controlDescriptor.AddElementProperty("createMasterTemplateLink", this.CreateMasterTemplateLink.ClientID);
      controlDescriptor.AddComponentProperty("detailViewsList", this.DetailViewNameList.ClientID);
      controlDescriptor.AddComponentProperty("masterViewsList", this.MasterViewNameList.ClientID);
      controlDescriptor.AddProperty("_widgetEditorDialogUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/ControlTemplateEditor?ViewName={0}"));
      controlDescriptor.AddProperty("_editTemplateViewName", (object) ControlTemplatesDefinitions.BackendEditDetailsViewName);
      controlDescriptor.AddProperty("_createTemplateViewName", (object) ControlTemplatesDefinitions.BackendInsertDetailsViewName);
      controlDescriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
      controlDescriptor.AddProperty("_modifyWidgetTemplatePermission", (object) this.ModifyWidgetTemplatePermission);
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
      ScriptReferenceCollection scriptReferences = PageManager.GetScriptReferences(ScriptRef.JQuery);
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.DynamicTypeTemplatesSelectorDesignerView.js", assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
