// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.ProcessChunk
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>
  /// Delegate for processing html chunk. Returns true if the chunk was modified.
  /// </summary>
  /// <param name="chunk">The chunk to be processed.</param>
  /// <returns>Returns true if the chunk was modified; otherwise false.</returns>
  public delegate bool ProcessChunk(HtmlChunk chunk);
}
