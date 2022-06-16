// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.IMultisiteDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.DataSource
{
  internal interface IMultisiteDataSource
  {
    /// <summary>
    /// Setting that tells the proxy whether selecting multiple providers in a multi site scenario for this datasource is valid or not
    /// </summary>
    bool AllowMultipleProviders { get; set; }
  }
}
