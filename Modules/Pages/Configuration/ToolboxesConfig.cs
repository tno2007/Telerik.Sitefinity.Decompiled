// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ToolboxesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Represents a configuration section for Sitefinity toolboxes.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxesConfigDescription", Title = "ToolboxesConfigTitle")]
  public class ToolboxesConfig : ConfigSection
  {
    private const string SocialToolboxesSectionName = "Social";
    /// <summary>Executes logic during the process widget validation</summary>
    internal static Func<string, Tuple<bool, string>> ValidateWidgetState;
    private readonly ConcurrentProperty<List<ToolboxesConfig.ModuleWidgetInfo>> moduleWidgets;
    private const string DynamicContentTypeNameParamKey = "DynamicContentTypeName";
    public const string ContentToolboxSectionName = "ContentToolboxSection";
    public const string NewslettersControlsToolboxName = "NewsletterControls";
    public const string NewslettersToolboxSectionName = "NewslettersToolboxSectionName";
    public const string NavigationControlsSectionName = "NavigationControlsSection";
    public const string LayoutsToolboxName = "PageLayouts";
    public const string PageControlsToolboxName = "PageControls";

    public ToolboxesConfig() => this.moduleWidgets = new ConcurrentProperty<List<ToolboxesConfig.ModuleWidgetInfo>>(new Func<List<ToolboxesConfig.ModuleWidgetInfo>>(this.GetModuleDependedWidgets));

    /// <summary>Gets the toolboxes.</summary>
    /// <value>The toolboxes.</value>
    [ConfigurationProperty("toolboxes")]
    [ConfigurationCollection(typeof (Toolbox), AddItemName = "toolbox")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolboxesDescription", Title = "ToolboxesTitle")]
    public ConfigElementDictionary<string, Toolbox> Toolboxes => (ConfigElementDictionary<string, Toolbox>) this["toolboxes"];

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion.Build < 2660)
      {
        foreach (Toolbox toolbox in (IEnumerable<Toolbox>) this.Toolboxes.Values)
        {
          foreach (ToolboxSection section in toolbox.Sections)
          {
            foreach (ToolboxItem tool in section.Tools)
              tool.ControlType = tool.ControlType;
          }
        }
      }
      if (oldVersion.Build < 4300 && this.Toolboxes.GetElementByKey("PageControls") is Toolbox elementByKey1 && elementByKey1.Sections.GetElementByKey("NavigationControlsSection") is ToolboxSection elementByKey2)
        elementByKey2.Tools.Add(new ToolboxItem((ConfigElement) elementByKey2.Tools)
        {
          Name = "ObsoleteNavigation",
          Title = "Navigation <span class=\"sfNote\">(obsolete)</span>",
          Description = "Group of links displayed in different ways – horizontal, vertical, dropdown menu, tree, etc.",
          ResourceClassId = "",
          CssClass = "sfNavigationIcn",
          ControlType = typeof (NavigationControl).AssemblyQualifiedName
        });
      if (oldVersion.Build >= 6800)
        return;
      Toolbox toolbox1;
      this.Toolboxes.TryGetValue("PageControls", out toolbox1);
      if (toolbox1 == null)
        return;
      ConfigElement elementByKey3 = toolbox1.Sections.GetElementByKey("Social");
      if (elementByKey3 == null || !(elementByKey3 is ToolboxSection toolboxSection))
        return;
      ConfigElement elementByKey4 = toolboxSection.Tools.GetElementByKey("SocialShare_MVC");
      if (elementByKey4 == null)
        return;
      toolboxSection.Tools.Remove(elementByKey4);
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized() => this.InitializePageToolbox();

    protected internal override void OnSectionChanged()
    {
      base.OnSectionChanged();
      this.moduleWidgets.Reset();
    }

    private void InitializePageToolbox()
    {
      IControlUtilities controlUtilities = ObjectFactory.Resolve<IControlUtilities>();
      string name1 = typeof (PageResources).Name;
      Toolbox element1 = new Toolbox((ConfigElement) this.Toolboxes)
      {
        Name = "PageControls",
        Title = "PageControlsToolboxTitle",
        Description = "PageControlsToolboxDescription",
        ResourceClassId = name1
      };
      this.Toolboxes.Add(element1);
      ToolboxSection element2 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "ContentToolboxSection",
        Title = "ContentToolboxSectionTitle",
        Description = "ContentToolboxSectionDescription",
        ResourceClassId = name1
      };
      element1.Sections.Add(element2);
      element2.Tools.Add(new ToolboxItem((ConfigElement) element2.Tools)
      {
        Name = "ContentBlock",
        Title = "ContentBlockTitle",
        Description = "ContentBlockDescription",
        ResourceClassId = name1,
        CssClass = "sfContentBlockIcn",
        ControlType = typeof (ContentBlock).AssemblyQualifiedName
      });
      element2.Tools.Add(new ToolboxItem((ConfigElement) element2.Tools)
      {
        Name = "ImageControl",
        Title = "ImageControlTitle",
        Description = "ImageControlDescription",
        ResourceClassId = name1,
        CssClass = "sfImageViewIcn",
        ControlType = typeof (ImageControl).AssemblyQualifiedName,
        ModuleName = "Libraries"
      });
      element2.Tools.Add(new ToolboxItem((ConfigElement) element2.Tools)
      {
        Name = "ImagesView",
        Title = "ImagesViewTitle",
        Description = "ImagesViewDescription",
        ResourceClassId = LibrariesModule.ResourceClassId,
        ModuleName = "Libraries",
        CssClass = "sfImageLibraryViewIcn",
        ControlType = typeof (ImagesView).AssemblyQualifiedName
      });
      ToolboxSection element3 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "NavigationControlsSection",
        Title = "NavigationControlsSectionTitle",
        Description = "NavigationControlsSectionDescription",
        ResourceClassId = name1
      };
      element1.Sections.Add(element3);
      element3.Tools.Add(new ToolboxItem((ConfigElement) element3.Tools)
      {
        Name = "Navigation",
        Title = "NavigationControlsSectionTitle",
        Description = "NavigationControlsSectionDescription",
        ResourceClassId = name1,
        CssClass = "sfNavigationIcn",
        ControlType = typeof (LightNavigationControl).AssemblyQualifiedName
      });
      element3.Tools.Add(new ToolboxItem((ConfigElement) element3.Tools)
      {
        Name = "ArchiveControl",
        Title = "ArchiveControlTitle",
        Description = "ArchiveControlDescription",
        ResourceClassId = name1,
        CssClass = "sfArchiveIcn",
        ControlType = typeof (ArchiveControl).AssemblyQualifiedName
      });
      element3.Tools.Add(new ToolboxItem((ConfigElement) element3.Tools)
      {
        Name = "LanguageSelectorControl",
        Title = "LanguageSelectorControlTitle",
        Description = "LanguageSelectorControlDescription",
        ResourceClassId = name1,
        CssClass = "sfLanguageSelectorIcn",
        ControlType = typeof (LanguageSelectorControl).AssemblyQualifiedName
      });
      element3.Tools.Add(new ToolboxItem((ConfigElement) element3.Tools)
      {
        Name = "Breadcrumb",
        Title = "BreadcrumbTitle",
        Description = "BreadcrumbDescription",
        ResourceClassId = name1,
        CssClass = "sfBreadcrumbIcn",
        ControlType = typeof (Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb).AssemblyQualifiedName
      });
      ToolboxSection element4 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "RadControls",
        Title = "RadControlsTitle",
        Description = "RadControlsDescription",
        ResourceClassId = name1,
        Enabled = false
      };
      element1.Sections.Add(element4);
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadCalendar",
        Title = "RadCalendarTitle",
        Description = "RadCalendarDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadCalendar).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadDateTimePicker",
        Title = "RadDateTimePickerTitle",
        Description = "RadDateTimePickerDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadDateTimePicker).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadCaptcha",
        Title = "RadCaptchaTitle",
        Description = "RadCaptchaDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadCaptcha).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadColorPicker",
        Title = "RadColorPickerTitle",
        Description = "RadColorPickerDescription",
        ResourceClassId = name1,
        ControlType = typeof (Telerik.Web.UI.RadColorPicker).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadComboBox",
        Title = "RadComboBoxTitle",
        Description = "RadComboBoxDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadComboBox).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadEditor",
        Title = "RadEditorTitle",
        Description = "RadEditorDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadEditor).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadGrid",
        Title = "RadGridTitle",
        Description = "RadGridDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadGrid).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadMenu",
        Title = "RadMenuTitle",
        Description = "RadMenuDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadMenu).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadPanelBar",
        Title = "RadPanelBarTitle",
        Description = "RadPanelBarDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadPanelBar).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadRotator",
        Title = "RadRotatorTitle",
        Description = "RadRotatorDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadRotator).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadSiteMap",
        Title = "RadSiteMapTitle",
        Description = "RadSiteMapDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadSiteMap).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadSkinManager",
        Title = "RadSkinManagerTitle",
        Description = "RadSkinManagerDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadSkinManager).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadSlider",
        Title = "RadSliderTitle",
        Description = "RadSliderDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadSlider).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadSpell",
        Title = "RadSpellTitle",
        Description = "RadSpellDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadSpell).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadSplitter",
        Title = "RadSplitterTitle",
        Description = "RadSplitterDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadSplitter).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadTabStrip",
        Title = "RadTabStripTitle",
        Description = "RadTabStripDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadTabStrip).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadToolBar",
        Title = "RadToolBarTitle",
        Description = "RadToolBarDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadToolBar).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadToolTip",
        Title = "RadToolTipTitle",
        Description = "RadToolTipDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadToolTip).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadTreeView",
        Title = "RadTreeViewTitle",
        Description = "RadTreeViewDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadTreeView).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadUpload",
        Title = "RadUploadTitle",
        Description = "RadUploadDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadUpload).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadWindow",
        Title = "RadWindowTitle",
        Description = "RadWindowDescription",
        ResourceClassId = name1,
        ControlType = typeof (Telerik.Web.UI.RadWindow).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadWindowManager",
        Title = "RadWindowManagerTitle",
        Description = "RadWindowManagerDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadWindowManager).AssemblyQualifiedName
      });
      element4.Tools.Add(new ToolboxItem((ConfigElement) element4.Tools)
      {
        Name = "RadXmlHttpPanel",
        Title = "RadXmlHttpPanelTitle",
        Description = "RadXmlHttpPanelDescription",
        ResourceClassId = name1,
        ControlType = typeof (RadXmlHttpPanel).AssemblyQualifiedName
      });
      ToolboxSection element5 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "Data",
        Title = "DataToolboxSectionTitle",
        Description = "DataToolboxSectionDescription",
        ResourceClassId = name1
      };
      element1.Sections.Add(element5);
      element5.Tools.Add(new ToolboxItem((ConfigElement) element5.Tools)
      {
        Name = "XmlDataSource",
        Title = "XmlDataSourceTitle",
        Description = "XmlDataSourceDescription",
        ResourceClassId = name1,
        ControlType = typeof (ExtendedXmlDataSource).AssemblyQualifiedName
      });
      element5.Tools.Add(new ToolboxItem((ConfigElement) element5.Tools)
      {
        Name = "SiteMapDataSource",
        Title = "SiteMapDataSourceTitle",
        Description = "SiteMapDataSourceDescription",
        ResourceClassId = name1,
        ControlType = typeof (ExtendedSiteMapDataSource).AssemblyQualifiedName
      });
      element5.Tools.Add(new ToolboxItem((ConfigElement) element5.Tools)
      {
        Name = "OpenAccessDataSource",
        Title = "OpenAccessDataSourceTitle",
        Description = "OpenAccessDataSourceDescription",
        ResourceClassId = name1,
        ControlType = typeof (ExtendedOpenAccessDataSource).AssemblyQualifiedName
      });
      ToolboxSection element6 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "ScriptsAndStylesControlsSection",
        Title = "ScriptsAndStylesControlsSectionTitle",
        Description = "ScriptsAndStylesControlsSectionDescription",
        ResourceClassId = name1
      };
      element1.Sections.Add(element6);
      element6.Tools.Add(new ToolboxItem((ConfigElement) element6.Tools)
      {
        Name = "CssEmbedControl",
        Title = "CssEmbedControlTitle",
        Description = "CssEmbedControlDescription",
        ResourceClassId = name1,
        CssClass = "sfLinkedFileViewIcn",
        ControlType = typeof (CssEmbedControl).AssemblyQualifiedName
      });
      element6.Tools.Add(new ToolboxItem((ConfigElement) element6.Tools)
      {
        Name = "JavaScriptEmbedControl",
        Title = "JavaScriptEmbedControlTitle",
        Description = "JavaScriptEmbedControlDescription",
        ResourceClassId = name1,
        CssClass = "sfLinkedFileViewIcn",
        ControlType = typeof (JavaScriptEmbedControl).AssemblyQualifiedName
      });
      element6.Tools.Add(new ToolboxItem((ConfigElement) element6.Tools)
      {
        Name = "GoogleAnalyticsEmbedControl",
        Title = "GoogleAnalyticsEmbedControlTitle",
        Description = "GoogleAnalyticsEmbedControlDescription",
        ResourceClassId = name1,
        CssClass = "sfLinkedFileViewIcn",
        ControlType = typeof (GoogleAnalyticsEmbedControl).AssemblyQualifiedName
      });
      string name2 = typeof (PublicControlsResources).Name;
      ToolboxSection element7 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "Login",
        Title = "LoginControlsTitle",
        Description = "LoginControlsDescription",
        ResourceClassId = name2
      };
      element1.Sections.Add(element7);
      element7.Tools.Add(new ToolboxItem((ConfigElement) element7.Tools)
      {
        Name = "Login",
        Title = "LoginControlTitle",
        Description = "LoginControlDescription",
        ResourceClassId = name2,
        ControlType = typeof (LoginControl).AssemblyQualifiedName,
        CssClass = "sfLoginIcn"
      });
      element7.Tools.Add(new ToolboxItem((ConfigElement) element7.Tools)
      {
        Name = "LoginName",
        Title = "LoginNameControlTitle",
        Description = "LoginNameControlDescription",
        ResourceClassId = name2,
        ControlType = typeof (LoginNameControl).AssemblyQualifiedName,
        CssClass = "sfLoginNameIcn"
      });
      element7.Tools.Add(new ToolboxItem((ConfigElement) element7.Tools)
      {
        Name = "LoginStatus",
        Title = "LoginStatusControlTitle",
        Description = "LoginStatusControlDescription",
        ResourceClassId = name2,
        ControlType = typeof (LoginStatusControl).AssemblyQualifiedName,
        CssClass = "sfLoginStatusIcn"
      });
      element7.Tools.Add(new ToolboxItem((ConfigElement) element7.Tools)
      {
        Name = "LoginWidget",
        Title = "LoginWidgetTitle",
        Description = "LoginControlDescription",
        ResourceClassId = name2,
        ControlType = typeof (LoginWidget).AssemblyQualifiedName,
        CssClass = "sfLoginIcn"
      });
      element7.Tools.Add(new ToolboxItem((ConfigElement) element7.Tools)
      {
        Name = "UserChangePasswordWidget",
        Title = "UserChangePasswordWidgetTitle",
        Description = "UserChangePasswordWidgetDescription",
        ResourceClassId = name2,
        ControlType = typeof (UserChangePasswordWidget).AssemblyQualifiedName,
        CssClass = "sfChangePasswordIcn"
      });
      string name3 = typeof (UserProfilesResources).Name;
      ToolboxSection element8 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "Users",
        Title = "UserSectionTitle",
        Description = "UserSectionDescription",
        ResourceClassId = name3
      };
      element1.Sections.Add(element8);
      element8.Tools.Add(new ToolboxItem((ConfigElement) element8.Tools)
      {
        Name = "UserProfileView",
        Title = "UserProfileViewTitle",
        Description = "UserProfilesViewDescription",
        ResourceClassId = name3,
        ControlType = typeof (UserProfileView).AssemblyQualifiedName,
        CssClass = "sfProfilecn"
      });
      element8.Tools.Add(new ToolboxItem((ConfigElement) element8.Tools)
      {
        Name = "UserProfilesView",
        Title = "UserProfilesViewTitle",
        Description = "UsersListViewDescription",
        ResourceClassId = name3,
        ControlType = typeof (UserProfilesView).AssemblyQualifiedName,
        CssClass = "sfUserListIcn"
      });
      element8.Tools.Add(new ToolboxItem((ConfigElement) element8.Tools)
      {
        Name = "RegistrationWidget",
        Title = "RegistrationWidgetTitle",
        Description = "RegistrationWidgetDescription",
        ResourceClassId = name3,
        ControlType = typeof (RegistrationForm).AssemblyQualifiedName,
        CssClass = "sfCreateAccountIcn"
      });
      element8.Tools.Add(new ToolboxItem((ConfigElement) element8.Tools)
      {
        Name = "AccountActivationWidget",
        Title = "AccountActivationWidgetTitle",
        Description = "AccountActivationWidgetDescription",
        ResourceClassId = name3,
        ControlType = typeof (UserActivationControl).AssemblyQualifiedName,
        CssClass = "sfAccountActivationIcn"
      });
      ToolboxSection element9 = new ToolboxSection((ConfigElement) element1.Sections)
      {
        Name = "Social",
        Title = "Social",
        Description = "SocialSectionDescription",
        ResourceClassId = name3
      };
      element1.Sections.Add(element9);
      element9.Tools.Add(new ToolboxItem((ConfigElement) element9.Tools)
      {
        Name = "TwitterWidget",
        Title = "TwitterWidgetTitle",
        Description = "TwitterWidgetDescription",
        ResourceClassId = name3,
        ControlType = typeof (TwitterWidget).AssemblyQualifiedName,
        CssClass = "sfTwitterFeedIcn"
      });
      string assemblyQualifiedName = typeof (LayoutControl).AssemblyQualifiedName;
      Toolbox element10 = new Toolbox((ConfigElement) this.Toolboxes)
      {
        Name = "PageLayouts",
        Title = "PageLayoutToolboxTitle",
        Description = "PageLayoutToolboxDescription",
        ResourceClassId = name1
      };
      this.Toolboxes.Add(element10);
      ToolboxSection element11 = new ToolboxSection((ConfigElement) element10.Sections)
      {
        Name = "TwoColumns",
        Title = "TwoColumnsSectionTitle",
        Description = "TwoColumnsSectionDescription",
        ResourceClassId = name1
      };
      element10.Sections.Add(element11);
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col1",
        Title = "Col1Title",
        Description = "Col1Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column1Template.ascx"),
        CssClass = "sfL100",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col2T1",
        Title = "Col2T1Title",
        Description = "Col2T1Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template1.ascx"),
        CssClass = "sfL25_75",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col2T2",
        Title = "Col2T2Title",
        Description = "Col2T2Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template2.ascx"),
        CssClass = "sfL33_67",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col2T3",
        Title = "Col2T3Title",
        Description = "Col2T3Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template3.ascx"),
        CssClass = "sfL50_50",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col2T4",
        Title = "Col2T4Title",
        Description = "Col2T4Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template4.ascx"),
        CssClass = "sfL67_33",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col2T5",
        Title = "Col2T5Title",
        Description = "Col2T5Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template5.ascx"),
        CssClass = "sfL75_25",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col3T1",
        Title = "Col3T1Title",
        Description = "Col3T1Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column3Template1.ascx"),
        CssClass = "sfL33_34_33",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col3T2",
        Title = "Col3T2Title",
        Description = "Col3T2Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column3Template2.ascx"),
        CssClass = "sfL25_50_25",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col4T1",
        Title = "Col4T1Title",
        Description = "Col4T1Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column4Template1.ascx"),
        CssClass = "sfL25_25_25_25",
        ControlType = assemblyQualifiedName
      });
      element11.Tools.Add(new ToolboxItem((ConfigElement) element11.Tools)
      {
        Name = "Col5T1",
        Title = "Col5T1Title",
        Description = "Col5T1Description",
        ResourceClassId = name1,
        LayoutTemplate = controlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Layouts.Column5Template1.ascx"),
        CssClass = "sfL20_20_20_20_20",
        ControlType = assemblyQualifiedName
      });
      string name4 = typeof (FormsResources).Name;
      Toolbox element12 = new Toolbox((ConfigElement) this.Toolboxes)
      {
        Name = "FormControls",
        Title = "FormControlsTitle",
        Description = "FormControlsDescription",
        ResourceClassId = name4
      };
      this.Toolboxes.Add(element12);
      ToolboxSection element13 = new ToolboxSection((ConfigElement) element12.Sections)
      {
        Name = "Common",
        Title = "CommonSectionTitle",
        Description = "CommonSectionDescription",
        ResourceClassId = name4
      };
      element12.Sections.Add(element13);
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "TextBox",
        Title = "TextBoxTitle",
        Description = "TextBoxDescription",
        ResourceClassId = name4,
        CssClass = "sfTextboxIcn",
        ControlType = typeof (FormTextBox).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "MultipleChoice",
        Title = "MultipleChoiceTitle",
        Description = "MultipleChoiceDescription",
        ResourceClassId = name4,
        CssClass = "sfMultipleChoiceIcn",
        ControlType = typeof (FormMultipleChoice).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "Checkboxes",
        Title = "CheckboxesTitle",
        Description = "CheckboxesDescription",
        ResourceClassId = name4,
        CssClass = "sfCheckboxesIcn",
        ControlType = typeof (FormCheckboxes).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "ParagraphTextBox",
        Title = "ParagraphTextBoxTitle",
        Description = "ParagraphTextBoxDescription",
        ResourceClassId = name4,
        CssClass = "sfParagraphboxIcn",
        ControlType = typeof (FormParagraphTextBox).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "DropDownList",
        Title = "DropDownListTitle",
        Description = "DropDownListDescription",
        ResourceClassId = name4,
        CssClass = "sfDropdownIcn",
        ControlType = typeof (FormDropDownList).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "Email",
        Title = "EmailTitle",
        Description = "EmailDescription",
        ResourceClassId = name4,
        CssClass = "sfTextboxIcn",
        ControlType = typeof (FormEmailTextBox).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "SectionHeader",
        Title = "SectionHeaderTitle",
        Description = "SectionHeaderDescription",
        ResourceClassId = name4,
        CssClass = "sfSectionHeaderIcn",
        ControlType = typeof (FormSectionHeader).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "InstructionalText",
        Title = "InstructionalTextTitle",
        Description = "InstructionalTextDescription",
        ResourceClassId = name4,
        CssClass = "sfInstructionIcn",
        ControlType = typeof (FormInstructionalText).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "FileUpload",
        Title = "FileUploadTitle",
        Description = "FileUploadDescription",
        ResourceClassId = name4,
        CssClass = "sfFileUploadIcn",
        ControlType = typeof (FormFileUpload).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "FormsCapcha",
        Title = "RadCaptchaTitle",
        Description = "RadCaptchaDescription",
        ResourceClassId = name4,
        CssClass = "sfCaptchaIcn",
        ControlType = typeof (FormCaptcha).AssemblyQualifiedName
      });
      element13.Tools.Add(new ToolboxItem((ConfigElement) element13.Tools)
      {
        Name = "SubmitButton",
        Title = "SubmitButtonTitle",
        Description = "SubmitButtonDescription",
        ResourceClassId = name4,
        CssClass = "sfSubmitBtnIcn",
        ControlType = typeof (FormSubmitButton).AssemblyQualifiedName
      });
      string name5 = typeof (NewslettersResources).Name;
      Toolbox element14 = new Toolbox((ConfigElement) this.Toolboxes)
      {
        Name = "NewsletterControls",
        Title = "NewslettersControlsTitle",
        Description = "NewslettersControlsDescription",
        ResourceClassId = name5
      };
      this.Toolboxes.Add(element14);
      ToolboxSection element15 = new ToolboxSection((ConfigElement) element14.Sections)
      {
        Name = "Common",
        Title = "CommonSectionTitle",
        Description = "CommonSectionDescription",
        ResourceClassId = typeof (PageResources).Name
      };
      element14.Sections.Add(element15);
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "ContentBlock",
        Title = "ContentBlockTitle",
        Description = "ContentBlockDescription",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfContentBlockIcn",
        ControlType = typeof (NewslettersContentBlock).AssemblyQualifiedName
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "ImageControl",
        Title = "ImageControlTitle",
        Description = "ImageControlDescription",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfImageViewIcn",
        ControlType = typeof (ImageControl).AssemblyQualifiedName,
        ModuleName = "Libraries"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "ImagesView",
        Title = "ImagesViewTitle",
        Description = "ImagesViewDescription",
        ResourceClassId = LibrariesModule.ResourceClassId,
        ModuleName = "Libraries",
        CssClass = "sfImageLibraryViewIcn",
        ControlType = typeof (ImagesView).AssemblyQualifiedName
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "NewsView",
        Title = "NewsViewTitle",
        Description = "NewsViewDescription",
        ResourceClassId = "NewsResources",
        ModuleName = "News",
        CssClass = "sfNewsViewIcn",
        ControlType = "Telerik.Sitefinity.Modules.News.Web.UI.NewsView, Telerik.Sitefinity.ContentModules"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "BlogPostsView",
        Title = "BlogPostsViewTitle",
        Description = "BlogPostsViewDescription",
        ResourceClassId = "BlogResources",
        ModuleName = "Blogs",
        CssClass = "sfBlogsViewIcn",
        ControlType = "Telerik.Sitefinity.Modules.Blogs.Web.UI.BlogPostView, Telerik.Sitefinity.ContentModules"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "ListItemsView",
        Title = "ListItemsViewTitle",
        Description = "ListItemsViewDescription",
        ResourceClassId = "ListsResources",
        ModuleName = "Lists",
        CssClass = "sfListitemsIcn",
        ControlType = "Telerik.Sitefinity.Modules.Lists.Web.UI.ListView, Telerik.Sitefinity.ContentModules"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "EventsView",
        Title = "EventsViewTitle",
        Description = "EventsViewDescription",
        ResourceClassId = "EventsResources",
        ModuleName = "Events",
        CssClass = "sfEventsViewIcn",
        ControlType = "Telerik.Sitefinity.Modules.Events.Web.UI.EventsView, Telerik.Sitefinity.ContentModules"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "MediaPlayerControl",
        Title = "MediaPlayerControlTitle",
        Description = "MediaPlayerControlDescription",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfVideoIcn",
        ControlType = typeof (MediaPlayerControl).AssemblyQualifiedName,
        ModuleName = "Libraries"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "VideosView",
        Title = "VideosViewTitle",
        Description = "VideosViewDescription",
        ResourceClassId = LibrariesModule.ResourceClassId,
        ModuleName = "Libraries",
        CssClass = "sfVideoListIcn",
        ControlType = typeof (VideosView).AssemblyQualifiedName
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "DocumentLinkControl",
        Title = "DocumentLinkControlTitle",
        Description = "DocumentLinkControlDescription",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfDownloadLinkIcn",
        ControlType = typeof (DocumentLink).AssemblyQualifiedName,
        ModuleName = "Libraries"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "DownloadListView",
        Title = "DownloadListViewTitle",
        Description = "DownloadListViewDescription",
        ResourceClassId = typeof (PageResources).Name,
        CssClass = "sfDownloadListIcn",
        ControlType = typeof (DownloadListView).AssemblyQualifiedName,
        ModuleName = "Libraries"
      });
      element15.Tools.Add(new ToolboxItem((ConfigElement) element15.Tools)
      {
        Name = "GenericContentView",
        Title = "GenericContentViewTitle",
        Description = "GenericContentViewDescription",
        ResourceClassId = ContentModule.ResourceClassId,
        ModuleName = "GenericContent",
        CssClass = "sfContentViewIcn",
        ControlType = typeof (GenericContentView).AssemblyQualifiedName
      });
    }

    private List<ToolboxesConfig.ModuleWidgetInfo> GetModuleDependedWidgets()
    {
      List<ToolboxesConfig.ModuleWidgetInfo> moduleDependedWidgets = new List<ToolboxesConfig.ModuleWidgetInfo>();
      foreach (ToolboxSection section in this.Toolboxes["PageControls"].Sections)
      {
        foreach (ToolboxItem tool in section.Tools)
        {
          string moduleName = tool.ModuleName;
          if (!string.IsNullOrEmpty(moduleName))
          {
            ToolboxesConfig.ModuleWidgetInfo moduleWidgetInfo = new ToolboxesConfig.ModuleWidgetInfo()
            {
              ModuleName = moduleName,
              WidgetType = string.IsNullOrEmpty(tool.ControllerType) ? tool.ControlType : tool.ControllerType
            };
            moduleDependedWidgets.Add(moduleWidgetInfo);
          }
        }
      }
      return moduleDependedWidgets;
    }

    private List<ToolboxesConfig.ModuleWidgetInfo> ModuleDependentWidgets => this.moduleWidgets.Value;

    /// <summary>
    /// Determines whether the specified widget implements the <see cref="T:Telerik.Sitefinity.DynamicModules.IDynamicContentWidget" />.
    /// </summary>
    /// <param name="widgetType">The full name of the widget.</param>
    private bool IsDynamicTypeWidget(string widgetTypeFullName)
    {
      bool flag = false;
      if (widgetTypeFullName.StartsWith("~/"))
        return flag;
      Type c = TypeResolutionService.ResolveType(widgetTypeFullName, false);
      if (c != (Type) null)
        flag = typeof (IDynamicContentWidget).IsAssignableFrom(c);
      return flag;
    }

    internal static string[] GetModuleWidgets(string moduleName) => Config.Get<ToolboxesConfig>().ModuleDependentWidgets.Where<ToolboxesConfig.ModuleWidgetInfo>((Func<ToolboxesConfig.ModuleWidgetInfo, bool>) (w => w.ModuleName == moduleName)).Select<ToolboxesConfig.ModuleWidgetInfo, string>((Func<ToolboxesConfig.ModuleWidgetInfo, string>) (w => w.WidgetType)).ToArray<string>();

    internal bool ValidateWidget(ControlData controlData, PageNode parent = null) => this.ValidateWidget(controlData, out string _, parent);

    internal bool ValidateWidget(ControlData controlData, out string moduleName, PageNode parent = null)
    {
      string widgetTypeFullName = this.GetWidgetTypeFullName(controlData);
      bool flag1 = true;
      if (ToolboxesConfig.ValidateWidgetState != null)
      {
        Tuple<bool, string> tuple = ToolboxesConfig.ValidateWidgetState(widgetTypeFullName);
        if (tuple != null)
        {
          flag1 = tuple.Item1;
          moduleName = tuple.Item2;
          if (!flag1 && moduleName != null)
            return false;
        }
      }
      if (!this.IsDynamicTypeWidget(widgetTypeFullName))
      {
        ToolboxesConfig.ModuleWidgetInfo moduleWidgetInfo = Config.Get<ToolboxesConfig>().ModuleDependentWidgets.Where<ToolboxesConfig.ModuleWidgetInfo>((Func<ToolboxesConfig.ModuleWidgetInfo, bool>) (w => w.WidgetType == widgetTypeFullName)).FirstOrDefault<ToolboxesConfig.ModuleWidgetInfo>();
        if (moduleWidgetInfo != null)
        {
          string empty = string.Empty;
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          string domain = parent == null || multisiteContext == null ? SystemManager.CurrentContext.CurrentSite.LiveUrl : multisiteContext.GetSiteBySiteMapRoot(parent.RootNodeId).LiveUrl;
          moduleName = moduleWidgetInfo.ModuleName;
          IModule module = SystemManager.GetModule(moduleName);
          if (module == null)
            return false;
          if (module is DynamicAppModule)
            return true;
          bool flag2 = LicenseState.CheckIsModuleLicensed(module.ModuleId, domain);
          return flag1 & flag2 && moduleWidgetInfo.Active;
        }
      }
      moduleName = (string) null;
      return true;
    }

    /// <summary>Gets the full name of the widget.</summary>
    /// <param name="controlData">The <see cref="T:Telerik.Sitefinity.Pages.Model.ControlData" /> of the widget.</param>
    /// <returns>The full name of the widget.</returns>
    private string GetWidgetTypeFullName(ControlData controlData)
    {
      ControlProperty controlProperty = controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == "ControllerName")).FirstOrDefault<ControlProperty>();
      return controlProperty == null || controlProperty.Value == null ? ToolboxesConfig.GetShortTypeName(controlData.ObjectType) : controlProperty.Value;
    }

    internal static string GetShortTypeName(string typeName)
    {
      int length = typeName.IndexOf(',');
      return length != -1 ? typeName.Substring(0, length).Trim() : typeName;
    }

    internal static ToolboxesConfig Current => Config.Get<ToolboxesConfig>();

    private class ModuleWidgetInfo
    {
      private string widgetType;
      private bool? active;

      public string ModuleName { get; set; }

      public string WidgetType
      {
        get => this.widgetType;
        set => this.widgetType = ToolboxesConfig.GetShortTypeName(value);
      }

      public bool Active
      {
        get
        {
          if (!this.active.HasValue)
            this.active = new bool?(SystemManager.IsModuleEnabled(this.ModuleName));
          return this.active.Value;
        }
      }
    }
  }
}
