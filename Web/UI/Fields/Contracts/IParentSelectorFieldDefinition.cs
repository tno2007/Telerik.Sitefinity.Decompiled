// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IParentSelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that mandates the members that need to be implemented by any control
  /// that wishes to act as a parent selector field.
  /// </summary>
  public interface IParentSelectorFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the type of the items that are displayed by the selector.
    /// </summary>
    /// <value>The type of the items.</value>
    string ItemsType { get; set; }

    /// <summary>Gets or sets the web service URL used for binding.</summary>
    /// <value>The web service URL.</value>
    string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the main field that is displayed in the selector.
    /// </summary>
    /// <value>The name of the main field.</value>
    string MainFieldName { get; set; }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    /// <value>The data key names.</value>
    string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    /// <value>
    ///   <c>true</c> if searching is allowed otherwise, <c>false</c>.
    /// </value>
    bool AllowSearching { get; set; }
  }
}
