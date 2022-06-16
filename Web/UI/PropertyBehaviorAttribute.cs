// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyBehaviorAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines the behavior of a property in the web editor.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class PropertyBehaviorAttribute : Attribute
  {
    /// <summary>
    /// Indicates whether a property should be displayed in Sitefinity’s Control Advanced Properties window.
    /// </summary>
    public bool Browsable { get; set; }

    /// <summary>Indicates whether a property should be persisted.</summary>
    public bool Persistable { get; set; }
  }
}
