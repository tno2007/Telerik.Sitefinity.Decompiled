// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.FileProcessors.IFileProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.Sitefinity.Processors;

namespace Telerik.Sitefinity.FileProcessors
{
  /// <summary>Defines functionality for file processors.</summary>
  public interface IFileProcessor : IProcessor
  {
    /// <summary>
    /// Determines whether this instance can process the specified file
    /// </summary>
    /// <param name="fileInput">The input file data and info.</param>
    /// <returns>
    ///   <c>true</c> if this instance can process the specified file; otherwise, <c>false</c>.
    /// </returns>
    bool CanProcessFile(FileProcessorInput fileInput);

    /// <summary>Process the input file</summary>
    /// <param name="fileInput">The input file data and info.</param>
    /// <returns>Processed file output stream</returns>
    Stream Process(FileProcessorInput fileInput);
  }
}
