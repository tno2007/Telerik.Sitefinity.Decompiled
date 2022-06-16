// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.RadListViewBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents ClientBinder implementation for the RadListView control.
  /// </summary>
  [ToolboxBitmap(typeof (RadListBoxBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:RadListViewBinder runat=\"server\"></{0}:RadListViewBinder>")]
  public class RadListViewBinder : ClientBinder
  {
    private string serviceChildItemsBaseUrl;
    internal static string binderScript = "Telerik.Sitefinity.Web.Scripts.RadListViewBinder.js";

    public RadListViewBinder() => this.AutoUpdateOrdinals = true;

    /// <summary>
    /// Gets or sets the delegate to be called after item order position is changend
    /// </summary>
    /// <value>The on item reorder.</value>
    public virtual string OnItemReordered { get; set; }

    public virtual bool HandleItemReordering { get; set; }

    /// <summary>
    /// If true when an item is reordered the new ordinal is automatically syncronized
    /// </summary>
    public virtual bool AutoUpdateOrdinals { get; set; }

    /// <summary>
    /// RadListView does not render any wrapping HTML
    /// In order to do client binding we use the pageId of the wrapping element in the LayoutTemplate
    /// </summary>
    public virtual string TargetLayoutContainerId { get; set; }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    [TypeConverter(typeof (StringTypeConverter))]
    public virtual Type ContentType { get; set; }

    /// <summary>Gets or sets the service child items base URL.</summary>
    /// <value>The service child items base URL.</value>
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

    /// <summary>
    /// Name of the node property that contains the parent pageId
    /// </summary>
    public string ParentDataKeyName { get; set; }

    protected override string BinderName => "Telerik.Sitefinity.Web.UI.RadListViewBinder";

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the
    /// event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null || !this.HandleItemReordering)
        return;
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.JQueryUI);
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      behaviorDescriptor.AddProperty("_handleItemReordering", (object) this.HandleItemReordering);
      behaviorDescriptor.AddProperty("_targetLayoutContainerId", (object) this.TargetLayoutContainerId);
      behaviorDescriptor.AddProperty("_autoUpdateOrdinals", (object) this.AutoUpdateOrdinals);
      if (this.Containers.Count > 0)
        behaviorDescriptor.AddProperty("_containerTag", (object) this.Containers[0].ContainerTag.ToString());
      if (!string.IsNullOrEmpty(this.ServiceChildItemsBaseUrl))
        behaviorDescriptor.AddProperty("serviceChildItemsBaseUrl", (object) VirtualPathUtility.ToAbsolute(VirtualPathUtility.RemoveTrailingSlash(this.ServiceChildItemsBaseUrl)));
      if (!string.IsNullOrEmpty(this.ParentDataKeyName))
        behaviorDescriptor.AddProperty("parentDataKeyName", (object) this.ParentDataKeyName);
      if (this.ContentType != (Type) null)
        behaviorDescriptor.AddProperty("contentType", (object) this.ContentType.FullName);
      if (!string.IsNullOrEmpty(this.OnItemReordered))
        behaviorDescriptor.AddEvent("onItemReordered", this.OnItemReordered);
      if (this.HandleItemReordering)
      {
        List<Guid> list = this.GetTarget<RadListView>().Items.Select<RadListViewDataItem, Guid>((Func<RadListViewDataItem, Guid>) (item => ((IDataItem) item.DataItem).Id)).ToList<Guid>();
        behaviorDescriptor.AddProperty("itemIds", (object) list);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string assembly = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference(RadListViewBinder.binderScript, assembly));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
