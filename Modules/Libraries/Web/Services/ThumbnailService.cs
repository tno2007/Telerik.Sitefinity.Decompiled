// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.ThumbnailService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing.Models;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// The WCF web service interface for thumbnails management.
  /// </summary>
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  public class ThumbnailService : IThumbnailService
  {
    /// <inheritdoc />
    public CollectionContext<WcfThumbnailProfile> GetThumbnailProfiles(
      string libraryType,
      string filter = "",
      string viewType = null)
    {
      ServiceUtility.RequestAuthentication();
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>().GetThumbnailProfiles(libraryType);
      if (thumbnailProfiles == null)
        return new CollectionContext<WcfThumbnailProfile>();
      IEnumerable<ThumbnailProfileConfigElement> source = (IEnumerable<ThumbnailProfileConfigElement>) thumbnailProfiles.Values;
      if (viewType != null)
      {
        string tags = this.GetTags(viewType);
        if (tags != null)
          source = source.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (p => p.Tags == null || ((IEnumerable<string>) p.Tags.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Intersect<string>((IEnumerable<string>) tags.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Any<string>()));
      }
      IQueryable<WcfThumbnailProfile> queryable = source.Select<ThumbnailProfileConfigElement, WcfThumbnailProfile>((Func<ThumbnailProfileConfigElement, WcfThumbnailProfile>) (s => new WcfThumbnailProfile(s, libraryType))).AsQueryable<WcfThumbnailProfile>();
      int? totalCount = new int?(queryable.Count<WcfThumbnailProfile>());
      IQueryable<WcfThumbnailProfile> items = DataProviderBase.SetExpressions<WcfThumbnailProfile>(queryable, filter, string.Empty, new int?(), new int?(), ref totalCount);
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfThumbnailProfile>((IEnumerable<WcfThumbnailProfile>) items)
      {
        TotalCount = totalCount.Value
      };
    }

    /// <inheritdoc />
    public CollectionContext<WcfThumbnailProfile> GetProfileNames(
      string libraryId,
      string provider,
      string viewType = null)
    {
      Guid id = Guid.Parse(libraryId);
      LibrariesManager manager = LibrariesManager.GetManager(provider);
      Library library = (Library) null;
      IFolder folder = manager.GetFolder(id);
      switch (folder)
      {
        case Folder _:
          library = manager.GetLibrary((folder as Folder).RootId);
          break;
        case Library _:
          library = folder as Library;
          break;
      }
      IList<string> keys = library.ThumbnailProfiles;
      string libraryType = library.GetType().FullName;
      IEnumerable<ThumbnailProfileConfigElement> source = (IEnumerable<ThumbnailProfileConfigElement>) Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>().GetThumbnailProfiles(libraryType).Values;
      if (viewType != null)
      {
        string tags = this.GetTags(viewType);
        if (tags != null)
          source = source.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (p => p.Tags == null || ((IEnumerable<string>) p.Tags.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Intersect<string>((IEnumerable<string>) tags.Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Any<string>()));
      }
      List<WcfThumbnailProfile> list = source.Where<ThumbnailProfileConfigElement>((Func<ThumbnailProfileConfigElement, bool>) (x => keys.Contains(x.Name))).OrderBy<ThumbnailProfileConfigElement, string>((Func<ThumbnailProfileConfigElement, string>) (x => x.Name)).Select<ThumbnailProfileConfigElement, WcfThumbnailProfile>((Func<ThumbnailProfileConfigElement, WcfThumbnailProfile>) (x => new WcfThumbnailProfile(x, libraryType))).ToList<WcfThumbnailProfile>();
      return new CollectionContext<WcfThumbnailProfile>((IEnumerable<WcfThumbnailProfile>) list)
      {
        TotalCount = list.Count
      };
    }

    /// <inheritdoc />
    public CollectionContext<WcfThumbnailProfile> BatchDeleteProfiles(
      string[] profileKeys,
      string libraryType)
    {
      LibrariesManager manager1 = LibrariesManager.GetManager();
      ConfigManager manager2 = ConfigManager.GetManager();
      LibrariesConfig section = manager2.GetSection<LibrariesConfig>();
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = section.GetThumbnailProfiles(libraryType);
      if (thumbnailProfiles != null)
      {
        foreach (string profileKey1 in profileKeys)
        {
          string profileKey = profileKey1;
          List<Guid> list = manager1.GetLibraries().Where<Library>((Expression<Func<Library, bool>>) (l => l.ThumbnailProfiles.Contains(profileKey))).Select<Library, Guid>((Expression<Func<Library, Guid>>) (x => x.Id)).ToList<Guid>();
          if (list.Count > 0)
            throw new Exception(Res.Get<LibrariesResources>("CannotDeleteSettingsInfo", (object) list.Count));
          thumbnailProfiles.Remove(profileKey);
        }
        manager2.SaveSection((ConfigSection) section, true);
      }
      return this.GetThumbnailProfiles(libraryType);
    }

    /// <inheritdoc />
    public Guid RegenerateThumbnails(
      string libraryType,
      string libraryProvider,
      string libraryId,
      string[] thumbnailProfiles)
    {
      LibrariesManager manager = LibrariesManager.GetManager(libraryProvider);
      return this.RegenerateThumbnails(libraryType, manager, libraryId, thumbnailProfiles);
    }

    internal Guid RegenerateThumbnails(
      string libraryType,
      LibrariesManager manager,
      string libraryId,
      string[] thumbnailProfiles)
    {
      Guid id = Guid.Parse(libraryId);
      Library library = manager.GetLibrary(id);
      if (library is Telerik.Sitefinity.Libraries.Model.Album)
      {
        library.Demand("Image", "ManageImage");
      }
      else
      {
        Library library1 = library;
      }
      if (library.RunningTask == new Guid())
        return this.StartThumbnailRegenerationTask(thumbnailProfiles, manager, library);
      if (SchedulingManager.GetManager().GetTaskData().FirstOrDefault<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Id == library.RunningTask)) != null)
        throw new Exception(Res.Get<LibrariesResources>().AnotherTaskRunning);
      library.RunningTask = new Guid();
      manager.SaveChanges();
      return this.StartThumbnailRegenerationTask(thumbnailProfiles, manager, library);
    }

    private static string ExtractControlType(string typeFromConfiguration)
    {
      int length = typeFromConfiguration.IndexOf(",");
      return length == -1 ? typeFromConfiguration : typeFromConfiguration.Substring(0, length);
    }

    /// <summary>
    /// Finds the Tags defined for this ViewType in PageControls element in Toolboxes section.
    /// </summary>
    /// <param name="viewType">The view type.</param>
    /// <returns>The Tags</returns>
    private string GetTags(string viewType)
    {
      foreach (Toolbox element in Telerik.Sitefinity.Configuration.Config.Get<ToolboxesConfig>().Toolboxes.Elements)
      {
        if (element.Name == "PageControls")
        {
          foreach (ToolboxSection section in element.Sections)
          {
            foreach (ToolboxItem tool in section.Tools)
            {
              string controlType1 = ThumbnailService.ExtractControlType(tool.ControlType);
              string controlType2 = ThumbnailService.ExtractControlType(tool.ControllerType);
              if (controlType1 == viewType || controlType2 == viewType)
                return tool.Tags;
            }
          }
        }
      }
      return (string) null;
    }

    private Guid StartThumbnailRegenerationTask(
      string[] thumbnailProfiles,
      LibrariesManager manager,
      Library library)
    {
      return thumbnailProfiles == null ? this.RegenerateWithCurrentProfiles(manager, library) : this.RegenerateWithNewProfiles(manager, library, thumbnailProfiles);
    }

    private Guid RegenerateWithNewProfiles(
      LibrariesManager manager,
      Library library,
      string[] thumbnailProfiles)
    {
      if (library is Telerik.Sitefinity.Libraries.Model.Album)
        library.Demand("Image", "ManageImage");
      string[] current = thumbnailProfiles;
      IList<string> thumbnailProfiles1 = library.ThumbnailProfiles;
      List<string> stringList1 = new List<string>(this.GetRemovedProfiles((IEnumerable<string>) thumbnailProfiles1, (IEnumerable<string>) current));
      List<string> stringList2 = new List<string>(this.GetAddedProfiles((IEnumerable<string>) thumbnailProfiles1, (IEnumerable<string>) current));
      library.ThumbnailProfiles = (IList<string>) thumbnailProfiles;
      if (string.IsNullOrEmpty(manager.TransactionName))
        manager.SaveChanges();
      else
        TransactionManager.CommitTransaction(manager.TransactionName);
      if (!(library.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.Album)) && !(library.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary)))
        throw new Exception(Res.Get<LibrariesResources>().ThumbnailMediaContentSupport);
      if (stringList2.Count == 0 && stringList1.Count > 0)
        return LibrariesManager.StartThumbnailDeleteTask(library.Id, manager.Provider.Name, stringList1.ToArray(), string.Join(",", stringList1.ToArray()));
      return stringList2.Count > 0 && stringList1.Count == 0 ? LibrariesManager.StartThumbnailRegenerationTask(library.Id, manager.Provider.Name, stringList2.ToArray(), (string) library.Title) : LibrariesManager.StartThumbnailRegenerationTask(library.Id, manager.Provider.Name, taskTitle: ((string) library.Title));
    }

    private Guid RegenerateWithCurrentProfiles(LibrariesManager manager, Library library) => LibrariesManager.StartThumbnailRegenerationTask(library.Id, manager.Provider.Name, taskTitle: ((string) library.Title));

    private IEnumerable<string> GetAddedProfiles(
      IEnumerable<string> original,
      IEnumerable<string> current)
    {
      List<string> addedProfiles = new List<string>();
      if (original != null && current != null)
      {
        foreach (string str in current)
        {
          if (!original.Contains<string>(str))
            addedProfiles.Add(str);
        }
      }
      else if (current != null)
        addedProfiles.AddRange(current);
      return (IEnumerable<string>) addedProfiles;
    }

    private IEnumerable<string> GetRemovedProfiles(
      IEnumerable<string> original,
      IEnumerable<string> current)
    {
      List<string> removedProfiles = new List<string>();
      if (current != null && original != null)
      {
        foreach (string str in original)
        {
          if (!current.Contains<string>(str))
            removedProfiles.Add(str);
        }
      }
      return (IEnumerable<string>) removedProfiles;
    }

    /// <summary>Gets the thumbnails.</summary>
    /// <param name="mediaContentId">The media content id.</param>
    /// <param name="libraryProvider">The library provider.</param>
    /// <returns>The thumbnails for the specified media content from a provider.</returns>
    public CollectionContext<ThumbnailViewModel> GetThumbnails(
      string mediaContentId,
      string libraryProvider)
    {
      return this.GetThumbnailsInternal(Guid.Parse(mediaContentId), libraryProvider);
    }

    /// <summary>
    /// Gets the properties needed for a given image processing method.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <returns>The method properties.</returns>
    public CollectionContext<MethodPropertyData> GetProcessingMethodProperties(
      string methodName)
    {
      return this.GetProcessingMethodPropertiesInternal(methodName);
    }

    /// <summary>
    /// Gets the url for a given custom image thumbnail by its parameters.
    /// </summary>
    /// <param name="imageId">The Id of the image.</param>
    /// <param name="customUrlParameters">JSON serialized custom image thumbnail parameters.</param>
    /// <param name="libraryProvider">The library provider.</param>
    /// <returns>
    /// If incorrect image Id the method returns empty string.
    /// If incorrect customUrlParameters the method returns the original image URL
    /// </returns>
    public string GetCustomImageThumbnailUrl(
      string imageId,
      string customUrlParameters,
      string libraryProvider)
    {
      return this.GetCustomImageThumbnailUrlInternal(imageId, customUrlParameters, libraryProvider);
    }

    /// <summary>
    /// Checks for a given custom image thumbnail if the parameters are correct.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="parameters">JSON serialized custom image thumbnail parameters.</param>
    /// <returns>The custom image thumbnail parameters.</returns>
    public string CheckCustomImageThumbnailParameters(string methodName, string parameters) => this.CheckCustomImageThumbnailParametersInternal(methodName, parameters);

    private string CheckCustomImageThumbnailParametersInternal(string methodName, string parameters)
    {
      IImageProcessor imageProcessor = ObjectFactory.Resolve<IImageProcessor>();
      ImageProcessingMethod processingMethod = (ImageProcessingMethod) null;
      if (!imageProcessor.Methods.TryGetValue(methodName, out processingMethod))
        return Res.Get<LibrariesResources>("InvalidMethodName", (object) methodName);
      Dictionary<string, string> dictionary = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(parameters);
      if (dictionary == null)
        return Res.Get<LibrariesResources>("InvalidMethodParametersString");
      try
      {
        NameValueCollection args = new NameValueCollection();
        foreach (KeyValuePair<string, string> keyValuePair in dictionary)
          args.Add(keyValuePair.Key, keyValuePair.Value);
        imageProcessor.Methods[methodName].ValidateArguments((object) args);
      }
      catch (Exception ex)
      {
        return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
      }
      return string.Empty;
    }

    private string GetCustomImageThumbnailUrlInternal(
      string imageId,
      string customUrlParameters,
      string libraryProvider)
    {
      ServiceUtility.RequestAuthentication(false, true);
      if (string.IsNullOrEmpty(imageId))
        return string.Empty;
      LibrariesManager manager = LibrariesManager.GetManager(libraryProvider);
      Guid imageGuidId = new Guid(imageId);
      Telerik.Sitefinity.Libraries.Model.Image mediaContent = manager.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (i => i.Id == imageGuidId)).Where<Telerik.Sitefinity.Libraries.Model.Image>(PredefinedFilters.PublishedItemsFilter<Telerik.Sitefinity.Libraries.Model.Image>()).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
      if (mediaContent == null)
        return string.Empty;
      if (!mediaContent.IsGranted("Image", "ManageImage"))
        throw new UnauthorizedAccessException("Library is not accessible");
      bool generateAbsoluteUrls = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
      Dictionary<string, string> dictionary = JsonSerializer.DeserializeFromString<Dictionary<string, string>>(customUrlParameters);
      if (dictionary == null)
        return mediaContent.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
      if (!dictionary.ContainsKey("Method"))
        return mediaContent.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
      IImageProcessor imageProcessor = ObjectFactory.Resolve<IImageProcessor>();
      ImageProcessingMethod processingMethod = (ImageProcessingMethod) null;
      if (!imageProcessor.Methods.TryGetValue(dictionary["Method"], out processingMethod))
        return mediaContent.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
      Dictionary<string, string> urlParameters = new Dictionary<string, string>();
      foreach (MethodPropertyData methodPropertyData in this.GetProcessingMethodPropertiesInternal(dictionary["Method"]).Items.ToList<MethodPropertyData>())
      {
        if (methodPropertyData.IsRequired && !dictionary.ContainsKey(methodPropertyData.Name))
          return mediaContent.ResolveMediaUrl(generateAbsoluteUrls, (CultureInfo) null);
        urlParameters[methodPropertyData.Name] = dictionary[methodPropertyData.Name];
      }
      urlParameters.Add("Method", dictionary["Method"]);
      return mediaContent.ResolveMediaUrl(urlParameters, generateAbsoluteUrls);
    }

    private CollectionContext<MethodPropertyData> GetProcessingMethodPropertiesInternal(
      string methodName)
    {
      IImageProcessor imageProcessor = ObjectFactory.Resolve<IImageProcessor>();
      ImageProcessingMethod processingMethod = (ImageProcessingMethod) null;
      if (imageProcessor.Methods.TryGetValue(methodName, out processingMethod))
      {
        List<MethodPropertyData> items = new List<MethodPropertyData>();
        foreach (ImageProcessingProperty argumentProperty in processingMethod.GetArgumentProperties())
        {
          PropertyInfo propertyInfo = argumentProperty.PropertyInfo;
          MethodPropertyData methodPropertyData = new MethodPropertyData()
          {
            Name = argumentProperty.Name,
            Title = argumentProperty.GetTitle(),
            PropertyType = propertyInfo.PropertyType.Name,
            PropertyBaseType = propertyInfo.PropertyType.BaseType.Name,
            RegularExpression = argumentProperty.RegularExpression,
            RegularExpressionValidationMessage = argumentProperty.GetRegularExpressionViolationMessage(),
            IsRequired = argumentProperty.IsRequired
          };
          if (propertyInfo.PropertyType.BaseType == typeof (Enum))
            methodPropertyData.Choices = ((IEnumerable<string>) Enum.GetNames(propertyInfo.PropertyType)).ToList<string>();
          items.Add(methodPropertyData);
        }
        return new CollectionContext<MethodPropertyData>((IEnumerable<MethodPropertyData>) items);
      }
      throw new InvalidOperationException(Res.Get<LibrariesResources>("InvalidMethodName", (object) methodName));
    }

    private CollectionContext<ThumbnailViewModel> GetThumbnailsInternal(
      Guid mediaContentId,
      string libraryProvider)
    {
      ServiceUtility.RequestAuthentication();
      MediaContent mediaItem = LibrariesManager.GetManager(libraryProvider).GetMediaItem(mediaContentId);
      IEnumerable<Thumbnail> source = mediaItem.GetThumbnails().Where<Thumbnail>((Func<Thumbnail, bool>) (t => t.Type != ThumbnailTypes.System));
      ConfigElementDictionary<string, ThumbnailProfileConfigElement> thumbnailProfiles = Telerik.Sitefinity.Configuration.Config.Get<LibrariesConfig>().GetThumbnailProfiles(mediaItem.Parent.GetType());
      List<ThumbnailViewModel> items = new List<ThumbnailViewModel>(source.Count<Thumbnail>() + 1);
      foreach (Thumbnail thumbnail in source)
      {
        string str = mediaItem.ResolveThumbnailUrl(thumbnail.Name);
        ThumbnailProfileConfigElement profileConfigElement = thumbnailProfiles[thumbnail.Name];
        items.Add(new ThumbnailViewModel()
        {
          Name = thumbnail.Name,
          IsOriginalSize = false,
          IsThumbnailUploaded = thumbnail.Type == ThumbnailTypes.Custom,
          Title = profileConfigElement != null ? profileConfigElement.Title : thumbnail.Name,
          Url = str,
          Size = this.GetSizeString((ISizeable) thumbnail)
        });
      }
      if (mediaItem is ISizeable sizeable)
      {
        string str = mediaItem.ResolveMediaUrl(false, (CultureInfo) null);
        items.Add(new ThumbnailViewModel()
        {
          IsOriginalSize = true,
          Title = Res.Get<LibrariesResources>().OriginalSize,
          Url = str,
          Size = this.GetSizeString(sizeable)
        });
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<ThumbnailViewModel>((IEnumerable<ThumbnailViewModel>) items)
      {
        TotalCount = items.Count
      };
    }

    private string GetSizeString(ISizeable sizeable) => "{0}x{1}px {2} KB".Arrange((object) sizeable.Width, (object) sizeable.Height, (object) (sizeable.TotalSize / 1024L));
  }
}
