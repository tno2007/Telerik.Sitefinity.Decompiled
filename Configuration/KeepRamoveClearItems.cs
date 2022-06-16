// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.KeepRemoveClearItemsAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Specifies the behavior of <see cref="T:Telerik.Sitefinity.Configuration.ConfigElementCollection">ConfigElementCollection</see>
  /// when remove or clear elements are declared in the configuration file.
  /// When Keep property is false, if clear element is encountered the entire collection is cleared,
  /// if remove element is encountered and there is a match in the collection to that
  /// element the item is removed.
  /// When Keep is true instead of clearing or removing items from the collection,
  /// new item is added that can be used at runtime to perform operation.
  /// The default value is true.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class KeepRemoveClearItemsAttribute : Attribute
  {
    private bool keep;

    /// <summary>
    /// Initializes KeepRemoveClearItemsAttribute instance with default value for Keep property.
    /// The default value is true.
    /// </summary>
    public KeepRemoveClearItemsAttribute()
      : this(true)
    {
    }

    /// <summary>
    /// Initializes KeepRemoveClearItemsAttribute instance with the specified value for Keep property.
    /// </summary>
    /// <param name="keep">The value assigned to Keep property.</param>
    public KeepRemoveClearItemsAttribute(bool keep) => this.keep = keep;

    /// <summary>
    /// Specifies the behavior of ConfigElementColleciotn for remove and clear elements.
    /// </summary>
    public bool Keep => this.keep;
  }
}
