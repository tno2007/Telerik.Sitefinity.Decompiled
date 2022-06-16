// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Messages.ContentLinkChange
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Services.RelatedData.Messages
{
  /// <summary>Represents the context for updating related data info</summary>
  [DataContract]
  public class ContentLinkChange
  {
    /// <summary>
    /// Determines the state of the updated related item. Possible values: 'Added', 'Removed'
    /// </summary>
    [DataMember]
    public ContentLinkChangeState State { get; set; }

    /// <summary>Gets or sets the Id of the item that is linked</summary>
    [DataMember]
    public Guid ChildItemId { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider for the child item
    /// </summary>
    [DataMember]
    public string ChildItemProviderName { get; set; }

    /// <summary>Gets or sets the type of the linked item.</summary>
    [DataMember]
    public virtual string ChildItemType { get; set; }

    /// <summary>
    /// Gets or sets additional, arbitrary, information about child item.
    /// </summary>
    [DataMember]
    public virtual string ChildItemAdditionalInfo { get; set; }

    /// <summary>Gets or sets the ordinal number.</summary>
    [DataMember]
    public virtual float? Ordinal { get; set; }

    /// <summary>
    /// Gets or sets the name of component property which links to the child item
    /// </summary>
    [DataMember]
    public virtual string ComponentPropertyName { get; set; }

    /// <summary>Indicates wheter the related child item is deleted</summary>
    [DataMember]
    internal virtual bool IsChildDeleted { get; set; }
  }
}
