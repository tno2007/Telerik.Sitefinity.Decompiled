// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsAverageRatingControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
  /// Comments Rating control. Used for displaying comments rating per thread
  /// </summary>
  public class CommentsAverageRatingControl : CompositeControl
  {
    private string displayMode = "FullText";
    private CommentsSettingsElement threadConfig;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the thread key.</summary>
    public string ThreadKey { set; get; }

    public string ThreadType { set; get; }

    internal CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(this.ThreadType);
        return this.threadConfig;
      }
    }

    /// <summary>
    /// Gets or sets the display mode. Could be set to "FullText", "ShortText", "IconOnly"
    /// </summary>
    public string DisplayMode
    {
      set => this.displayMode = value;
      get => this.displayMode;
    }

    public string NavigateUrl { set; get; }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnDataBinding(EventArgs e)
    {
      base.OnDataBinding(e);
      if (!this.NavigateUrl.IsNullOrEmpty())
        return;
      object dataItem = (this.GetDataItemContainer() ?? throw new InvalidOperationException("This control can be used only within a data bound item template or with set NavigateUrl property.")).DataItem;
      if (dataItem != null)
      {
        IContentView hostControlLoose = this.GetHostControlLoose<IContentView>();
        IContentViewMasterDefinition masterDefinition = hostControlLoose != null ? hostControlLoose.MasterViewDefinition : throw new InvalidOperationException("This control must be hosted by ContentView control or one that derives form it.");
        this.NavigateUrl = !(hostControlLoose is IRelatedDataView relatedDataView) || !relatedDataView.DisplayRelatedData() || relatedDataView.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.Url || !(dataItem is IDataItem) ? (masterDefinition == null || !(masterDefinition.DetailsPageId != Guid.Empty) ? DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, (string) null) : DataResolver.Resolve(dataItem, "URL", hostControlLoose.UrlKeyPrefix, masterDefinition.DetailsPageId.ToString())) : dataItem.GetDefaultUrl();
      }
      if (this.NavigateUrl.IsNullOrEmpty())
        return;
      this.NavigateUrl += "#commentsWidget";
    }

    protected override void CreateChildControls()
    {
      this.ConfigureAverageRatingControl();
      this.AddRatingControlBinder();
      this.ConfigureRatingResources();
      base.CreateChildControls();
    }

    private void ConfigureAverageRatingControl()
    {
      if (!SystemManager.IsModuleEnabled("Comments") || !this.ThreadConfig.AllowComments || !this.ThreadConfig.EnableRatings)
      {
        this.Visible = false;
      }
      else
      {
        if (!string.IsNullOrEmpty(this.NavigateUrl))
        {
          if (this.NavigateUrl.StartsWith("#"))
          {
            string virtualPath = SystemManager.CurrentHttpContext.Request.RawUrl;
            int length = virtualPath.IndexOf("?");
            if (length != -1)
              virtualPath = virtualPath.Substring(0, length);
            this.NavigateUrl = VirtualPathUtility.AppendTrailingSlash(virtualPath) + this.NavigateUrl;
          }
          else
            this.NavigateUrl = RouteHelper.ResolveUrl(this.NavigateUrl, UrlResolveOptions.Absolute);
          this.Attributes.Add("navigateUrl", this.NavigateUrl);
        }
        this.CssClass += " sfcommentsThreadRatingWrp";
        this.Attributes.Add("threadKey", this.ThreadKey);
        this.Attributes.Add("displayMode", this.DisplayMode);
        if (this.DisplayMode == "FullText")
          this.CssClass += " sfcommentsThreadRatingFull";
        else if (this.DisplayMode == "MediumText")
          this.CssClass += " sfcommentsThreadRatingMedium";
        else if (this.DisplayMode == "ShortText")
        {
          this.CssClass += " sfcommentsThreadRatingShort";
        }
        else
        {
          if (!(this.DisplayMode == "IconOnly"))
            return;
          this.CssClass += " sfcommentsThreadRatingIconOnly";
        }
      }
    }

    private void AddRatingControlBinder()
    {
      if (this.Controls.OfType<CommentsAverageRatingControlBinder>().Count<CommentsAverageRatingControlBinder>() != 0)
        return;
      ControlCollection controls = this.Controls;
      CommentsAverageRatingControlBinder child = new CommentsAverageRatingControlBinder();
      child.ID = "commentsAverageRatingControlBinder";
      controls.Add((Control) child);
    }

    /// <summary>
    /// Configures the rating control by adding the required css
    /// </summary>
    private void ConfigureRatingResources() => this.Controls.Add((Control) new ResourceLinks()
    {
      UseEmbeddedThemes = true,
      Links = {
        new ResourceFile()
        {
          Name = "Telerik.Sitefinity.Resources.Scripts.Rating.rating.css",
          Static = true
        }
      }
    });
  }
}
