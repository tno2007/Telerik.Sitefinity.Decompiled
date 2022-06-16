// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Processors.IProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;

namespace Telerik.Sitefinity.Processors
{
  /// <summary>
  /// Interface for processors in Sitefinity. These are classes that apply some logic against a flow of information in sitefinity.
  /// </summary>
  public interface IProcessor
  {
    /// <summary>Gets the name of the Processor</summary>
    string Name { get; }

    /// <summary>Initialize Processor instance</summary>
    /// <param name="name">The name of the file processor</param>
    /// <param name="config">Collection of additional configuration parameters</param>
    void Initialize(string name, NameValueCollection config);
  }
}
