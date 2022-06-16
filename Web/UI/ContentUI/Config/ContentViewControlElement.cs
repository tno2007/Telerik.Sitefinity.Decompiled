// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Configuration element that represents a configuration of a given <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentView" /> control.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlDescription", Title = "ContentViewControlTitle")]
  [RestartAppOnChange]
  public class ContentViewControlElement : 
    DefinitionConfigElement,
    IContentViewControlDefinition,
    IDefinition
  {
    private ViewDefinitionCollection views;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ContentViewControlElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewControlDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name type of the content view.</summary>
    /// <value>The type of the content.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlContentTypeDescription", Title = "ContentViewControlContentType")]
    [ConfigurationProperty("contentType")]
    public string ContentTypeName
    {
      get => (string) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlManagerTypeDescription", Title = "ContentViewControlManagerType")]
    [ConfigurationProperty("managerType")]
    public string ManagerTypeName
    {
      get => (string) this["managerType"];
      set => this["managerType"] = (object) value;
    }

    /// <summary>Gets or sets the name of the definition.</summary>
    /// <value>The name of the definition.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlDefinitionNameDescription", Title = "ContentViewControlDefinitionName")]
    [ConfigurationProperty("definitionName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    public string ControlDefinitionName
    {
      get => (string) this["definitionName"];
      set => this["definitionName"] = (object) value;
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    [ConfigurationProperty("views")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlViewsDescription", Title = "ContentViewControlViews")]
    [ConfigurationCollection(typeof (ContentViewDefinitionElement), AddItemName = "view")]
    [TypeConverter(typeof (GenericCollectionConverter))]
    public ConfigElementDictionary<string, ContentViewDefinitionElement> ViewsConfig => (ConfigElementDictionary<string, ContentViewDefinitionElement>) this["views"];

    /// <summary>Gets or sets the type of the content view.</summary>
    /// <value>The type of the content.</value>
    public Type ContentType
    {
      get
      {
        string contentTypeName = this.ContentTypeName;
        if (!string.IsNullOrEmpty(contentTypeName))
        {
          Type contentType = TypeResolutionService.ResolveType(contentTypeName, false);
          if (contentType != (Type) null)
            return contentType;
        }
        return (Type) null;
      }
      set => this.ContentTypeName = value.FullName + ", " + value.Assembly.GetName().Name;
    }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    public Type ManagerType
    {
      get
      {
        string managerTypeName = this.ManagerTypeName;
        if (!string.IsNullOrEmpty(managerTypeName))
        {
          Type managerType = TypeResolutionService.ResolveType(managerTypeName, false);
          if (managerType != (Type) null)
            return managerType;
        }
        return (Type) null;
      }
      set => this.ManagerTypeName = value.FullName + ", " + value.Assembly.GetName().Name;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentProviderDescription", Title = "ContentProviderTitle")]
    [ConfigurationProperty("providerName", DefaultValue = "", IsRequired = true)]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets value indicating weather control ought to use workflow.
    /// </summary>
    /// <value>True if control ought to use workflow; otherwise false.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseWorkflowDescription", Title = "UseWorkflowTitle")]
    [ConfigurationProperty("useWorkflow", DefaultValue = true, IsRequired = false)]
    public bool? UseWorkflow
    {
      get => (bool?) this["useWorkflow"];
      set => this["useWorkflow"] = (object) value;
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    public virtual ViewDefinitionCollection Views
    {
      get
      {
        if (this.views == null)
          this.views = new ViewDefinitionCollection((IEnumerable<IContentViewDefinition>) this.ViewsConfig.Elements.Select<ContentViewDefinitionElement, ContentViewDefinition>((Func<ContentViewDefinitionElement, ContentViewDefinition>) (i => (ContentViewDefinition) i.GetDefinition())));
        return this.views;
      }
    }

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("dialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FrontendGridDialogsDescription", Title = "FrontendGridDialogsCaption")]
    [ConfigurationCollection(typeof (DialogElement), AddItemName = "dialog")]
    public ConfigElementDictionary<string, DialogElement> DialogsConfig => (ConfigElementDictionary<string, DialogElement>) this["dialogs"];

    /// <summary>Gets the collection of dialog definitions.</summary>
    public IEnumerable<IDialogDefinition> Dialogs => (IEnumerable<IDialogDefinition>) this.DialogsConfig.Elements.Select<DialogElement, DialogDefinition>((Func<DialogElement, DialogDefinition>) (i => (DialogDefinition) i.GetDefinition()));

    /// <summary>Gets the default master view.</summary>
    /// <returns></returns>
    public IContentViewDefinition GetDefaultMasterView() => (IContentViewDefinition) this.ViewsConfig.Elements.FirstOrDefault<ContentViewDefinitionElement>((Func<ContentViewDefinitionElement, bool>) (view => view.IsMasterView));

    /// <summary>Gets the default detail view.</summary>
    /// <returns></returns>
    public IContentViewDefinition GetDefaultDetailView() => (IContentViewDefinition) this.ViewsConfig.Elements.FirstOrDefault<ContentViewDefinitionElement>((Func<ContentViewDefinitionElement, bool>) (view => !view.IsMasterView));

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public bool TryGetView(string viewName, out IContentViewDefinition view)
    {
      ContentViewDefinitionElement definitionElement;
      if (this.ViewsConfig.TryGetValue(viewName, out definitionElement))
      {
        view = (IContentViewDefinition) definitionElement;
        return true;
      }
      view = (IContentViewDefinition) null;
      return false;
    }

    /// <summary>
    /// Determines whether the views collection contains view with the specified name.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if the view exists; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsView(string viewName) => this.ViewsConfig.Keys.Contains(viewName);
  }
}
