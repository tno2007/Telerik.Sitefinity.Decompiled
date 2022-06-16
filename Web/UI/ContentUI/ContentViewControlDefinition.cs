// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Contains information to construct a ContentView control.
  /// </summary>
  public class ContentViewControlDefinition : 
    DefinitionBase,
    IContentViewControlDefinition,
    IDefinition
  {
    private string controlDefinitionName;
    private string providerName;
    private bool? useWorkflow;
    private Type contentType;
    private Type managerType;
    private ViewDefinitionCollection views;
    private IEnumerable<IDialogDefinition> dialogs;
    private bool allViewsLoaded;

    /// <summary>
    /// Provides a single point for initializing <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewControlDefinition" /> instances
    /// providing an extensibility point for their modification.
    /// </summary>
    /// <param name="controlDefinitionName">Name of the control definition.</param>
    /// <returns></returns>
    internal static ContentViewControlDefinition Initialize(
      string controlDefinitionName)
    {
      ContentViewControlDefinition contentViewControlDefinition = new ContentViewControlDefinition()
      {
        ControlDefinitionName = controlDefinitionName
      };
      foreach (IControlDefinitionExtender definitionExtender in ObjectFactory.Container.ResolveAll<IControlDefinitionExtender>())
        definitionExtender.ExtendDefinition((IContentViewControlDefinition) contentViewControlDefinition);
      return contentViewControlDefinition;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewControlDefinition" /> class.
    /// </summary>
    public ContentViewControlDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewControlDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewControlDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewControlDefinition GetDefinition() => this;

    /// <summary>Gets the configuration definition.</summary>
    /// <returns></returns>
    protected override ConfigElement GetConfigurationDefinition() => !string.IsNullOrEmpty(this.controlDefinitionName) ? (ConfigElement) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[this.controlDefinitionName] : base.GetConfigurationDefinition();

    /// <summary>Gets or sets the name of the definition.</summary>
    /// <value>The name of the definition.</value>
    [PropertyPersistence(IsKey = true)]
    public virtual string ControlDefinitionName
    {
      get => this.ResolveProperty<string>(nameof (ControlDefinitionName), this.controlDefinitionName);
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the type of the content view.</summary>
    /// <value>The type of the content.</value>
    public virtual Type ContentType
    {
      get => this.ResolveProperty<Type>(nameof (ContentType), this.contentType);
      set => this.contentType = value;
    }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    public virtual Type ManagerType
    {
      get => this.ResolveProperty<Type>(nameof (ManagerType), this.managerType);
      set => this.managerType = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public virtual string ProviderName
    {
      get => this.ResolveProperty<string>(nameof (ProviderName), this.providerName);
      set => this.providerName = value;
    }

    /// <summary>
    /// Gets or sets the value indicating weather this control should use workflow.
    /// </summary>
    /// <value>True if control ought to use workflow; otherwise false.</value>
    public virtual bool? UseWorkflow
    {
      get => this.ResolveProperty<bool?>(nameof (UseWorkflow), this.useWorkflow);
      set => this.useWorkflow = value;
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TypeConverter(typeof (GenericCollectionConverter))]
    public virtual ViewDefinitionCollection Views
    {
      get
      {
        if (this.views == null || !this.allViewsLoaded)
        {
          if (this.views == null)
            this.views = new ViewDefinitionCollection();
          ConfigElementDictionary<string, ContentViewDefinitionElement> viewsConfigPrivate = this.GetViewsConfig_private();
          if (viewsConfigPrivate != null)
          {
            foreach (ContentViewDefinitionElement element in viewsConfigPrivate.Elements)
            {
              if (!this.views.Contains(element.GetKey()))
                this.views.Add((IContentViewDefinition) element.GetDefinition());
            }
          }
          this.allViewsLoaded = true;
        }
        return this.views;
      }
    }

    /// <summary>Gets the collection of dialog definitions.</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public IEnumerable<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogs == null)
        {
          ConfigElementDictionary<string, DialogElement> dialogsConfigPrivate = this.GetDialogsConfig_private();
          this.dialogs = dialogsConfigPrivate == null ? (IEnumerable<IDialogDefinition>) new List<IDialogDefinition>() : dialogsConfigPrivate.Elements.Select<DialogElement, IDialogDefinition>((Func<DialogElement, IDialogDefinition>) (i => (IDialogDefinition) i.GetDefinition()));
        }
        return this.dialogs;
      }
    }

    /// <summary>Gets the default master view.</summary>
    /// <returns></returns>
    public IContentViewDefinition GetDefaultMasterView() => this.views != null ? this.views.GetFirstMasterView() : ((ContentViewControlElement) this.ConfigDefinition).GetDefaultMasterView();

    /// <summary>Gets the default detail view.</summary>
    /// <returns></returns>
    public IContentViewDefinition GetDefaultDetailView() => this.views != null ? this.views.GetFirstDetailView() : ((ContentViewControlElement) this.ConfigDefinition).GetDefaultDetailView();

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public bool TryGetView(string viewName, out IContentViewDefinition view)
    {
      if (this.views != null)
      {
        if (this.views.Contains(viewName))
        {
          view = this.views[viewName];
          return true;
        }
        if (this.allViewsLoaded)
        {
          view = (IContentViewDefinition) null;
          return false;
        }
      }
      IContentViewDefinition view1;
      if (((ContentViewControlElement) this.ConfigDefinition).TryGetView(viewName, out view1))
      {
        view = (IContentViewDefinition) view1.GetDefinition();
        if (this.views == null)
          this.views = new ViewDefinitionCollection();
        this.views.Add(view);
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
    public bool ContainsView(string viewName) => this.views != null && this.allViewsLoaded ? this.views.Contains(viewName) : ((ContentViewControlElement) this.ConfigDefinition).ContainsView(viewName);

    protected internal virtual ConfigElementDictionary<string, ContentViewDefinitionElement> GetViewsConfig_private() => this.ConfigDefinition != null ? ((ContentViewControlElement) this.ConfigDefinition).ViewsConfig : (ConfigElementDictionary<string, ContentViewDefinitionElement>) null;

    protected internal virtual ConfigElementDictionary<string, DialogElement> GetDialogsConfig_private() => this.ConfigDefinition != null ? ((ContentViewControlElement) this.ConfigDefinition).DialogsConfig : (ConfigElementDictionary<string, DialogElement>) null;
  }
}
