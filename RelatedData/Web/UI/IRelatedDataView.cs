// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.Web.UI.IRelatedDataView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.RelatedData.Web.UI
{
  /// <summary>
  /// Defines the common information that should be implemented by each control
  /// that is able to show related items.
  /// </summary>
  public interface IRelatedDataView
  {
    /// <summary>
    ///  Gets or sets an instance of the <see cref="P:Telerik.Sitefinity.RelatedData.Web.UI.IRelatedDataView.RelatedDataDefinition" /> containing all information needed to display related data items.
    /// </summary>
    /// <value>The related data definition.</value>
    RelatedDataDefinition RelatedDataDefinition { get; set; }

    /// <summary>
    /// Gets or sets the URL key prefix. Used when building or evaluating URLs for paging and filtering
    /// </summary>
    /// <value>The URL key prefix.</value>
    string UrlKeyPrefix { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether related data items should be displayed.
    /// </summary>
    /// <value>
    /// Value indicating whether related data items should be displayed.
    /// </value>
    bool? DisplayRelatedData { get; set; }

    /// <summary>Gets the content type</summary>
    /// <returns></returns>
    string GetContentType();

    /// <summary>Gets the provider name</summary>
    /// <returns></returns>
    string GetProviderName();
  }
}
