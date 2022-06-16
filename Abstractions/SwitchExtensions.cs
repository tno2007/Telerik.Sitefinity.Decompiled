// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.SwitchExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Extensions, because otherwise casing fails on Switch==null
  /// </summary>
  public static class SwitchExtensions
  {
    public static Switch Case<T>(this Switch s, Action<T> a) where T : class => s.Case<T>((Func<T, bool>) (o => true), a, true);

    public static Switch Case<T>(this Switch s, Action<T> a, bool fallThrough) where T : class => s.Case<T>((Func<T, bool>) (o => true), a, fallThrough);

    public static Switch Case<T>(this Switch s, Func<T, bool> c, Action<T> a) where T : class => s.Case<T>(c, a, true);

    public static Switch Default(this Switch s, Action a)
    {
      if (!s.IsMatch)
        a();
      return (Switch) null;
    }

    public static Switch Case<T>(
      this Switch s,
      Func<T, bool> c,
      Action<T> a,
      bool fallThrough)
      where T : class
    {
      if (s == null)
        return (Switch) null;
      if (s.IsMatch || !(s.Object is T obj) || !c(obj))
        return s;
      a(obj);
      s.IsMatch = true;
      return !fallThrough ? (Switch) null : s;
    }
  }
}
