// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.RenderBehaviour
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;
using Telerik.Sitefinity.Utilities.HtmlParsing;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>Render link that was modified</summary>
  /// <param name="htmlContent">Html content to append to</param>
  /// <param name="currentChunk">Html chunk</param>
  /// <param name="resolveResult">Result of url resolving</param>
  /// <param name="preserveOriginalValue">
  /// If true the original value of altered attributes is
  /// preserved in additional attribute named sfref.
  /// </param>
  public delegate void RenderBehaviour(
    StringBuilder htmlContent,
    HtmlChunk currentChunk,
    LinkParser.ResolveResult resolveResult,
    bool preserveOriginalValue);
}
