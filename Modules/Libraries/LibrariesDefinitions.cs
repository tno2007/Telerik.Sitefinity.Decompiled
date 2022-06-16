// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.Services;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Definition utilities shared between the differnt types of libraries.
  /// </summary>
  internal static class LibrariesDefinitions
  {
    public const string BlobStorageBackendDefinitionName = "BlobStorageBackend";
    public const string BlobStorageBackendListViewName = "BlobStorageBackendList";
    public const string BlobStorageBackendBasicSettingsExtensionScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.BlobStorageProvidersMasterViewExtensions.js";
    public const string ThumbnailsBackendDefinitionName = "ThumbnailsBackend";
    public const string ThumbnailsBackendListViewName = "ThumbnailsBackendList";
    public const string ThumbnailsBackendBasicSettingsExtensionScript = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.ThumbnailsMasterViewExtensions.js";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for changing url of a library.
    /// </summary>
    public const string RelocateLibraryCommandName = "relocateLibrary";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for moving library to a new blob storage.
    /// </summary>
    public const string TransferLibraryCommandName = "transferLibrary";
    /// <summary>
    /// Common name used for a command that navigates user to the screen for confirming the regeneration of thumbnails.
    /// </summary>
    public const string RegenerateThumbnailsCommandName = "regenerateThumbnails";
    public const string ThumbnailSettingsCommandName = "thumbnailSettings";
    public const string LibrariesStoredInColumnTemplate = "<p><span>{{BlobStorageProvider}}</span></p>";
    public const string LibrariesTitleColumnTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_viewItemsByParent sfItemTitle'>{{Title.htmlEncode()}}</a><p>{{ItemsCount}} {$ImagesResources, Images$}</p><p>{{getDateTemplate(LastUploadedDate, 'dd MMM, yyyy', '{$ImagesResources, LastUploaded$}')}}</p>";

    public static ContentViewControlElement DefineBlobStorageBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, "BlobStorageBackend", typeof (DataProviderSettingsViewModel)).Get();
      MasterGridViewElement masterGridViewElement = new MasterGridViewElement((ConfigElement) viewControlElement.ViewsConfig);
      masterGridViewElement.ViewName = "BlobStorageBackendList";
      masterGridViewElement.ViewType = typeof (MasterGridView);
      masterGridViewElement.AllowPaging = new bool?(false);
      masterGridViewElement.DisplayMode = FieldDisplayMode.Read;
      masterGridViewElement.WebServiceBaseUrl = string.Format("~/{0}/providers", (object) "Sitefinity/Services/Content/BlobStorage.svc");
      MasterGridViewElement element1 = masterGridViewElement;
      element1.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          string.Format("{0}, Telerik.Sitefinity", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.BlobStorageProvidersMasterViewExtensions.js"),
          "OnMasterViewLoaded"
        }
      };
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element2 = gridViewModeElement;
      element1.ViewModesConfig.Add((ViewModeElement) element2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig = element2.ColumnsConfig;
      ConfigElementDictionary<string, ColumnElement> elementDictionary1 = columnsConfig;
      DataColumnElement element3 = new DataColumnElement((ConfigElement) columnsConfig);
      element3.Name = "Name";
      element3.HeaderText = "ProviderName";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.HeaderCssClass = "sfTitleCol";
      element3.ItemCssClass = "sfTitleCol";
      element3.ClientTemplate = "<span sys:if='HasSettings'><a sys:href='javascript:void(0);' class='sf_binderCommand_edit sfItemTitle sfavailable'>{{Title}}</a></span><span class='sfItemTitle sfavailable' sys:if='!HasSettings'>{{Title}}</span>";
      elementDictionary1.Add((ColumnElement) element3);
      ConfigElementDictionary<string, ColumnElement> elementDictionary2 = columnsConfig;
      DataColumnElement element4 = new DataColumnElement((ConfigElement) columnsConfig);
      element4.Name = "Type";
      element4.HeaderText = "ProviderType";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.ClientTemplate = "{{ProviderTypeTitle}}";
      elementDictionary2.Add((ColumnElement) element4);
      Labels labels = Res.Get<Labels>();
      ConfigElementDictionary<string, ColumnElement> elementDictionary3 = columnsConfig;
      DataColumnElement dataColumnElement = new DataColumnElement((ConfigElement) columnsConfig);
      dataColumnElement.Name = "Default";
      dataColumnElement.ClientTemplate = "{{(IsDefault)?'" + labels.Default + "':''}}<a sys:href='javascript:void(0);' sys:class=\"{{(IsDefault?'sfDisplayNone' : 'sf_binderCommand_setdefault')}}\">" + labels.SetAsDefault + "</a>";
      DataColumnElement element5 = dataColumnElement;
      elementDictionary3.Add((ColumnElement) element5);
      ConfigElementDictionary<string, ColumnElement> elementDictionary4 = columnsConfig;
      DataColumnElement element6 = new DataColumnElement((ConfigElement) columnsConfig);
      element6.Name = "Remove";
      element6.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{(IsDefault?'sfDisplayNone':'sf_binderCommand_delete')}}\">" + labels.Remove + "</a>";
      elementDictionary4.Add((ColumnElement) element6);
      ConfigElementList<DialogElement> dialogsConfig = element1.DialogsConfig;
      dialogsConfig.Add(DefinitionsHelper.CreateModalDialogElement((ConfigElement) dialogsConfig, "create", "BlobStorageProviderSettingsDialog", string.Empty, 395, 250));
      dialogsConfig.Add(DefinitionsHelper.CreateModalDialogElement((ConfigElement) dialogsConfig, "edit", "BlobStorageProviderSettingsDialog", "?providerName={{Name}}", 395, 250));
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      return viewControlElement;
    }

    public static ContentViewControlElement DefineThumbnailsBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, "ThumbnailsBackend", typeof (WcfThumbnailProfile)).Get();
      MasterGridViewElement masterGridViewElement = new MasterGridViewElement((ConfigElement) viewControlElement.ViewsConfig);
      masterGridViewElement.ViewName = "ThumbnailsBackendList";
      masterGridViewElement.ViewType = typeof (MasterGridView);
      masterGridViewElement.AllowPaging = new bool?(false);
      masterGridViewElement.DisplayMode = FieldDisplayMode.Read;
      masterGridViewElement.WebServiceBaseUrl = string.Format("~/{0}/thumbnail-profiles?libraryType=Telerik.Sitefinity.Libraries.Model.Album", (object) "Sitefinity/Services/ThumbnailService.svc");
      MasterGridViewElement element1 = masterGridViewElement;
      element1.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          string.Format("{0}, Telerik.Sitefinity", (object) "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.ThumbnailsMasterViewExtensions.js"),
          "OnMasterViewLoaded"
        }
      };
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element2 = gridViewModeElement;
      element1.ViewModesConfig.Add((ViewModeElement) element2);
      ConfigElementDictionary<string, ColumnElement> columnsConfig = element2.ColumnsConfig;
      DataColumnElement element3 = new DataColumnElement((ConfigElement) columnsConfig);
      element3.Name = "Name";
      element3.HeaderText = "ThumbnailProfiles";
      element3.ResourceClassId = typeof (LibrariesResources).Name;
      element3.HeaderCssClass = "sfTitleCol";
      element3.ItemCssClass = "sfTitleCol";
      element3.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_edit sfItemTitle sfavailable'>\r\n                    {{Title}}\r\n                    </a>";
      columnsConfig.Add((ColumnElement) element3);
      DataColumnElement element4 = new DataColumnElement((ConfigElement) columnsConfig);
      element4.Name = "ThumbnailSize";
      element4.HeaderText = "SizeLabel";
      element4.ResourceClassId = typeof (LibrariesResources).Name;
      element4.ClientTemplate = "{{Size}}";
      columnsConfig.Add((ColumnElement) element4);
      DataColumnElement element5 = new DataColumnElement((ConfigElement) columnsConfig);
      element5.Name = "AppliedToLibraries";
      element5.HeaderText = "AppliedTo";
      element5.ResourceClassId = typeof (LibrariesResources).Name;
      element5.ClientTemplate = "{{LibrariesCount}} " + Res.Get<LibrariesResources>().LibrariesLowerCase;
      columnsConfig.Add((ColumnElement) element5);
      Labels labels = Res.Get<Labels>();
      DataColumnElement element6 = new DataColumnElement((ConfigElement) columnsConfig);
      element6.Name = "Default";
      element6.ClientTemplate = "<i sys:class=\"{{(IsDefault?'sfNote':'sfDisplayNone')}}\">" + Res.Get<LibrariesResources>().SelectedByDefault + "</i>";
      columnsConfig.Add((ColumnElement) element6);
      DataColumnElement element7 = new DataColumnElement((ConfigElement) columnsConfig);
      element7.Name = "Remove";
      element7.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class='sf_binderCommand_delete'>" + labels.Delete + "</a>";
      columnsConfig.Add((ColumnElement) element7);
      ConfigElementList<DialogElement> dialogsConfig = element1.DialogsConfig;
      dialogsConfig.Add(DefinitionsHelper.CreateModalDialogElement((ConfigElement) dialogsConfig, "create", "ThumbnailProfileDialog", "?libraryType=Telerik.Sitefinity.Libraries.Model.Album", 425, 250));
      dialogsConfig.Add(DefinitionsHelper.CreateModalDialogElement((ConfigElement) dialogsConfig, "edit", "ThumbnailProfileDialog", "?profile={{Id}}&librariesCount={{LibrariesCount}}&libraryType=Telerik.Sitefinity.Libraries.Model.Album", 395, 250));
      ConfigElementList<PromptDialogElement> promptDialogsConfig = element1.PromptDialogsConfig;
      PromptDialogElement element8 = new PromptDialogElement((ConfigElement) promptDialogsConfig)
      {
        DialogName = "deleteProfileForbiddenDialog",
        Message = Res.Get<LibrariesResources>().CannotDeleteSettingsInfo,
        Width = 395,
        Height = 250,
        Title = Res.Get<LibrariesResources>().CannotDeleteSettings,
        ShowOnLoad = false
      };
      element8.CommandsConfig.Add(DefinitionsHelper.CreateOkDialogCommand((ConfigElement) element8.CommandsConfig));
      promptDialogsConfig.Add(element8);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      return viewControlElement;
    }

    public static CacheProfileFieldElement DefineClientCacheProfileField(
      ConfigElement parent,
      string dataFieldName = "ClientCacheProfile")
    {
      CacheProfileFieldElement parent1 = new CacheProfileFieldElement(parent);
      parent1.FieldName = "clientCacheProfileElement";
      parent1.DisplayMode = FieldDisplayMode.Write;
      parent1.ResourceClassId = PagesDefinitions.ResourceClassId;
      parent1.WrapperTag = HtmlTextWriterTag.Li;
      parent1.FieldType = typeof (CacheProfileField);
      parent1.IsOutputCache = false;
      parent1.Title = "CachingOptions";
      ChoiceFieldElement choiceFieldElement1 = new ChoiceFieldElement((ConfigElement) parent1);
      choiceFieldElement1.ID = "clientProfileChoiceFieldDefinition";
      choiceFieldElement1.DataFieldName = dataFieldName;
      choiceFieldElement1.RenderChoiceAs = RenderChoicesAs.DropDown;
      choiceFieldElement1.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      choiceFieldElement1.WrapperTag = HtmlTextWriterTag.Div;
      ChoiceFieldElement choiceFieldElement2 = choiceFieldElement1;
      choiceFieldElement2.ChoicesConfig.Add(new ChoiceElement((ConfigElement) choiceFieldElement2.ChoicesConfig)
      {
        Text = "AsForWholeSite",
        ResourceClassId = ImagesDefinitions.ResourceClassId,
        Value = ""
      });
      parent1.ProfileChoiceFieldDefinition = choiceFieldElement2;
      return parent1;
    }

    public static string GetByLibrarySectionClientTemplate(
      string itemsSingular,
      string itemsPulral,
      string height = null)
    {
      return "<a href='javascript:void(0);' sys:class=\"{{ 'sfAlbumFilter' + (ItemsCount !== 0 ? '' : ' sfEmptyFilter') + ' sfClearfix sf_binderCommand_filter' }}\">\r\n                                                      \r\n                                <span class='sfAlbumThumb'>\r\n                                    <img class='thumbnailImg' alt=''" + (height == null ? "" : string.Format("height=\"{0}\"", (object) height)) + "/>\r\n                                </span>\r\n                                <span class='sfAlbumInfo'>\r\n                                    <span>{{Title}}</span>\r\n                                    <span class='sfImgInAlbum'>{{ItemsCount + ((ItemsCount ==1)?' " + itemsSingular + "':' " + itemsPulral + "') }} </span>\r\n                                </span>\r\n                            \r\n                        </a>";
    }
  }
}
