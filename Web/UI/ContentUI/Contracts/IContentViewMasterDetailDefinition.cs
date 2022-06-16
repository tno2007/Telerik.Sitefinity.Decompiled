// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDetailDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>Declares the contract for the MasterDetailView.</summary>
  public interface IContentViewMasterDetailDefinition : IContentViewDefinition, IDefinition
  {
    /// <summary>Gets or sets the master definition.</summary>
    /// <value>The master definition.</value>
    IContentViewMasterDefinition MasterDefinition { get; }

    /// <summary>Gets or sets the detail definition.</summary>
    /// <value>The detail definition.</value>
    IContentViewDetailDefinition DetailDefinition { get; }
  }
}
