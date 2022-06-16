// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.CustomSortingDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements
{
  /// <summary>Dialog for defining custom sorting expressions.</summary>
  public class CustomSortingDialog : AjaxDialogBase
  {
    public const string DialogScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.CustomSortingDialog.js";
    public const string SortConditionItemScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortConditionItem.js";
    public const string SortConditionScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortCondition.js";
    public const string SortDataScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortData.js";
    public const string SortDataItemScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortDataItem.js";
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.CustomSortingDialog.ascx");
    private Type contentType;
    private Guid dynamicModuleTypeId;
    private List<FieldType> dynamicTypeUnsupportedFieldTypes = new List<FieldType>()
    {
      FieldType.Media,
      FieldType.MultipleChoice,
      FieldType.Classification,
      FieldType.RelatedMedia,
      FieldType.RelatedData,
      FieldType.Address,
      FieldType.GuidArray,
      FieldType.Choices,
      FieldType.Guid,
      FieldType.Unknown
    };

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CustomSortingDialog.TemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the type of the content item.</summary>
    /// <value>The type of the content.</value>
    protected virtual Type ContentType
    {
      get
      {
        if (this.contentType == (Type) null && !string.IsNullOrEmpty(this.Page.Request.QueryString["contentType"]))
          this.contentType = TypeResolutionService.ResolveType(this.Page.Request.QueryString["contentType"]);
        return this.contentType;
      }
    }

    /// <summary>Gets the type of the dynamic module.</summary>
    /// <value>The type of the dynamic module.</value>
    protected virtual Guid DynamicModuleTypeId
    {
      get
      {
        if (this.dynamicModuleTypeId == Guid.Empty && !string.IsNullOrEmpty(this.Page.Request.QueryString["dynamicModuleTypeId"]))
          this.dynamicModuleTypeId = new Guid(this.Page.Request.QueryString["dynamicModuleTypeId"]);
        return this.dynamicModuleTypeId;
      }
    }

    /// <summary>Gets the reference to remove all sorting rules link.</summary>
    /// <value>The remove all link button.</value>
    protected internal virtual LinkButton RemoveAllLink => this.Container.GetControl<LinkButton>("removeAllLink", true);

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>
    /// Gets the control that will contain all sort conditions that are added.
    /// </summary>
    /// <value>The container control.</value>
    protected internal virtual Control ContainerControl => this.Container.GetControl<Control>("sortConditionContainer", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets the fields of a specified content type that can be included in a sorting expressions.
    /// The sortable fields are marked with the <see cref="T:Telerik.Sitefinity.SortableAttribute" /> attribute.
    /// </summary>
    /// <returns> A collection of sortable field names.</returns>
    public virtual IList<KeyValuePair<string, string>> GetSortableFields()
    {
      List<KeyValuePair<string, string>> sortableFields = new List<KeyValuePair<string, string>>();
      if (this.DynamicModuleTypeId == Guid.Empty)
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.ContentType);
        bool flag = false;
        if (this.ContentType == typeof (PageData))
          flag = true;
        foreach (PropertyDescriptor propertyDescriptor in properties)
        {
          if (propertyDescriptor.Attributes[typeof (SortableAttribute)] is SortableAttribute attribute)
          {
            string key = propertyDescriptor.Name;
            if (flag)
              key = string.Format("Page.{0}", (object) key);
            sortableFields.Add(new KeyValuePair<string, string>(key, attribute.DisplayName ?? propertyDescriptor.DisplayName));
          }
        }
      }
      else
      {
        ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
        Guid typeId = this.dynamicModuleTypeId;
        DynamicModuleType dynamicModuleType = manager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.Id == typeId)).FirstOrDefault<DynamicModuleType>();
        if (dynamicModuleType != null)
        {
          manager.LoadDynamicModuleTypeGraph(dynamicModuleType, true);
          foreach (DynamicModuleField field in dynamicModuleType.Fields)
          {
            if (!this.dynamicTypeUnsupportedFieldTypes.Contains(field.FieldType))
              sortableFields.Add(new KeyValuePair<string, string>(field.Name, field.Title));
          }
        }
      }
      sortableFields.Insert(0, new KeyValuePair<string, string>("select", Res.Get<Labels>().SelectCriteria));
      return (IList<KeyValuePair<string, string>>) sortableFields;
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
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>(this.GetBaseScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CustomSortingDialog).FullName, this.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      IList<KeyValuePair<string, string>> sortableFields = this.GetSortableFields();
      if (sortableFields.Count > 0)
        controlDescriptor.AddProperty("typeProperties", (object) scriptSerializer.Serialize((object) sortableFields));
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddProperty("containerId", (object) this.ContainerControl.ClientID);
      controlDescriptor.AddProperty("removeAllLink", (object) this.RemoveAllLink.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (CustomSortingDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.CustomSortingDialog.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortConditionItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortData.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortDataItem.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.SortCondition.js", fullName),
        new ScriptReference()
        {
          Assembly = "Telerik.Web.UI",
          Name = "Telerik.Web.UI.Common.Core.js"
        }
      }.ToArray();
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxTemplates;

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
