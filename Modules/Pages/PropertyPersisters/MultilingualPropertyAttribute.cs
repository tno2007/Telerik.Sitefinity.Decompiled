// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MultilingualPropertyAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Attribute that specifies if a control property is to be persised in multilingual mode
  /// By default all the properties are stored in a monolingual manner and cleared  upon serialization
  /// Marked as multilingual will preserve their multilingual values
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  [Obsolete("Not needed - each property can be persisted for a specific language")]
  public class MultilingualPropertyAttribute : Attribute
  {
    /// <summary>Gets or sets the name of the multilingual property.</summary>
    /// <value>The name of the multilingual property.</value>
    public string MultilingualPropertyName { get; set; }
  }
}
