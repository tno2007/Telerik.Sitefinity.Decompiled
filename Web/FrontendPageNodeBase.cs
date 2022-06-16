// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.FrontendPageNodeBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// This class represents the model of the Nodes that will be rendered inside the Navigation templates.
  /// </summary>
  /// <typeparam name="T">The frontend page node type.</typeparam>
  public class FrontendPageNodeBase<T> where T : FrontendPageNodeBase<T>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.FrontendPageNodeBase`1" /> class.
    /// </summary>
    public FrontendPageNodeBase() => this.ChildNodes = (IList<T>) new List<T>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.FrontendPageNodeBase`1" /> class
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
    public FrontendPageNodeBase(
      SiteMapNode node,
      string url,
      string target,
      bool isCurrentlyOpened,
      bool hasChildOpen)
    {
      this.OriginalSiteMapNode = node;
      if (node != null)
      {
        this.Title = node.Title;
        this.Key = node.Key;
      }
      this.Url = url;
      this.LinkTarget = target;
      this.ChildNodes = (IList<T>) new List<T>();
      this.IsCurrentlyOpened = isCurrentlyOpened;
      this.HasChildOpen = hasChildOpen;
    }

    /// <summary>Gets or sets the node key.</summary>
    /// <value>The key.</value>
    [DataMember]
    public string Key { get; set; }

    /// <summary>Gets or sets the node title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the node URL.</summary>
    /// <value>The URL.</value>
    [DataMember]
    public string Url { get; set; }

    /// <summary>Gets or sets the link target.</summary>
    /// <value>The link target.</value>
    [DataMember]
    public string LinkTarget { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this node represents currently opened page
    /// </summary>
    /// <value>
    ///   <c>true</c> if page node is currently opened; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsCurrentlyOpened { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the currently opened page is a descendent of this node.
    /// </summary>
    /// <value>
    ///   <c>true</c> if currently opened page is descendent of this node; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool HasChildOpen { get; set; }

    /// <summary>Gets or sets the original site map node.</summary>
    /// <value>The original site map node.</value>
    public SiteMapNode OriginalSiteMapNode { get; set; }

    /// <summary>Gets or sets the child nodes.</summary>
    /// <value>The child nodes.</value>
    [DataMember]
    public IList<T> ChildNodes { get; set; }
  }
}
