// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplateHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Provides helper methods for operating with Page templates
  /// </summary>
  internal class PageTemplateHelper
  {
    internal const string DefaultThumbnailImagePath = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif";
    internal static readonly Guid DefaultTemplateThumbnailImageId = new Guid("3b47ed9b-9073-4a36-8866-ed794fdeed22");

    /// <summary>
    /// Installs the template thumbnails user library.
    /// This libraries is intended for storing thumbnails of layout templates
    /// </summary>
    /// <param name="initializer">The SiteInitializer</param>
    /// <returns>Indicator whether the method succeeded</returns>
    internal static bool InstallTemplateThumbnailsLibrary(SiteInitializer initializer)
    {
      LibrariesManager managerInTransaction = initializer.GetManagerInTransaction<LibrariesManager>("SystemLibrariesProvider");
      bool suppressSecurityChecks = managerInTransaction.Provider.SuppressSecurityChecks;
      try
      {
        managerInTransaction.Provider.SuppressSecurityChecks = true;
        Album album = managerInTransaction.GetAlbums().FirstOrDefault<Album>((Expression<Func<Album, bool>>) (lib => lib.Id == LibrariesModule.DefaultTemplateThumbnailsLibraryId));
        if (album == null)
        {
          album = managerInTransaction.CreateAlbum(LibrariesModule.DefaultTemplateThumbnailsLibraryId);
          album.Title = (Lstring) "Template Thumbnails";
          album.UrlName = (Lstring) "template-thumbnails";
          managerInTransaction.RecompileItemUrls<Album>(album);
        }
        album.DownloadSecurityProviderName = "TemplateThumbnailsDownloadSecurityProvider";
        PageTemplateHelper.UploadDefaultTemplateImages(initializer, managerInTransaction, album);
      }
      finally
      {
        managerInTransaction.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      return true;
    }

    /// <summary>
    /// Gets the related image. This is the template thumbnail.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <returns>The thumbnail image.</returns>
    internal static Telerik.Sitefinity.Libraries.Model.Image GetRelatedImage(
      PageTemplate pageTemplate)
    {
      int? totalCount = new int?();
      IQueryable relatedItems = RelatedDataHelper.GetRelatedItems(pageTemplate.GetType().FullName, pageTemplate.GetProviderName(), pageTemplate.Id, PageTemplate.ThumbnailFieldName, new ContentLifecycleStatus?(), string.Empty, string.Empty, new int?(0), new int?(0), ref totalCount, typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName, "SystemLibrariesProvider");
      return relatedItems != null ? Queryable.OfType<IDataItem>(relatedItems).FirstOrDefault<IDataItem>() as Telerik.Sitefinity.Libraries.Model.Image : (Telerik.Sitefinity.Libraries.Model.Image) null;
    }

    /// <summary>
    /// Relates the default thumbnail image for templates to a given template.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns>The related default image.</returns>
    internal static Telerik.Sitefinity.Libraries.Model.Image RelateDefaultThumbnailImage(
      PageTemplate template)
    {
      Album thumbnailsLibrary = PageTemplateHelper.GetTemplateThumbnailsLibrary();
      if (thumbnailsLibrary == null)
        return (Telerik.Sitefinity.Libraries.Model.Image) null;
      Telerik.Sitefinity.Libraries.Model.Image image = thumbnailsLibrary.Images().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == PageTemplateHelper.DefaultTemplateThumbnailImageId));
      return PageTemplateHelper.RelateThumnailToTemplate(template, image);
    }

    /// <summary>
    /// Relates a thumbnail image for templates to a given template.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <param name="thumbnailTitle">The title of the thumbnail to be related.</param>
    /// <param name="transaction">The name of the transaction.</param>
    /// <returns>The related image.</returns>
    internal static Telerik.Sitefinity.Libraries.Model.Image RelateThumbnailImage(
      PageTemplate template,
      string thumbnailTitle,
      string transaction)
    {
      Album thumbnailsLibrary = PageTemplateHelper.GetTemplateThumbnailsLibrary(transaction);
      if (thumbnailsLibrary == null)
        return (Telerik.Sitefinity.Libraries.Model.Image) null;
      Telerik.Sitefinity.Libraries.Model.Image image = thumbnailsLibrary.Images().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Title == (Lstring) thumbnailTitle));
      return PageTemplateHelper.RelateThumnailToTemplate(template, image, transaction);
    }

    internal static Telerik.Sitefinity.Libraries.Model.Image GetDefaultThumbnailImage() => PageTemplateHelper.GetTemplateThumbnailsLibrary().Images().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == PageTemplateHelper.DefaultTemplateThumbnailImageId));

    internal static PageTemplate ResolvePageTemplate(
      PageData pageData,
      Func<PageSiteNode> pageSiteNodeRetriever)
    {
      IEnumerable<object> objects = ObjectFactory.Container.ResolveAll(typeof (IPageTemplateResolver));
      PageSiteNode pageSiteNode = (PageSiteNode) null;
      foreach (IPageTemplateResolver templateResolver in objects)
      {
        if (pageSiteNode == null)
          pageSiteNode = pageSiteNodeRetriever();
        PageSiteNode node = pageSiteNode;
        Guid id = templateResolver.Resolve(node);
        if (id != Guid.Empty)
          return PageManager.GetManager().GetTemplate(id);
      }
      return pageData.Template;
    }

    internal static IQueryable<PageTemplate> FilterFrameworkSpecificTemplates(
      IQueryable<PageTemplate> templates)
    {
      Expression<Func<PageTemplate, bool>> expression;
      switch (Config.Get<PagesConfig>().PageTemplatesFrameworks)
      {
        case PageTemplatesAvailability.All:
          return templates;
        case PageTemplatesAvailability.HybridAndMvc:
          expression = (Expression<Func<PageTemplate, bool>>) (t => (int) t.Framework == 1 || (int) t.Framework == 0);
          break;
        default:
          expression = (Expression<Func<PageTemplate, bool>>) (t => (int) t.Framework == 1);
          break;
      }
      Expression<Func<PageTemplate, bool>> predicate = expression;
      return templates.Where<PageTemplate>(predicate);
    }

    /// <summary>Mocks basic templates for Webforms framework.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>List from the available templates.</returns>
    internal static IList<PageTemplate> CreateWebformsBasicTemplates(Guid siteId)
    {
      PageManager manager = PageManager.GetManager();
      IQueryable<PageTemplate> queryable = (!(siteId != Guid.Empty) ? manager.GetTemplates() : manager.GetTemplates(siteId)).Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (x => x.Category == SiteInitializer.BasicTemplatesCategoryId));
      List<PageTemplate> source = new List<PageTemplate>();
      foreach (PageTemplate pageTemplate1 in (IEnumerable<PageTemplate>) queryable)
      {
        if (!(pageTemplate1.Id == SiteInitializer.HybridEmptyTemplateId))
        {
          PageTemplate pageTemplate2 = new PageTemplate()
          {
            ApplicationName = pageTemplate1.ApplicationName,
            Category = SiteInitializer.WebFormsBasicTemplatesCategoryId,
            Culture = pageTemplate1.Culture != null ? pageTemplate1.Culture : SystemManager.CurrentContext.Culture.ToString(),
            Title = pageTemplate1.Title,
            Id = pageTemplate1.Id,
            Visible = pageTemplate1.Visible,
            Ordinal = pageTemplate1.Ordinal,
            Name = pageTemplate1.Name,
            Framework = PageTemplateFramework.WebForms,
            Key = pageTemplate1.Key
          };
          source.Add(pageTemplate2);
        }
      }
      if (source.Count > 0 && !source.Any<PageTemplate>((Func<PageTemplate, bool>) (x => x.Id == SiteInitializer.WebFormsEmptyTemplateId)))
        source = source.Prepend<PageTemplate>(PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(SiteInitializer.WebFormsEmptyTemplateId, false)).ToList<PageTemplate>();
      return (IList<PageTemplate>) source;
    }

    internal static string GetMockedTemplateImage(Guid templateId)
    {
      PageTemplate pageTemplate = PageManager.GetManager().GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == templateId)).FirstOrDefault<PageTemplate>();
      return pageTemplate != null ? pageTemplate.GetBigThumbnailUrl((Telerik.Sitefinity.Libraries.Model.Image) null) : new PageTemplate().GetBigThumbnailUrl(PageTemplateHelper.GetDefaultThumbnailImage());
    }

    /// <summary>
    /// Checks whether one of the on demand default templates exist and if so, returns it.
    /// Otherwise creates and returns it.
    /// </summary>
    /// <param name="id">The Guid of the template.</param>
    /// <param name="create">Specifies whether the requested template should be created or only mocked.</param>
    /// <returns>The requested template.</returns>
    internal static PageTemplate CheckCreateOrMockAndReturnBasicEmptyTemplate(
      Guid id,
      bool create)
    {
      if (!PageTemplateHelper.IsOnDemandTempalteId(id))
        return (PageTemplate) null;
      PageManager manager = PageManager.GetManager();
      using (new ElevatedModeRegion((IManager) manager))
      {
        try
        {
          return manager.GetTemplate(id);
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case ItemNotFoundException _:
            case NoSuchObjectException _:
              return PageTemplateHelper.CreateBasicPageTemplate(id, manager, create);
            default:
              throw;
          }
        }
      }
    }

    /// <summary>Creates basic empty page template upon request.</summary>
    /// <param name="id">The template id from predefined values.</param>
    /// <param name="pageManager">Reference to the PageManager.</param>
    /// <param name="create">Specifies whether the requested template should be created or only mocked.</param>
    /// <returns>The created template.</returns>
    private static PageTemplate CreateBasicPageTemplate(
      Guid id,
      PageManager pageManager,
      bool create)
    {
      if (!PageTemplateHelper.IsOnDemandTempalteId(id))
        throw new System.InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Creating basic templates on demand is available only for predefined set of Guid-s and {0} is not included.", (object) id));
      bool flag = id == SiteInitializer.WebFormsEmptyTemplateId;
      string key = flag ? "BasicWebformsTemplate" : "BasicHybridTemplate";
      PageTemplate template = pageManager.CreateTemplate(id);
      template.Framework = flag ? PageTemplateFramework.WebForms : PageTemplateFramework.Hybrid;
      template.Category = flag ? SiteInitializer.WebFormsBasicTemplatesCategoryId : SiteInitializer.BasicTemplatesCategoryId;
      template.Key = key;
      template.Name = key;
      template.Ordinal = (short) -2;
      Res.SetLstring(template.Title, typeof (PageResources), key);
      if (create)
      {
        template.SetParentTemplate((PageTemplate) null);
        template.LanguageData.Add(pageManager.CreatePublishedLanguageData());
        pageManager.SaveChanges();
        PageTemplateHelper.RelateThumnailToTemplate(template, PageTemplateHelper.GetDefaultThumbnailImage());
      }
      return template;
    }

    internal static bool IsOnDemandTempalteId(Guid id) => id == SiteInitializer.WebFormsEmptyTemplateId || id == SiteInitializer.HybridEmptyTemplateId;

    private static Album GetTemplateThumbnailsLibrary(string transaction = null) => LibrariesManager.GetManager("SystemLibrariesProvider", transaction).GetAlbums().FirstOrDefault<Album>((Expression<Func<Album, bool>>) (lib => lib.Id == LibrariesModule.DefaultTemplateThumbnailsLibraryId));

    private static Telerik.Sitefinity.Libraries.Model.Image RelateThumnailToTemplate(
      PageTemplate template,
      Telerik.Sitefinity.Libraries.Model.Image image,
      string transaction = null)
    {
      if (image != null)
      {
        ContentLinkChange[] changedRelations = new ContentLinkChange[1]
        {
          new ContentLinkChange()
          {
            ChildItemId = image.Id,
            ChildItemProviderName = image.GetProviderName(),
            ChildItemType = image.GetType().FullName,
            ComponentPropertyName = PageTemplate.ThumbnailFieldName,
            Ordinal = new float?(-2f),
            State = ContentLinkChangeState.Added
          }
        };
        PageManager manager = PageManager.GetManager(template.GetProviderName(), transaction);
        try
        {
          RelatedDataHelper.SaveRelatedDataChanges((IManager) manager, (IDataItem) template, changedRelations);
        }
        catch (Exception ex)
        {
          return (Telerik.Sitefinity.Libraries.Model.Image) null;
        }
      }
      return image;
    }

    /// <summary>
    /// Moving the default images for the templates in the new template thumbnail library
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="librariesManager">The Libraries manager.</param>
    /// <param name="album">The template thumbnails album</param>
    private static void UploadDefaultTemplateImages(
      SiteInitializer initializer,
      LibrariesManager librariesManager,
      Album album)
    {
      TemplateInitializer templateInitializer = new TemplateInitializer(initializer.PageManager);
      foreach (PageTemplate pageTemplate in initializer.PageManager.GetTemplates().ToList<PageTemplate>().Where<PageTemplate>((Func<PageTemplate, bool>) (t => templateInitializer.IsDefaultTemplate(t.Id) && !t.IsBackend)))
      {
        if (PageTemplateHelper.GetRelatedImage(pageTemplate) == null)
        {
          string image = pageTemplate.GetImage("icon");
          if (!string.IsNullOrEmpty(image))
          {
            string imageTitle = PageTemplateHelper.GetImageTitle(image);
            Telerik.Sitefinity.Libraries.Model.Image dataItem = PageTemplateHelper.UploadImageFromResources(librariesManager, album, imageTitle, image, ImageFormat.Png, ".png");
            ContentLinkChange[] changedRelations = new ContentLinkChange[1]
            {
              new ContentLinkChange()
              {
                ChildItemId = dataItem.Id,
                ChildItemProviderName = dataItem.GetProviderName(),
                ChildItemType = dataItem.GetType().FullName,
                ComponentPropertyName = PageTemplate.ThumbnailFieldName,
                Ordinal = new float?(-2f),
                State = ContentLinkChangeState.Added
              }
            };
            RelatedDataHelper.SaveRelatedDataChanges((IManager) initializer.PageManager, (IDataItem) pageTemplate, changedRelations);
          }
        }
      }
      PageTemplateHelper.UploadDefaulTemplateThumbnail(librariesManager, album);
    }

    /// <summary>
    /// The same logic can be found in LayoutFileManager -&gt; UpdateDefaultTemplateImages(). In need of a change do it in both places
    /// </summary>
    /// <param name="pageManager">The Pages manager.</param>
    /// <param name="librariesManager">The Libraries manager.</param>
    /// <param name="album">The template thumbnails album</param>
    internal static void UpdateDefaultTemplateImages(
      PageManager pageManager,
      LibrariesManager librariesManager,
      Album album)
    {
      TemplateInitializer templateInitializer = new TemplateInitializer(pageManager);
      foreach (PageTemplate pageTemplate in pageManager.GetTemplates().ToList<PageTemplate>().Where<PageTemplate>((Func<PageTemplate, bool>) (t => templateInitializer.IsDefaultTemplate(t.Id) && !t.IsBackend)))
      {
        Telerik.Sitefinity.Libraries.Model.Image relatedImage = PageTemplateHelper.GetRelatedImage(pageTemplate);
        if (relatedImage != null)
        {
          string image1 = pageTemplate.GetImage("icon");
          if (!string.IsNullOrEmpty(image1))
          {
            using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream(image1))
            {
              if (manifestResourceStream != null)
              {
                using (System.Drawing.Image image2 = System.Drawing.Image.FromStream(manifestResourceStream))
                {
                  MemoryStream source = new MemoryStream();
                  image2.Save((Stream) source, ImageFormat.Png);
                  Telerik.Sitefinity.Libraries.Model.Image master = librariesManager.GetMaster(librariesManager.GetImage(relatedImage.Id));
                  librariesManager.Upload((MediaContent) master, (Stream) source, ".png", true);
                  librariesManager.Lifecycle.Publish((ILifecycleDataItem) master);
                }
              }
            }
          }
        }
      }
      Telerik.Sitefinity.Libraries.Model.Image content = album.Images().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == PageTemplateHelper.DefaultTemplateThumbnailImageId && (int) i.Status == 0));
      if (content == null)
        return;
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif"))
      {
        using (System.Drawing.Image image = System.Drawing.Image.FromStream(manifestResourceStream))
        {
          MemoryStream source = new MemoryStream();
          image.Save((Stream) source, ImageFormat.Png);
          librariesManager.Upload((MediaContent) content, (Stream) source, ".png", true);
          librariesManager.Lifecycle.Publish((ILifecycleDataItem) content);
        }
      }
    }

    /// <summary>Uploads the image from resources.</summary>
    /// <param name="librariesManager">The libraries manager.</param>
    /// <param name="album">The album.</param>
    /// <param name="imageTitle">The image title.</param>
    /// <param name="imageResource">The image resource.</param>
    /// <param name="imageFormat">The image format.</param>
    /// <param name="imageExtension">The image extension.</param>
    /// <param name="imageId">The image id.</param>
    /// <returns>Returns the uploaded image.</returns>
    private static Telerik.Sitefinity.Libraries.Model.Image UploadImageFromResources(
      LibrariesManager librariesManager,
      Album album,
      string imageTitle,
      string imageResource,
      ImageFormat imageFormat,
      string imageExtension = null,
      Guid imageId = default (Guid))
    {
      Telerik.Sitefinity.Libraries.Model.Image content = !(imageId == Guid.Empty) ? librariesManager.CreateImage(imageId) : librariesManager.CreateImage();
      string str = imageTitle;
      content.Parent = (Library) album;
      content.Title = (Lstring) str;
      content.UrlName = (Lstring) str.ToLower().Replace(' ', '-');
      content.Description = (Lstring) ("Description_" + imageTitle);
      content.AlternativeText = (Lstring) ("AltText_" + imageTitle);
      content.ApprovalWorkflowState = (Lstring) "Published";
      librariesManager.RecompileItemUrls<Telerik.Sitefinity.Libraries.Model.Image>(content);
      using (Stream manifestResourceStream = Assembly.Load("Telerik.Sitefinity.Resources").GetManifestResourceStream(imageResource))
      {
        using (System.Drawing.Image image = System.Drawing.Image.FromStream(manifestResourceStream))
        {
          MemoryStream source = new MemoryStream();
          image.Save((Stream) source, imageFormat);
          librariesManager.Upload((MediaContent) content, (Stream) source, imageExtension ?? Path.GetExtension(imageResource));
          librariesManager.Lifecycle.Publish((ILifecycleDataItem) content);
        }
      }
      return content;
    }

    /// <summary>Gets the image title from a given path to a resource.</summary>
    /// <param name="builtInThumbnailPath">The built in thumbnail path.</param>
    /// <returns>The image title</returns>
    private static string GetImageTitle(string builtInThumbnailPath)
    {
      string[] source = builtInThumbnailPath.Split(new char[1]
      {
        '.'
      }, StringSplitOptions.RemoveEmptyEntries);
      return ((IEnumerable<string>) source).Count<string>() < 2 ? Path.GetFileNameWithoutExtension(builtInThumbnailPath) : source[((IEnumerable<string>) source).Count<string>() - 2];
    }

    /// <summary>
    /// Uploads the default template thumbnail. It will be used when new items are created or duplicated.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="album">The album.</param>
    private static void UploadDefaulTemplateThumbnail(LibrariesManager manager, Album album)
    {
      if (album.Images().FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == PageTemplateHelper.DefaultTemplateThumbnailImageId)) != null)
        return;
      PageTemplateHelper.UploadImageFromResources(manager, album, PageTemplateHelper.GetImageTitle("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif"), "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif", ImageFormat.Png, ".png", PageTemplateHelper.DefaultTemplateThumbnailImageId);
    }
  }
}
