// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DocumentLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>
  /// A control that displays a link to a single document from libraries
  /// </summary>
  [ControlDesigner(typeof (SingleMediaContentItemDesigner))]
  [PropertyEditorTitle(typeof (DocumentsResources), "DocumentLinkPropertyEditorTitle")]
  public class DocumentLink : 
    SimpleView,
    ICustomWidgetVisualization,
    IBrowseAndEditable,
    IWidgetData,
    IRelatedDataView,
    IHasCacheDependency,
    IContentLocatableView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.DocumentLink.ascx");
    private BrowseAndEditToolbar browseAndEditToolbar;
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    private LibrariesManager manager;
    private RelatedDataDefinition relatedDataDefinition;

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the text of the document link control.</summary>
    /// <value>The text.</value>
    public string Text { get; set; }

    /// <summary>Gets or sets the ID of the document to link to</summary>
    public Guid DocumentId { get; set; }

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => this.DocumentId == Guid.Empty;

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<DocumentsResources>().SelectDocument;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DocumentLink.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the type of the thumbnail.</summary>
    /// <value>The type of the thumbnail.</value>
    public ThumbnailType ThumbnailType { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
    /// </summary>
    /// <value></value>
    /// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
    public override string CssClass
    {
      get => string.IsNullOrEmpty(base.CssClass) ? "sfdownloadFileWrp" : base.CssClass;
      set => base.CssClass = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets the manager.</summary>
    public LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
        {
          try
          {
            this.manager = LibrariesManager.GetManager(this.ProviderName);
          }
          catch (MissingProviderConfigurationException ex)
          {
            this.manager = LibrariesManager.GetManager();
          }
        }
        return this.manager;
      }
    }

    /// <summary>Gets the document link control.</summary>
    /// <value>The document link control.</value>
    protected virtual HyperLink DocumentLinkControl => this.Container.GetControl<HyperLink>("documentLink", true);

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>("browseAndEditToolbar", true);
        return this.browseAndEditToolbar;
      }
    }

    /// <summary>Gets the items container.</summary>
    /// <value>The items container.</value>
    protected internal virtual HtmlGenericControl ItemsContainer => this.Container.GetControl<HtmlGenericControl>("itemsContainer", true);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
      writer.RenderBeginTag(this.TagKey);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DocumentId != Guid.Empty || this.DisplayRelatedData())
      {
        Document mediaContent = (Document) null;
        bool flag = true;
        if (this.DocumentId != Guid.Empty)
        {
          Guid documentId = this.DocumentId;
          mediaContent = this.Manager.GetDocuments().Where<Document>((Expression<Func<Document, bool>>) (d => d.Id == documentId)).Where<Document>(PredefinedFilters.PublishedItemsFilter<Document>()).SingleOrDefault<Document>();
        }
        else if (this.IsModuleEnabledForCurrentSite())
          mediaContent = this.GetRelatedItem() as Document;
        else
          flag = false;
        if (mediaContent != null)
        {
          if (this.TitleLabel != null)
            this.TitleLabel.Text = HttpUtility.HtmlEncode(this.Title);
          this.DocumentLinkControl.NavigateUrl = mediaContent.ResolveMediaUrl(false, (CultureInfo) null);
          this.DocumentLinkControl.Text = HttpUtility.HtmlEncode((string) mediaContent.Title);
          string str = mediaContent.Extension;
          if (str.Length > 0)
            str = str.Remove(0, 1);
          this.DocumentLinkControl.CssClass = "sf" + str.ToLower();
          this.SubscribeCacheDependency();
        }
        else if (this.IsDesignMode())
        {
          this.Controls.Clear();
          if (flag)
            this.Controls.Add((Control) new LiteralControl(Res.Get<DocumentsResources>().DocumentWasNotSelectedOrHasBeenDeleted));
          else
            this.Controls.Add(this.GetModuleErrorMessageControl());
        }
        else
          this.DocumentLinkControl.Visible = false;
        if (this.ItemsContainer != null)
        {
          if (this.ThumbnailType == ThumbnailType.SmallIcons)
            this.ItemsContainer.Attributes["class"] += " sfSmallIcns";
          else if (this.ThumbnailType == ThumbnailType.BigIcons)
            this.ItemsContainer.Attributes["class"] += " sfLargeIcns";
        }
      }
      else
      {
        this.DocumentLinkControl.Visible = false;
        this.TitleLabel.Visible = false;
      }
      if (!SystemManager.IsBrowseAndEditMode)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    public string GetProviderName() => !this.ProviderName.IsNullOrEmpty() ? this.ProviderName : this.Manager.Provider.Name;

    public bool DefinedProviderNotAvailable() => !this.ProviderName.IsNullOrEmpty() && this.ProviderName != this.Manager.Provider.Name;

    public string ContentType => typeof (Document).FullName;

    /// <summary>Subscribes the cache dependency.</summary>
    protected virtual void SubscribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageDataCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageDataCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageDataCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    /// <value>A collection of  instances of type <see cref="!:CacheDependencyNotifiedObject" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> dependencyObjects = new List<CacheDependencyKey>();
      if (this.DocumentId != Guid.Empty)
        dependencyObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof (Document), this.DocumentId));
      return (IList<CacheDependencyKey>) dependencyObjects;
    }

    /// <inheritdoc />
    public bool? DisplayRelatedData { get; set; }

    /// <inheritdoc />
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public RelatedDataDefinition RelatedDataDefinition
    {
      get
      {
        if (this.relatedDataDefinition == null)
          this.relatedDataDefinition = new RelatedDataDefinition();
        return this.relatedDataDefinition;
      }
      set => this.relatedDataDefinition = value;
    }

    /// <inheritdoc />
    public string GetContentType() => this.ContentType;

    /// <inheritdoc />
    public string UrlKeyPrefix { get; set; }

    /// <inheritdoc />
    public IEnumerable<IContentLocationInfo> GetLocations()
    {
      if (!(this.DocumentId != Guid.Empty))
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo contentLocationInfo = new ContentLocationInfo();
      contentLocationInfo.ContentType = typeof (Document);
      contentLocationInfo.ProviderName = this.ProviderName;
      contentLocationInfo.Priority = ContentLocationPriority.Lowest;
      Document document = this.Manager.GetDocument(this.DocumentId);
      List<string> itemIds = new List<string>();
      List<string> stringList1 = itemIds;
      Guid guid = document.Id;
      string str1 = guid.ToString();
      stringList1.Add(str1);
      if (document.OriginalContentId != Guid.Empty)
      {
        List<string> stringList2 = itemIds;
        guid = document.OriginalContentId;
        string str2 = guid.ToString();
        stringList2.Add(str2);
      }
      ContentLocationSingleItemFilter singleItemFilter = new ContentLocationSingleItemFilter((IEnumerable<string>) itemIds);
      contentLocationInfo.Filters.Add((IContentLocationFilter) singleItemFilter);
      return (IEnumerable<IContentLocationInfo>) new ContentLocationInfo[1]
      {
        contentLocationInfo
      };
    }

    /// <inheritdoc />
    public bool? DisableCanonicalUrlMetaTag { get; set; }
  }
}
