// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.DeserializeValueEnhancementDelegate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Utilities
{
  public delegate object DeserializeValueEnhancementDelegate(
    PropertyDescriptor propertyDescriptor,
    Type type,
    object deserializedValue,
    IDynamicFieldsContainer objectInstance);
}
