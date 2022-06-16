// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators.DetailsViewGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Web.UI.Backend;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Versioning.Web.UI.Views;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators
{
  /// <summary>
  /// This class generates the insert/edit definitions for the given dynamic module type.
  /// </summary>
  internal class DetailsViewGenerator
  {
    private const string webServiceBaseUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
    private const string fieldIdSuffix = "Control";
    private const string titleFieldCssClass = "sfTitleField";
    private const string separatorFieldCssClass = "sfFormSeparator";
    internal const string ContentLocationFieldName = "ContentLocationInfoField";

    /// <summary>
    /// Generates edit view element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.IDynamicModuleType" /> for which the backend view ought to be
    /// generated.
    /// </param>
    /// <param name="backendContentView">
    /// The parent element of the backend view element.
    /// </param>
    /// <remarks>
    /// The generated element will not be added to the ContentViewControlElement element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> representing the EditDetailsView.
    /// </returns>
    public static DetailFormViewElement GenerateEditDetailsView(
      IDynamicModuleType moduleType,
      ContentViewControlElement backendContentView)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (backendContentView == null)
        throw new ArgumentNullException(nameof (backendContentView));
      if (string.IsNullOrEmpty(moduleType.DisplayName))
        throw new ArgumentException("ModuleType DisplayName cannot be null or empty!");
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      bool flag = ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields);
      MultilingualMode multilingualMode = flag ? MultilingualMode.On : MultilingualMode.Off;
      DetailFormViewElement detailFormViewElement = new DetailFormViewElement((ConfigElement) backendContentView.ViewsConfig);
      detailFormViewElement.Title = string.Format("{0} {1} {2}", (object) DetailsViewGenerator.GetEditDetailViewTitle(), (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName);
      detailFormViewElement.ViewName = ModuleNamesGenerator.GenerateBackendEditViewName(moduleType.DisplayName);
      detailFormViewElement.ViewType = typeof (DynamicContentDetailFormView);
      detailFormViewElement.ShowSections = new bool?(true);
      detailFormViewElement.DisplayMode = FieldDisplayMode.Write;
      detailFormViewElement.ShowTopToolbar = new bool?(true);
      detailFormViewElement.WebServiceBaseUrl = DetailsViewGenerator.GetWebServiceUrl(moduleType);
      detailFormViewElement.IsToRenderTranslationView = new bool?(flag);
      detailFormViewElement.UseWorkflow = new bool?(true);
      detailFormViewElement.MultilingualMode = multilingualMode;
      DetailFormViewElement editDetailsView = detailFormViewElement;
      DetailsViewGenerator.CreateBackendSections(editDetailsView, moduleType, FieldDisplayMode.Write);
      if (flag)
      {
        if (!editDetailsView.Sections.Contains("toolbarSection"))
        {
          ContentViewSectionElement element1 = new ContentViewSectionElement((ConfigElement) editDetailsView.Sections)
          {
            Name = "toolbarSection"
          };
          LanguageListFieldElement listFieldElement = new LanguageListFieldElement((ConfigElement) element1.Fields);
          listFieldElement.ID = "languageListField";
          listFieldElement.FieldType = typeof (LanguageListField);
          listFieldElement.Title = DetailsViewGenerator.GetLanguageListFieldTitle();
          listFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
          listFieldElement.FieldName = "languageListField";
          listFieldElement.DataFieldName = "AvailableLanguages";
          LanguageListFieldElement element2 = listFieldElement;
          element1.Fields.Add((FieldDefinitionElement) element2);
          editDetailsView.Sections.Add(element1);
        }
      }
      else if (editDetailsView.Sections.Contains("toolbarSection"))
        editDetailsView.Sections.Remove("toolbarSection");
      DetailsViewGenerator.CreateWarningsSection(editDetailsView);
      ContentViewSectionElement element3 = new ContentViewSectionElement((ConfigElement) editDetailsView.Sections)
      {
        Name = "SidebarSection",
        CssClass = "sfItemReadOnlyInfo"
      };
      ConfigElementDictionary<string, FieldDefinitionElement> fields1 = element3.Fields;
      DynamicContentWorkflowStatusInfoFieldElement element4 = new DynamicContentWorkflowStatusInfoFieldElement((ConfigElement) element3.Fields);
      element4.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element4.FieldName = "ItemWorkflowStatusInfoField";
      element4.WrapperTag = HtmlTextWriterTag.Li;
      element4.FieldType = typeof (DynamicContentWorkflowStatusInfoField);
      fields1.Add((FieldDefinitionElement) element4);
      ConfigElementDictionary<string, FieldDefinitionElement> fields2 = element3.Fields;
      ContentStatisticsFieldElement element5 = new ContentStatisticsFieldElement((ConfigElement) element3.Fields);
      element5.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element5.FieldName = "ItemStatisticsField";
      element5.WrapperTag = HtmlTextWriterTag.Li;
      element5.FieldType = typeof (ContentStatisticsField);
      fields2.Add((FieldDefinitionElement) element5);
      ContentLocationInfoFieldElement infoFieldElement = new ContentLocationInfoFieldElement((ConfigElement) element3.Fields);
      infoFieldElement.FieldName = "ContentLocationInfoField";
      infoFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      infoFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      ContentLocationInfoFieldElement element6 = infoFieldElement;
      element3.Fields.Add((FieldDefinitionElement) element6);
      ConfigElementDictionary<string, FieldDefinitionElement> fields3 = element3.Fields;
      RelatingDataFieldDefinitionElement element7 = new RelatingDataFieldDefinitionElement((ConfigElement) element3.Fields);
      element7.FieldName = "RelatingDataField";
      element7.WrapperTag = HtmlTextWriterTag.Li;
      element7.FieldType = typeof (RelatingDataField);
      fields3.Add((FieldDefinitionElement) element7);
      editDetailsView.Sections.Add(element3);
      DetailsViewGenerator.CreateBackendFormToolbar(editDetailsView, moduleType, true);
      if (moduleType.IsSelfReferencing)
        editDetailsView.Scripts.Add(new ClientScriptElement((ConfigElement) editDetailsView.Scripts)
        {
          ScriptLocation = "Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsDetailsViewExtensions.js, Telerik.Sitefinity",
          LoadMethodName = "OnDetailViewLoaded"
        });
      return editDetailsView;
    }

    /// <summary>
    /// Generates preview view element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.IDynamicModuleType" /> for which the backend view ought to be
    /// generated.
    /// </param>
    /// <param name="backendContentView">
    /// The parent element of the backend view element.
    /// </param>
    /// <remarks>
    /// The generated element will not be added to the ContentViewControlElement element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> representing the PreviewDetailsView.
    /// </returns>
    public static DetailFormViewElement GeneratePreviewDetailsView(
      IDynamicModuleType moduleType,
      ContentViewControlElement backendContentView)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (backendContentView == null)
        throw new ArgumentNullException(nameof (backendContentView));
      if (string.IsNullOrEmpty(moduleType.DisplayName))
        throw new ArgumentException("ModuleType DisplayName cannot be null or empty!");
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      MultilingualMode multilingualMode = ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields) ? MultilingualMode.On : MultilingualMode.Off;
      DetailFormViewElement detailView = new DetailFormViewElement((ConfigElement) backendContentView.ViewsConfig);
      detailView.Title = string.Format("{0} {1} {2}", (object) DetailsViewGenerator.GetPreviewDetailViewTitle(), (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName);
      detailView.ViewName = ModuleNamesGenerator.GenerateBackendPreviewViewName(moduleType.DisplayName);
      detailView.ViewType = typeof (DynamicContentDetailFormView);
      detailView.ShowSections = new bool?(true);
      detailView.DisplayMode = FieldDisplayMode.Read;
      detailView.ShowTopToolbar = new bool?(false);
      detailView.ShowNavigation = new bool?(false);
      detailView.WebServiceBaseUrl = DetailsViewGenerator.GetWebServiceUrl(moduleType);
      detailView.UseWorkflow = new bool?(false);
      detailView.MultilingualMode = multilingualMode;
      DetailsViewGenerator.CreateBackendSections(detailView, moduleType, FieldDisplayMode.Read);
      return detailView;
    }

    /// <summary>
    /// Generates history version preview view element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.IDynamicModuleType" /> for which the backend view ought to be
    /// generated.
    /// </param>
    /// <param name="ContentViewControlElement">
    /// The parent element of the backend view element.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> representing the DetailFormViewElement.
    /// </returns>
    public static DetailFormViewElement GenerateHistoryVersionDetailsView(
      IDynamicModuleType moduleType,
      ContentViewControlElement backendContentView)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (backendContentView == null)
        throw new ArgumentNullException(nameof (backendContentView));
      if (string.IsNullOrEmpty(moduleType.DisplayName))
        throw new ArgumentException("ModuleType DisplayName cannot be null or empty!");
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      MultilingualMode multilingualMode = ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields) ? MultilingualMode.On : MultilingualMode.Off;
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "ItemVersionOfClientTemplate",
          Res.Get<VersionResources>().ItemVersionOfClientTemplate
        },
        {
          "PreviouslyPublished",
          Res.Get<VersionResources>().PreviouslyPublishedBrackets
        },
        {
          "CannotDeleteLastPublishedVersion",
          Res.Get<VersionResources>().CannotDeleteLastPublishedVersion
        }
      };
      DetailFormViewElement detailFormViewElement = new DetailFormViewElement((ConfigElement) backendContentView.ViewsConfig);
      detailFormViewElement.Title = string.Format("{0} {1} {2}", (object) DetailsViewGenerator.GetPreviewDetailViewTitle(), (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName);
      detailFormViewElement.ViewName = ModuleNamesGenerator.GenerateBackendVersionPreview(moduleType.DisplayName);
      detailFormViewElement.ViewType = typeof (DynamicContentDetailFormView);
      detailFormViewElement.ShowSections = new bool?(true);
      detailFormViewElement.DisplayMode = FieldDisplayMode.Read;
      detailFormViewElement.ShowTopToolbar = new bool?(false);
      detailFormViewElement.ShowNavigation = new bool?(true);
      detailFormViewElement.WebServiceBaseUrl = DetailsViewGenerator.GetWebServiceUrl(moduleType);
      detailFormViewElement.UseWorkflow = new bool?(false);
      detailFormViewElement.MultilingualMode = multilingualMode;
      detailFormViewElement.Localization = dictionary;
      detailFormViewElement.ExternalClientScripts = DefinitionsHelper.GetExtenalClientScripts("Telerik.Sitefinity.Versioning.Web.UI.Scripts.VersionHistoryExtender.js, Telerik.Sitefinity", "OnDetailViewLoaded");
      DetailFormViewElement detailView = detailFormViewElement;
      ContentViewSectionElement element1 = new ContentViewSectionElement((ConfigElement) detailView.Sections)
      {
        Name = "Sidebar",
        DisplayMode = new FieldDisplayMode?(detailView.DisplayMode),
        ResourceClassId = detailView.ResourceClassId,
        CssClass = "sfItemReadOnlyInfo"
      };
      ConfigElementDictionary<string, FieldDefinitionElement> fields = element1.Fields;
      VersionNoteControlDefinitionElement element2 = new VersionNoteControlDefinitionElement((ConfigElement) element1.Fields);
      element2.WrapperTag = HtmlTextWriterTag.Li;
      element2.DisplayMode = new FieldDisplayMode?(detailView.DisplayMode);
      element2.ResourceClassId = detailView.ResourceClassId;
      element2.FieldName = "Comment";
      fields.Add((FieldDefinitionElement) element2);
      detailView.Sections.Add(element1);
      DetailsViewGenerator.CreateBackendSections(detailView, moduleType, FieldDisplayMode.Read);
      DefinitionsHelper.CreateHistoryPreviewToolbar(detailView, typeof (ModuleBuilderResources).Name);
      return detailView;
    }

    /// <summary>
    /// Generates history comparison version preview view element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.IDynamicModuleType" /> for which the backend view ought to be
    /// generated.
    /// </param>
    /// <param name="backendContentView">
    /// The parent element of the backend view element.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Versioning.Web.UI.Config.ComparisonViewElement" /> representing the ComparisonViewElement.
    /// </returns>
    public static ComparisonViewElement DefineBackendVersioningComparisonView(
      IDynamicModuleType moduleType,
      ContentViewControlElement backendContentView)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (backendContentView == null)
        throw new ArgumentNullException(nameof (backendContentView));
      if (string.IsNullOrEmpty(moduleType.DisplayName))
        throw new ArgumentException("ModuleType DisplayName cannot be null or empty!");
      new IndefiniteArticleResolver().ResolveModuleTypeName(moduleType);
      ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields);
      ComparisonViewElement comparisonViewElement1 = new ComparisonViewElement((ConfigElement) backendContentView.ViewsConfig);
      comparisonViewElement1.Title = "VersionComparison";
      comparisonViewElement1.ViewName = ModuleNamesGenerator.GenerateBackendVersionComparerPreview(moduleType.DisplayName);
      comparisonViewElement1.ViewType = typeof (VersionComparisonView);
      comparisonViewElement1.DisplayMode = FieldDisplayMode.Read;
      comparisonViewElement1.UseWorkflow = new bool?(false);
      comparisonViewElement1.ResourceClassId = typeof (ModuleBuilderResources).Name;
      ComparisonViewElement comparisonViewElement2 = comparisonViewElement1;
      foreach (IDynamicModuleField dynamicModuleField in moduleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => (f.SpecialType == FieldSpecialType.None || f.SpecialType == FieldSpecialType.UrlName) && f.FieldStatus != DynamicModuleFieldStatus.Removed)))
      {
        ConfigElementDictionary<string, ComparisonFieldElement> fields = comparisonViewElement2.Fields;
        ComparisonFieldElement element = new ComparisonFieldElement((ConfigElement) comparisonViewElement2.Fields);
        element.FieldName = dynamicModuleField.Name;
        element.Title = dynamicModuleField.Title;
        fields.Add(element);
      }
      return comparisonViewElement2;
    }

    /// <summary>
    /// Generates insert view element for the given dynamic module type and returns it.
    /// </summary>
    /// <param name="moduleType">The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.IDynamicModuleType" /> for which the backend view ought to be
    /// generated.</param>
    /// <param name="backendContentView">The parent element of the backend view element.</param>
    /// <param name="isDuplicate">Determines whether the inserted item is duplication of another item.</param>
    /// <remarks>
    /// The generated element will not be added to the ContentViewControlElement element.
    /// </remarks>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> representing the InsertDetailsView.
    /// </returns>
    public static DetailFormViewElement GenerateInsertDetailsView(
      IDynamicModuleType moduleType,
      ContentViewControlElement backendContentView,
      bool isDuplicate = false)
    {
      if (moduleType == null)
        throw new ArgumentNullException(nameof (moduleType));
      if (backendContentView == null)
        throw new ArgumentNullException(nameof (backendContentView));
      if (string.IsNullOrEmpty(moduleType.DisplayName))
        throw new ArgumentException("ModuleType DisplayName cannot be null or empty!");
      IndefiniteArticleResolver indefiniteArticleResolver = new IndefiniteArticleResolver();
      indefiniteArticleResolver.ResolveModuleTypeName(moduleType);
      MultilingualMode multilingualMode = ModuleInstallerHelper.ContainsLocalizableFields(moduleType.Fields) ? MultilingualMode.On : MultilingualMode.Off;
      string str1;
      string str2;
      if (isDuplicate)
      {
        str1 = DetailsViewGenerator.GetDuplicateDetailViewTitle();
        str2 = ModuleNamesGenerator.GenerateBackendDuplicateViewName(moduleType.DisplayName);
      }
      else
      {
        str1 = DetailsViewGenerator.GetInsertDetailViewTitle();
        str2 = ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName);
      }
      DetailFormViewElement insertDetailsView = new DetailFormViewElement((ConfigElement) backendContentView.ViewsConfig);
      insertDetailsView.Title = string.Format("{0} {1} {2}", (object) str1, (object) indefiniteArticleResolver.Prefix, (object) moduleType.DisplayName);
      insertDetailsView.ViewName = str2;
      insertDetailsView.ViewType = typeof (DynamicContentDetailFormView);
      insertDetailsView.ShowSections = new bool?(true);
      insertDetailsView.DisplayMode = FieldDisplayMode.Write;
      insertDetailsView.ShowTopToolbar = new bool?(true);
      insertDetailsView.WebServiceBaseUrl = DetailsViewGenerator.GetWebServiceUrl(moduleType);
      insertDetailsView.IsToRenderTranslationView = new bool?(false);
      insertDetailsView.UseWorkflow = new bool?(true);
      insertDetailsView.MultilingualMode = multilingualMode;
      DetailsViewGenerator.CreateWarningsSection(insertDetailsView);
      DetailsViewGenerator.CreateBackendSections(insertDetailsView, moduleType, FieldDisplayMode.Write);
      ContentViewSectionElement element1 = new ContentViewSectionElement((ConfigElement) insertDetailsView.Sections)
      {
        Name = "SidebarSection",
        CssClass = "sfItemReadOnlyInfo"
      };
      ConfigElementDictionary<string, FieldDefinitionElement> fields1 = element1.Fields;
      ContentStatisticsFieldElement element2 = new ContentStatisticsFieldElement((ConfigElement) element1.Fields);
      element2.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element2.FieldName = "ItemStatisticsField";
      element2.WrapperTag = HtmlTextWriterTag.Li;
      element2.FieldType = typeof (ContentStatisticsField);
      fields1.Add((FieldDefinitionElement) element2);
      ConfigElementDictionary<string, FieldDefinitionElement> fields2 = element1.Fields;
      DynamicContentWorkflowStatusInfoFieldElement element3 = new DynamicContentWorkflowStatusInfoFieldElement((ConfigElement) element1.Fields);
      element3.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      element3.FieldName = "ItemWorkflowStatusInfoField";
      element3.WrapperTag = HtmlTextWriterTag.Li;
      element3.FieldType = typeof (DynamicContentWorkflowStatusInfoField);
      fields2.Add((FieldDefinitionElement) element3);
      ContentLocationInfoFieldElement infoFieldElement = new ContentLocationInfoFieldElement((ConfigElement) element1.Fields);
      infoFieldElement.FieldName = "ContentLocationInfoField";
      infoFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      infoFieldElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      ContentLocationInfoFieldElement element4 = infoFieldElement;
      element1.Fields.Add((FieldDefinitionElement) element4);
      insertDetailsView.Sections.Add(element1);
      DetailsViewGenerator.CreateBackendFormToolbar(insertDetailsView, moduleType, true);
      return insertDetailsView;
    }

    internal static void CreateWarningsSection(DetailFormViewElement view)
    {
      ContentViewSectionElement element1;
      if (!view.Sections.TryGetValue("HeaderSection", out element1))
      {
        element1 = new ContentViewSectionElement((ConfigElement) view.Sections)
        {
          Name = "HeaderSection",
          CssClass = "sfItemHeaderSection"
        };
        view.Sections.Add(element1);
      }
      if (element1.Fields.Values.Any<FieldDefinitionElement>((Func<FieldDefinitionElement, bool>) (f => f.FieldType == typeof (WarningField))))
        return;
      WarningFieldElement warningFieldElement = new WarningFieldElement((ConfigElement) element1.Fields);
      warningFieldElement.ID = "warningsField";
      warningFieldElement.WrapperTag = HtmlTextWriterTag.Li;
      warningFieldElement.FieldType = typeof (WarningField);
      warningFieldElement.FieldName = "warningsField";
      WarningFieldElement element2 = warningFieldElement;
      element1.Fields.Add((FieldDefinitionElement) element2);
    }

    private static void CreateBackendFormToolbar(
      DetailFormViewElement detailView,
      IDynamicModuleType moduleType,
      bool isCreateMode)
    {
      WidgetBarSectionElement element1 = new WidgetBarSectionElement((ConfigElement) detailView.Toolbar.Sections)
      {
        Name = "BackendForm",
        WrapperTagKey = HtmlTextWriterTag.Div,
        CssClass = "sfWorkflowMenuWrp"
      };
      ConfigElementList<WidgetElement> items1 = element1.Items;
      CommandWidgetElement element2 = new CommandWidgetElement((ConfigElement) element1.Items);
      element2.Name = "SaveChangesWidgetElement";
      element2.ButtonType = CommandButtonType.Save;
      element2.CommandName = "save";
      element2.Text = isCreateMode ? string.Format("Create {0}", (object) moduleType.DisplayName) : "Save Changes";
      element2.WrapperTagKey = HtmlTextWriterTag.Span;
      element2.WidgetType = typeof (CommandWidget);
      items1.Add((WidgetElement) element2);
      ConfigElementList<WidgetElement> items2 = element1.Items;
      CommandWidgetElement element3 = new CommandWidgetElement((ConfigElement) element1.Items);
      element3.Name = "CancelWidgetElement";
      element3.ButtonType = CommandButtonType.Cancel;
      element3.CommandName = "cancel";
      element3.Text = "Back to Press Releases";
      element3.WrapperTagKey = HtmlTextWriterTag.Span;
      element3.WidgetType = typeof (CommandWidget);
      items2.Add((WidgetElement) element3);
      detailView.Toolbar.Sections.Add(element1);
    }

    private static void CreateBackendSections(
      DetailFormViewElement detailView,
      IDynamicModuleType moduleType,
      FieldDisplayMode displayMode)
    {
      if (moduleType.Fields == null)
        throw new ArgumentNullException("moduleType.Fields!");
      if (moduleType.Fields.Count<IDynamicModuleField>() == 0)
        throw new ArgumentException("moduleType.Fields has no fields!");
      int num = 0;
      foreach (IFieldsBackendSection section1 in moduleType.Sections)
      {
        IFieldsBackendSection section = section1;
        IOrderedEnumerable<IDynamicModuleField> source1 = moduleType.Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => f.ParentSectionId == section.Id)).OrderBy<IDynamicModuleField, int>((Func<IDynamicModuleField, int>) (f => f.Ordinal));
        if (source1.Count<IDynamicModuleField>() != 0)
        {
          ContentViewSectionElement viewSectionElement;
          if (detailView.Sections.ContainsKey(section.Name))
          {
            viewSectionElement = detailView.Sections[section.Name];
          }
          else
          {
            viewSectionElement = new ContentViewSectionElement((ConfigElement) detailView.Sections)
            {
              Name = section.Name
            };
            detailView.Sections.Add(viewSectionElement);
            ++num;
          }
          if (num == 1)
            viewSectionElement.CssClass = "sfFirstForm";
          if (section.IsExpandable)
          {
            viewSectionElement.Title = HttpUtility.HtmlEncode(HtmlStripper.StripHtmlTags(section.Title));
            viewSectionElement.CssClass = "sfExpandableForm";
            viewSectionElement.ExpandableDefinitionConfig.Expanded = new bool?(section.IsExpandedByDefault);
          }
          FieldSpecialType[] source2 = new FieldSpecialType[6]
          {
            FieldSpecialType.Author,
            FieldSpecialType.PublicationDate,
            FieldSpecialType.Actions,
            FieldSpecialType.Translations,
            FieldSpecialType.LastModified,
            FieldSpecialType.DateCreated
          };
          foreach (IDynamicModuleField dynamicModuleField in (IEnumerable<IDynamicModuleField>) source1)
          {
            IDynamicModuleField sectionField = dynamicModuleField;
            if (!sectionField.IsHiddenField && !((IEnumerable<FieldSpecialType>) source2).Any<FieldSpecialType>((Func<FieldSpecialType, bool>) (x => x == sectionField.SpecialType)) && (sectionField.FieldStatus != DynamicModuleFieldStatus.Removed || sectionField.SpecialType != FieldSpecialType.None))
            {
              if (sectionField.SpecialType == FieldSpecialType.UrlName)
              {
                MirrorTextFieldElement textFieldElement = new MirrorTextFieldElement((ConfigElement) viewSectionElement.Fields);
                textFieldElement.ID = "UrlNameFieldControl";
                textFieldElement.MirroredControlId = string.Format("{0}{1}", (object) moduleType.MainShortTextFieldName, (object) "Control");
                textFieldElement.DataFieldName = "UrlName.PersistedValue";
                textFieldElement.DisplayMode = new FieldDisplayMode?(displayMode);
                textFieldElement.Title = "UrlNameTitle";
                textFieldElement.Example = "UrlNameExample";
                textFieldElement.CssClass = "sfFormSeparator";
                textFieldElement.ResourceClassId = typeof (ModuleBuilderResources).Name;
                textFieldElement.WrapperTag = HtmlTextWriterTag.Li;
                textFieldElement.FieldType = typeof (ContentUrlField);
                textFieldElement.propertyResolver = (PropertyResolverBase) new MirrorTextFieldPropertyResolver();
                MirrorTextFieldElement element = textFieldElement;
                element.ValidatorConfig.MessageCssClass = "sfError";
                IValidatorDefinition validation = element.Validation;
                validation.Required = new bool?(true);
                validation.RequiredViolationMessage = DetailsViewGenerator.GetUrlNameCannotBeEmpty();
                validation.RegularExpression = DefinitionsHelper.UrlRegularExpressionFilterForContentValidator;
                validation.RegularExpressionViolationMessage = DetailsViewGenerator.GetUrlNameInvalidSymbols();
                viewSectionElement.Fields.Add((FieldDefinitionElement) element);
              }
              else if (sectionField.SpecialType == FieldSpecialType.ParentId)
              {
                ParentSelectorFieldElement selectorFieldElement = new ParentSelectorFieldElement((ConfigElement) viewSectionElement.Fields);
                DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) selectorFieldElement, sectionField, displayMode);
                selectorFieldElement.WebServiceUrl = "~/Sitefinity/Services/DynamicModules/Data.svc/";
                IDynamicModuleType parentType = moduleType.ParentType;
                if (parentType != null)
                {
                  selectorFieldElement.ItemsType = parentType.GetFullTypeName();
                  selectorFieldElement.MainFieldName = parentType.MainShortTextFieldName;
                  selectorFieldElement.Title = PluralsResolver.Instance.ToPlural(parentType.DisplayName);
                }
                viewSectionElement.Fields.Add((FieldDefinitionElement) selectorFieldElement);
              }
              else if (sectionField.SpecialType == FieldSpecialType.IncludeInSitemap)
              {
                FieldControlDefinitionElement yesNoField = DetailsViewGenerator.CreateYesNoField(sectionField, viewSectionElement, displayMode);
                viewSectionElement.Fields.Add((FieldDefinitionElement) yesNoField);
              }
              else
              {
                FieldControlDefinitionElement field = DetailsViewGenerator.CreateField(sectionField, viewSectionElement, displayMode);
                viewSectionElement.Fields.Add((FieldDefinitionElement) field);
              }
            }
          }
        }
      }
    }

    private static string GetInsertDetailViewTitle() => Res.Get<ModuleBuilderResources>("InsertDetailViewTitle");

    private static string GetPreviewDetailViewTitle() => Res.Get<ModuleBuilderResources>("PreviewDetailViewTitle");

    private static string GetEditDetailViewTitle() => Res.Get<ModuleBuilderResources>("EditDetailViewTitle");

    private static string GetDuplicateDetailViewTitle() => Res.Get<ModuleBuilderResources>("DuplicateDetailsViewTitle");

    private static string GetUrlNameCannotBeEmpty() => Res.Get<ModuleBuilderResources>("UrlNameCannotBeEmpty");

    private static string GetUrlNameInvalidSymbols() => Res.Get<ModuleBuilderResources>("UrlNameInvalidSymbols");

    private static string GetLanguageListFieldTitle() => Res.Get<LocalizationResources>("OtherTranslationsColon");

    private static FieldControlDefinitionElement CreateField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      switch (field.FieldType)
      {
        case FieldType.Unknown:
          return DetailsViewGenerator.CreateUnknownField(field, mainSection, displayMode);
        case FieldType.ShortText:
          return DetailsViewGenerator.CreateShortTextField(field, mainSection, displayMode);
        case FieldType.LongText:
          return DetailsViewGenerator.CreateLongTextField(field, mainSection, displayMode);
        case FieldType.MultipleChoice:
        case FieldType.Choices:
          return DetailsViewGenerator.CreateMultipleChoiceField(field, mainSection, displayMode);
        case FieldType.YesNo:
          return DetailsViewGenerator.CreateYesNoField(field, mainSection, displayMode);
        case FieldType.DateTime:
          return DetailsViewGenerator.CreateDateField(field, mainSection, displayMode);
        case FieldType.Number:
          return DetailsViewGenerator.CreateNumberField(field, mainSection, displayMode);
        case FieldType.Classification:
          return DetailsViewGenerator.CreateClassificationField(field, mainSection, displayMode);
        case FieldType.Media:
          return DetailsViewGenerator.CreateMediaElement(field, mainSection, displayMode);
        case FieldType.Guid:
          return DetailsViewGenerator.CreateGuidField(field, mainSection, displayMode);
        case FieldType.GuidArray:
          return DetailsViewGenerator.CreateGuidField(field, mainSection, displayMode);
        case FieldType.Address:
          return DetailsViewGenerator.CreateAddressField(field, mainSection, displayMode);
        case FieldType.RelatedMedia:
          return DetailsViewGenerator.CreateRelatedMediaElement(field, mainSection, displayMode);
        case FieldType.RelatedData:
          return DetailsViewGenerator.CreateRelatedDataElement(field, mainSection, displayMode);
        default:
          throw new NotSupportedException("The dynamic field cannot be resolved.");
      }
    }

    private static FieldControlDefinitionElement CreateRelatedDataElement(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      RelatedDataFieldDefinitionElement relatedDataElement = new RelatedDataFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) relatedDataElement, field, displayMode);
      DetailsViewGenerator.SetRelatedDataFieldProperties(field, relatedDataElement);
      return (FieldControlDefinitionElement) relatedDataElement;
    }

    private static FieldControlDefinitionElement CreateRelatedMediaElement(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      RelatedMediaFieldDefinitionElement relatedMediaElement = new RelatedMediaFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) relatedMediaElement, field, displayMode);
      DetailsViewGenerator.SetRelatedMediaFieldProperties(field, relatedMediaElement);
      return (FieldControlDefinitionElement) relatedMediaElement;
    }

    private static FieldControlDefinitionElement CreateMediaElement(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      AssetsFieldDefinitionElement mediaElement = new AssetsFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) mediaElement, field, displayMode);
      DetailsViewGenerator.SetCommonMediaFieldProperties(field, mediaElement);
      return (FieldControlDefinitionElement) mediaElement;
    }

    private static void SetRelatedMediaFieldProperties(
      IDynamicModuleField field,
      RelatedMediaFieldDefinitionElement mediaElement)
    {
      string lower = field.MediaType.ToLower();
      if (!(lower == "image"))
      {
        if (!(lower == "video"))
        {
          if (!(lower == "file"))
            throw new NotSupportedException("media format not supported");
          if (field.AllowMultipleFiles)
            mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.MultipleDocuments);
          else
            mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.SingleDocument);
        }
        else if (field.AllowMultipleVideos)
          mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.MultipleVideos);
        else
          mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.SingleVideo);
      }
      else if (field.AllowMultipleImages)
        mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.MultipleImages);
      else
        mediaElement.WorkMode = new AssetsWorkMode?(AssetsWorkMode.SingleImage);
      mediaElement.RelatedDataProvider = field.RelatedDataProvider;
      mediaElement.AllowedExtensions = field.FileExtensions;
      mediaElement.MaxFileSize = field.FileMaxSize * 1048576;
      if (string.IsNullOrEmpty(field.FrontendWidgetTypeName))
        return;
      if (field.FrontendWidgetTypeName.StartsWith("~/") || "inline".Equals(field.FrontendWidgetTypeName))
      {
        mediaElement.FrontendWidgetVirtualPath = field.FrontendWidgetTypeName;
        mediaElement.FrontendWidgetType = (Type) null;
      }
      else if (!string.IsNullOrEmpty(field.FrontendWidgetTypeName))
      {
        ConfigProperty configProperty;
        mediaElement.Properties.TryGetValue("frontendWidgetType", out configProperty);
        if (configProperty != null)
          mediaElement[configProperty] = (object) new LazyValue(field.FrontendWidgetTypeName, configProperty);
        else
          mediaElement.FrontendWidgetType = TypeResolutionService.ResolveType(field.FrontendWidgetTypeName, false);
      }
      mediaElement.FrontendWidgetLabel = field.FrontendWidgetLabel;
    }

    private static void SetRelatedDataFieldProperties(
      IDynamicModuleField field,
      RelatedDataFieldDefinitionElement relatedDataElement)
    {
      if (string.IsNullOrEmpty(field.FrontendWidgetTypeName))
        return;
      if (field.FrontendWidgetTypeName.StartsWith("~/") || "inline".Equals(field.FrontendWidgetTypeName))
      {
        relatedDataElement.FrontendWidgetVirtualPath = field.FrontendWidgetTypeName;
        relatedDataElement.FrontendWidgetType = (Type) null;
      }
      else if (!string.IsNullOrEmpty(field.FrontendWidgetTypeName))
      {
        ConfigProperty configProperty;
        relatedDataElement.Properties.TryGetValue("frontendWidgetType", out configProperty);
        if (configProperty != null)
          relatedDataElement[configProperty] = (object) new LazyValue(field.FrontendWidgetTypeName, configProperty);
        else
          relatedDataElement.FrontendWidgetType = TypeResolutionService.ResolveType(field.FrontendWidgetTypeName, false);
      }
      relatedDataElement.FrontendWidgetLabel = field.FrontendWidgetLabel;
      relatedDataElement.RelatedDataType = field.RelatedDataType;
      relatedDataElement.RelatedDataProvider = field.RelatedDataProvider;
      relatedDataElement.AllowMultipleSelection = field.CanSelectMultipleItems;
    }

    private static void SetCommonMediaFieldProperties(
      IDynamicModuleField field,
      AssetsFieldDefinitionElement mediaElement)
    {
      string lower = field.MediaType.ToLower();
      if (!(lower == "image"))
      {
        if (!(lower == "video"))
        {
          if (!(lower == "file"))
            throw new NotSupportedException("media format not supported");
          mediaElement.WorkMode = !field.AllowMultipleFiles ? new AssetsWorkMode?(AssetsWorkMode.SingleDocument) : new AssetsWorkMode?(AssetsWorkMode.MultipleDocuments);
          mediaElement.AllowedExtensions = field.FileExtensions;
          mediaElement.MaxFileSize = field.FileMaxSize * 1048576;
        }
        else
        {
          mediaElement.WorkMode = !field.AllowMultipleVideos ? new AssetsWorkMode?(AssetsWorkMode.SingleVideo) : new AssetsWorkMode?(AssetsWorkMode.MultipleVideos);
          mediaElement.AllowedExtensions = field.VideoExtensions;
          mediaElement.MaxFileSize = field.VideoMaxSize * 1048576;
        }
      }
      else
      {
        mediaElement.WorkMode = !field.AllowMultipleImages ? new AssetsWorkMode?(AssetsWorkMode.SingleImage) : new AssetsWorkMode?(AssetsWorkMode.MultipleImages);
        mediaElement.AllowedExtensions = field.ImageExtensions;
        mediaElement.MaxFileSize = field.ImageMaxSize * 1048576;
      }
    }

    private static FieldControlDefinitionElement CreateGuidField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      FieldControlDefinitionElement definitionElement = DetailsViewGenerator.CreateDefinitionElement(field, (ConfigElement) mainSection.Fields, typeof (TextFieldDefinitionElement));
      DetailsViewGenerator.SetCommonProperties(definitionElement, field, displayMode);
      if (!string.IsNullOrEmpty(field.RegularExpression))
      {
        definitionElement.ValidatorConfig.RegularExpression = field.RegularExpression;
        definitionElement.ValidatorConfig.RegularExpressionViolationMessage = "The value is not valid";
      }
      definitionElement.ValidatorConfig.MinLength = field.MinLength;
      definitionElement.ValidatorConfig.MaxLength = field.MaxLength;
      return definitionElement;
    }

    private static FieldControlDefinitionElement CreateShortTextField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      TextFieldDefinitionElement fieldElement = new TextFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      if (field.IsLocalizable)
        fieldElement.DataFieldName = string.Format("{0}.PersistedValue", (object) field.Name);
      else
        fieldElement.DataFieldName = field.Name;
      if (!string.IsNullOrEmpty(field.RegularExpression))
      {
        fieldElement.ValidatorConfig.RegularExpression = field.RegularExpression;
        fieldElement.ValidatorConfig.RegularExpressionViolationMessage = "The value is not valid";
      }
      fieldElement.ValidatorConfig.MinLength = field.MinLength;
      fieldElement.ValidatorConfig.MaxLength = field.MaxLength;
      if (field.RecommendedCharactersCount.HasValue)
        fieldElement.RecommendedCharactersCount = field.RecommendedCharactersCount.Value;
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateAddressField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      AddressFieldDefinitionElement fieldElement = new AddressFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      switch (field.AddressFieldMode)
      {
        case AddressFieldMode.FormOnly:
          fieldElement.WorkMode = AddressWorkMode.FormOnly;
          break;
        case AddressFieldMode.MapOnly:
          fieldElement.WorkMode = AddressWorkMode.MapOnly;
          break;
        case AddressFieldMode.Hybrid:
          fieldElement.WorkMode = AddressWorkMode.Hybrid;
          break;
      }
      fieldElement.IsRequired = field.IsRequired;
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateLongTextField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      if (!string.IsNullOrEmpty(field.WidgetTypeName) && field.WidgetTypeName.EndsWith("TextField"))
      {
        TextFieldDefinitionElement fieldElement = new TextFieldDefinitionElement((ConfigElement) mainSection.Fields);
        DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
        if (field.IsLocalizable)
          fieldElement.DataFieldName = string.Format("{0}.PersistedValue", (object) field.Name);
        else
          fieldElement.DataFieldName = field.Name;
        fieldElement.Rows = 5;
        fieldElement.ValidatorConfig.RegularExpression = field.RegularExpression;
        fieldElement.ValidatorConfig.RegularExpressionViolationMessage = "The value is not valid";
        fieldElement.ValidatorConfig.MinLength = field.MinLength;
        fieldElement.ValidatorConfig.MaxLength = field.MaxLength;
        int? recommendedCharactersCount = field.RecommendedCharactersCount;
        if (recommendedCharactersCount.HasValue)
        {
          TextFieldDefinitionElement definitionElement = fieldElement;
          recommendedCharactersCount = field.RecommendedCharactersCount;
          int num = recommendedCharactersCount.Value;
          definitionElement.RecommendedCharactersCount = num;
        }
        return (FieldControlDefinitionElement) fieldElement;
      }
      HtmlFieldElement fieldElement1 = new HtmlFieldElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement1, field, displayMode);
      if (field.IsLocalizable)
        fieldElement1.DataFieldName = string.Format("{0}.PersistedValue", (object) field.Name);
      else
        fieldElement1.DataFieldName = field.Name;
      fieldElement1.ValidatorConfig.RegularExpression = (string) null;
      fieldElement1.ValidatorConfig.RegularExpressionViolationMessage = "";
      return (FieldControlDefinitionElement) fieldElement1;
    }

    private static ITaxonomy GetTaxonomy(Guid classificationId) => TaxonomyManager.GetManager().GetTaxonomy(classificationId);

    private static FieldControlDefinitionElement CreateClassificationField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      ITaxonomy taxonomy = DetailsViewGenerator.GetTaxonomy(field.ClassificationId);
      if (typeof (FlatTaxonomy).IsAssignableFrom(taxonomy.GetType()))
      {
        FlatTaxonFieldDefinitionElement fieldElement = new FlatTaxonFieldDefinitionElement((ConfigElement) mainSection.Fields);
        DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
        fieldElement.TaxonomyId = field.ClassificationId;
        fieldElement.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
        fieldElement.FieldType = typeof (FlatTaxonField);
        fieldElement.AllowMultipleSelection = field.CanSelectMultipleItems;
        fieldElement.AllowCreating = field.CanCreateItemsWhileSelecting;
        return (FieldControlDefinitionElement) fieldElement;
      }
      if (!typeof (HierarchicalTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        throw new NotImplementedException();
      HierarchicalTaxonFieldDefinitionElement fieldElement1 = new HierarchicalTaxonFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement1, field, displayMode);
      fieldElement1.TaxonomyId = field.ClassificationId;
      fieldElement1.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
      fieldElement1.FieldType = typeof (HierarchicalTaxonField);
      fieldElement1.AllowMultipleSelection = field.CanSelectMultipleItems;
      fieldElement1.AllowCreating = field.CanCreateItemsWhileSelecting;
      return (FieldControlDefinitionElement) fieldElement1;
    }

    private static FieldControlDefinitionElement CreateDateField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      DateFieldElement fieldElement = new DateFieldElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      CultureInfo invariantCulture = CultureInfo.InvariantCulture;
      DateTime result1;
      if (!string.IsNullOrEmpty(field.MinNumberRange) && DateTime.TryParseExact(field.MinNumberRange, "d/M/yyyy", (IFormatProvider) invariantCulture, DateTimeStyles.None, out result1))
      {
        fieldElement.ValidatorConfig.MinValue = (object) result1;
        fieldElement.ValidatorConfig.MinValueViolationMessage = "The date should be after " + fieldElement.ValidatorConfig.MinValue;
      }
      DateTime result2;
      if (!string.IsNullOrEmpty(field.MaxNumberRange) && DateTime.TryParseExact(field.MaxNumberRange, "d/M/yyyy", (IFormatProvider) invariantCulture, DateTimeStyles.None, out result2))
      {
        fieldElement.ValidatorConfig.MaxValue = (object) result2;
        fieldElement.ValidatorConfig.MaxValueViolationMessage = "The date should be before " + fieldElement.ValidatorConfig.MaxValue;
      }
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateMultipleChoiceField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      ChoiceFieldElement fieldElement = new ChoiceFieldElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      if (field.FieldType == FieldType.MultipleChoice)
      {
        fieldElement.ReturnValuesAlwaysInArray = true;
        string str1 = field.Choices.Trim();
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in str1.Split(chArray))
          fieldElement.ChoicesConfig.Add(new ChoiceElement((ConfigElement) fieldElement.ChoicesConfig)
          {
            Text = str2.Trim(),
            Value = str2.Trim(),
            Selected = false,
            Enabled = true
          });
      }
      else if (field.FieldType == FieldType.Choices)
      {
        fieldElement.ReturnValuesAlwaysInArray = field.CanSelectMultipleItems;
        foreach (XElement element in XDocument.Parse(field.Choices.Trim()).Root.Elements((XName) "choice"))
        {
          string str3 = element.Attribute((XName) "value").Value;
          string str4 = element.Attribute((XName) "text").Value;
          fieldElement.ChoicesConfig.Add(new ChoiceElement((ConfigElement) fieldElement.ChoicesConfig)
          {
            Text = str4.Trim(),
            Value = str3.Trim(),
            Selected = false,
            Enabled = true
          });
        }
      }
      if (field.ChoiceRenderType != null)
      {
        string lower = field.ChoiceRenderType.ToLower();
        if (!(lower == "checkbox"))
        {
          if (!(lower == "radiobutton"))
          {
            if (!(lower == "dropdownlist") && !(lower == "custom"))
              throw new NotSupportedException("Not supported Render Type");
            fieldElement.RenderChoiceAs = RenderChoicesAs.DropDown;
          }
          else
            fieldElement.RenderChoiceAs = RenderChoicesAs.RadioButtons;
        }
        else
          fieldElement.RenderChoiceAs = RenderChoicesAs.CheckBoxes;
      }
      else
      {
        field.ChoiceRenderType = "dropdownlist";
        fieldElement.RenderChoiceAs = RenderChoicesAs.DropDown;
      }
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateNumberField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      TextFieldDefinitionElement fieldElement = new TextFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      string empty = string.Empty;
      string str = field.DecimalPlacesCount != 0 ? (!field.AllowNulls ? "^((\\-(?!0$))?(0|([1-9]\\d*))(((\\.|\\,)\\d{1," + (object) field.DecimalPlacesCount + "}){1})?)$" : "^((\\-(?!0$))?(0|([1-9]\\d*))(((\\.|\\,)\\d{1," + (object) field.DecimalPlacesCount + "}){1})?)?$") : (!field.AllowNulls ? "^(0|(-)?[1-9]{1}\\d*)$" : "^(0|(-)?[1-9]{1}\\d*)?$");
      fieldElement.ValidatorConfig.RegularExpression = str;
      fieldElement.ValidatorConfig.RegularExpressionViolationMessage = "Invalid number";
      fieldElement.Unit = field.NumberUnit;
      fieldElement.AllowNulls = field.AllowNulls;
      fieldElement.CssClass = "sfNumberField";
      Decimal result1;
      if (!string.IsNullOrEmpty(field.MinNumberRange) && Decimal.TryParse(field.MinNumberRange, out result1))
      {
        fieldElement.ValidatorConfig.MinValue = (object) result1;
        fieldElement.ValidatorConfig.MinValueViolationMessage = "The number should be bigger than or equal to " + fieldElement.ValidatorConfig.MinValue;
      }
      Decimal result2;
      if (!string.IsNullOrEmpty(field.MaxNumberRange) && Decimal.TryParse(field.MaxNumberRange, out result2))
      {
        fieldElement.ValidatorConfig.MaxValue = (object) result2;
        fieldElement.ValidatorConfig.MaxValueViolationMessage = "The number should be less than or equal to " + fieldElement.ValidatorConfig.MaxValue;
      }
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateUnknownField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      TextFieldDefinitionElement fieldElement = new TextFieldDefinitionElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static FieldControlDefinitionElement CreateYesNoField(
      IDynamicModuleField field,
      ContentViewSectionElement mainSection,
      FieldDisplayMode displayMode)
    {
      ChoiceFieldElement fieldElement = new ChoiceFieldElement((ConfigElement) mainSection.Fields);
      DetailsViewGenerator.SetCommonProperties((FieldControlDefinitionElement) fieldElement, field, displayMode);
      fieldElement.ChoicesConfig.Add(new ChoiceElement((ConfigElement) fieldElement.ChoicesConfig)
      {
        Text = field.Title,
        Value = field.Title,
        Selected = field.CheckedByDefault
      });
      fieldElement.RenderChoiceAs = RenderChoicesAs.SingleCheckBox;
      return (FieldControlDefinitionElement) fieldElement;
    }

    private static void SetCommonProperties(
      FieldControlDefinitionElement fieldElement,
      IDynamicModuleField field,
      FieldDisplayMode displayMode)
    {
      fieldElement.ID = string.Format("{0}{1}", (object) field.Name, (object) "Control");
      fieldElement.DataFieldName = displayMode == FieldDisplayMode.Write ? field.Name : field.Name;
      fieldElement.DisplayMode = new FieldDisplayMode?(displayMode);
      fieldElement.Title = field.Title;
      fieldElement.Description = field.InstructionalText;
      fieldElement.CssClass = "sfFormSeparator";
      fieldElement.WrapperTag = HtmlTextWriterTag.Li;
      fieldElement.ValidatorConfig.MessageCssClass = "sfError";
      if (fieldElement is DateFieldElement)
        ((DateFieldElement) fieldElement).IsLocalizable = field.IsLocalizable;
      if (fieldElement is TextFieldDefinitionElement)
        ((TextFieldDefinitionElement) fieldElement).IsLocalizable = field.IsLocalizable;
      if (field.IsRequired)
      {
        fieldElement.ValidatorConfig.Required = new bool?(true);
        fieldElement.ValidatorConfig.RequiredViolationMessage = "This field is required!";
        fieldElement.ValidatorConfig.MinLength = field.MinLength;
        fieldElement.ValidatorConfig.MaxLength = field.MaxLength;
      }
      else
      {
        fieldElement.ValidatorConfig.RegularExpression = "^(.{" + (object) field.MinLength + ",})?$";
        fieldElement.ValidatorConfig.MaxLength = field.MaxLength;
      }
      fieldElement.ValidatorConfig.MinLengthViolationMessage = "The input is too short";
      fieldElement.ValidatorConfig.MaxLengthViolationMessage = "The input is too long";
      if (string.IsNullOrEmpty(field.WidgetTypeName))
        return;
      if (field.WidgetTypeName.StartsWith("~/"))
      {
        fieldElement.FieldVirtualPath = field.WidgetTypeName;
        fieldElement.FieldType = (Type) null;
      }
      else
      {
        if (string.IsNullOrEmpty(field.WidgetTypeName))
          return;
        ConfigProperty configProperty;
        fieldElement.Properties.TryGetValue("fieldType", out configProperty);
        if (configProperty != null)
          fieldElement[configProperty] = (object) new LazyValue(field.WidgetTypeName, configProperty);
        else
          fieldElement.FieldType = TypeResolutionService.ResolveType(field.WidgetTypeName, false);
      }
    }

    private static string GetWebServiceUrl(IDynamicModuleType moduleType) => "~/Sitefinity/Services/DynamicModules/Data.svc/" + "?itemType=" + moduleType.GetFullTypeName();

    private static FieldControlDefinitionElement CreateDefinitionElement(
      IDynamicModuleField field,
      ConfigElement parentConfig,
      Type defaultDefinitionElementType)
    {
      FieldControlDefinitionElement definitionElement = (FieldControlDefinitionElement) null;
      if (!string.IsNullOrEmpty(field.WidgetTypeName) && !field.WidgetTypeName.StartsWith("~/"))
      {
        Type fieldControlType = TypeResolutionService.ResolveType(field.WidgetTypeName, false);
        if (fieldControlType != (Type) null)
        {
          Type definitionElementType = DefinitionBuilder.GetDefinitionElementType(fieldControlType, defaultDefinitionElementType);
          if (typeof (FieldControlDefinitionElement).IsAssignableFrom(definitionElementType))
            definitionElement = Activator.CreateInstance(definitionElementType, (object) parentConfig) as FieldControlDefinitionElement;
        }
      }
      if (definitionElement == null)
        definitionElement = Activator.CreateInstance(defaultDefinitionElementType, (object) parentConfig) as FieldControlDefinitionElement;
      return definitionElement;
    }
  }
}
