// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormHttpPostedFile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Represents a file posted with a Form.</summary>
  public class FormHttpPostedFile
  {
    /// <summary>Gets or sets the size of an uploaded file, in bytes.</summary>
    public long ContentLength { get; set; }

    /// <summary>
    /// Gets or sets the MIME content type of an uploaded file.
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the fully qualified name of the file on the client.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets a System.IO.Stream object that points to an uploaded file to prepare for reading the contents of the file.
    /// </summary>
    public Stream InputStream { get; set; }
  }
}
