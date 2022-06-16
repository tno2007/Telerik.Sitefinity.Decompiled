// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>Base class for the views of the DynamicContentView</summary>
  public abstract class DynamicContentViewBase : SimpleView
  {
    private ModuleBuilderManager moduleBuilderManager;
    private DynamicModuleManager dynamicManager;
    private DynamicContentView host;
    private DynamicModuleType dynamicModuleType;

    public DynamicContentViewBase(DynamicModuleManager dynamicModuleManager = null) => this.dynamicManager = dynamicModuleManager;

    /// <summary>Gets or sets the title of this widget.</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the type displayed by this widget.</summary>
    public Type DynamicContentType { get; set; }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" /> to be used by this widget.
    /// </summary>
    protected virtual ModuleBuilderManager ModuleManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleManager" /> to be used by this widget.
    /// </summary>
    protected virtual DynamicModuleManager DynamicManager
    {
      get
      {
        if (this.dynamicManager == null)
          this.dynamicManager = DynamicModuleManager.GetManager(DynamicModuleManager.GetDefaultProviderName(this.DynamicModuleType.ModuleName));
        return this.dynamicManager;
      }
    }

    /// <summary>Gets the title control.</summary>
    /// <value>The title control.</value>
    protected virtual SitefinityLabel TitleControl => this.Container.GetControl<SitefinityLabel>("title", false);

    /// <summary>
    /// Gets or sets the container for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView" />.
    /// </summary>
    public DynamicContentView Host
    {
      get => this.host;
      set
      {
        this.host = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the the dynamic module type.</summary>
    /// <value>The dynamic module type.</value>
    internal DynamicModuleType DynamicModuleType
    {
      get
      {
        if (this.dynamicModuleType == null)
          this.dynamicModuleType = this.ModuleManager.GetDynamicModuleType(this.DynamicContentType.FullName);
        return this.dynamicModuleType;
      }
      set => this.dynamicModuleType = value;
    }
  }
}
