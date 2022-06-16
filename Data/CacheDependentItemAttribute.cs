// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.CacheDependentItemAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public class CacheDependentItemAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.CacheDependentItemAttribute" /> class.
    /// </summary>
    /// <param name="HandlerType">Type of the handler.</param>
    /// <param name="ItemType">Type of the item.</param>
    /// <param name="key">The key.</param>
    public CacheDependentItemAttribute(Type handlerType, Type itemType, string key)
    {
      this.HandlerType = handlerType;
      this.ItemType = itemType;
      this.Key = key;
    }

    /// <summary>Gets or sets the type of the handler.</summary>
    /// <value>The type of the handler.</value>
    public Type HandlerType { get; set; }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public Type ItemType { get; set; }

    /// <summary>Gets or sets the key.</summary>
    /// <value>The key.</value>
    public string Key { get; set; }
  }
}
