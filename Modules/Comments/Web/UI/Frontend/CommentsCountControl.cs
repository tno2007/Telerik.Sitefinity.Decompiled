// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsCountControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// Comments Count control. Used for displaying comments count per thread
  /// </summary>
  public class CommentsCountControl : SitefinityHyperLink
  {
    private string displayMode = "FullText";
    private CommentsSettingsElement threadConfig;

    /// <summary>Gets or sets the thread key.</summary>
    public string ThreadKey { set; get; }

    public string ThreadType { set; get; }

    /// <summary>Gets or sets the comments count.</summary>
    internal int CommentsCount { set; get; }

    /// <summary>
    /// Gets or sets the display mode. Could be set to "FullText", "ShortText", "IconOnly"
    /// </summary>
    public string DisplayMode
    {
      set => this.displayMode = value;
      get => this.displayMode;
    }

    /// <summary>Gets or sets the allow comments.</summary>
    /// <value>The allow comments.</value>
    public bool? AllowComments { get; set; }

    /// <summary>Gets the Comments Settings element</summary>
    private CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(this.ThreadType);
        return this.threadConfig;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      if (this.NavigateUrl.IsNullOrEmpty())
      {
        object dataItem = (this.GetDataItemContainer() ?? throw new InvalidOperationException("This control can be used only within a data bound item template or with set NavigateUrl proeprty.")).DataItem;
        if (dataItem != null)
        {
          IContentView hostControlLoose = this.GetHostControlLoose<IContentView>();
          IRelatedDataView relatedDataView = hostControlLoose != null ? hostControlLoose as IRelatedDataView : throw new InvalidOperationException("This control must be hosted by ContentView control or one that derives form it.");
          IContentViewMasterDefinition masterViewDefinition = hostControlLoose.MasterViewDefinition;
          if (relatedDataView != null && relatedDataView.DisplayRelatedData() && relatedDataView.RelatedDataDefinition.RelatedItemSource != RelatedItemSource.Url && dataItem is IDataItem)
            this.NavigateUrl = dataItem.GetDefaultUrl();
          else if (masterViewDefinition != null && masterViewDefinition.DetailsPageId != Guid.Empty)
            this.NavigateUrl = DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, masterViewDefinition.DetailsPageId.ToString());
          else
            this.NavigateUrl = DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, (string) null);
        }
        if (this.NavigateUrl.IsNullOrEmpty())
          return;
        this.NavigateUrl += "#commentsWidget";
      }
      else
        this.ResolveNavigateUrl = false;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.ConfigureCounter();
      base.CreateChildControls();
    }

    /// <summary>
    /// Configure styling of the counter depending on the DisplayMode
    /// </summary>
    protected void ConfigureCounter()
    {
      if (!SystemManager.IsModuleEnabled("Comments") || (this.AllowComments.HasValue ? (!this.AllowComments.Value ? 1 : 0) : (!this.ThreadConfig.AllowComments ? 1 : 0)) != 0 || this.ThreadConfig.EnableRatings)
      {
        this.Visible = false;
      }
      else
      {
        if (this.Controls.OfType<CommentsCountControlBinder>().Count<CommentsCountControlBinder>() == 0)
          this.Controls.Add((Control) new CommentsCountControlBinder());
        this.CssClass += " sfcommentsCounterWrp";
        this.Attributes.Add("threadKey", this.ThreadKey);
        this.Attributes.Add("displayMode", this.DisplayMode);
        if (this.DisplayMode == "FullText")
          this.CssClass += " sfcommentsFull";
        else if (this.DisplayMode == "ShortText")
        {
          this.CssClass += " sfcommentsShort";
        }
        else
        {
          if (!(this.DisplayMode == "IconOnly"))
            return;
          this.CssClass += " sfcommentsIconOnly";
        }
      }
    }
  }
}
