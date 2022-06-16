// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Backend
{
  /// <summary>
  /// A content view control which displays the a list of dynamic content items in the backend
  /// </summary>
  internal class DynamicContentMasterGridView : MasterGridView
  {
    private const string ChildTypesWidgetsID = "childTypesWidgets";
    internal readonly string dynamicContentMasterGridViewScript = "Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentMasterGridView.js";
    private DynamicModuleType dynamicModuleType;
    private ModuleBuilderManager moduleBuilderManager;
    private DynamicModule dynamicModule;
    private List<string> restrictedItemCommands = new List<string>();

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> to be used when installing
    /// the module.
    /// </summary>
    protected ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <inheritdoc />
    protected DynamicModule DynamicModule
    {
      get
      {
        if (this.dynamicModule == null)
          this.dynamicModule = this.ModuleBuilderMngr.GetDynamicModule(this.DynamicModuleType.ParentModuleId);
        return this.dynamicModule;
      }
    }

    /// <inheritdoc />
    protected DynamicModuleType DynamicModuleType
    {
      get
      {
        if (this.dynamicModuleType == null)
          this.dynamicModuleType = this.ModuleBuilderMngr.GetDynamicModuleType(this.Host.ControlDefinition.ContentType.FullName);
        return this.dynamicModuleType;
      }
    }

    /// <inheritdoc />
    protected internal override void InitializeProvider()
    {
      base.InitializeProvider();
      if (!this.Host.ControlDefinition.ProviderName.IsNullOrEmpty())
        return;
      this.Host.ControlDefinition.ProviderName = DynamicModuleManager.GetDefaultProviderName(this.DynamicModule.Name);
    }

    /// <summary>Gets the parent item from parameters.</summary>
    /// <param name="parameters">The parameters.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <returns>The parent data item.</returns>
    protected override IDataItem GetParentItemFromParameters(
      string[] parameters,
      UrlDataProviderBase provider,
      Type parentType)
    {
      string name = this.Page.Request.QueryString["lang"];
      string redirectUrl;
      if (name == null)
        return provider.GetItemFromUrl(parentType, "/" + string.Join("/", parameters), out redirectUrl);
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      try
      {
        SystemManager.CurrentContext.Culture = new CultureInfo(name);
        return provider.GetItemFromUrl(parentType, "/" + string.Join("/", parameters), out redirectUrl);
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
      }
    }

    /// <summary>Sets grid dialogs properties.</summary>
    /// <param name="dialogs">The dialogs.</param>
    /// <param name="itemsList"></param>
    internal override void SetDialogs(IList<IDialogDefinition> dialogs, ItemsListBase itemsList)
    {
      foreach (IDialogDefinition dialog in (IEnumerable<IDialogDefinition>) dialogs)
      {
        if (!string.IsNullOrEmpty(dialog.Parameters))
        {
          Dictionary<string, string> paramsDictionary = this.GetParamsDictionary(dialog.Parameters);
          string str;
          paramsDictionary.TryGetValue("overrideParent", out str);
          bool result = str == null || bool.TryParse(str, out result) & result;
          if (result && this.ParentItem != null)
          {
            bool flag = false;
            if (paramsDictionary.ContainsKey("parentId"))
            {
              paramsDictionary.Remove("parentId");
              flag = true;
            }
            if (paramsDictionary.ContainsKey("parentType"))
            {
              paramsDictionary.Remove("parentType");
              flag = true;
            }
            if (flag)
              dialog.Parameters = this.GetParamsString(paramsDictionary);
            dialog.Parameters += string.Format("&parentId={0}&parentType={1}", (object) this.ParentItem.Id, (object) this.ParentItem.GetType().FullName);
          }
        }
        else
          dialog.Parameters = string.Format("?parentId={0}&parentType={1}", (object) this.ParentItem.Id, (object) this.ParentItem.GetType().FullName);
        itemsList.Dialogs.Add(dialog);
      }
    }

    /// <summary>Initializes the items list base.</summary>
    /// <param name="itemsListBase">The items list base.</param>
    /// <param name="masterDefinition">The master definition.</param>
    protected override void InitializeItemsListBase(
      ItemsListBase itemsListBase,
      IMasterViewDefinition masterDefinition)
    {
      base.InitializeItemsListBase(itemsListBase, masterDefinition);
      itemsListBase.ContentType = this.Host.ControlDefinition.ContentType;
      itemsListBase.SupportsMultilingual = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual && DynamicTypesHelper.IsTypeLocalizable(itemsListBase.ContentType));
    }

    /// <inheritdoc />
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      base.InitializeControls(container, definition);
      this.BindChildTypesSelector();
      this.DetermineCurrentTitle();
    }

    /// <summary>
    /// Determines the current title from the parent item if possible, if not it is already set from the master view definition
    /// </summary>
    protected virtual void DetermineCurrentTitle()
    {
      if (!(this.ParentItem is DynamicContent parentItem) || DynamicContentExtensions.GetTitle(parentItem) == null)
        return;
      this.TitleControl.InnerHtml = HttpUtility.HtmlEncode(DynamicContentExtensions.GetTitle(parentItem));
    }

    /// <summary>
    /// Binds the silibling types control representing the child types selector
    /// </summary>
    protected virtual void BindChildTypesSelector()
    {
      Control control = this.Container.GetControl<Control>("childTypesWidgets", false);
      if (control == null || this.ChildTypesSelector == null || this.ParentItem == null)
        return;
      DynamicContent parentItem = (DynamicContent) this.ParentItem;
      if (parentItem.Id != Guid.Empty)
      {
        string cultureName = string.Empty;
        string str = this.Page.Request.QueryString["lang"];
        if (str != null)
        {
          this.ChildTypesSelector.Language = str;
          cultureName = str;
        }
        this.ChildTypesSelector.CurrentType = this.Host.ControlDefinition.ContentType;
        this.ChildTypesSelector.SystemParenUrl = parentItem.ItemDefaultUrl[cultureName];
        this.ChildTypesSelector.ProviderName = parentItem.Provider is DataProviderBase ? (parentItem.Provider as DataProviderBase).Name : (string) null;
        control.Visible = true;
      }
      else
        control.Visible = false;
    }

    /// <inheritdoc />
    internal override void SetLinks(IList<ILinkDefinition> links, ItemsListBase itemsList)
    {
      foreach (IDefinition link in (IEnumerable<ILinkDefinition>) links)
      {
        LinkDefinition definition = link.GetDefinition<LinkDefinition>();
        if (definition.CommandName == "parentComments" && this.ParentItem != null)
        {
          UrlDataProviderBase provider = (UrlDataProviderBase) this.Manager.Provider;
          string str = this.ParentItem is ILocatable parentItem ? provider.GetItemUrl(parentItem) : string.Empty;
          definition.NavigateUrl = string.Format(definition.NavigateUrl, (object) this.ParentItem.Id, (object) str);
        }
        if (BackendSiteMap.FindSiteMapNode(RouteHelper.ResolveNodeId(definition.NavigateUrl), true) != null)
        {
          definition.NavigateUrl = RouteHelper.ResolveUrl(definition.NavigateUrl, UrlResolveOptions.Rooted);
          string newValue = string.Empty;
          string name = (string) null;
          if (this.ParentItem != null)
          {
            DynamicContent parentItem = (DynamicContent) this.ParentItem;
            if (parentItem.SystemParentId != Guid.Empty)
            {
              name = this.Page.Request.QueryString["lang"];
              if (name != null)
              {
                CultureInfo culture = SystemManager.CurrentContext.Culture;
                try
                {
                  SystemManager.CurrentContext.Culture = new CultureInfo(name);
                  newValue = parentItem.SystemParentItem.SystemUrl;
                }
                finally
                {
                  SystemManager.CurrentContext.Culture = culture;
                }
              }
              else
                newValue = parentItem.SystemParentItem.SystemUrl;
            }
          }
          if (definition.NavigateUrl.Contains("{{SystemParentUrl}}"))
          {
            definition.NavigateUrl = definition.NavigateUrl.Replace("{{SystemParentUrl}}", newValue);
            if (!name.IsNullOrEmpty())
              definition.NavigateUrl = definition.NavigateUrl.IndexOf("?") != -1 ? definition.NavigateUrl + "&lang=" + name : definition.NavigateUrl + "?lang=" + name;
          }
          itemsList.Links.Add((ILinkDefinition) definition);
        }
        else
          this.restrictedItemCommands.Add(definition.CommandName);
      }
    }

    /// <summary>
    /// Gets the security root that is used to check permissions
    /// </summary>
    /// <returns></returns>
    protected override ISecuredObject GetSecurityRoot() => DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) this.ModuleBuilderMngr, (ISecuredObject) this.DynamicModuleType, this.Host.ControlDefinition.ProviderName);

    /// <summary>
    /// Determines from the parent item, what is the secured object used to evaluate command widgets permissions.
    /// </summary>
    protected override void DetermineSecuredObjectFromParent(IDataItem parentItem)
    {
    }

    /// <summary>
    /// Determines whether the providers list should be displayed for the current content type
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    protected override bool ShowProvidersList(Type contentType) => this.DynamicModuleType == null || !(this.DynamicModuleType.ParentModuleTypeId != Guid.Empty);

    /// <summary>Binds the providers list.</summary>
    public override void BindProvidersList()
    {
      bool? providerSelector = this.ShowProviderSelector;
      bool flag = true;
      if (providerSelector.GetValueOrDefault() == flag & providerSelector.HasValue)
        this.ProviderSelectorPanel.DynamicModuleName = this.DynamicModule.Name;
      base.BindProvidersList();
    }

    /// <inheritdoc />
    protected override void RedirectToModuleRoot()
    {
      if (this.DynamicModuleType == null || !(this.DynamicModuleType.ParentModuleTypeId != Guid.Empty))
        return;
      DynamicModuleType dynamicModuleType = this.DynamicModuleType;
      while (dynamicModuleType.ParentModuleType != null)
        dynamicModuleType = dynamicModuleType.ParentModuleType;
      PageNode pageNode = PageManager.GetManager().GetPageNode(dynamicModuleType.PageId);
      QueryStringBuilder collection = new QueryStringBuilder(this.Context.Request.QueryString.ToString());
      this.Page.Response.Redirect(RouteHelper.ResolveUrl(pageNode.GetUrl(), UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + collection.ToQueryString());
    }

    /// <inheritdoc />
    protected internal override object GetSecuredObject(
      IManager manager,
      Type securedObjectType,
      Guid securedObjectId)
    {
      return (object) SecurityUtility.GetSecuredObject(manager, securedObjectType.FullName, securedObjectId, this.Host.ControlDefinition.ProviderName);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference(this.dynamicContentMasterGridViewScript, typeof (DynamicContentMasterGridView).Assembly.FullName)
    };

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = typeof (DynamicContentMasterGridView).FullName;
      controlDescriptor.AddProperty("mainShortTextFieldName", (object) this.DynamicModuleType.MainShortTextFieldName);
      controlDescriptor.AddProperty("restrictedItemCommands", (object) this.restrictedItemCommands);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (DynamicContentMasterGridView).FullName;
  }
}
