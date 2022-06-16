// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IFileFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of text field element.
  /// </summary>
  public interface IFileFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the name of the single item being managed by the file field control.
    /// </summary>
    string ItemName { get; set; }

    /// <summary>
    /// Gets or sets the name of the single item in plural being managed by the file field control.
    /// </summary>
    string ItemNamePlural { get; set; }

    /// <summary>Gets or sets the type of the library content.</summary>
    /// <value>The type of the library content.</value>
    Type LibraryContentType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the uploader allows multiple files to be selected.
    /// </summary>
    bool IsMultiselect { get; set; }

    /// <summary>
    /// Gets or sets the maximum allowed number of files selected in the uploader.
    /// </summary>
    int MaxFileCount { get; set; }
  }
}
