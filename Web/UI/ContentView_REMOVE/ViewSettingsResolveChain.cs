// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ViewSettingsResolveChain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI
{
  public class ViewSettingsResolveChain
  {
    private List<ISettingsResolver> resovleChain;

    public ViewSettingsResolveChain() => this.resovleChain = new List<ISettingsResolver>();

    public void AddResolver(ISettingsResolver props) => this.resovleChain.Add(props);

    public object ResolveSetting(string name)
    {
      for (int index = this.resovleChain.Count - 1; index >= 0; --index)
      {
        object obj = this.resovleChain[index].ResolveSetting(name);
        if (obj != null)
          return obj;
      }
      return (object) null;
    }

    public T ResolveSetting<T>(string name)
    {
      object obj = this.ResolveSetting(name);
      if (obj == null)
        return default (T);
      if (!typeof (T).IsAssignableFrom(obj.GetType()))
        return this.GetConvertedValue<T>(obj);
      if (!(typeof (T) == typeof (string)))
        return (T) obj;
      return string.IsNullOrEmpty((string) obj) ? default (T) : (T) obj;
    }

    private T GetConvertedValue<T>(object value)
    {
      if (value == null)
        return default (T);
      if (this.IsNullableType(typeof (T)))
        return (T) Convert.ChangeType(value, typeof (T).GetGenericArguments()[0]);
      if (typeof (Type).IsAssignableFrom(typeof (T)))
        return (T) TypeResolutionService.ResolveType((string) value);
      return typeof (T).IsEnum ? (T) Enum.Parse(typeof (T), (string) value) : (T) Convert.ChangeType(value, typeof (T));
    }

    private bool IsNullableType(Type theType) => theType.IsGenericType && theType.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
  }
}
