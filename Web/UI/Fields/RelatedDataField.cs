// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RelatedDataField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>This class represents related data field control.</summary>
  [FieldDefinitionElement(typeof (RelatedDataFieldDefinitionElement))]
  public class RelatedDataField : FieldControl
  {
    private string relatedDataType;
    private string relatedDataProvider;
    private bool allowMultipleSelection;
    internal const string ScriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatedDataField.js";
    internal const string RelatedDataFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatedDataField.js";
    private const string ContentLinkChangeStateScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLinkChangeState.js";
    private const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    private static readonly string LayoutTemplatePathLocal = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RelatedDataField.ascx");

    /// <summary>
    /// Gets or sets the provider of the content that can be related.
    /// </summary>
    public string RelatedDataProvider
    {
      get => this.relatedDataProvider;
      set => this.relatedDataProvider = value;
    }

    /// <summary>
    /// Gets or sets the type of the content that can be related.
    /// </summary>
    public string RelatedDataType
    {
      get => this.relatedDataType;
      set => this.relatedDataType = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    public Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the error message element for the field control.
    /// </summary>
    public Label ErrorMessageLabel => this.Container.GetControl<Label>("errorMessageLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    public Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>Gets the wrapper control of the field.</summary>
    public Panel FieldBodyWrapper => this.Container.GetControl<Panel>("fieldBodyWrapper", true);

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Type contentType = TypeResolutionService.ResolveType(this.RelatedDataType, false);
      if (contentType != (Type) null)
      {
        this.CanResolveContentType = true;
        this.IsModuleEnabledForCurrentSite = RelatedDataHelper.IsModuleEnabledForCurrentSite(contentType, this.RelatedDataProvider);
      }
      else
      {
        this.CanResolveContentType = false;
        this.IsModuleEnabledForCurrentSite = false;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ErrorMessageLabel.Visible = false;
      if (this.CanResolveContentType && this.IsModuleEnabledForCurrentSite)
        return;
      this.FieldBodyWrapper.Visible = false;
      this.ErrorMessageLabel.Text = Res.Get<ModuleEditorResources>().DeletedModuleWarningForField;
      this.ErrorMessageLabel.Visible = true;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IRelatedDataFieldDefinition dataFieldDefinition))
        return;
      this.RelatedDataProvider = !(dataFieldDefinition.RelatedDataProvider == "sf-site-default-provider") ? dataFieldDefinition.RelatedDataProvider : RelatedDataHelper.ResolveProvider(dataFieldDefinition.RelatedDataType);
      this.RelatedDataType = dataFieldDefinition.RelatedDataType;
      this.AllowMultipleSelection = dataFieldDefinition.AllowMultipleSelection;
      this.Title = dataFieldDefinition.Title;
      this.Description = dataFieldDefinition.Description;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = source.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (!this.CanResolveContentType || !this.IsModuleEnabledForCurrentSite)
      {
        controlDescriptor.AddProperty("_initializeControl", (object) false);
      }
      else
      {
        controlDescriptor.AddProperty("_siteBaseUrl", (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/"));
        controlDescriptor.AddProperty("_relatedDataService", (object) VirtualPathUtility.ToAbsolute("~/restapi/sitefinity/related-data"));
        controlDescriptor.AddProperty("_genericDataService", (object) VirtualPathUtility.ToAbsolute("~/restapi/sitefinity/generic-data"));
        controlDescriptor.AddProperty("_dataSourceService", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DataSourceService"));
        controlDescriptor.AddProperty("_relatedDataType", (object) this.RelatedDataType);
        controlDescriptor.AddProperty("_relatedDataProvider", (object) this.RelatedDataProvider);
        controlDescriptor.AddProperty("_allowMultipleSelection", (object) this.AllowMultipleSelection);
        controlDescriptor.AddProperty("_relatedTypeIdentifierField", (object) RelatedDataHelper.GetRelatedTypeIdentifierField(this.RelatedDataType));
        controlDescriptor.AddProperty("_isBackendReadMode", (object) (bool) (this.DisplayMode != FieldDisplayMode.Read ? 0 : (this.IsBackend() ? 1 : 0)));
        controlDescriptor.AddProperty("_isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
        Type itemType = TypeResolutionService.ResolveType(this.RelatedDataType, false);
        string str1 = itemType != (Type) null ? RelatedDataResponseHelper.GetInsertViewUrl(itemType) : string.Empty;
        controlDescriptor.AddProperty("_createRelatedItemUrl", (object) str1);
        if (itemType.IsSubclassOf(typeof (DynamicContent)))
          controlDescriptor.AddProperty("_relatedDataSupportsMultilingualSearch", (object) false);
        string itemTypeTitle = RelatedDataHelper.GetItemTypeTitle(Res.Get<ModuleEditorResources>().RelatedDataSelectorTitle, this.RelatedDataType, true);
        controlDescriptor.AddProperty("_relatedDataSelectorTitle", (object) itemTypeTitle);
        SortingExpressionElement expressionElement = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().SortingExpressionSettings.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.ContentType == this.RelatedDataType && !s.IsCustom)).FirstOrDefault<SortingExpressionElement>();
        string str2 = expressionElement != null ? expressionElement.SortingExpression : "LastModified DESC";
        controlDescriptor.AddProperty("_defaultRelatedItemsSortExpression", (object) str2);
        controlDescriptor.AddProperty("_siteId", (object) SystemManager.CurrentContext.CurrentSite.Id);
      }
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used. By default
    /// it returns current type.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (RelatedDataField).FullName;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = RelatedDataField.LayoutTemplatePathLocal;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (RelatedDataField).Assembly.FullName;
      return (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
      {
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatedDataField.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatedDataField.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLinkChangeState.js", fullName)
      };
    }

    private bool CanResolveContentType { get; set; }

    private bool IsModuleEnabledForCurrentSite { get; set; }
  }
}
