// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Backend
{
  internal class DynamicContentDetailFormView : DetailFormView
  {
    private ModuleBuilderManager moduleBuilderManager;
    private string itemType;
    private Guid? itemId;
    private DynamicModuleManager dynamicModuleManager;
    private readonly string dynamicContentDetailFormViewScript = "Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentDetailFormView.js";

    private Guid? ItemId
    {
      get
      {
        if (!this.itemId.HasValue)
        {
          string input = this.Page.Request.QueryString["Id"];
          Guid result;
          if (!string.IsNullOrEmpty(input) && Guid.TryParse(input, out result))
            this.itemId = new Guid?(result);
        }
        return this.itemId;
      }
    }

    private ModuleBuilderManager ModuleBuilderManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <param name="definition"></param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (!string.IsNullOrEmpty(this.Page.Request.QueryString["provider"]) && this.Page.Request.QueryString["provider"] != "undefined")
        this.Host.ControlDefinition.ProviderName = this.Page.Request.QueryString["provider"];
      base.InitializeControls(container, definition);
      IDetailFormViewDefinition formViewDefinition = definition as IDetailFormViewDefinition;
      int startIndex = formViewDefinition.WebServiceBaseUrl.IndexOf("itemType=");
      if (startIndex <= -1)
        return;
      this.itemType = formViewDefinition.WebServiceBaseUrl.Substring(startIndex, formViewDefinition.WebServiceBaseUrl.Length - startIndex).Replace("itemType=", string.Empty);
    }

    /// <summary>Ensures the field controls.</summary>
    protected override void EnsureFieldControls()
    {
      if (string.IsNullOrEmpty(this.itemType))
        return;
      DynamicModuleType dynamicModuleType = this.ModuleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeNamespace + "." + t.TypeName == this.itemType)).FirstOrDefault<DynamicModuleType>();
      if (dynamicModuleType != null)
        this.ModuleBuilderManager.LoadDynamicModuleTypeGraph(dynamicModuleType, true);
      this.LoadControls(dynamicModuleType, this.sectionControls, this.FieldControls);
      this.LoadControls(dynamicModuleType, this.translationSectionControls, this.translationFieldControls);
      IDetailFormViewDefinition definition = this.Definition as IDetailFormViewDefinition;
      if (!this.SupportsMultiligual)
        return;
      bool? renderTranslationView = definition.IsToRenderTranslationView;
      if (!renderTranslationView.HasValue)
        return;
      renderTranslationView = definition.IsToRenderTranslationView;
      if (!renderTranslationView.Value)
        return;
      this.fieldControls.Add((Control) this.TranlationSelector);
    }

    /// <summary>
    /// Determines the base service url that should be used for the service calls in this view
    /// </summary>
    /// <param name="contentType"></param>
    /// <param name="definition"></param>
    protected override void DetermineServiceUrl(
      Type contentType,
      IDetailFormViewDefinition definition)
    {
      string fullName = contentType.FullName;
      string managerTypeName = string.Empty;
      Type type = (Type) null;
      string providerName = this.Host.ControlDefinition.ProviderName;
      string str1 = string.IsNullOrEmpty(definition.WebServiceBaseUrl) ? "~/Sitefinity/Services/Content/ContentService.svc/" : definition.WebServiceBaseUrl;
      if (!string.IsNullOrEmpty(definition.WebServiceBaseUrl))
        this.CancelChangesServiceUrl = VirtualPathUtility.ToAbsolute(definition.WebServiceBaseUrl);
      this.ContentLocationPreviewUrl = VirtualPathUtility.ToAbsolute("~/" + ContentLocationRoute.path);
      IManager manager;
      if (this.Host.ControlDefinition.ManagerType != (Type) null)
      {
        managerTypeName = this.Host.ControlDefinition.ManagerType.FullName;
        manager = ManagerBase.GetManager(managerTypeName, providerName);
      }
      else
        ManagerBase.TryGetMappedManager(contentType, providerName, out manager);
      if (manager != null && manager.Provider is IHierarchyProvider provider)
        type = provider.GetParentType(contentType);
      this.dynamicModuleManager = manager as DynamicModuleManager;
      if (type != (Type) null)
      {
        string str2 = "";
        if (this.Page.Request.QueryString["parentId"] != null)
          str2 = this.Page.Request.QueryString["parentId"];
        if (!str2.IsNullOrEmpty())
        {
          if (!str1.Contains("?"))
          {
            this.ServiceUrl = string.Format(str1 + "parent/{{{{parentId}}}}/?itemType={1}&parentItemType={2}&providerName={3}&managerType={4}&parentType={{{{parentType}}}}", (object) str2, (object) fullName, (object) type, (object) providerName, (object) managerTypeName);
            return;
          }
          this.ServiceUrl = string.Format(str1.Split('?')[0] + "parent/{{{{parentId}}}}/?itemType={1}&parentItemType={2}&providerName={3}&managerType={4}&parentType={{{{parentType}}}}", (object) str2, (object) fullName, (object) type, (object) providerName, (object) managerTypeName);
          return;
        }
      }
      this.ServiceUrl = string.Format(str1.Split('?')[0] + "?itemType={0}&providerName={1}&managerType={2}", (object) fullName, (object) providerName, (object) managerTypeName);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference(this.dynamicContentDetailFormViewScript, typeof (DynamicContentDetailFormView).Assembly.FullName)
    };

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = typeof (DynamicContentDetailFormView).FullName;
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    private void LoadControls(
      DynamicModuleType dataItemType,
      List<SectionControl> sectionContorlsList,
      List<Control> controlsList)
    {
      DynamicContent dynamicContentItem = this.GetDynamicContentItem(dataItemType);
      string providerName = this.Host.ControlDefinition.ProviderName;
      foreach (SectionControl sectionContorls in sectionContorlsList)
      {
        foreach (Control fieldControl1 in sectionContorls.FieldControls)
        {
          if (dataItemType != null)
          {
            FieldControl fieldControl = fieldControl1 as FieldControl;
            if (fieldControl != null)
            {
              DynamicModuleField dynamicModuleField = ((IEnumerable<DynamicModuleField>) dataItemType.Fields).FirstOrDefault<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == fieldControl.FieldName));
              if (dynamicModuleField != null && dynamicModuleField.SpecialType == FieldSpecialType.None)
              {
                if (dynamicContentItem == null)
                  fieldControl.Enabled = dynamicModuleField.IsEditable(providerName) && dynamicModuleField.IsVisible(providerName);
                else
                  fieldControl.Enabled = dynamicContentItem.IsDynamicFieldEditable(dynamicModuleField) && dynamicContentItem.IsDynamicFieldVisible(dynamicModuleField);
              }
            }
            if (fieldControl is ParentSelectorField)
              this.BindParents(fieldControl as ParentSelectorField, dataItemType);
            controlsList.Add(fieldControl1);
          }
        }
      }
    }

    private DynamicContent GetDynamicContentItem(DynamicModuleType dataItemType) => this.ItemId.HasValue ? this.dynamicModuleManager.GetDataItem(TypeResolutionService.ResolveType(dataItemType.GetFullTypeName()), this.ItemId.Value) : (DynamicContent) null;

    private void BindParents(
      ParentSelectorField parentSelectorField,
      DynamicModuleType dataItemType)
    {
      if (parentSelectorField == null)
        return;
      if (dataItemType != null && dataItemType.ParentModuleType != null)
      {
        parentSelectorField.ValidatorDefinition.Required = new bool?(true);
        string g = this.Page.Request.QueryString["parentId"];
        if (g.IsNullOrEmpty() || !g.IsGuid())
          return;
        Guid id = new Guid(g);
        DynamicContent dataItem = this.dynamicModuleManager.GetDataItem(TypeResolutionService.ResolveType(dataItemType.ParentModuleType.GetFullTypeName()), id);
        PropertyDescriptor typeMainProperty = ModuleBuilderManager.GetTypeMainProperty(dataItem.GetType());
        string empty = string.Empty;
        if (typeMainProperty is LstringPropertyDescriptor)
        {
          string name = this.Page.Request.QueryString["language"] ?? CultureInfo.InvariantCulture.Name;
          empty = ((LstringPropertyDescriptor) typeMainProperty).GetString((object) dataItem, CultureInfo.GetCultureInfo(name), false);
        }
        else
        {
          object obj = typeMainProperty.GetValue((object) dataItem);
          if (obj != null)
            empty = obj.ToString();
        }
        parentSelectorField.InitialValue = dataItem.Id.ToString();
        parentSelectorField.InitialText = empty;
      }
      else
        parentSelectorField.Visible = false;
    }
  }
}
