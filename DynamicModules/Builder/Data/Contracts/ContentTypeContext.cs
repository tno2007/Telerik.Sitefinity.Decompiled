// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.ContentTypeContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// This class represents the state of the content type data created or edited through the
  /// wizard for creating or editing a content type.
  /// </summary>
  [DataContract]
  public class ContentTypeContext
  {
    /// <summary>Gets or sets the id of the content type.</summary>
    [DataMember]
    public Guid ContentTypeId { get; set; }

    /// <summary>Gets or sets the module id.</summary>
    /// <value>The module id.</value>
    [DataMember]
    public Guid ModuleId { get; set; }

    /// <summary>Gets or sets the id of the content type.</summary>
    [DataMember]
    public Guid ContentTypePageId { get; set; }

    /// <summary>Gets or sets the id of the parent content type .</summary>
    /// <value>The id of the parent content type.</value>
    [DataMember]
    public Guid ParentContentTypeId { get; set; }

    /// <summary>Gets or sets the name of the content type.</summary>
    [DataMember]
    public string ContentTypeName { get; set; }

    /// <summary>Gets or sets the title of the content type.</summary>
    [DataMember]
    public string ContentTypeTitle { get; set; }

    /// <summary>Gets or sets the status of the content type.</summary>
    [DataMember]
    public string ContentTypeStatus { get; set; }

    /// <summary>Gets or sets the status of the content type.</summary>
    [DataMember]
    public string ContentTypeStatusTitle { get; set; }

    /// <summary>Gets or sets the description of the content type.</summary>
    [DataMember]
    public string ContentTypeDescription { get; set; }

    /// <summary>
    /// Gets or sets the last modified date of the content type.
    /// </summary>
    [DataMember]
    public string LastModified { get; set; }

    /// <summary>Gets or sets the title of the content type item.</summary>
    [DataMember]
    public string ContentTypeItemTitle { get; set; }

    /// <summary>Gets or sets the name of the content type item.</summary>
    [DataMember]
    public string ContentTypeItemName { get; set; }

    /// <summary>Gets or sets the fields of the content type items.</summary>
    [DataMember]
    public ContentTypeItemFieldContext[] Fields { get; set; }

    /// <summary>
    /// Gets or sets the main short text field for the content type item.
    /// </summary>
    [DataMember]
    public string MainShortTextFieldName { get; set; }

    /// <summary>
    /// Gets or sets the name of the owner of the content type
    /// </summary>
    [DataMember]
    public string Owner { get; set; }

    /// <summary>Gets or sets the has same type children.</summary>
    /// <value>The has same type children.</value>
    [DataMember]
    public bool IsSelfReferencing { get; set; }

    /// <summary>Gets or sets the update context.</summary>
    /// <value>The update context.</value>
    [DataMember]
    public string UpdateContext { get; set; }

    /// <summary>Gets or sets the check field permissions.</summary>
    /// <value>The check field permissions.</value>
    [DataMember]
    public bool CheckFieldPermissions { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the type is deletable
    /// </summary>
    /// <value>The is deletable indicator.</value>
    [DataMember]
    public bool IsDeletable { get; set; }

    /// <summary>Gets or sets the origin.</summary>
    /// <value>The origin.</value>
    [DataMember]
    public string Origin { get; set; }
  }
}
