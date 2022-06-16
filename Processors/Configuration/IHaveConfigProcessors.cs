// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Processors.Configuration.IHaveConfigProcessors
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Processors.Configuration
{
  internal interface IHaveConfigProcessors
  {
    /// <summary>Gets the config processors in the config element</summary>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Processors.Configuration.ProcessorConfigElement" /></returns>
    ConfigElementDictionary<string, ProcessorConfigElement> GetConfigProcessors();
  }
}
