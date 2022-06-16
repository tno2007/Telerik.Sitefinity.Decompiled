// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.DualResourceEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Data class which represents a resource in two cultures (display and edit) at once.
  /// </summary>
  [DataContract]
  public class DualResourceEntry
  {
    /// <summary>
    /// Gets or sets a value indicating whether the resource for display culture is built-in.
    /// </summary>
    /// <value><c>true</c> if [display built in]; otherwise, <c>false</c>.</value>
    [DataMember]
    public virtual bool DisplayBuiltIn { get; set; }

    /// <summary>
    /// Gets or sets the class pageId of the resource in the display culture.
    /// </summary>
    /// <value>The display class pageId.</value>
    [DataMember]
    public virtual string DisplayClassId { get; set; }

    /// <summary>
    /// Gets or sets the UI culture of the resource in the display culture.
    /// </summary>
    /// <value>The display UI culture.</value>
    [DataMember]
    public virtual string DisplayUICulture { get; set; }

    /// <summary>
    /// Gets or sets the description of the resource in the display culture.
    /// </summary>
    /// <value>The display description.</value>
    [DataMember]
    public virtual string DisplayDescription { get; set; }

    /// <summary>
    /// Gets or sets the key of the resource in the display culture.
    /// </summary>
    /// <value>The display key.</value>
    [DataMember]
    public virtual string DisplayKey { get; set; }

    /// <summary>
    /// Gets or sets the last modified date of the resource in the display culture.
    /// </summary>
    /// <value>The display last modified.</value>
    [DataMember]
    public virtual DateTime DisplayLastModified { get; set; }

    /// <summary>
    /// Gets or sets the value of the resource in the display culture.
    /// </summary>
    /// <value>The display value.</value>
    [DataMember]
    public virtual string DisplayValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the resource for edit culture is built-in.
    /// </summary>
    /// <value><c>true</c> if [edit built in]; otherwise, <c>false</c>.</value>
    [DataMember]
    public virtual bool EditBuiltIn { get; set; }

    /// <summary>
    /// Gets or sets the class pageId of the resource in the edit culture.
    /// </summary>
    /// <value>The edit class pageId.</value>
    [DataMember]
    public virtual string EditClassId { get; set; }

    /// <summary>
    /// Gets or sets the UI culture of the resource in the edit culture.
    /// </summary>
    /// <value>The edit UI culture.</value>
    [DataMember]
    public virtual string EditUICulture { get; set; }

    /// <summary>
    /// Gets or sets the description of the resource in the edit culture.
    /// </summary>
    /// <value>The edit description.</value>
    [DataMember]
    public virtual string EditDescription { get; set; }

    /// <summary>
    /// Gets or sets the key of the resource in the edit culture.
    /// </summary>
    /// <value>The edit key.</value>
    [DataMember]
    public virtual string EditKey { get; set; }

    /// <summary>
    /// Gets or sets the last modified date of the resource in the edit culture.
    /// </summary>
    /// <value>The edit last modified.</value>
    [DataMember]
    public virtual DateTime EditLastModified { get; set; }

    /// <summary>
    /// Gets or sets the value of the resource in the edit culture.
    /// </summary>
    /// <value>The edit value.</value>
    [DataMember]
    public virtual string EditValue { get; set; }
  }
}
