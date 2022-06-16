// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.ImageControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Text;
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
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  [ControlDesigner(typeof (SingleMediaContentItemDesigner))]
  [PropertyEditorTitle(typeof (LibrariesResources), "ImageControlPropertyEditorTitle")]
  public class ImageControl : 
    SimpleView,
    ICustomWidgetVisualization,
    IBrowseAndEditable,
    IHasContainerType,
    IWidgetData,
    IRelatedDataView,
    IPersonalizable,
    IContentLocatableView,
    IHasCacheDependency
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.ImageControl.ascx");
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    private BrowseAndEditToolbar browseAndEditToolbar;
    private LibrariesManager manager;
    private Telerik.Sitefinity.Libraries.Model.Image image;
    private string alignment = "None";
    private Guid imageId;
    private RelatedDataDefinition relatedDataDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.ImageControl" /> class.
    /// </summary>
    public ImageControl() => this.DisableCanonicalUrlMetaTag = new bool?(true);

    /// <summary>The id of the image that will be displayed.</summary>
    public Guid ImageId
    {
      get => this.imageId;
      set => this.imageId = value;
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ImageControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>The processing method of the image to be displayed</summary>
    public string Method { get; set; }

    /// <summary>The processing method properties</summary>
    public string CustomSizeMethodProperties { get; set; }

    /// <summary>
    /// Height of the image to be displayed
    /// <remarks>Currently, resizing is done just by the width</remarks>
    /// </summary>
    public int Height { get; set; }

    /// <summary>Width of the image to be displayed</summary>
    public int Width { get; set; }

    /// <summary>
    /// If true when click the control will open the original image
    /// </summary>
    public bool OpenOriginalImageOnClick { get; set; }

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
      get => string.IsNullOrEmpty(base.CssClass) ? "sfimageWrp" : base.CssClass;
      set => base.CssClass = value;
    }

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

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    public virtual string Title { get; set; }

    /// <summary>Gets or sets the alt text of the image item.</summary>
    public string AlternateText { get; set; }

    /// <summary>Gets or sets the name of the thumbnail.</summary>
    /// <value>The name of the thumbnail.</value>
    public string ThumbnailName { get; set; }

    /// <summary>Gets or sets the margin top style value.</summary>
    public int? MarginTop { get; set; }

    /// <summary>Gets or sets the margin bottom style value.</summary>
    public int? MarginBottom { get; set; }

    /// <summary>Gets or sets the margin left style value.</summary>
    public int? MarginLeft { get; set; }

    /// <summary>Gets or sets the margin right style value.</summary>
    public int? MarginRight { get; set; }

    /// <summary>
    /// Gets or sets the alignment of the image. Possible values: None, Left, Center, Right
    /// </summary>
    public string Alignment
    {
      get => this.alignment;
      set => this.alignment = value;
    }

    /// <summary>Gets or sets the min width limitation.</summary>
    public int? MinWidth { get; set; }

    /// <summary>Gets or sets the min height limitation.</summary>
    public int? MinHeight { get; set; }

    /// <summary>Gets or sets the max width limitation.</summary>
    public int? MaxWidth { get; set; }

    /// <summary>Gets or sets the max height limitation.</summary>
    public int? MaxHeight { get; set; }

    /// <summary>Gets or sets the max file size that can be uploaded.</summary>
    public int? UploadedFileMaxSize { get; set; }

    /// <summary>
    /// Gets or sets the display mode that specify the size of the image to be rendered (original, thumbnail or custom).
    /// By default the original size is shown.
    /// </summary>
    public ImageDisplayMode DisplayMode { get; set; }

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

    internal string DataFieldName { get; set; }

    /// <inheritdoc />
    Type IHasContainerType.ContainerType { get; set; }

    /// <summary>Returns the type of the control</summary>
    /// <remarks> This property is used in the widget designer js </remarks>
    public virtual Type ViewType => this.GetType();

    /// <summary>
    /// Gets the image editor that will contain uploaded image.
    /// </summary>
    /// <value>The grid.</value>
    protected internal virtual System.Web.UI.WebControls.Image ImageItem => this.Container.GetControl<System.Web.UI.WebControls.Image>("imageItem", true);

    /// <summary>
    /// Gets the link that will represent the image which on click will open the original one
    /// </summary>
    /// <value>The grid.</value>
    protected internal virtual HtmlAnchor OriginalImageLink => this.Container.GetControl<HtmlAnchor>("originalImageLink", true);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      base.AddAttributesToRender(writer);
      string lowerInvariant = this.Alignment.ToLowerInvariant();
      if (!(lowerInvariant == "left"))
      {
        if (!(lowerInvariant == "center"))
        {
          if (!(lowerInvariant == "right"))
            return;
          this.ImageItem.Style.Add("float", "right");
        }
        else
          this.ImageItem.Style.Add(HtmlTextWriterStyle.VerticalAlign, "middle");
      }
      else
        this.ImageItem.Style.Add("float", "left");
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      if (this.image != null && this.IsInlineEditingMode() && !this.DisplayRelatedData())
      {
        if (this.GetDataItem() is DynamicContent)
        {
          writer.AddAttribute("data-sf-ftype", "ImageField");
          writer.AddAttribute("data-sf-field", this.DataFieldName);
        }
        else if (this.IsEditable())
        {
          writer.AddAttribute("data-sf-provider", this.ProviderName);
          writer.AddAttribute("data-sf-type", typeof (PageDraftControl).FullName);
          writer.AddAttribute("data-sf-ftype", nameof (ImageControl));
        }
      }
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
      bool flag1 = false;
      if (this.ImageId != Guid.Empty || this.DisplayRelatedData())
      {
        bool flag2 = true;
        if (this.ImageId != Guid.Empty)
        {
          Guid imageId = this.ImageId;
          this.image = this.Manager.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == imageId)).Where<Telerik.Sitefinity.Libraries.Model.Image>(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
        }
        else if (this.IsModuleEnabledForCurrentSite())
          this.image = this.GetRelatedItem() as Telerik.Sitefinity.Libraries.Model.Image;
        else
          flag2 = false;
        if (this.image != null)
        {
          if (this.TitleLabel != null)
            this.TitleLabel.Text = HttpUtility.HtmlEncode(this.Title);
          object resultItem = (object) null;
          if (ContentLocatableViewExtensions.TryGetItemWithRequestedStatus((ILifecycleDataItem) this.image, (ILifecycleManager) this.Manager, out resultItem))
            this.image = resultItem as Telerik.Sitefinity.Libraries.Model.Image;
          string str1 = (string) null;
          bool generateAbsoluteUrls = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
          string str2;
          if (this.DisplayMode == ImageDisplayMode.Thumbnail || !string.IsNullOrWhiteSpace(this.ThumbnailName))
          {
            str2 = this.image.ResolveThumbnailUrl(this.ThumbnailName, generateAbsoluteUrls);
            if (this.image.IsVectorGraphics())
              this.image.ApplyThumbnailProfileToControl((WebControl) this.ImageItem, this.ThumbnailName);
          }
          else if (this.DisplayMode == ImageDisplayMode.Custom)
          {
            Dictionary<string, string> urlParameters = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(this.CustomSizeMethodProperties);
            if (this.image.IsVectorGraphics())
            {
              str1 = this.image.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
              str2 = str1;
              if (urlParameters["MaxWidth"] != null)
                this.ImageItem.Attributes.Add("width", urlParameters["MaxWidth"]);
              if (urlParameters["MaxHeight"] != null)
                this.ImageItem.Attributes.Add("height", urlParameters["MaxHeight"]);
            }
            else
            {
              urlParameters.Add("Method", this.Method);
              str2 = this.image.ResolveMediaUrl(urlParameters, generateAbsoluteUrls);
            }
          }
          else
          {
            str1 = this.image.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
            str2 = str1;
          }
          int num;
          if (this.Height > 0)
          {
            this.ImageItem.Attributes.Add("height", this.Height.ToString());
            System.Web.UI.AttributeCollection attributes = this.ImageItem.Attributes;
            num = this.Width;
            string str3 = num.ToString();
            attributes.Add("width", str3);
          }
          else if (this.Width > 0)
          {
            System.Web.UI.AttributeCollection attributes = this.ImageItem.Attributes;
            num = this.Width;
            string str4 = num.ToString();
            attributes.Add("width", str4);
          }
          this.ImageItem.ImageUrl = str2;
          this.ImageItem.AlternateText = HttpUtility.HtmlEncode(string.IsNullOrEmpty(this.AlternateText) ? this.image.AlternativeText.ToString() : this.AlternateText);
          if (string.IsNullOrEmpty(this.ToolTip) && !string.IsNullOrEmpty((string) this.image.Title))
            this.ImageItem.ToolTip = HttpUtility.HtmlEncode(this.image.Title.ToString());
          else
            this.ImageItem.ToolTip = HttpUtility.HtmlEncode(this.ToolTip);
          int? marginTop = this.MarginTop;
          num = 0;
          if (marginTop.GetValueOrDefault() == num & marginTop.HasValue)
          {
            int? marginBottom = this.MarginBottom;
            num = 0;
            if (marginBottom.GetValueOrDefault() == num & marginBottom.HasValue)
            {
              int? marginLeft = this.MarginLeft;
              num = 0;
              if (marginLeft.GetValueOrDefault() == num & marginLeft.HasValue)
              {
                int? marginRight = this.MarginRight;
                num = 0;
                if (marginRight.GetValueOrDefault() == num & marginRight.HasValue)
                {
                  this.ImageItem.Style.Add(HtmlTextWriterStyle.Margin, "0");
                  goto label_42;
                }
              }
            }
          }
          this.ImageItem.Style.Remove(HtmlTextWriterStyle.Margin);
          if (this.MarginLeft.HasValue)
            this.ImageItem.Style.Add(HtmlTextWriterStyle.MarginLeft, this.MarginLeft.ToString() + "px");
          int? nullable = this.MarginBottom;
          if (nullable.HasValue)
            this.ImageItem.Style.Add(HtmlTextWriterStyle.MarginBottom, this.MarginBottom.ToString() + "px");
          nullable = this.MarginRight;
          if (nullable.HasValue)
            this.ImageItem.Style.Add(HtmlTextWriterStyle.MarginRight, this.MarginRight.ToString() + "px");
          nullable = this.MarginTop;
          if (nullable.HasValue)
            this.ImageItem.Style.Add(HtmlTextWriterStyle.MarginTop, this.MarginTop.ToString() + "px");
label_42:
          this.ImageItem.GenerateEmptyAlternateText = true;
          if (this.OpenOriginalImageOnClick)
          {
            this.OriginalImageLink.Controls.Add((Control) this.ImageItem);
            this.OriginalImageLink.HRef = str1 ?? this.image.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
            this.OriginalImageLink.Title = HttpUtility.HtmlEncode((string) this.image.AlternativeText);
          }
          else
            this.OriginalImageLink.Visible = false;
          this.AddCanonicalUrlTagIfEnabled(this.Page, (IDataItem) this.image);
          this.SubscribeCacheDependency();
        }
        else if (this.IsDesignMode() && !this.IsInlineEditingMode())
        {
          this.Controls.Clear();
          if (flag2)
            this.Controls.Add((Control) new LiteralControl(Res.Get<ImagesResources>().ImageWasNotSelectedOrHasBeenDeleted));
          else
            this.Controls.Add(this.GetModuleErrorMessageControl());
        }
        else
          flag1 = true;
      }
      else
        flag1 = true;
      this.IsEmpty = this.ImageId == Guid.Empty;
      if (flag1)
      {
        this.ImageItem.Visible = false;
        this.OriginalImageLink.Visible = false;
        this.TitleLabel.Visible = false;
      }
      if (!SystemManager.IsBrowseAndEditMode)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

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
      if (this.image != null)
        dependencyObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(this.image.GetType(), this.image.Id));
      return (IList<CacheDependencyKey>) dependencyObjects;
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

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty { get; protected set; }

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<ImagesResources>().SelectAnImage;

    public string GetProviderName() => !this.ProviderName.IsNullOrEmpty() ? this.ProviderName : this.Manager.Provider.Name;

    public bool DefinedProviderNotAvailable() => !this.ProviderName.IsNullOrEmpty() && this.ProviderName != this.Manager.Provider.Name;

    public string ContentType => typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName;

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
      if (!(this.ImageId != Guid.Empty))
        return (IEnumerable<IContentLocationInfo>) null;
      ContentLocationInfo contentLocationInfo = new ContentLocationInfo();
      contentLocationInfo.ContentType = typeof (Telerik.Sitefinity.Libraries.Model.Image);
      contentLocationInfo.ProviderName = this.ProviderName;
      contentLocationInfo.Priority = ContentLocationPriority.Lowest;
      Telerik.Sitefinity.Libraries.Model.Image image = this.Manager.GetImage(this.ImageId);
      List<string> itemIds = new List<string>();
      List<string> stringList1 = itemIds;
      Guid guid = image.Id;
      string str1 = guid.ToString();
      stringList1.Add(str1);
      if (image.OriginalContentId != Guid.Empty)
      {
        List<string> stringList2 = itemIds;
        guid = image.OriginalContentId;
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
