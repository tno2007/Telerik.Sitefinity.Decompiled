// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceCombining.FakeWorkerRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web.Hosting;

namespace Telerik.Sitefinity.Web.ResourceCombining
{
  /// <summary>This class fakes the request redirects</summary>
  internal class FakeWorkerRequest : SimpleWorkerRequest
  {
    private TextWriter writer;

    public FakeWorkerRequest(string page, string query, TextWriter output)
      : base(page, query, output)
    {
      this.writer = output;
    }

    public FakeWorkerRequest(
      string appVirutalDir,
      string appPhysicalDir,
      string page,
      string query,
      TextWriter output)
      : base(appVirutalDir, appPhysicalDir, page, query, output)
    {
      this.writer = output;
    }

    public override void EndOfRequest() => base.EndOfRequest();

    public override void FlushResponse(bool finalFlush) => base.FlushResponse(finalFlush);

    /// <summary>Specifies the HTTP status code and status description of the response,
    /// such as SendStatus(200, "Ok").</summary>
    /// <param name="statusCode">The status code to send </param>
    /// <param name="statusDescription">The status description to send. </param>
    public override void SendStatus(int statusCode, string statusDescription) => base.SendStatus(200, "Ok");

    /// <summary>Adds a standard HTTP header to the response.</summary>
    /// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />.
    /// </param>
    /// <param name="value">The value of the header. </param>
    public override void SendKnownResponseHeader(int index, string value)
    {
    }

    /// <summary>Adds a nonstandard HTTP header to the response.</summary>
    /// <param name="name">The name of the header to send. </param>
    /// <param name="value">The value of the header. </param>
    public override void SendUnknownResponseHeader(string name, string value)
    {
    }

    /// <summary>Adds the specified number of bytes from a byte array to the response.
    /// </summary>
    /// <param name="data">The byte array to send. </param>
    /// <param name="length">The number of bytes to send, starting at the first byte.</param>
    public override void SendResponseFromMemory(byte[] data, int length)
    {
    }

    /// <summary>Adds the contents of the specified file to the response and specifies
    /// the starting position in the file and the number of bytes to send.</summary>
    /// <param name="filename">The name of the file to send. </param>
    /// <param name="offset">The starting position in the file. </param>
    /// <param name="length">The number of bytes to send. </param>
    public override void SendResponseFromFile(string filename, long offset, long length)
    {
    }

    /// <summary>Adds the contents of the specified file to the response and specifies
    /// the starting position in the file and the number of bytes to send.</summary>
    /// <param name="filename">The name of the file to send. </param>
    /// <param name="offset">The starting position in the file. </param>
    /// <param name="length">The number of bytes to send. </param>
    public override void SendResponseFromFile(IntPtr handle, long offset, long length)
    {
    }
  }
}
