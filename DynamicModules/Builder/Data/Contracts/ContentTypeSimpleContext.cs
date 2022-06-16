// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeSimpleContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// This class is used to update content types's name and description
  /// </summary>
  [DataContract]
  internal class ContentTypeSimpleContext
  {
    /// <summary>Gets or sets the id of the content type.</summary>
    [DataMember]
    public Guid ContentTypeId { get; set; }

    /// <summary>Gets or sets the title of the content type.</summary>
    [DataMember]
    public string ContentTypeTitle { get; set; }

    /// <summary>Gets or sets the description of the content type.</summary>
    [DataMember]
    public string ContentTypeDescription { get; set; }
  }
}
