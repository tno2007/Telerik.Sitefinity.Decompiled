// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewDetail
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend;
using Telerik.Sitefinity.Taxonomies.Extensions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// Control that displays the detail view of the dynamic content view.
  /// </summary>
  public class DynamicContentViewDetail : DynamicContentViewBase, IBreadcrumExtender
  {
    private string title;
    internal static readonly string defaultLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.DynamicModules.DynamicContentViewDetail.ascx");

    public DynamicContentViewDetail(DynamicModuleManager dynamicModuleManager = null)
      : base(dynamicModuleManager)
    {
    }

    /// <summary>
    /// Gets or sets the data item to be displayed in the detail mode.
    /// </summary>
    public DynamicContent DataItem { get; set; }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
        {
          if (this.DetailViewDefinition != null)
            base.LayoutTemplatePath = this.DetailViewDefinition.TemplatePath;
          if (string.IsNullOrEmpty(base.LayoutTemplatePath) && string.IsNullOrEmpty(this.LayoutTemplateName))
            base.LayoutTemplatePath = DynamicContentViewDetail.defaultLayoutTemplatePath;
        }
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the definition which specifies the way this control should behave.
    /// </summary>
    public IContentViewDetailDefinition DetailViewDefinition { get; set; }

    /// <summary>Gets or sets the page title modes.</summary>
    /// <value>The page title modes.</value>
    public ContentView.PageTitleModes PageTitleMode { get; set; }

    /// <summary>
    /// Gets or sets the meta title field name. The runtime value of this field will be used to set the page title tag
    /// If this is not set the title tag will be set to the default title.
    /// This setting is effective in detail mode of the content view and the field should exist as a property on the detail item type.
    /// </summary>
    public string MetaTitleField { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    [Obsolete]
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the control that represents the container of
    /// the dynamic item detail.
    /// </summary>
    protected virtual DynamicDetailContainer DetailContainer => this.Container.GetControl<DynamicDetailContainer>("detailContainer", true);

    /// <summary>Gets the comments widget.</summary>
    protected virtual CommentsWidget CommentsWidget => this.Container.GetControl<CommentsWidget>("commentsWidget", false);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DetailContainer.DataSource = (object) new DynamicContent[1]
      {
        this.DataItem
      };
      this.DetailContainer.DataBound += new EventHandler(this.DetailContainer_DataBound);
      if (this.TitleControl != null && this.DataItem != null)
        this.TitleControl.Text = this.Title;
      this.ConfigureCommentsWidget();
      this.ResolvePageTitle();
    }

    /// <summary>Configures the comments widget.</summary>
    protected virtual void ConfigureCommentsWidget()
    {
      if (this.CommentsWidget == null || this.DataItem == null || this.DynamicModuleType == null)
        return;
      this.CommentsWidget.ThreadKey = ControlUtilities.GetLocalizedKey((object) this.DataItem.Id, (string) null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(this.DynamicContentType.FullName));
      this.CommentsWidget.ThreadType = this.DynamicContentType.FullName;
      this.CommentsWidget.GroupKey = CommentsUtilities.GetGroupKey(this.DynamicModuleType.ModuleName, this.DynamicManager.Provider.Name);
      this.CommentsWidget.DataSource = this.DynamicManager.Provider.Name;
      this.CommentsWidget.ThreadTitle = this.DataItem.GetValue(this.DynamicModuleType.MainShortTextFieldName).ToString();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Sets the page title to the value of the identificator field of the data item.
    /// Depends on ContentViewDisplayMode property.
    /// </summary>
    protected virtual void ResolvePageTitle()
    {
      if (this.DataItem == null || this.Page == null)
        return;
      if (this.PageTitleMode != ContentView.PageTitleModes.DoNotSet)
      {
        switch (this.PageTitleMode)
        {
          case ContentView.PageTitleModes.Replace:
            if (this.GetPageTitleValue() != null)
            {
              this.Page.Title = this.GetPageTitleValue();
              break;
            }
            this.Page.Title = "";
            break;
          case ContentView.PageTitleModes.Append:
            if (this.GetPageTitleValue() != null)
            {
              this.Page.Title = string.Format("{0} {1}", (object) this.Page.Title, (object) this.GetPageTitleValue());
              break;
            }
            break;
        }
      }
      if (this.DetailViewDefinition == null || !(this.DetailViewDefinition.DataItemId == Guid.Empty))
        return;
      this.Page.RegisterBreadcrumbExtender((IBreadcrumExtender) this);
    }

    IEnumerable<SiteMapNode> IBreadcrumExtender.GetVirtualNodes(
      SiteMapProvider provider)
    {
      List<SiteMapNode> virtualNodes = new List<SiteMapNode>();
      if (this.DataItem != null)
      {
        Guid guid;
        for (DynamicContent systemParentItem = this.DataItem.SystemParentItem; systemParentItem != null; systemParentItem = systemParentItem.SystemParentItem)
        {
          if (this.Host != null)
          {
            string empty1 = string.Empty;
            string str1;
            if (this.Host.MasterViewDefinition == null || !(this.Host.MasterViewDefinition.DetailsPageId != Guid.Empty))
            {
              str1 = (string) null;
            }
            else
            {
              guid = this.Host.MasterViewDefinition.DetailsPageId;
              str1 = guid.ToString();
            }
            string args = str1;
            string str2 = DataResolver.Resolve((object) systemParentItem, "URL", this.Host.UrlKeyPrefix, args);
            SiteMapProvider provider1 = provider;
            guid = systemParentItem.Id;
            string key = guid.ToString();
            string url = str2;
            string title = DynamicContentExtensions.GetTitle(systemParentItem);
            string empty2 = string.Empty;
            SiteMapNode siteMapNode = new SiteMapNode(provider1, key, url, title, empty2);
            virtualNodes.Insert(0, siteMapNode);
          }
          else
          {
            string rawUrl = this.Page.Request.RawUrl;
            int length = rawUrl.IndexOf(systemParentItem.SystemUrl);
            if (length > -1)
            {
              SiteMapProvider provider2 = provider;
              guid = systemParentItem.Id;
              string key = guid.ToString();
              string url = rawUrl.Substring(0, length) + systemParentItem.SystemUrl;
              string title = DynamicContentExtensions.GetTitle(systemParentItem);
              string empty = string.Empty;
              SiteMapNode siteMapNode = new SiteMapNode(provider2, key, url, title, empty);
              virtualNodes.Insert(0, siteMapNode);
            }
          }
        }
        SiteMapProvider provider3 = provider;
        guid = this.DataItem.Id;
        string key1 = guid.ToString();
        string empty3 = string.Empty;
        string pageTitleValue = this.GetPageTitleValue();
        string empty4 = string.Empty;
        SiteMapNode siteMapNode1 = new SiteMapNode(provider3, key1, empty3, pageTitleValue, empty4);
        virtualNodes.Add(siteMapNode1);
      }
      return (IEnumerable<SiteMapNode>) virtualNodes;
    }

    /// <summary>
    /// Gets the value of the field which will be used in page title
    /// </summary>
    /// <returns>The value of the main short text field</returns>
    protected virtual string GetPageTitleValue()
    {
      if (this.title == null)
      {
        if (!string.IsNullOrEmpty(this.MetaTitleField))
          this.title = this.GetMetaValue((object) this.DataItem, this.MetaTitleField);
        if (string.IsNullOrEmpty(this.title))
          this.title = this.DataItem.GetValue(this.DynamicModuleType.MainShortTextFieldName).ToString();
      }
      return this.title;
    }

    private string GetMetaValue(object detailItem, string fieldName)
    {
      PropertyDescriptor property1 = TypeDescriptor.GetProperties(detailItem)[fieldName];
      if (property1 == null)
        return (string) null;
      if (property1 is TaxonomyPropertyDescriptor property2)
        return property2.GetTaxaText(detailItem);
      return property1.GetValue(detailItem)?.ToString();
    }

    /// <summary>Handles the data bound event of the detail container.</summary>
    /// <param name="sender">
    /// The instance of the control that invoked the event.
    /// </param>
    /// <param name="e">The event arguments associated with the event.</param>
    protected virtual void DetailContainer_DataBound(object sender, EventArgs e)
    {
      AssetsFieldBinder.BindAllAssetsFields((Control) this.DetailContainer, (IDataItem) this.DataItem);
      AddressFieldBinder.BindAllAddressFields((Control) this.DetailContainer, (IDataItem) this.DataItem);
    }
  }
}
