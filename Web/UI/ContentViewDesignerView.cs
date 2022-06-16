// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// Base abstract class that provides common implementation for all designers
  /// for <see cref="N:Telerik.Sitefinity.Web.UI.ControlDesign.ContentView" /> based controls.
  /// </summary>
  public abstract class ContentViewDesignerView : ControlDesignerView
  {
    protected bool isControlDefinitionProviderCorrect = true;
    protected IManager contentManager;

    /// <summary>Gets the current content view.</summary>
    /// <value>The current content view.</value>
    protected IContentView CurrentContentView => this.ParentDesigner != null && this.ParentDesigner.PropertyEditor != null ? this.ParentDesigner.PropertyEditor.Control as IContentView : (IContentView) null;

    /// <summary>
    /// Gets the content manager for the content shown by content view
    /// </summary>
    /// <value>The content manager.</value>
    protected virtual IManager ContentManager
    {
      get
      {
        if (this.contentManager == null)
          this.InitializeContentManager();
        return this.contentManager;
      }
    }

    /// <summary>
    /// Gets or sets the correctness of the provider from the ControlDefinition.
    /// </summary>
    protected virtual bool IsControlDefinitionProviderCorrect
    {
      get => this.isControlDefinitionProviderCorrect;
      set => this.isControlDefinitionProviderCorrect = value;
    }

    /// <summary>Retrieves the UI culture from the PropertyEditor.</summary>
    /// <returns></returns>
    protected virtual string GetUICulture()
    {
      string uiCulture = (string) null;
      if (this.ParentDesigner != null && this.ParentDesigner.PropertyEditor != null)
        uiCulture = this.ParentDesigner.PropertyEditor.PropertyValuesCulture;
      return uiCulture;
    }

    /// <summary>Initializes the content manager.</summary>
    protected void InitializeContentManager()
    {
      if (this.CurrentContentView == null)
        return;
      string providerName = this.CurrentContentView.ControlDefinition.ProviderName;
      try
      {
        this.contentManager = ManagerBase.GetMappedManager(this.CurrentContentView.ControlDefinition.ContentType, providerName);
      }
      catch (ConfigurationErrorsException ex)
      {
        this.contentManager = ManagerBase.GetMappedManager(this.CurrentContentView.ControlDefinition.ContentType);
        this.IsControlDefinitionProviderCorrect = false;
      }
    }
  }
}
