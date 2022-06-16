// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.Serialization.ISiteSyncSerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.SiteSync.Serialization
{
  public interface ISiteSyncSerializer
  {
    MappingSettings GetTypeMappings(Type type);

    void SetProperties(object destination, object source, object args = null);

    void SetProperties(
      object destination,
      object source,
      Func<PropertyDescriptor, PropertyDescriptor, bool> setPredicate,
      object args = null);
  }
}
