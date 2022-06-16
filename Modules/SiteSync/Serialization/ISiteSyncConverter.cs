// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.Serialization.ISiteSyncConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.SiteSync.Serialization
{
  public interface ISiteSyncConverter
  {
    string Serialize(object obj, Type type, object settings);

    object Deserialize(string str, Type type, object settings);

    void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args);
  }
}
