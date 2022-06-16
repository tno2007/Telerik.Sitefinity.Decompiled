// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.FrontendPageNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
  /// </summary>
  public class FrontendPageNode : FrontendPageNodeBase<FrontendPageNode>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.FrontendPageNode" /> class.
    /// </summary>
    public FrontendPageNode()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.FrontendPageNode" /> class.
    /// </summary>
    /// <param name="node">The original SiteMapNode.</param>
    /// <param name="url">The URL.</param>
    /// <param name="target">The target.</param>
    /// <param name="isCurrentlyOpened">
    /// if set to <c>true</c> is currently opened.
    /// </param>
    /// <param name="hasChildOpen">
    /// if set to <c>true</c> currently opened page is descendent of this node.
    /// </param>
    public FrontendPageNode(
      SiteMapNode node,
      string url,
      string target,
      bool isCurrentlyOpened,
      bool hasChildOpen)
      : base(node, url, target, isCurrentlyOpened, hasChildOpen)
    {
    }
  }
}
