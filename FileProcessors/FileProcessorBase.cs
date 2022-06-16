// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.FileProcessors.FileProcessorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using Telerik.Sitefinity.Processors;

namespace Telerik.Sitefinity.FileProcessors
{
  /// <summary>Defines a base abstraction for a library sanitizers.</summary>
  [Obsolete("This abstract class has been deprecated. Please, use ProcessorBase and IFileProcessor interface.")]
  public abstract class FileProcessorBase : IFileProcessor, IProcessor
  {
    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    void IProcessor.Initialize(string name, NameValueCollection config)
    {
      this.Name = name;
      this.Initialize(config);
    }

    /// <summary>Initialize FileProcessor instance</summary>
    /// <param name="config">Collection of additional configuration parameters</param>
    protected abstract void Initialize(NameValueCollection config);

    /// <inheritdoc />
    public abstract bool CanProcessFile(FileProcessorInput fileInput);

    /// <inheritdoc />
    public abstract Stream Process(FileProcessorInput fileInput);
  }
}
