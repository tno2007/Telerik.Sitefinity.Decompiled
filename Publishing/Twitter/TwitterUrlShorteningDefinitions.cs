// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShorteningDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  /// <summary>Defines twitter url shortening settings</summary>
  public class TwitterUrlShorteningDefinitions
  {
    public static string BackendDefinitionName = "TwitterUrlShorteningSettings";
    /// <summary>
    /// Name of the view used to display url shortening settings.
    /// </summary>
    public static string BackendListViewName = "TwitterUrlShorteningList";

    /// <summary>
    /// Defines the ContentView control for Events on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade fluentContentView = App.WorkWith().Module().DefineContainer(parent, TwitterUrlShorteningDefinitions.BackendDefinitionName, typeof (TwitterConfig));
      ContentViewControlElement viewControlElement = fluentContentView.Get();
      viewControlElement.ViewsConfig.AddLazy((object) TwitterUrlShorteningDefinitions.BackendListViewName, (Func<ConfigElement>) (() => TwitterUrlShorteningDefinitions.DefineBackendListView(fluentContentView)));
      return viewControlElement;
    }

    private static ConfigElement DefineBackendListView(
      ContentViewControlDefinitionFacade fluentContentView)
    {
      MasterViewDefinitionFacade definitionFacade = fluentContentView.AddMasterView(TwitterUrlShorteningDefinitions.BackendListViewName).DisablePaging().SetServiceBaseUrl("~/Sitefinity/Services/Twitter/TwitterUrlShortConfigService.svc/twitter/");
      MasterGridViewElement masterGridViewElement = definitionFacade.Get();
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element1 = gridViewModeElement;
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element1);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      dataColumnElement1.Name = "AccountName";
      dataColumnElement1.HeaderText = "AccountName";
      dataColumnElement1.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement1.HeaderCssClass = "sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfTitleCol";
      dataColumnElement1.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_edit sfItemTitle sfavailable'>\r\n                    <strong>{{AccountName}}</strong>   \r\n                    </a>";
      DataColumnElement element2 = dataColumnElement1;
      element1.ColumnsConfig.Add((ColumnElement) element2);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      dataColumnElement2.Name = "ProviderName";
      dataColumnElement2.HeaderText = "ShorteningService";
      dataColumnElement2.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement2.HeaderCssClass = "sfTitleCol";
      dataColumnElement2.ClientTemplate = "<span>{{ProviderName}}<span>";
      DataColumnElement element3 = dataColumnElement2;
      element1.ColumnsConfig.Add((ColumnElement) element3);
      DataColumnElement dataColumnElement3 = new DataColumnElement((ConfigElement) element1.ColumnsConfig);
      dataColumnElement3.Name = "EditColumn";
      dataColumnElement3.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_edit'>\r\n                    <span>edit</span>   \r\n                    </a>";
      DataColumnElement element4 = dataColumnElement3;
      element1.ColumnsConfig.Add((ColumnElement) element4);
      definitionFacade.AddDialog<TwitterUrlShortConfigDialog>("edit").MakeFullScreen().Done();
      return (ConfigElement) masterGridViewElement;
    }
  }
}
