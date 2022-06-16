// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  public class ContentTypesMasterView : KendoView
  {
    private ModuleBuilderManager moduleBuilderMng;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.ContentTypesMasterView.ascx");
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DynamicModules/ContentTypeService.svc/");
    internal const string scriptRef = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.ContentTypesMasterView.js";
    private const int fieldsTextLength = 30;
    private const string fieldsTextSuffix = "...";
    private const string fieldsTextSeparator = ", ";

    /// <summary>
    /// Gets or sets the id of the module which is the parent of the content types to be displayed.
    /// </summary>
    /// <value>The module id.</value>
    public Guid ModuleId { get; set; }

    /// <summary>Gets the default type editor title value</summary>
    public string TypeEditorDefaultTitlePrefix => Res.Get<ModuleBuilderResources>().TypeEditorDefaultTitlePrefix;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ContentTypesMasterView.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the module title.</summary>
    /// <value>The module title.</value>
    protected virtual string ModuleTitle { get; set; }

    /// <summary>Gets or sets the module name.</summary>
    /// <value>The module title.</value>
    protected virtual string ModuleName { get; set; }

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    protected ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderMng == null)
          this.moduleBuilderMng = ModuleBuilderManager.GetManager();
        return this.moduleBuilderMng;
      }
    }

    /// <summary>
    /// Gets a collection of the types which are children of the current module.
    /// </summary>
    protected IQueryable<DynamicModuleType> ModuleTypes => this.ModuleBuilderMngr.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId));

    /// <summary>
    /// Gets the reference to the contentTypesRepeater control.
    /// </summary>
    protected virtual Repeater ContentTypesRepeater => this.Container.GetControl<Repeater>("contentTypesRepeater", true);

    /// <summary>
    /// Gets the reference to the type editor which displays the fields of the content type.
    /// </summary>
    protected virtual TypeEditor TypeEditor => this.Container.GetControl<TypeEditor>("typeEditor", true);

    /// <summary>
    /// Gets the reference to the button which displays dialog for adding new content type.
    /// </summary>
    protected virtual LinkButton AddContentTypeButton => this.Container.GetControl<LinkButton>("addContentTypeButton", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (!(this.ModuleId != Guid.Empty))
        return;
      IDynamicModule dynamicModule = ModuleBuilderManager.GetModules().First<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == this.ModuleId));
      this.ModuleTitle = dynamicModule.Title;
      this.ModuleName = dynamicModule.Name;
      this.InitializeContentTypes();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ContentTypesMasterView).FullName, this.ClientID);
      controlDescriptor.AddProperty("_webServiceUrl", (object) ContentTypesMasterView.webServiceUrl);
      controlDescriptor.AddProperty("_moduleId", (object) this.ModuleId.ToString());
      controlDescriptor.AddProperty("_moduleTitle", (object) this.ModuleTitle);
      controlDescriptor.AddProperty("_moduleName", (object) this.ModuleName);
      controlDescriptor.AddProperty("_typeEditorTitle", (object) this.TypeEditorDefaultTitlePrefix);
      controlDescriptor.AddElementProperty("typeEditor", this.TypeEditor.ClientID);
      controlDescriptor.AddElementProperty("addContentTypeButton", this.AddContentTypeButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.ContentTypesMasterView.js", typeof (ContentTypesMasterView).Assembly.FullName)
    };

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ContentTypesRepeater.DataBind();
    }

    private void InitializeContentTypes()
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      IEnumerable<ContentTypeTreeItemContext> typeTreeItemContexts = ContentTypesMasterView.BuildContentTypeTreeHierarchy((IEnumerable<ContentTypeTreeItemContext>) this.ModuleTypes.Select<DynamicModuleType, ContentTypeTreeItemContext>(Expression.Lambda<Func<DynamicModuleType, ContentTypeTreeItemContext>>((Expression) Expression.MemberInit(Expression.New(typeof (ContentTypeTreeItemContext)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentTypeTreeItemContext.set_ContentTypeId)), ))))); //unable to render the statement
      this.ContentTypesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      this.ContentTypesRepeater.DataSource = (object) typeTreeItemContexts;
    }

    private void ContentTypesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      ContentTypeTreeItemContext dataItem = e.Item.DataItem as ContentTypeTreeItemContext;
      (e.Item.FindControl("fieldsLabel") as Label).Text = this.GetFieldsText(dataItem.ContentTypeId);
      if (dataItem.Items == null || dataItem.Items.Length == 0)
        return;
      PlaceHolder control = e.Item.FindControl("childrenPlaceholder") as PlaceHolder;
      Repeater child = this.CloneRepeater(sender as Repeater);
      child.DataSource = (object) dataItem.Items;
      control.Controls.Add((Control) child);
      child.DataBind();
    }

    private static IEnumerable<ContentTypeTreeItemContext> BuildContentTypeTreeHierarchy(
      IEnumerable<ContentTypeTreeItemContext> contentTypeTreeItems)
    {
      foreach (ContentTypeTreeItemContext contentTypeTreeItem1 in contentTypeTreeItems)
      {
        ContentTypeTreeItemContext contentTypeTreeItem = contentTypeTreeItem1;
        contentTypeTreeItem.Items = contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct => ct.ParentContentTypeId == contentTypeTreeItem.ContentTypeId)).ToArray<ContentTypeTreeItemContext>();
      }
      return contentTypeTreeItems.Where<ContentTypeTreeItemContext>((Func<ContentTypeTreeItemContext, bool>) (ct =>
      {
        Guid parentContentTypeId = ct.ParentContentTypeId;
        return Guid.Empty.Equals(ct.ParentContentTypeId);
      }));
    }

    private Repeater CloneRepeater(Repeater source)
    {
      Repeater repeater = new Repeater();
      repeater.HeaderTemplate = source.HeaderTemplate;
      repeater.ItemTemplate = source.ItemTemplate;
      repeater.FooterTemplate = source.FooterTemplate;
      repeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      return repeater;
    }

    /// <summary>
    /// Gets the fields names text to be shown in content types hierarchical tree.
    /// </summary>
    /// <param name="contentTypeId">The content type id.</param>
    /// <returns>The text for fields names</returns>
    private string GetFieldsText(Guid contentTypeId)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IDynamicModuleType dynamicModuleType = ModuleBuilderManager.GetModules().First<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == this.ModuleId)).Types.First<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.Id == contentTypeId));
      List<Guid> sectionIds = dynamicModuleType.Sections.OrderBy<IFieldsBackendSection, int>((Func<IFieldsBackendSection, int>) (s => s.Ordinal)).Select<IFieldsBackendSection, Guid>((Func<IFieldsBackendSection, Guid>) (s => s.Id)).ToList<Guid>();
      IOrderedEnumerable<IDynamicModuleField> source = dynamicModuleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.None)).OrderBy<IDynamicModuleField, int>((Func<IDynamicModuleField, int>) (f => sectionIds.IndexOf(f.ParentSectionId))).ThenBy<IDynamicModuleField, int>((Func<IDynamicModuleField, int>) (f => f.Ordinal));
      int index;
      for (index = 0; index < source.Count<IDynamicModuleField>(); ++index)
      {
        IDynamicModuleField dynamicModuleField = source.ElementAt<IDynamicModuleField>(index);
        if (stringBuilder.Length + dynamicModuleField.Name.Length <= 30 || index == 0)
        {
          stringBuilder.Append(dynamicModuleField.Name);
          stringBuilder.Append(", ");
        }
        else
          break;
      }
      int startIndex = stringBuilder.Length - ", ".Length;
      stringBuilder.Replace(", ", string.Empty, startIndex, ", ".Length);
      if (index < source.Count<IDynamicModuleField>() - 1)
        stringBuilder.Append("...");
      return stringBuilder.ToString();
    }
  }
}
