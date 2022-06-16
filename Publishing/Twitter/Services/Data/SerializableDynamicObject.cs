// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.Data.SerializableDynamicObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Publishing.Twitter.Services.Data
{
  [DataContract]
  public class SerializableDynamicObject : IDynamicMetaObjectProvider
  {
    [DataMember]
    private IDictionary<string, object> dynamicProperties = (IDictionary<string, object>) new Dictionary<string, object>();

    public DynamicMetaObject GetMetaObject(Expression expression) => (DynamicMetaObject) new SerializableDynamicMetaObject(expression, BindingRestrictions.GetInstanceRestriction(expression, (object) this), (object) this);

    internal object setValue(string name, object value)
    {
      this.dynamicProperties.Add(name, value);
      return value;
    }

    internal object getValue(string name)
    {
      object obj;
      if (!this.dynamicProperties.TryGetValue(name, out obj))
        obj = (object) null;
      return obj;
    }

    internal IEnumerable<string> getDynamicMemberNames() => (IEnumerable<string>) this.dynamicProperties.Keys;
  }
}
