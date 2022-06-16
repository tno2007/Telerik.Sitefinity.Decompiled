// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.PropertyChange
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>Describes the property change operation</summary>
  public class PropertyChange
  {
    /// <summary>The name of the changed property</summary>
    public string PropertyName { get; set; }

    /// <summary>The old value of the property</summary>
    public object OldValue { get; set; }

    /// <summary>The new value of the property</summary>
    public object NewValue { get; set; }
  }
}
