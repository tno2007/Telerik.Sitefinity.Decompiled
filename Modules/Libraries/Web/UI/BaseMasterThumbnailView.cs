// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BaseMasterThumbnailView`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Extensions;
using Telerik.Sitefinity.Web.UI.ContentUI.Views;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Represents the base class for media content master view.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class BaseMasterThumbnailView<T> : MasterViewBase, IBrowseAndEditable
    where T : MediaContent
  {
    private bool isControlDefinitionProviderCorrect = true;
    private LibrariesManager manager;
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    private BrowseAndEditToolbar browseAndEditToolbar;
    private string thumbnailsName;
    private bool? includeChildLibraryItems;

    /// <summary>Gets the query.</summary>
    /// <value>The query.</value>
    public abstract IQueryable<T> Query { get; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
          this.InitializeManager();
        return this.manager;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get
      {
        string layoutTemplatePath = base.LayoutTemplatePath;
        return string.IsNullOrEmpty(layoutTemplatePath) ? this.DefaultLayoutTemplateName : layoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected abstract string DefaultLayoutTemplateName { get; }

    /// <summary>Gets or sets the name of the thumbnail.</summary>
    /// <value>The name of the thumbnail.</value>
    protected string ThumbnailName
    {
      get
      {
        if (this.thumbnailsName == null && this.MasterDefinition is IMediaContentMasterDefinition masterDefinition)
          this.thumbnailsName = masterDefinition.ThumbnailsName;
        return this.thumbnailsName;
      }
      set => this.thumbnailsName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include child library items.
    /// </summary>
    protected virtual bool? IncludeChildLibraryItems
    {
      get
      {
        if (!this.includeChildLibraryItems.HasValue && this.MasterDefinition is IMediaContentMasterDefinition masterDefinition)
          this.includeChildLibraryItems = new bool?(((int) masterDefinition.IncludeDescendantItems ?? 1) != 0);
        return this.includeChildLibraryItems;
      }
      set => this.includeChildLibraryItems = value;
    }

    private void InitializeManager()
    {
      try
      {
        this.manager = LibrariesManager.GetManager(this.Host.ControlDefinition.ProviderName);
      }
      catch (ConfigurationErrorsException ex)
      {
        this.manager = LibrariesManager.GetManager();
        this.isControlDefinitionProviderCorrect = false;
      }
    }

    /// <summary>Gets the repeater for items list.</summary>
    /// <value>The repeater.</value>
    protected virtual RadListView ItemsList => this.Container.GetControl<RadListView>(nameof (ItemsList), true);

    /// <summary>Gets the pager.</summary>
    /// <value>The pager.</value>
    protected virtual Pager Pager => this.Container.GetControl<Pager>("pager", true);

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>(nameof (BrowseAndEditToolbar), false);
        return this.browseAndEditToolbar;
      }
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (this.TitleLabel != null && this.Host is MediaContentView)
        this.TitleLabel.Text = (this.Host as MediaContentView).Title;
      this.InitializeManager();
      if (!this.isControlDefinitionProviderCorrect)
      {
        this.ItemsList.Visible = false;
        this.TitleLabel.Visible = false;
      }
      else
      {
        string empty1 = string.Empty;
        string sortExpression = this.SortExpression;
        Guid parentId = Guid.Empty;
        int? totalCount = new int?(0);
        int? skip = new int?(0);
        bool? nullable1 = this.AllowPaging;
        if (nullable1.HasValue)
        {
          nullable1 = this.AllowPaging;
          if (nullable1.Value)
            skip = new int?(this.GetItemsToSkipCount(this.ItemsPerPage, this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix));
        }
        IQueryable<T> source;
        if (this.DataSource != null)
        {
          IQueryable<T> query = this.DataSource.OfType<T>().AsQueryable<T>();
          string empty2 = string.Empty;
          source = DataProviderBase.SetExpressions<T>(query, empty1, empty2, skip, this.ItemsPerPage, ref totalCount);
        }
        else
        {
          IRelatedDataView host = (IRelatedDataView) this.Host;
          if (host != null && host.DisplayRelatedData())
          {
            source = Queryable.OfType<T>(host.GetRelatedItems(this.MasterViewDefinition.FilterExpression, this.MasterViewDefinition.SortExpression, this.MasterViewDefinition.ItemsPerPage, ref totalCount)).AsQueryable<T>();
          }
          else
          {
            IQueryable<T> query = this.Query;
            nullable1 = this.AllowUrlQueries;
            if (nullable1.HasValue)
            {
              nullable1 = this.AllowUrlQueries;
              if (nullable1.Value)
                query = this.EvaluateUrl<T>(this.EvaluateUrl<T>(this.EvaluateUrl<T>(query, "Date", "PublicationDate", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix), "Author", "Owner", this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix), "Taxonomy", "", typeof (T), this.Host.UrlEvaluationMode, this.Host.UrlKeyPrefix);
            }
            this.MasterViewDefinition.AdaptMultilingualFilterExpression();
            string filterExpression = DefinitionsHelper.GetFilterExpression(this.FilterExpression, this.AdditionalFilter);
            IQueryable<T> queryable = DataProviderBase.SetExpressions<T>(query, filterExpression, sortExpression, new int?(), new int?(), ref totalCount);
            parentId = this.ItemsParentId;
            if (parentId != Guid.Empty)
            {
              Folder folder = this.Manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == parentId));
              if (folder == null)
              {
                nullable1 = this.IncludeChildLibraryItems;
                if (nullable1.HasValue)
                {
                  nullable1 = this.IncludeChildLibraryItems;
                  if (!nullable1.Value)
                  {
                    queryable = queryable.Where<T>((Expression<Func<T, bool>>) (p => p.Parent.Id == parentId && (p.FolderId == new Guid?() || p.FolderId == (Guid?) Guid.Empty)));
                    goto label_24;
                  }
                }
                queryable = queryable.Where<T>((Expression<Func<T, bool>>) (p => p.Parent.Id == parentId));
              }
              else
              {
                nullable1 = this.IncludeChildLibraryItems;
                if (nullable1.HasValue)
                {
                  nullable1 = this.IncludeChildLibraryItems;
                  if (!nullable1.Value)
                  {
                    queryable = queryable.Where<T>((Expression<Func<T, bool>>) (m => m.FolderId == (Guid?) folder.Id));
                    goto label_24;
                  }
                }
                queryable = this.Manager.GetDescendantsFromQuery<T>(queryable, (IFolder) folder);
              }
            }
label_24:
            source = DataProviderBase.SetExpressions<T>(queryable, (string) null, (string) null, skip, this.ItemsPerPage, ref totalCount);
          }
        }
        int? nullable2 = totalCount;
        int num1 = 0;
        if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
        {
          this.ItemsList.Visible = false;
          this.TitleLabel.Visible = false;
        }
        else
        {
          this.ConfigurePager(this.Pager, totalCount.Value);
          this.ItemsList.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.ItemsList_ItemDataBound);
          List<T> list = source.ToList<T>();
          this.ItemsList.DataSource = (object) list;
          ((IEnumerable<IDataItem>) list).SetRelatedDataSourceContext();
        }
        int num2;
        if (parentId == Guid.Empty)
        {
          nullable2 = totalCount;
          int num3 = 0;
          num2 = nullable2.GetValueOrDefault() == num3 & nullable2.HasValue ? 1 : 0;
        }
        else
          num2 = 0;
        this.IsEmptyView = num2 != 0;
        if (!SystemManager.IsBrowseAndEditMode || this.BrowseAndEditToolbar == null)
          return;
        this.SetDefaultBrowseAndEditCommands();
        this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
        BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
      }
    }

    /// <summary>Configures the detail link.</summary>
    /// <param name="singleItemLink">The single item link.</param>
    /// <param name="dataItem">The data item.</param>
    /// <param name="item">The item.</param>
    protected virtual void ConfigureDetailLink(
      HyperLink singleItemLink,
      T dataItem,
      RadListViewItem item)
    {
      singleItemLink.ImageUrl = !string.IsNullOrWhiteSpace(this.ThumbnailName) ? dataItem.ResolveThumbnailUrl(this.ThumbnailName) : dataItem.ResolveThumbnailUrl("0");
      singleItemLink.Text = (string) dataItem.Title;
      Lstring lstring = dataItem.Description;
      if (string.IsNullOrEmpty((string) lstring))
        lstring = dataItem.Title;
      singleItemLink.ToolTip = (string) lstring;
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;

    /// <summary>
    /// Handles the ItemDataBound event of the ItemsList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void ItemsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      switch (e.Item.ItemType)
      {
        case RadListViewItemType.DataItem:
        case RadListViewItemType.AlternatingItem:
          T dataItem = (T) ((RadListViewDataItem) e.Item).DataItem;
          this.ConfigureDetailLink((HyperLink) e.Item.FindControl("singleItemLink"), dataItem, e.Item);
          break;
      }
    }

    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }
  }
}
