// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeTreeItemContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// This class represents the state of the content type data used in content types tree view
  /// </summary>
  [DataContract]
  internal class ContentTypeTreeItemContext
  {
    /// <summary>Gets or sets the id of the content type.</summary>
    [DataMember(Name = "value")]
    public Guid ContentTypeId { get; set; }

    /// <summary>Gets or sets the name of the content type.</summary>
    [DataMember(Name = "text")]
    public string Text { get; set; }

    /// <summary>Gets or sets the child items of the content type.</summary>
    [DataMember(Name = "items")]
    public ContentTypeTreeItemContext[] Items { get; set; }

    /// <summary>Gets or sets the id of the parent content type .</summary>
    /// <value>The id of the parent content type.</value>
    public Guid ParentContentTypeId { get; set; }
  }
}
