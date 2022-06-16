// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  /// <summary>Defines publishing module's default UI</summary>
  public class TwitterDefinitions
  {
    public static string BackendDefinitionName = "TwitterBackend";
    /// <summary>
    /// Name of the view used to display list of events on the backend.
    /// </summary>
    public static string BackendListViewName = "TwitterBackendList";
    public static string BackendInsertDetailsViewName = "create";
    public static string BackendEditDetailsViewName = "edit";

    /// <summary>
    /// Defines the ContentView control for Events on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade definitionFacade1 = App.WorkWith().Module().DefineContainer(parent, TwitterDefinitions.BackendDefinitionName, typeof (TwitterApplication));
      ContentViewControlElement viewControlElement = definitionFacade1.Get();
      MasterViewDefinitionFacade definitionFacade2 = definitionFacade1.AddMasterView(TwitterDefinitions.BackendListViewName).DisablePaging().SetServiceBaseUrl("~/Sitefinity/Services/Twitter/TwitterCredentialsService.svc/").SetExternalClientScripts(new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Scripts.TwitterMasterViewExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      });
      MasterGridViewElement element1 = definitionFacade2.Get();
      DecisionScreenElement element2 = new DecisionScreenElement((ConfigElement) element1.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        MessageText = "TwitterNoTwitterApplicationsAdded",
        ResourceClassId = typeof (PublishingMessages).Name
      };
      element1.DecisionScreensConfig.Add(element2);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) element1.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element3 = gridViewModeElement;
      element1.ViewModesConfig.Add((ViewModeElement) element3);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element3.ColumnsConfig);
      dataColumnElement1.Name = "Name";
      dataColumnElement1.HeaderText = "TwitterApplicationName";
      dataColumnElement1.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement1.HeaderCssClass = "sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfTitleCol";
      dataColumnElement1.ClientTemplate = "<a sys:href='javascript:void(0);' class='sf_binderCommand_edit sfItemTitle sfavailable'>\r\n                    <strong>{{key.Name}}</strong>   \r\n                    </a>";
      DataColumnElement element4 = dataColumnElement1;
      element3.ColumnsConfig.Add((ColumnElement) element4);
      ActionMenuColumnElement menuColumnElement = new ActionMenuColumnElement((ConfigElement) element3.ColumnsConfig);
      menuColumnElement.Name = "Actions";
      menuColumnElement.HeaderText = "Actions";
      menuColumnElement.HeaderCssClass = "sfMoreActions";
      menuColumnElement.ItemCssClass = "sfMoreActions";
      menuColumnElement.ResourceClassId = typeof (Labels).Name;
      ActionMenuColumnElement element5 = menuColumnElement;
      ConfigElementList<WidgetElement> menuItems1 = element5.MenuItems;
      CommandWidgetElement element6 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element6.Name = "AssociateUser";
      element6.WrapperTagKey = HtmlTextWriterTag.Li;
      element6.CommandName = "associateUsr";
      element6.Text = "TwitterAssociateTwitterUser";
      element6.ResourceClassId = typeof (PublishingMessages).Name;
      element6.WidgetType = typeof (CommandWidget);
      element6.CssClass = "sfPublishItm";
      menuItems1.Add((WidgetElement) element6);
      ConfigElementList<WidgetElement> menuItems2 = element5.MenuItems;
      CommandWidgetElement element7 = new CommandWidgetElement((ConfigElement) element5.MenuItems);
      element7.Name = "Delete";
      element7.WrapperTagKey = HtmlTextWriterTag.Li;
      element7.CommandName = "delete";
      element7.Text = "TwitterDelete";
      element7.ResourceClassId = typeof (PublishingMessages).Name;
      element7.WidgetType = typeof (CommandWidget);
      element7.CssClass = "sfDeleteItm";
      menuItems2.Add((WidgetElement) element7);
      element3.ColumnsConfig.Add((ColumnElement) element5);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element3.ColumnsConfig);
      dataColumnElement2.Name = "AssocUsrs";
      dataColumnElement2.HeaderText = "TwitterAssociatedUsers";
      dataColumnElement2.ResourceClassId = typeof (PublishingMessages).Name;
      dataColumnElement2.ClientTemplate = "{{ printUsersContent(value)  }}";
      DataColumnElement element8 = dataColumnElement2;
      element3.ColumnsConfig.Add((ColumnElement) element8);
      definitionFacade2.AddDialog<TwitterDetailDialog>("create").MakeFullScreen().Done().AddDialog<TwitterDetailDialog>("edit").MakeFullScreen().SetParameters("?AppName={{key.Name}}");
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      return viewControlElement;
    }
  }
}
