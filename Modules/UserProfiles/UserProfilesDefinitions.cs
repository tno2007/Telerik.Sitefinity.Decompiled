// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfilesDefinitions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.ModuleEditor.Web;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services;
using Telerik.Sitefinity.Modules.UserProfiles.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Security.Web.UI.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  /// <summary>
  /// This is a static class used to initialize the properties for all ContentView control views
  /// of supplied by default for the user profiles.
  /// </summary>
  internal static class UserProfilesDefinitions
  {
    public const string BackendProfileTypesDefinitionName = "UserProfileTypesBackend";
    public const string BackendProfileTypesListViewName = "UserProfileTypesBackendList";
    public const string BackendProfileTypesEditViewName = "UserProfileTypesBackendEdit";
    public const string BackendProfileTypesInsertViewName = "UserProfileTypesBackendInsert";
    public const string BackendProfileTypeFieldsViewName = "UserProfilesBackendInsert";
    public const string BackendSingleProfileDefinitionName = "BackendSingleProfile";
    public const string BackendDetailReadViewName = "UserProfilesBackendDetailsRead";
    public const string BackendDetailWriteViewName = "UserProfilesBackendDetailsWrite";
    public const string BackendChangePasswordViewName = "BackendChangePasswordDetailView";
    public const string BackendChangeQuestionAndAnswerViewName = "BackendChangeQuestionAndAnswerDetailView";
    public const string BackendListViewName = "UserProfilesBackendList";
    public const string FrontendSingleProfileDefinitionName = "FrontendSingleProfile";
    public const string FrontendListProfilesDefinitionName = "FrontendUsersList";
    public const string FrontendProfilesDefinitionName = "UserProfilesFrontend";
    public const string FrontendListViewName = "UserProfilesFrontendList";
    public const string FrontendDetailViewName = "UserProfilesFrontendDetails";
    public const string FrontendMasterViewName = "UserProfilesFrontendMaster";
    public const string FrontendDetailNoUserFriendlyViewName = "Not logged";
    public const string FrontendMasterFriendlyViewName = "Names and details";
    public const string FrontendMasterNamesOnlyFriendlyViewName = "Names only";
    public const string FrontendDetailReadViewName = "UserProfilesFrontendDetailsRead";
    public const string FrontendDetailReadFriendlyViewName = "Article-like";
    public const string FrontendDetailReadAutoGenerateFieldsFriendlyViewName = "Auto generated fields";
    public const string FrontendDetailReadListLikeFieldsFriendlyViewName = "List-like";
    public const string FrontendDetailWriteViewName = "UserProfilesFrontendDetailsWrite";
    public const string FrontendDetailWriteAuteGeneratedFieldsViewFrindlyName = "Auto generated fields";
    public const string FrontendDetailWriteViewFriendlyName = "Profile form";
    public const string FrontendChangePasswordViewName = "ChangePasswordDetailView";
    public const string FrontendChangePasswordViewFriendlyName = "Default";
    public const string FrontendChangePasswordWidgetFriendlyName = "Change password";
    public const string FrontendChangeQuestionAndAnswerViewName = "ChangeQuestionAndAnswerDetailView";
    public const string FrontendChangeQuestionAndAnswerViewFriendlyName = "Default";
    public const string FrontendChangeQuestionAndAnswerWidgetFriendlyName = "Change Question and Answer";
    public const string FrontendBasicRegistrationFormFriendlyName = "Basic fields only";
    public const string FrontendFullRegistrationFormFriendlyName = "All fields";
    public const string FrontendRegistrationSuccessEmailFriendlyName = "Success email";
    public const string FrontendRegistrationConfirmationEmailFriendlyName = "Confirmation email";
    public const string EditFieldsCommandName = "moduleEditor";
    public const string DefaultPhotoUrl = "~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png";

    /// <summary>
    /// Defines the ContentView control for User Profile Types on the backend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineProfileTypesBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlDefinitionFacade definitionFacade = App.WorkWith().Module().DefineContainer(parent, "UserProfileTypesBackend", typeof (UserProfileTypeViewModel));
      ContentViewControlElement viewControlElement = definitionFacade.Get();
      MasterViewDefinitionFacade fluentFacade = definitionFacade.AddMasterView("UserProfileTypesBackendList").LocalizeUsing<UserProfilesResources>().SetTitle("UserProfileTypesHtmlTitle").SetServiceBaseUrl("~/Sitefinity/Services/UserProfiles/UserProfileTypesService.svc/").SetSortExpression("Title ASC");
      MasterGridViewElement masterGridViewElement = fluentFacade.Get();
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.ToolbarConfig.Sections)
      {
        Name = "toolbar"
      };
      CommandWidgetElement commandWidgetElement1 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement1.Name = "CreateUserProfileWidget";
      commandWidgetElement1.ButtonType = CommandButtonType.Create;
      commandWidgetElement1.CommandName = "create";
      commandWidgetElement1.Text = "CreateItem";
      commandWidgetElement1.ResourceClassId = typeof (UserProfilesResources).Name;
      commandWidgetElement1.CssClass = "sfMainAction";
      commandWidgetElement1.WidgetType = typeof (CommandWidget);
      commandWidgetElement1.PermissionSet = "General";
      commandWidgetElement1.ActionName = "Create";
      CommandWidgetElement element2 = commandWidgetElement1;
      element1.Items.Add((WidgetElement) element2);
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "DeleteUserProfileWidget";
      commandWidgetElement2.ButtonType = CommandButtonType.Standard;
      commandWidgetElement2.CommandName = "groupDelete";
      commandWidgetElement2.Text = "Delete";
      commandWidgetElement2.ResourceClassId = typeof (Labels).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      commandWidgetElement2.CssClass = "sfGroupBtn";
      CommandWidgetElement element3 = commandWidgetElement2;
      element1.Items.Add((WidgetElement) element3);
      masterGridViewElement.ToolbarConfig.Sections.Add(element1);
      WidgetBarSectionElement element4 = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
      {
        Name = "Settings",
        Title = "Settings",
        ResourceClassId = typeof (Labels).Name,
        CssClass = "sfWidgetsList sfSettings",
        WrapperTagId = "settingsSection"
      };
      CommandWidgetElement commandWidgetElement3 = new CommandWidgetElement((ConfigElement) element4.Items);
      commandWidgetElement3.Name = "UserProfileSettings";
      commandWidgetElement3.CommandName = "settings";
      commandWidgetElement3.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement3.Text = "Settings";
      commandWidgetElement3.ResourceClassId = typeof (UserProfilesResources).Name;
      commandWidgetElement3.WidgetType = typeof (CommandWidget);
      commandWidgetElement3.IsSeparator = false;
      CommandWidgetElement element5 = commandWidgetElement3;
      element4.Items.Add((WidgetElement) element5);
      masterGridViewElement.SidebarConfig.ResourceClassId = typeof (UserProfilesResources).Name;
      masterGridViewElement.SidebarConfig.Title = "ManageUserProfiles";
      masterGridViewElement.SidebarConfig.Sections.Add(element4);
      GridViewModeElement gridViewModeElement = new GridViewModeElement((ConfigElement) masterGridViewElement.ViewModesConfig);
      gridViewModeElement.Name = "Grid";
      GridViewModeElement element6 = gridViewModeElement;
      masterGridViewElement.ExternalClientScripts = new Dictionary<string, string>()
      {
        {
          "Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProfileTypesMasterGridViewExtensions.js, Telerik.Sitefinity",
          "OnMasterViewLoaded"
        }
      };
      masterGridViewElement.ViewModesConfig.Add((ViewModeElement) element6);
      DataColumnElement dataColumnElement1 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      dataColumnElement1.Name = "Title";
      dataColumnElement1.HeaderText = "Title";
      dataColumnElement1.ResourceClassId = typeof (Labels).Name;
      dataColumnElement1.HeaderCssClass = "sfTitleCol";
      dataColumnElement1.ItemCssClass = "sfTitleCol";
      dataColumnElement1.ClientTemplate = "<a sys:href='javascript:void(0);' sys:class=\"{{ 'sf_binderCommand_moduleEditor sfItemTitle sfprofiletype' }}\">\r\n                    <span>{{Title}}</span></a>";
      DataColumnElement element7 = dataColumnElement1;
      element6.ColumnsConfig.Add((ColumnElement) element7);
      DataColumnElement dataColumnElement2 = new DataColumnElement((ConfigElement) element6.ColumnsConfig);
      dataColumnElement2.Name = "AppliedTo";
      dataColumnElement2.HeaderText = "AppliedTo";
      dataColumnElement2.ResourceClassId = typeof (UserProfilesResources).Name;
      dataColumnElement2.ClientTemplate = "<span>{{AppliedTo}}</span>";
      dataColumnElement2.HeaderCssClass = "sfRegular";
      dataColumnElement2.ItemCssClass = "sfRegular";
      DataColumnElement element8 = dataColumnElement2;
      element6.ColumnsConfig.Add((ColumnElement) element8);
      ActionMenuColumnElement menuColumnElement1 = new ActionMenuColumnElement((ConfigElement) element6.ColumnsConfig);
      menuColumnElement1.Name = "Actions";
      menuColumnElement1.HeaderText = "Actions";
      menuColumnElement1.HeaderCssClass = "sfMoreActions";
      menuColumnElement1.ItemCssClass = "sfMoreActions";
      menuColumnElement1.ResourceClassId = typeof (Labels).Name;
      ActionMenuColumnElement menuColumnElement2 = menuColumnElement1;
      UserProfilesDefinitions.FillActionMenuItems(menuColumnElement2.MenuItems, (ConfigElement) menuColumnElement2, typeof (Labels).Name);
      element6.ColumnsConfig.Add((ColumnElement) menuColumnElement2);
      DecisionScreenElement element9 = new DecisionScreenElement((ConfigElement) masterGridViewElement.DecisionScreensConfig)
      {
        Name = "NoItemsExistScreen",
        DecisionType = DecisionType.NoItemsExist,
        MessageType = MessageType.Neutral,
        Displayed = new bool?(false),
        Title = "WhatDoYouWantToDoNow",
        MessageText = "NoUserProfileTypes",
        ResourceClassId = typeof (UserProfilesResources).Name
      };
      CommandWidgetElement commandWidgetElement4 = new CommandWidgetElement((ConfigElement) element9.Actions);
      commandWidgetElement4.Name = "Create";
      commandWidgetElement4.ButtonType = CommandButtonType.Create;
      commandWidgetElement4.CommandName = "create";
      commandWidgetElement4.Text = "CreateItem";
      commandWidgetElement4.ResourceClassId = typeof (UserProfilesResources).Name;
      commandWidgetElement4.CssClass = "sfCreateItem";
      commandWidgetElement4.PermissionSet = "General";
      commandWidgetElement4.ActionName = "Create";
      CommandWidgetElement element10 = commandWidgetElement4;
      element9.Actions.Add(element10);
      masterGridViewElement.DecisionScreensConfig.Add(element9);
      UserProfilesDefinitions.CreateDialogs(fluentFacade);
      masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
      {
        Name = "viewSettings",
        CommandName = "settings",
        NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.AdvancedSettingsNodeId) + "/UserProfiles"
      });
      DefinitionsHelper.CreateNotImplementedLink(masterGridViewElement);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) masterGridViewElement);
      Dictionary<string, string> extenalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Scripts.UserProfileTypesDetailExtensions.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailViewDefinitionFacade fluentDetailView1 = definitionFacade.AddDetailView("UserProfileTypesBackendInsert").SetTitle("CreateNewItem").HideTopToolbar().LocalizeUsing<UserProfilesResources>().SetServiceBaseUrl("~/Sitefinity/Services/UserProfiles/UserProfileTypesService.svc/").DoNotRenderTranslationView().SetExternalClientScripts(extenalClientScripts).DoNotUseWorkflow();
      DetailFormViewElement detailFormViewElement1 = fluentDetailView1.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) detailFormViewElement1);
      UserProfilesDefinitions.CreateBackendSectionsProfileTypes(fluentDetailView1);
      DefinitionsHelper.CreateBackendFormToolbar(detailFormViewElement1, typeof (UserProfilesResources).Name, true);
      DetailViewDefinitionFacade fluentDetailView2 = definitionFacade.AddDetailView("UserProfileTypesBackendEdit").SetTitle("EditItem").HideTopToolbar().LocalizeUsing<UserProfilesResources>().SetServiceBaseUrl("~/Sitefinity/Services/UserProfiles/UserProfileTypesService.svc/").DoNotRenderTranslationView().SetExternalClientScripts(extenalClientScripts).DoNotUseWorkflow().SetAlternativeTitle("CreateNewItem");
      DetailFormViewElement detailFormViewElement2 = fluentDetailView2.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) detailFormViewElement2);
      UserProfilesDefinitions.CreateBackendSectionsProfileTypes(fluentDetailView2);
      DefinitionsHelper.CreateBackendFormToolbar(detailFormViewElement2, typeof (UserProfilesResources).Name, false);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control for User Profiles on the frontend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineProfilesFrontendContentView(
      ConfigElement parent)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, "FrontendUsersList", typeof (UserProfile)).Get();
      UserProfileDetailElement element1 = new UserProfileDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element1.ViewName = "UserProfilesFrontendDetailsRead";
      element1.ViewType = typeof (UserProfileDetailReadView);
      element1.DisplayMode = FieldDisplayMode.Read;
      element1.ResourceClassId = typeof (UserProfilesResources).Name;
      element1.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      element1.ShowAdditionalModesLinks = new bool?(false);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      UserProfileViewMasterElement element2 = new UserProfileViewMasterElement((ConfigElement) viewControlElement.ViewsConfig);
      element2.ViewName = "UserProfilesFrontendMaster";
      element2.ViewType = typeof (UserProfileMasterView);
      element2.DisplayMode = FieldDisplayMode.Read;
      element2.ResourceClassId = typeof (UserProfilesResources).Name;
      element2.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      element2.UsersDisplayMode = new UsersDisplayMode?(UsersDisplayMode.All);
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control for User Profiles on the frontend
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineProfileFrontendContentView(
      ConfigElement parent)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, "FrontendSingleProfile", typeof (UserProfile)).Get();
      UserProfileDetailElement element1 = new UserProfileDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element1.ViewName = "UserProfilesFrontendDetailsRead";
      element1.ViewType = typeof (UserProfileDetailReadView);
      element1.DisplayMode = FieldDisplayMode.Read;
      element1.ResourceClassId = typeof (UserProfilesResources).Name;
      element1.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      UserProfileDetailElement element2 = new UserProfileDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element2.ViewName = "UserProfilesFrontendDetailsWrite";
      element2.ViewType = typeof (UserProfileDetailWriteView);
      element2.ResourceClassId = typeof (UserProfilesResources).Name;
      element2.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      ContentViewDetailElement element3 = new ContentViewDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element3.ViewName = "ChangePasswordDetailView";
      element3.ViewType = typeof (UserChangePasswordView);
      element3.ResourceClassId = typeof (UserProfilesResources).Name;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element3);
      ContentViewDetailElement element4 = new ContentViewDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element4.ViewName = "ChangeQuestionAndAnswerDetailView";
      element4.ViewType = typeof (UserChangePasswordQuestionAndAnswerView);
      element4.ResourceClassId = typeof (UserProfilesResources).Name;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element4);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control for displaying Sitefinity(Basic) profile in the backend.
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineProfileBackendContentView(
      ConfigElement parent)
    {
      ContentViewControlElement viewControlElement = App.WorkWith().Module().DefineContainer(parent, "BackendSingleProfile", typeof (UserProfile)).Get();
      UserProfileDetailElement element1 = new UserProfileDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element1.ViewName = "UserProfilesBackendDetailsRead";
      element1.ViewType = typeof (UserProfileDetailReadView);
      element1.DisplayMode = FieldDisplayMode.Read;
      element1.ResourceClassId = typeof (UserProfilesResources).Name;
      element1.ShowAdditionalModesLinks = new bool?(true);
      element1.SubmittingUserProfileSuccessAction = new SubmittingSuccessAction?(SubmittingSuccessAction.SwitchToReadMode);
      element1.TemplatePath = UserProfileDetailReadView.autoGeneratedFieldsBackendLayoutTemplatePath;
      element1.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      UserProfileDetailElement element2 = new UserProfileDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element2.ViewName = "UserProfilesBackendDetailsWrite";
      element2.ViewType = typeof (UserProfileDetailWriteView);
      element2.ResourceClassId = typeof (UserProfilesResources).Name;
      element2.TemplatePath = UserProfileDetailWriteView.autoGeneratedFieldsBackendLayoutTemplatePath;
      element2.SubmittingUserProfileSuccessAction = new SubmittingSuccessAction?(SubmittingSuccessAction.SwitchToReadMode);
      element2.ProfileTypeFullName = typeof (SitefinityProfile).FullName;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      ContentViewDetailElement element3 = new ContentViewDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element3.ViewName = "BackendChangePasswordDetailView";
      element3.ViewType = typeof (UserChangePasswordView);
      element3.TemplatePath = "~/SFRes/Telerik.Sitefinity.Resources.Templates.Backend.Security.UserChangePasswordViewBackend.ascx";
      element3.ResourceClassId = typeof (UserProfilesResources).Name;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element3);
      ContentViewDetailElement element4 = new ContentViewDetailElement((ConfigElement) viewControlElement.ViewsConfig);
      element4.ViewName = "BackendChangeQuestionAndAnswerDetailView";
      element4.ViewType = typeof (UserChangePasswordQuestionAndAnswerView);
      element4.TemplatePath = "~/SFRes/Telerik.Sitefinity.Resources.Templates.Backend.Security.UserChangePasswordQuestionAndAnswerViewBackend.ascx";
      element4.ResourceClassId = typeof (UserProfilesResources).Name;
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element4);
      return viewControlElement;
    }

    /// <summary>
    /// Defines the ContentView control used when automatic user profile templates are used. When the user adds a custom field
    /// for Sitefinity(Basic) profile type, it is automatically added to the views of this control.
    /// </summary>
    /// <param name="parent">The parent configuration element.</param>
    /// <returns>A configured instance of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewControlElement" />.</returns>
    internal static ContentViewControlElement DefineSitefinityProfileContentViews(
      ConfigElement parent)
    {
      string viewDefinitionName = UserProfilesHelper.GetContentViewDefinitionName(typeof (SitefinityProfile));
      ContentViewControlDefinitionFacade definitionFacade = App.WorkWith().Module().DefineContainer(parent, viewDefinitionName, typeof (SitefinityProfile));
      ContentViewControlElement viewControlElement = definitionFacade.Get();
      DetailViewDefinitionFacade fluentDetailView1 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.FrontendCreate)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.FrontendCreate)).HideTopToolbar().LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element1 = fluentDetailView1.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element1);
      UserProfilesDefinitions.CreateFrontendSectionsNewProfileView(fluentDetailView1, FieldDisplayMode.Write);
      DetailViewDefinitionFacade fluentDetailView2 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.FrontendEdit)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.FrontendEdit)).HideTopToolbar().LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element2 = fluentDetailView2.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element2);
      UserProfilesDefinitions.CreateFrontendSectionsEditProfileView(fluentDetailView2, FieldDisplayMode.Write);
      DetailViewDefinitionFacade fluentDetailView3 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.FrontendView)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.FrontendView)).HideTopToolbar().SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element3 = fluentDetailView3.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element3);
      UserProfilesDefinitions.CreateFrontendSectionsViewProfileView(fluentDetailView3, FieldDisplayMode.Read);
      DetailViewDefinitionFacade fluentDetailView4 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.BackendCreate)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.BackendCreate)).HideTopToolbar().LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element4 = fluentDetailView4.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element4);
      UserProfilesDefinitions.CreateBackendSectionsNewProfileView(fluentDetailView4, FieldDisplayMode.Write, ImageFieldUploadMode.Dialog, true);
      DetailViewDefinitionFacade fluentDetailView5 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.BackendEdit)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.BackendEdit)).HideTopToolbar().LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element5 = fluentDetailView5.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element5);
      UserProfilesDefinitions.CreateBackendSectionsEditProfileView(fluentDetailView5, FieldDisplayMode.Write, ImageFieldUploadMode.Dialog, true);
      DetailViewDefinitionFacade fluentDetailView6 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.BackendView)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.BackendView)).HideTopToolbar().SetDisplayMode(FieldDisplayMode.Read).LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element6 = fluentDetailView6.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element6);
      UserProfilesDefinitions.CreateBackendSectionsViewOwnProfileView(fluentDetailView6, FieldDisplayMode.Read, ImageFieldUploadMode.InputField);
      DetailViewDefinitionFacade fluentDetailView7 = definitionFacade.AddDetailView(UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.BackendOwnEdit)).SetTitle(UserProfilesHelper.GetContentViewTitle(ProfileTypeViewKind.BackendOwnEdit)).HideTopToolbar().LocalizeUsing<UserProfilesResources>().DoNotRenderTranslationView().DoNotUseWorkflow();
      DetailFormViewElement element7 = fluentDetailView7.Get();
      viewControlElement.ViewsConfig.Add((ContentViewDefinitionElement) element7);
      UserProfilesDefinitions.CreateBackendSectionsEditOwnProfileView(fluentDetailView7, FieldDisplayMode.Write, ImageFieldUploadMode.InputField);
      return viewControlElement;
    }

    /// <summary>Creates the backend sections for profile types.</summary>
    private static void CreateBackendSectionsProfileTypes(
      DetailViewDefinitionFacade fluentDetailView)
    {
      SectionDefinitionFacade<DetailViewDefinitionFacade> definitionFacade1 = fluentDetailView.AddFirstSection("MainSection");
      ContentViewSectionElement viewSectionElement = definitionFacade1.Get();
      TextFieldDefinitionFacade<SectionDefinitionFacade<DetailViewDefinitionFacade>> definitionFacade2 = definitionFacade1.AddTextField("Title").SetId("titleFieldControl").SetTitle("Title").SetCssClass("sfTitleField").SetExample("ExampleTitle").AddValidation().MakeRequired().SetRequiredViolationMessage("TitleCannotBeEmpty").Done();
      definitionFacade1.AddMirrorTextField("Name").SetTitle("NameForDevs").LocalizeUsing<Labels>().SetId("formName").SetMirroredControlId(definitionFacade2.Get().ID).SetRegularExpressionFilter("^[^A-Za-z]+|[^\\w_]+").SetReplaceWithValue("").SetPrefixText("").AddValidation().MakeRequired().SetRegularExpression("^[a-zA-Z]+[\\w|_]*$").SetRequiredViolationMessage("NameCannotBeEmpty").SetRegularExpressionViolationMessage("ProfileNameNotValid").LocalizeUsing<UserProfilesResources>();
      definitionFacade1.AddChoiceField<ProfileProviderChoiceField>("ProfileProviderName", RenderChoicesAs.DropDown).SetId("choiceField").SetTitle("ProfileProvider").SetDisplayMode(FieldDisplayMode.Write).MakeMutuallyExclusive().SetWrapperTag(HtmlTextWriterTag.Li).Done();
      UserProvidersFieldDefinitionElement definitionElement = new UserProvidersFieldDefinitionElement((ConfigElement) viewSectionElement.Fields);
      definitionElement.Title = "UserProvidersFieldTitle";
      definitionElement.ResourceClassId = typeof (UserProfilesResources).Name;
      definitionElement.ID = "userProvidersField";
      definitionElement.DataFieldName = "ShowInNavigation";
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      UserProvidersFieldDefinitionElement element = definitionElement;
      viewSectionElement.Fields.Add((FieldDefinitionElement) element);
    }

    private static void CreateBackendSectionsNewProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode,
      ImageFieldUploadMode uploadMode,
      bool includeNickname)
    {
      UserProfilesDefinitions.AddCommonProfileFields(fluentDetailView.AddSection(CustomFieldsContext.CustomFieldsSectionName).SetCssClass("sfFirstProfileTypeForm").SetDisplayMode(displayMode).AddExpandableBehavior().Expand().Done(), displayMode, uploadMode, true, includeNickname);
    }

    private static ContentViewSectionElement CreateBackendSectionsEditProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode,
      ImageFieldUploadMode uploadMode,
      bool includeNickname,
      bool includeEmailField = false)
    {
      SectionDefinitionFacade<DetailViewDefinitionFacade> mainSectionFacade = fluentDetailView.AddSection(CustomFieldsContext.CustomFieldsSectionName).SetCssClass("sfFirstProfileTypeForm").SetDisplayMode(displayMode).AddExpandableBehavior().Expand().Done();
      ContentViewSectionElement sectionsEditProfileView = mainSectionFacade.Get();
      UserProfilesDefinitions.AddCommonProfileFields(mainSectionFacade, displayMode, uploadMode, true, includeNickname, includeEmailField);
      return sectionsEditProfileView;
    }

    private static void CreateBackendSectionsEditOwnProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode,
      ImageFieldUploadMode uploadMode)
    {
      ContentViewSectionElement sectionsEditProfileView = UserProfilesDefinitions.CreateBackendSectionsEditProfileView(fluentDetailView, displayMode, uploadMode, true, true);
      LanguageChoiceFieldElement element = new LanguageChoiceFieldElement((ConfigElement) sectionsEditProfileView.Fields);
      element.ID = "preferredLanguageFieldControl";
      element.DataFieldName = "PreferredLanguage";
      element.DisplayMode = new FieldDisplayMode?(displayMode);
      element.HideIfSingleLanguage = new bool?(true);
      element.Title = "PreferredLanguage";
      element.ResourceClassId = typeof (UserProfilesResources).Name;
      element.WrapperTag = HtmlTextWriterTag.Li;
      element.FieldType = typeof (LanguageChoiceFieldProfiles);
      element.FieldName = "languageField";
      element.RenderChoiceAs = RenderChoicesAs.DropDown;
      element.LanguageSource = new LanguageSource?(LanguageSource.Backend);
      element.MutuallyExclusive = true;
      sectionsEditProfileView.Fields.Add((FieldDefinitionElement) element);
    }

    private static void CreateBackendSectionsViewOwnProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode,
      ImageFieldUploadMode uploadMode)
    {
      UserProfilesDefinitions.CreateBackendSectionsEditOwnProfileView(fluentDetailView, displayMode, uploadMode);
    }

    private static void CreateFrontendSectionsNewProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      UserProfilesDefinitions.CreateBackendSectionsNewProfileView(fluentDetailView, displayMode, ImageFieldUploadMode.InputField, false);
    }

    private static void CreateFrontendSectionsEditProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      UserProfilesDefinitions.CreateBackendSectionsEditProfileView(fluentDetailView, displayMode, ImageFieldUploadMode.InputField, false, true);
    }

    private static void CreateFrontendSectionsViewProfileView(
      DetailViewDefinitionFacade fluentDetailView,
      FieldDisplayMode displayMode)
    {
      UserProfilesDefinitions.CreateBackendSectionsViewOwnProfileView(fluentDetailView, displayMode, ImageFieldUploadMode.InputField);
    }

    private static void AddCommonProfileFields(
      SectionDefinitionFacade<DetailViewDefinitionFacade> mainSectionFacade,
      FieldDisplayMode displayMode,
      ImageFieldUploadMode uploadMode,
      bool includeAboutField,
      bool includeNicknameField,
      bool includeEmailField = false)
    {
      ContentViewSectionElement viewSectionElement = mainSectionFacade.Get();
      ImageFieldElement element = new ImageFieldElement((ConfigElement) viewSectionElement.Fields);
      element.ID = "avatarField";
      element.DataFieldName = "Avatar";
      element.DisplayMode = new FieldDisplayMode?(displayMode);
      element.UploadMode = new ImageFieldUploadMode?(uploadMode);
      element.Title = "Photo";
      element.WrapperTag = HtmlTextWriterTag.Li;
      element.CssClass = "sfUserAvatar";
      element.ResourceClassId = typeof (UserProfilesResources).Name;
      element.DataFieldType = typeof (ContentLink);
      element.DefaultSrc = "~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png";
      element.SizeInPx = new int?(100);
      viewSectionElement.Fields.Add((FieldDefinitionElement) element);
      mainSectionFacade.AddTextField("FirstName").SetId("firstNameFieldControl").SetTitle("FirstName").LocalizeUsing<Labels>().AddValidation().MakeRequired().SetRequiredViolationMessage("CreateUserWizardDefaultFirstNameRequiredErrorMessage").SetMaxLength(250).SetMaxLengthViolationMessage("NameLength").LocalizeUsing<ErrorMessages>().Done().Done().AddTextField("LastName").SetId("lastNameFieldControl").SetTitle("LastName").LocalizeUsing<Labels>().AddValidation().MakeRequired().SetRequiredViolationMessage("CreateUserWizardDefaultLastNameRequiredErrorMessage").SetMaxLength(250).SetMaxLengthViolationMessage("NameLength").LocalizeUsing<ErrorMessages>().Done().Done();
      if (includeEmailField)
        mainSectionFacade.AddTextField("sf_Email").SetId("emailFieldControl").SetTitle("Email").LocalizeUsing<Labels>().AddValidation().MakeRequired().SetRequiredViolationMessage("EmailCannotBeEmpty").SetRegularExpression("[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,4}").SetRegularExpressionViolationMessage("EmailAddressViolationMessage").LocalizeUsing<ErrorMessages>().Done().Done();
      if (includeNicknameField)
        mainSectionFacade.AddTextField("Nickname").SetId("nicknameFieldControl").SetTitle("Nickname").LocalizeUsing<UserProfilesResources>().AddValidation().SetMaxLength(64).SetMaxLengthViolationMessage("NicknameMaxLength").LocalizeUsing<ErrorMessages>().SetMessageCssClass("sfError").Done().Done();
      if (!includeAboutField)
        return;
      mainSectionFacade.AddTextField("About").SetId("firstNameFieldControl").SetTitle("About").SetRows(5).Done();
    }

    /// <summary>Fills the action menu items.</summary>
    /// <param name="menuItems">The menu items.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    public static void FillActionMenuItems(
      ConfigElementList<WidgetElement> menuItems,
      ConfigElement parent,
      string resourceClassId)
    {
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Delete", HtmlTextWriterTag.Li, "delete", "Delete", resourceClassId, "sfDeleteItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "Edit", HtmlTextWriterTag.Li, "edit", "Edit", resourceClassId, "sfEditItm"));
      menuItems.Add((WidgetElement) DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuItems, "EditFields", HtmlTextWriterTag.Li, "moduleEditor", "EditFields", typeof (UserProfilesResources).Name, "sfEditItm"));
    }

    private static void CreateDialogs(MasterViewDefinitionFacade fluentFacade)
    {
      string parameters = "?AllowContentLinks=True&RefreshParentOnCancel={{RefreshParentOnCancel}}&TypeName={{DynamicTypeName}}&Title={{Title}}" + "&BackLabelText=" + Res.Get<UserProfilesResources>().BackToItems + "&ItemsName=" + Res.Get<UserProfilesResources>().PluralItemName;
      fluentFacade.AddInsertDialog("UserProfileTypesBackendInsert").Done().AddEditDialog("UserProfileTypesBackendEdit").Done().AddDialog<ModuleEditorDialog>("moduleEditor").SetParameters(parameters).MakeFullScreen();
    }
  }
}
