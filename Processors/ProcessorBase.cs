// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Processors.ProcessorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;

namespace Telerik.Sitefinity.Processors
{
  /// <summary>
  /// Base class for processors. Processors are Sitefinity classes that apply some processing logic to flows of information in Sitefinity.
  /// </summary>
  public abstract class ProcessorBase : IProcessor
  {
    /// <summary>Gets the name of the processor.</summary>
    public string Name { get; private set; }

    /// <summary>Initializes the processor instance.</summary>
    /// <param name="name">The name of the processor.</param>
    /// <param name="config">Collection of additional configuration parameters</param>
    void IProcessor.Initialize(string name, NameValueCollection config)
    {
      this.Name = name;
      this.Initialize(config);
    }

    /// <summary>Initialize the processor instance</summary>
    /// <param name="config">Collection of additional configuration parameters</param>
    protected abstract void Initialize(NameValueCollection config);
  }
}
