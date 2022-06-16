// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ILanguageDataManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Interface for managers that will support LanguageData handling - objects that store values that are culture specific
  /// </summary>
  public interface ILanguageDataManager
  {
    /// <summary>Creates a language data instance</summary>
    LanguageData CreateLanguageData();

    /// <summary>Creates a language data instance</summary>
    /// <param name="id">The id.</param>
    LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data instance by its Id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    LanguageData GetLanguageData(Guid id);
  }
}
