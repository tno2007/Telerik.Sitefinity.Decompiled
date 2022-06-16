// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.FileProcessors.FileProcessorInput
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.FileProcessors
{
  /// <summary>Defines input parameters for FileProcessors.</summary>
  public class FileProcessorInput
  {
    /// <summary>Gets or sets a value for the input file extension</summary>
    public string FileExtension { get; set; }

    /// <summary>Gets or sets a value for the input file MIME Type</summary>
    public string MimeType { get; set; }

    /// <summary>Gets or sets a value for the input file stream</summary>
    public Stream FileStream { get; set; }
  }
}
