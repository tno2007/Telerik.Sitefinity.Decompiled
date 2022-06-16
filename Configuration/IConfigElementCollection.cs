// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.IConfigElementCollection`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Base generic interface for collections of <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" />s.
  /// </summary>
  /// <typeparam name="TElement">The type of the collection elements.</typeparam>
  public interface IConfigElementCollection<out TElement> where TElement : ConfigElement
  {
    IEnumerable<TElement> Elements { get; }
  }
}
