// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfControlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.UI.ClientBinders;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>
  /// Represents the control property and is used for transferring that data through WCF.
  /// </summary>
  [DataContract]
  [DebuggerDisplay("[WcfControlProperty] {PropertyName}, PropertyValue={PropertyValue}")]
  public class WcfControlProperty : IFormElement
  {
    private Guid propertyId;
    private string propertyName;
    private string originalPropertyName;
    private string propertyPath;
    private string itemTypeName;
    private string clientPropertyTypeName;
    private int inCategoryOrdinal;
    private string elementCssClass;
    private string categoryName;
    private string categoryNameSafe;
    private string propertyValue;
    private object originalPropertyValue;
    private bool needsEditor;
    private string typeEditor;
    private bool isEditable = true;

    [DataMember]
    public bool IsEditable
    {
      get => this.isEditable;
      set => this.isEditable = value;
    }

    /// <summary>Gets or sets the pageId of the property.</summary>
    [DataMember]
    public virtual Guid PropertyId
    {
      get => this.propertyId;
      set => this.propertyId = value;
    }

    /// <summary>Gets or sets the original name of the property.</summary>
    internal virtual string OriginalPropertyName
    {
      get => this.originalPropertyName;
      set => this.originalPropertyName = value;
    }

    /// <summary>Gets or sets the name of the property.</summary>
    [DataMember]
    public virtual string PropertyName
    {
      get => this.propertyName;
      set => this.propertyName = value;
    }

    /// <summary>
    /// Gets or sets the path of the property. Path of the property
    /// determines the property's position in the hierarchy of
    /// properties.
    /// </summary>
    [DataMember]
    public virtual string PropertyPath
    {
      get => this.propertyPath;
      set => this.propertyPath = value;
    }

    /// <summary>Gets or sets the full name of the property type.</summary>
    [DataMember]
    public virtual string ItemTypeName
    {
      get => this.itemTypeName;
      set => this.itemTypeName = value;
    }

    /// <summary>
    /// Gets or sets the property type name that can be understoon by the client scripts
    /// </summary>
    [DataMember]
    public virtual string ClientPropertyTypeName
    {
      get => this.clientPropertyTypeName;
      set => this.clientPropertyTypeName = value;
    }

    /// <summary>
    /// Gets or sets the ordinal (number determining the position) of the
    /// property, when displayed in the category mode.
    /// </summary>
    [DataMember]
    public virtual int InCategoryOrdinal
    {
      get => this.inCategoryOrdinal;
      set => this.inCategoryOrdinal = value;
    }

    /// <summary>
    /// Gets or sets the css class that should be applied to the property.
    /// Depending on it's type, properties will be assigned different
    /// css classes.
    /// </summary>
    [DataMember]
    public virtual string ElementCssClass
    {
      get => this.elementCssClass;
      set => this.elementCssClass = value;
    }

    /// <summary>
    /// Gets or sets the name of the category to which the property belongs to.
    /// </summary>
    [DataMember]
    public virtual string CategoryName
    {
      get => this.categoryName;
      set => this.categoryName = value;
    }

    /// <summary>
    /// Gets or sets the safe name (one that can be safely used on the
    /// client for pageId of the element) of the category.
    /// </summary>
    [DataMember]
    public virtual string CategoryNameSafe
    {
      get => this.categoryNameSafe;
      set => this.categoryNameSafe = value;
    }

    /// <summary>Gets or sets the value of the property.</summary>
    [DataMember]
    public virtual string PropertyValue
    {
      get => this.propertyValue;
      set => this.propertyValue = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether property needs
    /// a special editor.
    /// </summary>
    [DataMember]
    public virtual bool NeedsEditor
    {
      get => this.needsEditor;
      set => this.needsEditor = value;
    }

    [DataMember]
    public virtual bool IsProxy { get; set; }

    /// <summary>
    /// Gets of sets the value determining the type of the editor
    /// that ought to be used for editing the property.
    /// </summary>
    [DataMember]
    public virtual string TypeEditor
    {
      get => this.typeEditor;
      set => this.typeEditor = value;
    }

    /// <summary>Gets or sets the original value of the property.</summary>
    internal object OriginalPropertyValue
    {
      get => this.originalPropertyValue;
      set => this.originalPropertyValue = value;
    }

    /// <summary>
    /// Gets of sets if the property can contain dynamic links that should be resolved
    /// </summary>
    internal bool SupportsDynamicLinks { get; set; }
  }
}
