// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective master detail view control.
  /// </summary>
  public class ContentViewMasterDetailDefinition : 
    ContentViewDefinition,
    IContentViewMasterDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private IContentViewDetailDefinition detailDefinition;
    private IContentViewMasterDefinition masterDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDetailDefinition" /> class.
    /// </summary>
    public ContentViewMasterDetailDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewMasterDetailDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewMasterDetailDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewMasterDetailDefinition GetDefinition() => this;

    /// <summary>Gets or sets the master definition.</summary>
    /// <value>The master definition.</value>
    public IContentViewMasterDefinition MasterDefinition
    {
      get
      {
        if (this.masterDefinition == null)
          this.masterDefinition = (IContentViewMasterDefinition) this.ResolveProperty<IContentViewMasterDefinition>(nameof (MasterDefinition), this.masterDefinition).GetDefinition();
        return this.masterDefinition;
      }
      set => this.masterDefinition = value;
    }

    /// <summary>Gets or sets the detail definition.</summary>
    /// <value>The detail definition.</value>
    public IContentViewDetailDefinition DetailDefinition
    {
      get
      {
        if (this.detailDefinition == null)
          this.detailDefinition = (IContentViewDetailDefinition) this.ResolveProperty<IContentViewDetailDefinition>(nameof (DetailDefinition), this.detailDefinition).GetDefinition();
        return this.detailDefinition;
      }
      set => this.detailDefinition = value;
    }
  }
}
