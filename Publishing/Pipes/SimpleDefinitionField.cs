// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.SimpleDefinitionField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>
  /// Implementation of IDefinitionField that is usable in WCF services
  /// </summary>
  [DataContract(Name = "DefinitionField", Namespace = "Telerik.Sitefinity.Publishing.Model")]
  [DebuggerDisplay("{Name}, Title={Title}, IsRequired: {IsRequired}")]
  public class SimpleDefinitionField : 
    IDefinitionField,
    ICopyProvider<SimpleDefinitionField>,
    ICopyProvider<IDefinitionField>
  {
    /// <summary>
    /// Create a required definition field with empty name and title
    /// </summary>
    public SimpleDefinitionField()
      : this(string.Empty, string.Empty, true)
    {
    }

    /// <summary>
    /// Create a required definition field whose name and title are identical
    /// </summary>
    /// <param name="name">Name and title of the field</param>
    public SimpleDefinitionField(string name)
      : this(name, name, true)
    {
    }

    /// <summary>
    /// Creates a definition field whose name and title are identical
    /// </summary>
    /// <param name="name">Name and title of the field</param>
    /// <param name="required">True if field is required, false otherwize</param>
    public SimpleDefinitionField(string name, bool required)
      : this(name, name, required)
    {
    }

    /// <summary>
    /// Create a required definition field with distinct title and name
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="title">Text for the UI</param>
    public SimpleDefinitionField(string name, string title)
      : this(name, title, true)
    {
    }

    /// <summary>
    /// Create a definition field with distinct title and name and explicitly set if it is required
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="title">Text for the UI</param>
    /// <param name="required">True if field is required, false otherwize</param>
    public SimpleDefinitionField(string name, string title, bool required)
    {
      this.Name = name;
      this.Title = title;
      this.IsRequired = required;
    }

    /// <summary>
    /// Text that is shown in the user interface for this field (e.g. Blog Title instead of BlogTitle)
    /// </summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Name of the field, as used in code and Type/Property Descriptors
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Determines whether a field is required or optional</summary>
    [DataMember]
    public bool IsRequired { get; set; }

    /// <summary>
    /// Gets or sets the assembly qualified name of the CLR type of this field.
    /// </summary>
    [DataMember]
    public string ClrType { get; set; }

    /// <summary>Gets or sets DB type of this field.</summary>
    [DataMember]
    public string DBType { get; set; }

    /// <summary>Gets or sets SQL DB type of this field.</summary>
    [DataMember]
    public string SQLDBType { get; set; }

    /// <summary>
    /// Gets or sets default value of this field (converted to string, in case its type supports default values)
    /// </summary>
    [DataMember]
    public string DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets default precision of this field (in case its type supports dprecision)
    /// </summary>
    [DataMember]
    public int Precision { get; set; }

    /// <summary>Gets or sets the maximum length of this fied</summary>
    [DataMember]
    public int MaxLength { get; set; }

    /// <summary>
    /// Gets or sets the taxonomy id of this field (in case its type represents a taxonomy)
    /// </summary>
    [DataMember]
    public string TaxonomyId { get; set; }

    [DataMember]
    public string TaxonomyProviderName { get; set; }

    [DataMember]
    public bool AllowMultipleTaxons { get; set; }

    [DataMember]
    public bool IsModified { get; set; }

    [DataMember]
    public bool IsMetaField { get; set; }

    [DataMember]
    public bool HideInUI { get; set; }

    public virtual void CopyTo(SimpleDefinitionField destination)
    {
      this.Copy((IDefinitionField) destination);
      destination.HideInUI = this.HideInUI;
      destination.IsMetaField = this.IsMetaField;
    }

    public virtual void CopyFrom(SimpleDefinitionField source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      source.Copy((IDefinitionField) this);
      this.HideInUI = source.HideInUI;
      this.IsMetaField = source.IsMetaField;
    }

    public void CopyTo(IDefinitionField destination) => this.Copy(destination);

    public void CopyFrom(IDefinitionField source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      source.Copy((IDefinitionField) this);
    }
  }
}
