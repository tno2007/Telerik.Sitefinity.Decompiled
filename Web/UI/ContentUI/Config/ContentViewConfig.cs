// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// 
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewConfigDescription", Title = "ContentViewConfig")]
  public class ContentViewConfig : ConfigSection, IContentViewConfig
  {
    /// <summary>Gets a collection of data Content View Controls.</summary>
    [ConfigurationProperty("contentViewControls")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlsDescription", Title = "ContentViewControls")]
    [ConfigurationCollection(typeof (ContentViewControlElement), AddItemName = "contentViewControl")]
    public ConfigElementDictionary<string, ContentViewControlElement> ContentViewControls => (ConfigElementDictionary<string, ContentViewControlElement>) this["contentViewControls"];

    /// <summary>Gets the configured SortingExpression settings.</summary>
    /// <value>The SortExpression settings.</value>
    [ConfigurationProperty("sortingExpressionSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortingExpressionSettingsDescription", Title = "SortingExpressionSettingsTitle")]
    public ConfigElementList<SortingExpressionElement> SortingExpressionSettings => (ConfigElementList<SortingExpressionElement>) this["sortingExpressionSettings"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      this.InitializeSortingExpressionSettings();
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls = this.ContentViewControls;
      if (!contentViewControls.ContainsKey("FrontendPages"))
        contentViewControls.Add(PagesDefinitions.DefineFrontendPagesContentView(this, (ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("BackendPages"))
        contentViewControls.Add(PagesDefinitions.DefineBackendPagesContentView(this, (ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("FrontendPageTemplates"))
        contentViewControls.Add(PageTemplatesDefinitions.DefineFrontendPageTemplatesContentView(this, (ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("BackendPageTemplates"))
        contentViewControls.Add(PageTemplatesDefinitions.DefineBackendPageTemplatesContentView(this, (ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("UserProfileTypesBackend"))
        contentViewControls.Add(UserProfilesDefinitions.DefineProfileTypesBackendContentView((ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("UserProfilesFrontend"))
        contentViewControls.Add(UserProfilesDefinitions.DefineSitefinityProfileContentViews((ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("FrontendSingleProfile"))
        contentViewControls.Add(UserProfilesDefinitions.DefineProfileFrontendContentView((ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("FrontendUsersList"))
        contentViewControls.Add(UserProfilesDefinitions.DefineProfilesFrontendContentView((ConfigElement) contentViewControls));
      if (!contentViewControls.ContainsKey("BackendSingleProfile"))
        contentViewControls.Add(UserProfilesDefinitions.DefineProfileBackendContentView((ConfigElement) contentViewControls));
      if (!this.ContentViewControls.ContainsKey("TaxonomyBackend"))
        this.ContentViewControls.Add(TaxonomyDefinitions.DefineTaxonomiesBackendContentView((ConfigElement) this.ContentViewControls));
      if (!this.ContentViewControls.ContainsKey("FlatTaxonBackend"))
        this.ContentViewControls.Add(FlatTaxonDefinitions.DefineFlatTaxonsBackendContentView((ConfigElement) this.ContentViewControls));
      if (!this.ContentViewControls.ContainsKey("HierarchicalTaxonBackend"))
        this.ContentViewControls.Add(HierarchicalTaxonDefinitions.DefineHierarchicalTaxonsBackendContentView((ConfigElement) this.ContentViewControls));
      if (this.ContentViewControls.ContainsKey(MarkedItemsDefinitions.Name))
        return;
      this.ContentViewControls.Add(MarkedItemsDefinitions.DefineContentView(this.ContentViewControls));
    }

    /// <summary>Initializes the sorting expression settings.</summary>
    private void InitializeSortingExpressionSettings()
    {
      string fullName1 = typeof (PageData).FullName;
      ConfigElementList<SortingExpressionElement> expressionSettings1 = this.SortingExpressionSettings;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element1.ContentType = fullName1;
      element1.SortingExpressionTitle = "ByHierarchy";
      element1.SortingExpression = "showHierarchical";
      expressionSettings1.Add(element1);
      ConfigElementList<SortingExpressionElement> expressionSettings2 = this.SortingExpressionSettings;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element2.ContentType = fullName1;
      element2.SortingExpressionTitle = "NewCreatedFirst";
      element2.SortingExpression = "DateCreated DESC";
      expressionSettings2.Add(element2);
      ConfigElementList<SortingExpressionElement> expressionSettings3 = this.SortingExpressionSettings;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element3.ContentType = fullName1;
      element3.SortingExpressionTitle = "NewModifiedFirst";
      element3.SortingExpression = "LastModified DESC";
      expressionSettings3.Add(element3);
      ConfigElementList<SortingExpressionElement> expressionSettings4 = this.SortingExpressionSettings;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element4.ContentType = fullName1;
      element4.SortingExpressionTitle = "AlphabeticallyAsc";
      element4.SortingExpression = "Title ASC";
      expressionSettings4.Add(element4);
      ConfigElementList<SortingExpressionElement> expressionSettings5 = this.SortingExpressionSettings;
      SortingExpressionElement element5 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element5.ContentType = fullName1;
      element5.SortingExpressionTitle = "AlphabeticallyDesc";
      element5.SortingExpression = "Title DESC";
      expressionSettings5.Add(element5);
      ConfigElementList<SortingExpressionElement> expressionSettings6 = this.SortingExpressionSettings;
      SortingExpressionElement element6 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element6.ContentType = fullName1;
      element6.SortingExpressionTitle = "CustomSorting";
      element6.SortingExpression = "Custom";
      element6.IsCustom = true;
      expressionSettings6.Add(element6);
      string fullName2 = typeof (HierarchicalTaxon).FullName;
      ConfigElementList<SortingExpressionElement> expressionSettings7 = this.SortingExpressionSettings;
      SortingExpressionElement element7 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element7.ContentType = fullName2;
      element7.SortingExpressionTitle = "ByTitleAsc";
      element7.SortingExpression = "Title ASC";
      expressionSettings7.Add(element7);
      ConfigElementList<SortingExpressionElement> expressionSettings8 = this.SortingExpressionSettings;
      SortingExpressionElement element8 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element8.ContentType = fullName2;
      element8.SortingExpressionTitle = "ByTitleDesc";
      element8.SortingExpression = "Title DESC";
      expressionSettings8.Add(element8);
      ConfigElementList<SortingExpressionElement> expressionSettings9 = this.SortingExpressionSettings;
      SortingExpressionElement element9 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element9.ContentType = fullName2;
      element9.SortingExpressionTitle = "NewModifiedFirst";
      element9.SortingExpression = "LastModified DESC";
      expressionSettings9.Add(element9);
      ConfigElementList<SortingExpressionElement> expressionSettings10 = this.SortingExpressionSettings;
      SortingExpressionElement element10 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element10.ContentType = fullName2;
      element10.SortingExpressionTitle = "UrlAsc";
      element10.SortingExpression = "UrlName ASC";
      expressionSettings10.Add(element10);
      ConfigElementList<SortingExpressionElement> expressionSettings11 = this.SortingExpressionSettings;
      SortingExpressionElement element11 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element11.ContentType = fullName2;
      element11.SortingExpressionTitle = "UrlDesc";
      element11.SortingExpression = "UrlName DESC";
      expressionSettings11.Add(element11);
      ConfigElementList<SortingExpressionElement> expressionSettings12 = this.SortingExpressionSettings;
      SortingExpressionElement element12 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element12.ContentType = fullName2;
      element12.SortingExpressionTitle = "CustomSorting";
      element12.SortingExpression = "Custom";
      element12.IsCustom = true;
      expressionSettings12.Add(element12);
      string fullName3 = typeof (FlatTaxon).FullName;
      ConfigElementList<SortingExpressionElement> expressionSettings13 = this.SortingExpressionSettings;
      SortingExpressionElement element13 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element13.ContentType = fullName3;
      element13.SortingExpressionTitle = "ByTitleAsc";
      element13.SortingExpression = "Title ASC";
      expressionSettings13.Add(element13);
      ConfigElementList<SortingExpressionElement> expressionSettings14 = this.SortingExpressionSettings;
      SortingExpressionElement element14 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element14.ContentType = fullName3;
      element14.SortingExpressionTitle = "ByTitleDesc";
      element14.SortingExpression = "Title DESC";
      expressionSettings14.Add(element14);
      ConfigElementList<SortingExpressionElement> expressionSettings15 = this.SortingExpressionSettings;
      SortingExpressionElement element15 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element15.ContentType = fullName3;
      element15.SortingExpressionTitle = "NewModifiedFirst";
      element15.SortingExpression = "LastModified DESC";
      expressionSettings15.Add(element15);
      ConfigElementList<SortingExpressionElement> expressionSettings16 = this.SortingExpressionSettings;
      SortingExpressionElement element16 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element16.ContentType = fullName3;
      element16.SortingExpressionTitle = "UrlAsc";
      element16.SortingExpression = "UrlName ASC";
      expressionSettings16.Add(element16);
      ConfigElementList<SortingExpressionElement> expressionSettings17 = this.SortingExpressionSettings;
      SortingExpressionElement element17 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element17.ContentType = fullName3;
      element17.SortingExpressionTitle = "UrlDesc";
      element17.SortingExpression = "UrlName DESC";
      expressionSettings17.Add(element17);
      ConfigElementList<SortingExpressionElement> expressionSettings18 = this.SortingExpressionSettings;
      SortingExpressionElement element18 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element18.ContentType = fullName3;
      element18.SortingExpressionTitle = "CustomSorting";
      element18.SortingExpression = "Custom";
      element18.IsCustom = true;
      expressionSettings18.Add(element18);
      string fullName4 = typeof (PageTemplate).FullName;
      ConfigElementList<SortingExpressionElement> expressionSettings19 = this.SortingExpressionSettings;
      SortingExpressionElement element19 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element19.ContentType = fullName4;
      element19.SortingExpressionTitle = "NewModifiedFirst";
      element19.SortingExpression = "LastModified DESC";
      expressionSettings19.Add(element19);
      ConfigElementList<SortingExpressionElement> expressionSettings20 = this.SortingExpressionSettings;
      SortingExpressionElement element20 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element20.ContentType = fullName4;
      element20.SortingExpressionTitle = "AlphabeticallyAsc";
      element20.SortingExpression = "Title ASC";
      expressionSettings20.Add(element20);
      ConfigElementList<SortingExpressionElement> expressionSettings21 = this.SortingExpressionSettings;
      SortingExpressionElement element21 = new SortingExpressionElement((ConfigElement) this.SortingExpressionSettings);
      element21.ContentType = fullName4;
      element21.SortingExpressionTitle = "AlphabeticallyDesc";
      element21.SortingExpression = "Title DESC";
      expressionSettings21.Add(element21);
    }
  }
}
