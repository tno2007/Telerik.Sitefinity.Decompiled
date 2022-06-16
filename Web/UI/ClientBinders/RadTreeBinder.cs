// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadTreeBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Client binder component that binds data to Telerik RadTreeView control.
  /// </summary>
  public class RadTreeBinder : ClientBinder
  {
    private string serviceChildItemsBaseUrl;
    private string loadingText;
    private string loadMoreText;
    private int taxonomyChildPagingSize = Config.Get<TaxonomyConfig>().DefaultPageSize;
    private int initialExpandingNodesLimit;
    private int initialExpandingNodesPerLevelLimit;
    private int initialExpandingNodesPerSubtreeLimit;
    private int initialExpandingSubtreesLimit;
    private bool hierarchicalTreeRootBindModeEnabled = true;
    private const int InitialExpandingNodesLimitDefault = 500;
    private const int InitialExpandingNodesPerLevelLimitDefault = 100;
    private const int InitialExpandingNodesPerSubtreeLimitDefault = 500;
    private const int InitialExpandingSubtreesLimitDefault = 20;

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public bool AllowMultipleSelection { get; set; }

    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    /// <value></value>
    protected override string BinderName => typeof (RadTreeBinder).FullName;

    public string ServiceChildItemsBaseUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.serviceChildItemsBaseUrl))
          this.serviceChildItemsBaseUrl = this.ServiceUrl;
        return this.serviceChildItemsBaseUrl;
      }
      set => this.serviceChildItemsBaseUrl = value;
    }

    public string LoadingText
    {
      get
      {
        if (string.IsNullOrEmpty(this.loadingText))
          this.loadingText = Res.Get<Labels>().Loading;
        return this.loadingText;
      }
      set => this.loadingText = value;
    }

    /// <summary>
    /// Label used to load more items when child items' paging is enabled
    /// </summary>
    public string LoadMoreText
    {
      get
      {
        if (string.IsNullOrEmpty(this.loadMoreText))
          this.loadMoreText = Res.Get<Labels>().LoadMoreLabel;
        return this.loadMoreText;
      }
      set => this.loadMoreText = value;
    }

    /// <summary>Gets or sets the size of the child paging.</summary>
    /// <value>The size of the child paging.</value>
    public int TaxonomyChildPagingSize
    {
      get => this.taxonomyChildPagingSize;
      set => this.taxonomyChildPagingSize = value;
    }

    /// <summary>
    /// Gets or sets the service url used to get the minimalist subtree encompassing a node.
    /// </summary>
    public string ServicePredecessorBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the service URL that is serving a tree structure.
    /// </summary>
    /// <value>The service tree URL.</value>
    public string ServiceTreeUrl { get; set; }

    /// <summary>
    /// Name of the node property that contains the parent pageId
    /// </summary>
    public string ParentDataKeyName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootTaxonID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether drag-and-drop functionality is enabled.
    /// </summary>
    /// <value><c>true</c> if drag-and-drop functionality is enabled; otherwise, <c>false</c>.</value>
    public bool EnableDragAndDrop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to store the expansion of the tree per user.
    /// </summary>
    public bool EnableInitialExpanding { get; set; }

    /// <summary>
    /// Gets or sets the name of the cookie that will contain the information of the expanded nodes.
    /// </summary>
    public string ExpandedNodesCookieName { get; set; }

    /// <summary>Gets or sets the initial expanding nodes limit.</summary>
    /// <value>The initial expanding nodes limit.</value>
    public int InitialExpandingNodesLimit
    {
      get => this.initialExpandingNodesLimit > 0 ? this.initialExpandingNodesLimit : 500;
      set => this.initialExpandingNodesLimit = value;
    }

    /// <summary>
    /// Gets or sets the initial expanding nodes per level limit.
    /// </summary>
    /// <value>The initial expanding nodes per level limit.</value>
    public int InitialExpandingNodesPerLevelLimit
    {
      get => this.initialExpandingNodesPerLevelLimit > 0 ? this.initialExpandingNodesPerLevelLimit : 100;
      set => this.initialExpandingNodesPerLevelLimit = value;
    }

    /// <summary>
    /// Gets or sets the initial expanding nodes per subtree limit.
    /// </summary>
    /// <value>The initial expanding nodes per subtree limit.</value>
    public int InitialExpandingNodesPerSubtreeLimit
    {
      get => this.initialExpandingNodesPerSubtreeLimit > 0 ? this.initialExpandingNodesPerSubtreeLimit : 500;
      set => this.initialExpandingNodesPerSubtreeLimit = value;
    }

    /// <summary>Gets or sets the initial expanding subtrees limit.</summary>
    /// <value>The initial expanding subtrees limit.</value>
    public int InitialExpandingSubtreesLimit
    {
      get => this.initialExpandingSubtreesLimit > 0 ? this.initialExpandingSubtreesLimit : 20;
      set => this.initialExpandingSubtreesLimit = value;
    }

    /// <summary>
    /// Enable or disable BindingMode = HierarchicalTreeRootBind. Default is true.
    /// </summary>
    public bool HierarchicalTreeRootBindModeEnabled
    {
      get => this.hierarchicalTreeRootBindModeEnabled;
      set => this.hierarchicalTreeRootBindModeEnabled = value;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use
    /// composition-based implementation to create any child controls they contain in
    /// preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      RadTreeView target = this.GetTarget<RadTreeView>();
      target.CheckBoxes = this.AllowMultipleSelection;
      target.EnableDragAndDrop = this.EnableDragAndDrop;
      target.EnableDragAndDropBetweenNodes = this.EnableDragAndDrop;
      if (this.EnableInitialExpanding && string.IsNullOrEmpty(this.ExpandedNodesCookieName))
        throw new ArgumentException("In order to enable the initial expanding functionality set the ExpandedNodesCookieName property.");
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.RadTreeBinder.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (!string.IsNullOrEmpty(this.ServiceChildItemsBaseUrl))
      {
        string absolute = this.VirtualPathToAbsolute(this.ServiceChildItemsBaseUrl);
        behaviorDescriptor.AddProperty("serviceChildItemsBaseUrl", (object) absolute);
      }
      behaviorDescriptor.AddProperty("_loadingText", (object) this.LoadingText);
      behaviorDescriptor.AddProperty("_loadMoreText", (object) this.LoadMoreText);
      behaviorDescriptor.AddProperty("_allowMultipleSelection", (object) this.AllowMultipleSelection);
      this.ServicePredecessorBaseUrl = this.VirtualPathToAbsolute(this.ServicePredecessorBaseUrl);
      behaviorDescriptor.AddProperty("servicePredecessorBaseUrl", (object) this.ServicePredecessorBaseUrl);
      this.ServiceTreeUrl = this.VirtualPathToAbsolute(this.ServiceTreeUrl);
      behaviorDescriptor.AddProperty("serviceTreeUrl", (object) this.ServiceTreeUrl);
      behaviorDescriptor.AddProperty("parentDataKeyName", (object) this.ParentDataKeyName);
      behaviorDescriptor.AddProperty("rootTaxonID", (object) this.RootTaxonID);
      behaviorDescriptor.AddProperty("_enableDragAndDrop", (object) this.EnableDragAndDrop);
      behaviorDescriptor.AddProperty("_enableInitialExpanding", (object) this.EnableInitialExpanding);
      behaviorDescriptor.AddProperty("_expandedNodesCookieName", (object) this.ExpandedNodesCookieName);
      behaviorDescriptor.AddProperty("_initialExpandingNodesLimit", (object) this.InitialExpandingNodesLimit);
      behaviorDescriptor.AddProperty("_InitialExpandingNodesPerLevelLimit", (object) this.InitialExpandingNodesPerLevelLimit);
      behaviorDescriptor.AddProperty("_initialExpandingNodesPerSubtreeLimit", (object) this.InitialExpandingNodesPerSubtreeLimit);
      behaviorDescriptor.AddProperty("_initialExpandingSubtreesLimit", (object) this.InitialExpandingSubtreesLimit);
      behaviorDescriptor.AddProperty("_hierarchicalTreeRootBindModeEnabled", (object) this.HierarchicalTreeRootBindModeEnabled);
      behaviorDescriptor.AddProperty("_supportsTaxonomyChildPaging", (object) (bool) (this.TaxonomyChildPagingSize <= 0 ? 0 : ("ParentTaxonId".Equals(this.ParentDataKeyName) ? 1 : 0)));
      behaviorDescriptor.AddProperty("_taxonomyChildPagingSize", (object) this.TaxonomyChildPagingSize);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    private string VirtualPathToAbsolute(string path)
    {
      string virtualPath = path;
      if (!string.IsNullOrEmpty(path))
      {
        if (virtualPath.IndexOf("?") > -1)
        {
          string[] strArray = virtualPath.Split('?');
          virtualPath = VirtualPathUtility.ToAbsolute(strArray[0]) + "?" + strArray[1];
        }
        else
          virtualPath = VirtualPathUtility.ToAbsolute(virtualPath);
      }
      return virtualPath;
    }
  }
}
