// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.WidgetTemplateInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.ModuleEditor.WidgetTemplates;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  /// <summary>
  /// This class provides logic for installing default widget templates.
  /// </summary>
  internal class WidgetTemplateInstaller
  {
    private static IList<string> excludedFieldNames = (IList<string>) new List<string>()
    {
      "MetaTitle",
      "MetaDescription",
      "OpenGraphTitle",
      "OpenGraphDescription",
      "OpenGraphImage",
      "OpenGraphVideo"
    };
    private const string mainShortTextFieldOnlyMasterTemplate = "<%@ Control Language=\"C#\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.ContentUI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.Comments\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.Fields\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register Assembly=\"Telerik.Sitefinity\" Namespace=\"Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend\" TagPrefix=\"sf\" %>\r\n<%@ Register TagPrefix=\"telerik\" Namespace=\"Telerik.Web.UI\" Assembly=\"Telerik.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Modules.Comments\" %>\r\n\r\n<sf:SitefinityLabel id=\"title\" runat=\"server\" WrapperTagName=\"div\" HideIfNoText=\"true\" HideIfNoTextMode=\"Server\" CssClass=\"sfitemFieldLbl\" />\r\n<telerik:RadListView ID=\"dynamicContentListView\" ItemPlaceholderID=\"ItemsContainer\" runat=\"server\" EnableEmbeddedSkins=\"false\" EnableEmbeddedBaseStylesheet=\"false\">\r\n    <LayoutTemplate>\r\n        <ul class=\"sfitemsList sfitemsListTitleDateTmb sflist\">\r\n            <asp:PlaceHolder ID=\"ItemsContainer\" runat=\"server\" />\r\n        </ul>\r\n    </LayoutTemplate>\r\n    <ItemTemplate>\r\n        <li class=\"sfitem sflistitem sfClearfix\"  data-sf-provider='<%# Eval(\"Provider.Name\")%>' data-sf-id='<%# Eval(\"Id\")%>' data-sf-type='<%# Container.DataItem.GetType().FullName%>'>\r\n            !#MainPictureSection#!\r\n            <h2 class=\"sfitemTitle sftitle\">\r\n                <sf:DetailsViewHyperLink ID=\"DetailsViewHyperLink\" TextDataField=\"!#MainShortTextField#!\" runat=\"server\" data-sf-field=\"!#MainShortTextField#!\" data-sf-ftype=\"ShortText\" TextMode=\"Encode\"/>\r\n            </h2>\r\n            !#PublicationDateSection#!\r\n              <sf:CommentsCountControl runat=\"server\" ID=\"commentsCounterControl\" CssClass=\"sfCommentsCounter\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' DisplayMode=\"ShortText\" />\r\n              <sf:CommentsAverageRatingControl runat=\"server\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' DisplayMode=\"MediumText\" />\r\n        </li>\r\n    </ItemTemplate>\r\n</telerik:RadListView>\r\n<sf:Pager id=\"pager\" runat=\"server\"></sf:Pager>";
    private const string allFieldsDetailTemplate = "<%@ Control Language=\"C#\" %>\r\n!#RelatedDataResourceSection#!\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Modules.Comments\" %>\r\n\r\n<sf:SitefinityLabel id=\"title\" runat=\"server\" WrapperTagName=\"div\" HideIfNoText=\"true\" HideIfNoTextMode=\"Server\" CssClass=\"sfitemFieldLbl\" TextMode=\"Encode\"/>\r\n<sf:DynamicDetailContainer id=\"detailContainer\" runat=\"server\">\r\n    <LayoutTemplate>        \r\n        <div class=\"sfitemDetails sfdetails\" data-sf-provider='<%# Eval(\"Provider.Name\")%>' data-sf-id='<%# Eval(\"Id\")%>' data-sf-type='<%# Container.DataItem.GetType().FullName%>'>\r\n            !#MainShortTextSection#!\r\n            !#PublicationDateSection#!\r\n            <sf:CommentsCountControl runat=\"server\" ID=\"commentsCounterControl\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' NavigateUrl=\"#commentsWidget\" DisplayMode=\"ShortText\" />\r\n\t\t\t<sf:CommentsAverageRatingControl runat=\"server\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' NavigateUrl=\"#commentsWidget\" DisplayMode=\"FullText\" />\r\n            !#LongFieldsTypeTextAreaSection#!\r\n            !#MediaTypeImagesSection#!\r\n            !#ShortTextSection#!\r\n            !#MultipleChoiceSection#!\r\n            !#YesNoSection#!\r\n            !#DatesSection#!\r\n            !#NumberSection#!\r\n            !#PriceSection#!\r\n            !#LongFieldsTypeRichTextSection#!\r\n            !#MediaVideoSection#!\r\n            !#MediaFilesSection#!\r\n            !#AddressFieldSection#!\r\n            !#ClassificationSection#!\r\n            !#RelatedMediaSection#!\r\n            !#RelatedDataSection#!\r\n        </div>\r\n    </LayoutTemplate>\r\n</sf:DynamicDetailContainer>\r\n<sf:CommentsWidget runat=\"server\" ID=\"commentsWidget\" />";
    private const string mainPropertySectionTemplate = "<sf:SitefinityLabel ID=\"mainShortTextFieldLiteral\" runat=\"server\" Text='<%#: Eval(\"{0}\") %>' WrapperTagName=\"h1\" HideIfNoText=\"true\" CssClass=\"sfitemTitle sftitle\" data-sf-field=\"{0}\" data-sf-ftype=\"ShortText\" />";
    private const string publicationDateTemplateFormated = "<sf:FieldListView ID=\"{0}\" runat=\"server\" Format=\"{1}\" WrapperTagName=\"div\" WrapperTagCssClass=\"sfitemPublicationDate\" />";
    private const string longTextFieldTypeTextAreaTemplate = "        \r\n            <sf:SitefinityLabel runat=\"server\" Text='<%# ControlUtilities.Sanitize(Eval(\"{0}\")) %>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemLongText\" data-sf-field=\"{0}\" data-sf-ftype=\"LongText\" />";
    private const string mediaFieldTypeImageTemplate = "      \r\n            <sf:ImageAssetsField runat=\"server\" DataFieldName=\"{0}\" IsThumbnail=\"{1}\" />";
    private const string mediaFieldTypeImageGalleryTemplate = "      \r\n            <sf:ImageGalleryAssetsField runat=\"server\" DataFieldName=\"{0}\" ImageGalleryMasterViewName=\"{1}\" />";
    private const string shortTextTemplate = "        \r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\") %>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemShortTxt\" data-sf-field=\"{0}\" data-sf-ftype=\"ShortText\"/>";
    private const string multipleChoiceFieldTemplate = "\r\n            <sf:ChoiceField runat=\"server\" DisplayMode=\"Read\" RenderChoicesAs=\"{0}\" DataItemType=\"{1}\" DataFieldName=\"{2}\" Title=\"{3}:\" Value='{4}' CssClass=\"sfitemChoices\" DisableClientScripts=\"true\" data-sf-field=\"{5}\" data-sf-ftype=\"{6}\">\r\n            </sf:ChoiceField>";
    private const string choiceFieldTemplate = "\r\n            <sf:DynamicChoiceField runat=\"server\" DisplayMode=\"Read\" RenderChoicesAs=\"{0}\" DataItemType=\"{1}\" DataFieldName=\"{2}\" Title=\"{3}:\" Value='{4}' CssClass=\"sfitemChoices\" DisableClientScripts=\"true\" data-sf-field=\"{5}\" data-sf-ftype=\"{6}\">\r\n            </sf:DynamicChoiceField>";
    private const string datesFieldTemplate = "\r\n                <sf:FieldListView ID=\"{0}\" runat=\"server\" Format=\"{1}\" WrapperTagName=\"div\" WrapperTagCssClass=\"sfitemDate\" />";
    private const string simpleTextLabelTemplate = "        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />";
    private const string simpleValueLabelTemplate = "        \r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\")%>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemNumber\" />";
    private const string priceLabelTemplate = "\r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\")%>' Format=\"{0:C}\" WrapperTagName=\"div\" HideIfNoText=\"false\" CssClass=\"sfitemPrice\" />";
    private const string longTextFieldTypeRichTextTemplate = "\r\n            <sf:SitefinityLabel runat=\"server\" Text='<%# ControlUtilities.Sanitize(Eval(\"{0}\"))%>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemRichText\" data-sf-field=\"{0}\" data-sf-ftype=\"LongText\"/>";
    private const string mediaFieldTypeVideoTemplate = "      \r\n            <sf:AssetsField runat=\"server\" DataFieldName=\"{0}\" />";
    private const string mediaFieldTypeFilesTemplate = "      \r\n            <sf:AssetsField runat=\"server\" DataFieldName=\"{0}\" />";
    private const string relatedResourceTemplate = "      \r\n<%@ Register Assembly=\"{0}\" Namespace=\"{1}\" TagPrefix=\"sf\" %>";
    private const string addressFieldTemplate = "      \r\n                <sf:AddressField runat=\"server\" DataFieldName=\"{0}\" IsMapExpanded=\"false\" AddressTemplate=\"#=Street# #=City# #=State# #=Country#\" />";
    private const string flatTaxonTemplate = "\r\n                <sf:FlatTaxonField runat=\"server\" DisplayMode=\"Read\" WebServiceUrl=\"{0}\" TaxonomyId=\"{1}\" AllowMultipleSelection=\"true\" TaxonomyMetafieldName=\"{2}\" Expanded=\"false\" BindOnServer=\"true\" LayoutTemplatePath=\"~/SFRes/Telerik.Sitefinity.Resources.Templates.Fields.FlatTaxonFieldReadMode.ascx\" />";
    private const string hierarchicalTaxonTemplate = "\r\n                <sf:HierarchicalTaxonField runat=\"server\" DisplayMode=\"Read\" WebServiceUrl=\"{0}\" TaxonomyId=\"{1}\" Expanded=\"false\" TaxonomyMetafieldName=\"{2}\" BindOnServer=\"true\" LayoutTemplatePath=\"~/SFRes/Telerik.Sitefinity.Resources.Templates.Fields.HierarchicalTaxonFieldReadMode.ascx\"/>";
    private static readonly Regex contextRegex = new Regex("(!#(?<ReplaceKey>([^#]*)?)#!)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly string flatTaxonomyUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
    private static readonly string hierarchicalTaxonomyUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
    private static readonly string dateFormat = "MMM d, yyyy, HH:mm tt";
    private static readonly PluralsResolver pluralsNameResolver = PluralsResolver.Instance;
    private static readonly string emptyLine = "\r\n";
    private readonly PageManager pageManager;
    private readonly ModuleBuilderManager moduleBuilderManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.WidgetTemplateInstaller" /> class.
    /// </summary>
    /// <param name="pageManager">The page manager.</param>
    /// <param name="moduleBuilderManager">The module builder manager.</param>
    public WidgetTemplateInstaller(
      PageManager pageManager,
      ModuleBuilderManager moduleBuilderManager)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      if (moduleBuilderManager == null)
        throw new ArgumentNullException(nameof (moduleBuilderManager));
      this.pageManager = pageManager;
      this.moduleBuilderManager = moduleBuilderManager;
    }

    /// <summary>Installs the default master template.</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <returns></returns>
    public Guid InstallDefaultMasterTemplate(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      Guid guid = this.GetDefaultMasterTemplateID(dynamicModule, moduleType);
      if (guid == Guid.Empty)
      {
        string templateName = "List of " + WidgetTemplateInstaller.pluralsNameResolver.ToPlural(moduleType.DisplayName).ToLower();
        string templateBody = WidgetTemplateInstaller.RemoveEmptyLines(this.GetDefaultTemplate(moduleType, typeof (DynamicContentViewMaster)));
        string appliedTo = this.GetAppliedTo(dynamicModule.Title, moduleType.DisplayName, " - list");
        string areaName = this.GetAreaName(dynamicModule.Title, moduleType.DisplayName);
        guid = this.InstallWidgetTemplate(typeof (DynamicContentViewMaster), moduleType.GetFullTypeName(), templateName, templateBody, areaName, appliedTo);
      }
      return guid;
    }

    /// <summary>
    /// Installs the template for the specified dynamic details view.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public Guid InstallDefaultDetailsTemplate(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      Guid guid = this.GetDefaultDetailsTemplateID(dynamicModule, moduleType);
      if (guid == Guid.Empty)
      {
        string templateName = "Full " + moduleType.DisplayName.ToLower() + " content";
        string templateBody = WidgetTemplateInstaller.RemoveEmptyLines(this.GetDefaultTemplate(moduleType, typeof (DynamicContentViewDetail)));
        string appliedTo = this.GetAppliedTo(dynamicModule.Title, moduleType.DisplayName, " - single");
        string areaName = this.GetAreaName(dynamicModule.Title, moduleType.DisplayName);
        guid = this.InstallWidgetTemplate(typeof (DynamicContentViewDetail), moduleType.GetFullTypeName(), templateName, templateBody, areaName, appliedTo);
      }
      return guid;
    }

    /// <summary>
    /// Gets the default template for the specified module and control type.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="ControlType">Type of the control.</param>
    public string GetDefaultTemplate(DynamicModuleType moduleType, Type controlType)
    {
      string input1 = !(controlType == typeof (DynamicContentViewMaster)) ? "<%@ Control Language=\"C#\" %>\r\n!#RelatedDataResourceSection#!\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Modules.Comments\" %>\r\n\r\n<sf:SitefinityLabel id=\"title\" runat=\"server\" WrapperTagName=\"div\" HideIfNoText=\"true\" HideIfNoTextMode=\"Server\" CssClass=\"sfitemFieldLbl\" TextMode=\"Encode\"/>\r\n<sf:DynamicDetailContainer id=\"detailContainer\" runat=\"server\">\r\n    <LayoutTemplate>        \r\n        <div class=\"sfitemDetails sfdetails\" data-sf-provider='<%# Eval(\"Provider.Name\")%>' data-sf-id='<%# Eval(\"Id\")%>' data-sf-type='<%# Container.DataItem.GetType().FullName%>'>\r\n            !#MainShortTextSection#!\r\n            !#PublicationDateSection#!\r\n            <sf:CommentsCountControl runat=\"server\" ID=\"commentsCounterControl\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' NavigateUrl=\"#commentsWidget\" DisplayMode=\"ShortText\" />\r\n\t\t\t<sf:CommentsAverageRatingControl runat=\"server\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' NavigateUrl=\"#commentsWidget\" DisplayMode=\"FullText\" />\r\n            !#LongFieldsTypeTextAreaSection#!\r\n            !#MediaTypeImagesSection#!\r\n            !#ShortTextSection#!\r\n            !#MultipleChoiceSection#!\r\n            !#YesNoSection#!\r\n            !#DatesSection#!\r\n            !#NumberSection#!\r\n            !#PriceSection#!\r\n            !#LongFieldsTypeRichTextSection#!\r\n            !#MediaVideoSection#!\r\n            !#MediaFilesSection#!\r\n            !#AddressFieldSection#!\r\n            !#ClassificationSection#!\r\n            !#RelatedMediaSection#!\r\n            !#RelatedDataSection#!\r\n        </div>\r\n    </LayoutTemplate>\r\n</sf:DynamicDetailContainer>\r\n<sf:CommentsWidget runat=\"server\" ID=\"commentsWidget\" />" : "<%@ Control Language=\"C#\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.ContentUI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.Comments\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI.Fields\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<%@ Register Assembly=\"Telerik.Sitefinity\" Namespace=\"Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend\" TagPrefix=\"sf\" %>\r\n<%@ Register TagPrefix=\"telerik\" Namespace=\"Telerik.Web.UI\" Assembly=\"Telerik.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Web.UI\" %>\r\n<%@ Import Namespace=\"Telerik.Sitefinity.Modules.Comments\" %>\r\n\r\n<sf:SitefinityLabel id=\"title\" runat=\"server\" WrapperTagName=\"div\" HideIfNoText=\"true\" HideIfNoTextMode=\"Server\" CssClass=\"sfitemFieldLbl\" />\r\n<telerik:RadListView ID=\"dynamicContentListView\" ItemPlaceholderID=\"ItemsContainer\" runat=\"server\" EnableEmbeddedSkins=\"false\" EnableEmbeddedBaseStylesheet=\"false\">\r\n    <LayoutTemplate>\r\n        <ul class=\"sfitemsList sfitemsListTitleDateTmb sflist\">\r\n            <asp:PlaceHolder ID=\"ItemsContainer\" runat=\"server\" />\r\n        </ul>\r\n    </LayoutTemplate>\r\n    <ItemTemplate>\r\n        <li class=\"sfitem sflistitem sfClearfix\"  data-sf-provider='<%# Eval(\"Provider.Name\")%>' data-sf-id='<%# Eval(\"Id\")%>' data-sf-type='<%# Container.DataItem.GetType().FullName%>'>\r\n            !#MainPictureSection#!\r\n            <h2 class=\"sfitemTitle sftitle\">\r\n                <sf:DetailsViewHyperLink ID=\"DetailsViewHyperLink\" TextDataField=\"!#MainShortTextField#!\" runat=\"server\" data-sf-field=\"!#MainShortTextField#!\" data-sf-ftype=\"ShortText\" TextMode=\"Encode\"/>\r\n            </h2>\r\n            !#PublicationDateSection#!\r\n              <sf:CommentsCountControl runat=\"server\" ID=\"commentsCounterControl\" CssClass=\"sfCommentsCounter\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' DisplayMode=\"ShortText\" />\r\n              <sf:CommentsAverageRatingControl runat=\"server\" ThreadType='<%# Container.DataItem.GetType().FullName%>' ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval(\"Id\"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' DisplayMode=\"MediumText\" />\r\n        </li>\r\n    </ItemTemplate>\r\n</telerik:RadListView>\r\n<sf:Pager id=\"pager\" runat=\"server\"></sf:Pager>";
      List<KeyValuePair<string, string>> resources = new List<KeyValuePair<string, string>>()
      {
        new KeyValuePair<string, string>("Telerik.Sitefinity.DynamicModules.Web.UI.Frontend", "Telerik.Sitefinity"),
        new KeyValuePair<string, string>("Telerik.Sitefinity.Web.UI.Fields", "Telerik.Sitefinity"),
        new KeyValuePair<string, string>("Telerik.Sitefinity.Web.UI", "Telerik.Sitefinity"),
        new KeyValuePair<string, string>("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend", "Telerik.Sitefinity")
      };
      string input2 = WidgetTemplateInstaller.contextRegex.Replace(input1, (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, resources)));
      return WidgetTemplateInstaller.PreventServerScriptInjection(WidgetTemplateInstaller.RemoveEmptyLines(WidgetTemplateInstaller.contextRegex.Replace(input2, (MatchEvaluator) (m => this.ReplaceResourcesSampleKey(m, resources)))));
    }

    /// <summary>
    /// Gets the default template for the specified module and control type.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="controlType">Type of the control.</param>
    public string GetDefaultTemplate(Type moduleType, Type controlType)
    {
      DynamicModuleType dynamicModuleType = this.moduleBuilderManager.GetDynamicModuleType(moduleType);
      this.moduleBuilderManager.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
      return this.GetDefaultTemplate(dynamicModuleType, controlType);
    }

    /// <summary>Gets the template ID of the default master view.</summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public Guid GetDefaultMasterTemplateID(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      return this.GetWidgetTemplateID(typeof (DynamicContentViewMaster), this.GetAreaName(dynamicModule.Title, moduleType.DisplayName), moduleType.GetFullTypeName());
    }

    /// <summary>
    /// Gets the template ID of the installed dynamic details view.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public Guid GetDefaultDetailsTemplateID(
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      return this.GetWidgetTemplateID(typeof (DynamicContentViewDetail), this.GetAreaName(dynamicModule.Title, moduleType.DisplayName), moduleType.GetFullTypeName());
    }

    /// <summary>
    /// Checks if widget template with specific ID exists for specified module and type
    /// </summary>
    public bool WidgetTemplateExists(
      Guid templateId,
      string controlTypeName,
      DynamicModule dynamicModule,
      DynamicModuleType moduleType)
    {
      string moduleTypeFullName = moduleType.GetFullTypeName();
      string areaName = this.GetAreaName(dynamicModule.Title, moduleType.DisplayName);
      return this.pageManager.GetPresentationItems<ControlPresentation>().Any<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.DataType == "ASP_NET_TEMPLATE" && p.ControlType == controlTypeName && p.AreaName == areaName && p.Condition == moduleTypeFullName && p.Id == templateId));
    }

    /// <summary>
    /// Uninstalls the widget templates for the specified module type.
    /// </summary>
    /// <param name="contentTypeName">The name of the contetn type.</param>
    public void UnInstallWidgetTemplates(string contentTypeName)
    {
      IQueryable<ControlPresentation> presentationItems = this.pageManager.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (t => t.Condition == contentTypeName);
      foreach (PresentationData presentationData in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
        this.pageManager.Delete(presentationData);
    }

    private static string GenerateMainTextSection(DynamicModuleType moduleType) => string.Format("<sf:SitefinityLabel ID=\"mainShortTextFieldLiteral\" runat=\"server\" Text='<%#: Eval(\"{0}\") %>' WrapperTagName=\"h1\" HideIfNoText=\"true\" CssClass=\"sfitemTitle sftitle\" data-sf-field=\"{0}\" data-sf-ftype=\"ShortText\" />", (object) moduleType.MainShortTextFieldName);

    private static string GeneratePublicationDateSection() => string.Format("<sf:FieldListView ID=\"{0}\" runat=\"server\" Format=\"{1}\" WrapperTagName=\"div\" WrapperTagCssClass=\"sfitemPublicationDate\" />", (object) "PublicationDate", (object) ("{PublicationDate.ToLocal():" + WidgetTemplateInstaller.dateFormat + "}"));

    private static string GenerateLongTextFieldsTypeTextAreaSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (m => m.FieldType == FieldType.LongText && m.FieldStatus != DynamicModuleFieldStatus.Removed && m.WidgetTypeName.EndsWith("TextField") && !m.IsHiddenField && !WidgetTemplateInstaller.excludedFieldNames.Contains(m.Name)));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
        stringBuilder.Append(string.Format("        \r\n            <sf:SitefinityLabel runat=\"server\" Text='<%# ControlUtilities.Sanitize(Eval(\"{0}\")) %>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemLongText\" data-sf-field=\"{0}\" data-sf-ftype=\"LongText\" />", (object) dynamicModuleField.Name));
      return stringBuilder.ToString();
    }

    private static string GenerateMediaTypeImagesSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.FieldStatus != DynamicModuleFieldStatus.Removed && f.MediaType == "image"));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField field in source)
        stringBuilder.Append(WidgetTemplateInstaller.GetImageFieldTypeTemplate(field, false));
      return stringBuilder.ToString();
    }

    private static string GenerateShortTextSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => (f.FieldType == FieldType.ShortText || f.FieldType == FieldType.Guid) && f.FieldStatus != DynamicModuleFieldStatus.Removed && f.Name != moduleType.MainShortTextFieldName && !WidgetTemplateInstaller.excludedFieldNames.Contains(f.Name) && !f.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        stringBuilder.Append("\r\n            <div class='sfitemShortTxtWrp'>");
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Title));
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\") %>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemShortTxt\" data-sf-field=\"{0}\" data-sf-ftype=\"ShortText\"/>", (object) dynamicModuleField.Name));
        stringBuilder.Append("\r\n            </div>");
      }
      return stringBuilder.ToString();
    }

    private static string GenerateMultipleChoiceSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => (f.FieldType == FieldType.MultipleChoice || f.FieldType == FieldType.Choices) && f.FieldStatus != DynamicModuleFieldStatus.Removed));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        string str1 = WidgetTemplateInstaller.ResolveMultipleChoiceRenderType(dynamicModuleField.ChoiceRenderType);
        string fullTypeName = moduleType.GetFullTypeName();
        string name = dynamicModuleField.Name;
        string str2 = "<%# Eval(\"" + name + "\") %>";
        string empty = string.Empty;
        string str3 = "Choices" + str1;
        string str4;
        if (dynamicModuleField.FieldType == FieldType.MultipleChoice)
          str4 = string.Format("\r\n            <sf:ChoiceField runat=\"server\" DisplayMode=\"Read\" RenderChoicesAs=\"{0}\" DataItemType=\"{1}\" DataFieldName=\"{2}\" Title=\"{3}:\" Value='{4}' CssClass=\"sfitemChoices\" DisableClientScripts=\"true\" data-sf-field=\"{5}\" data-sf-ftype=\"{6}\">\r\n            </sf:ChoiceField>", (object) str1, (object) fullTypeName, (object) name, (object) dynamicModuleField.Title, (object) str2, (object) name, (object) str3);
        else
          str4 = string.Format("\r\n            <sf:DynamicChoiceField runat=\"server\" DisplayMode=\"Read\" RenderChoicesAs=\"{0}\" DataItemType=\"{1}\" DataFieldName=\"{2}\" Title=\"{3}:\" Value='{4}' CssClass=\"sfitemChoices\" DisableClientScripts=\"true\" data-sf-field=\"{5}\" data-sf-ftype=\"{6}\">\r\n            </sf:DynamicChoiceField>", (object) str1, (object) fullTypeName, (object) name, (object) dynamicModuleField.Title, (object) str2, (object) name, (object) str3);
        stringBuilder.Append(str4);
      }
      return stringBuilder.ToString();
    }

    private static string GenerateYesNoChoiceSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.YesNo && f.FieldStatus != DynamicModuleFieldStatus.Removed && !f.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        string str1 = "SingleCheckBox";
        string fullTypeName = moduleType.GetFullTypeName();
        string name = dynamicModuleField.Name;
        string str2 = "<%# Eval(\"" + name + "\") %>";
        stringBuilder.Append(string.Format("\r\n            <sf:ChoiceField runat=\"server\" DisplayMode=\"Read\" RenderChoicesAs=\"{0}\" DataItemType=\"{1}\" DataFieldName=\"{2}\" Title=\"{3}:\" Value='{4}' CssClass=\"sfitemChoices\" DisableClientScripts=\"true\" data-sf-field=\"{5}\" data-sf-ftype=\"{6}\">\r\n            </sf:ChoiceField>", (object) str1, (object) fullTypeName, (object) name, (object) dynamicModuleField.Title, (object) str2, (object) name, (object) "YesNo"));
      }
      return stringBuilder.ToString();
    }

    private static string GenerateDatesSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.DateTime && f.FieldStatus != DynamicModuleFieldStatus.Removed && !f.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        stringBuilder.AppendFormat("\r\n            <div class='sfitemDateWrp' data-sf-field='{0}' data-sf-ftype='DateTime'>", (object) dynamicModuleField.Name);
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Name));
        string str = "{" + dynamicModuleField.Name + ".ToLocal():" + WidgetTemplateInstaller.dateFormat + "}";
        stringBuilder.Append(string.Format("\r\n                <sf:FieldListView ID=\"{0}\" runat=\"server\" Format=\"{1}\" WrapperTagName=\"div\" WrapperTagCssClass=\"sfitemDate\" />", (object) dynamicModuleField.Name, (object) str));
        stringBuilder.Append("\r\n            </div>");
      }
      return stringBuilder.ToString();
    }

    private static string GenerateNumbersSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Number && f.FieldStatus != DynamicModuleFieldStatus.Removed && !f.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        stringBuilder.Append("\r\n            <div class='sfitemNumberWrp'>");
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Name));
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\")%>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemNumber\" />", (object) dynamicModuleField.Name));
        if (!string.IsNullOrEmpty(dynamicModuleField.NumberUnit))
        {
          stringBuilder.Append(" <span class=\"sfUnit\">");
          stringBuilder.Append(dynamicModuleField.NumberUnit);
          stringBuilder.Append("</span>");
        }
        stringBuilder.Append("\r\n            </div>");
      }
      return stringBuilder.ToString();
    }

    private static string GeneratePriceSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Currency && f.FieldStatus != DynamicModuleFieldStatus.Removed && !f.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        stringBuilder.Append("\r\n            <div class='sfitemPriceWrp'>");
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Name));
        stringBuilder.Append(string.Format("\r\n                <sf:SitefinityLabel runat=\"server\" Text='<%#: Eval(\"{0}\")%>' Format=\"{0:C}\" WrapperTagName=\"div\" HideIfNoText=\"false\" CssClass=\"sfitemPrice\" />", (object) dynamicModuleField.Name));
        stringBuilder.Append("\r\n            </div>");
      }
      return stringBuilder.ToString();
    }

    private static string GenerateLongTextFieldsTypeRichTextSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (m => m.FieldType == FieldType.LongText && m.FieldStatus != DynamicModuleFieldStatus.Removed && m.WidgetTypeName.EndsWith("HtmlField") && !WidgetTemplateInstaller.excludedFieldNames.Contains(m.Name) && !m.IsHiddenField));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
        stringBuilder.Append(string.Format("\r\n            <sf:SitefinityLabel runat=\"server\" Text='<%# ControlUtilities.Sanitize(Eval(\"{0}\"))%>' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemRichText\" data-sf-field=\"{0}\" data-sf-ftype=\"LongText\"/>", (object) dynamicModuleField.Name));
      return stringBuilder.ToString();
    }

    private static string GenerateMediaTypeVideoSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.FieldStatus != DynamicModuleFieldStatus.Removed && f.MediaType == "video"));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
        stringBuilder.Append(string.Format("      \r\n            <sf:AssetsField runat=\"server\" DataFieldName=\"{0}\" />", (object) dynamicModuleField.Name));
      return stringBuilder.ToString();
    }

    private static string GenerateMediaTypeFilesSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.FieldStatus != DynamicModuleFieldStatus.Removed && f.MediaType == "file"));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
        stringBuilder.Append(string.Format("      \r\n            <sf:AssetsField runat=\"server\" DataFieldName=\"{0}\" />", (object) dynamicModuleField.Name));
      return stringBuilder.ToString();
    }

    private static string GenerateAddressFieldSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Address && f.FieldStatus != DynamicModuleFieldStatus.Removed));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        stringBuilder.Append("\r\n            <div class='sfitemAddressWrp'>");
        stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Title));
        stringBuilder.Append(string.Format("      \r\n                <sf:AddressField runat=\"server\" DataFieldName=\"{0}\" IsMapExpanded=\"false\" AddressTemplate=\"#=Street# #=City# #=State# #=Country#\" />", (object) dynamicModuleField.Name));
        stringBuilder.Append("\r\n            </div>");
      }
      return stringBuilder.ToString();
    }

    private static string GenerateClassificationSection(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Classification && f.FieldStatus != DynamicModuleFieldStatus.Removed));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        ITaxonomy taxonomy = TaxonomyManager.GetManager().GetTaxonomy(dynamicModuleField.ClassificationId);
        if (taxonomy is FlatTaxonomy)
        {
          stringBuilder.Append("\r\n            <div class='sfitemFlatTaxon sfitemTaxonWrp' data-sf-field='{0}' data-sf-ftype='FlatTaxon'>".Arrange((object) dynamicModuleField.Name));
          stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Title));
          stringBuilder.Append(string.Format("\r\n                <sf:FlatTaxonField runat=\"server\" DisplayMode=\"Read\" WebServiceUrl=\"{0}\" TaxonomyId=\"{1}\" AllowMultipleSelection=\"true\" TaxonomyMetafieldName=\"{2}\" Expanded=\"false\" BindOnServer=\"true\" LayoutTemplatePath=\"~/SFRes/Telerik.Sitefinity.Resources.Templates.Fields.FlatTaxonFieldReadMode.ascx\" />", (object) WidgetTemplateInstaller.flatTaxonomyUrl, (object) dynamicModuleField.ClassificationId, (object) dynamicModuleField.Name));
          stringBuilder.Append("\r\n            </div>");
        }
        else if (taxonomy is HierarchicalTaxonomy)
        {
          stringBuilder.Append("\r\n            <div class='sfitemHierarchicalTaxon sfitemTaxonWrp' data-sf-field='{0}' data-sf-ftype='HierarchicalTaxon'>".Arrange((object) dynamicModuleField.Name));
          stringBuilder.Append(string.Format("        \r\n                <sf:SitefinityLabel runat=\"server\" Text='{0}:' WrapperTagName=\"div\" HideIfNoText=\"true\" CssClass=\"sfitemFieldLbl\" />", (object) dynamicModuleField.Title));
          stringBuilder.Append(string.Format("\r\n                <sf:HierarchicalTaxonField runat=\"server\" DisplayMode=\"Read\" WebServiceUrl=\"{0}\" TaxonomyId=\"{1}\" Expanded=\"false\" TaxonomyMetafieldName=\"{2}\" BindOnServer=\"true\" LayoutTemplatePath=\"~/SFRes/Telerik.Sitefinity.Resources.Templates.Fields.HierarchicalTaxonFieldReadMode.ascx\"/>", (object) WidgetTemplateInstaller.hierarchicalTaxonomyUrl, (object) dynamicModuleField.ClassificationId, (object) dynamicModuleField.Name));
          stringBuilder.Append("\r\n            </div>");
        }
      }
      return stringBuilder.ToString();
    }

    private static string GenerateMainPictureSection(DynamicModuleType moduleType)
    {
      DynamicModuleField field = ((IEnumerable<DynamicModuleField>) moduleType.Fields).FirstOrDefault<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.Media && f.FieldStatus != DynamicModuleFieldStatus.Removed && f.MediaType == "image"));
      return field != null ? WidgetTemplateInstaller.GetImageFieldTypeTemplate(field, true) : WidgetTemplateInstaller.emptyLine;
    }

    private static string GenerateRelatedMediaSection(
      DynamicModuleType moduleType,
      List<KeyValuePair<string, string>> resources)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedMedia && f.FieldStatus != DynamicModuleFieldStatus.Removed && !WidgetTemplateInstaller.excludedFieldNames.Contains(f.Name)));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      if (!resources.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (r => r.Key == "Telerik.Sitefinity.Modules.Libraries.Web.UI" && r.Value == "Telerik.Sitefinity")))
        resources.Add(new KeyValuePair<string, string>("Telerik.Sitefinity.Modules.Libraries.Web.UI", "Telerik.Sitefinity"));
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        Type frontendWidgetType = TypeResolutionService.ResolveType(dynamicModuleField.FrontendWidgetTypeName, false);
        if (frontendWidgetType != (Type) null && !resources.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (r => r.Key == frontendWidgetType.Namespace && r.Value == frontendWidgetType.Assembly.GetName().Name)))
          resources.Add(new KeyValuePair<string, string>(frontendWidgetType.Namespace, frontendWidgetType.Assembly.GetName().Name));
        bool isMasterView = false;
        string childTypeName = string.Empty;
        string mediaType = dynamicModuleField.MediaType;
        if (!(mediaType == "image"))
        {
          if (!(mediaType == "video"))
          {
            if (mediaType == "file")
            {
              isMasterView = dynamicModuleField.AllowMultipleFiles;
              childTypeName = typeof (Document).FullName;
            }
          }
          else
          {
            isMasterView = dynamicModuleField.AllowMultipleVideos;
            childTypeName = typeof (Video).FullName;
          }
        }
        else
        {
          isMasterView = dynamicModuleField.AllowMultipleImages;
          childTypeName = typeof (Image).FullName;
        }
        stringBuilder.Append(FieldTemplateBuilder.BuildRelatedDataFieldTemplate(dynamicModuleField.FrontendWidgetTypeName, dynamicModuleField.FrontendWidgetLabel, dynamicModuleField.FieldNamespace, childTypeName, dynamicModuleField.RelatedDataProvider, dynamicModuleField.Name, isMasterView, false, false));
      }
      return stringBuilder.ToString();
    }

    private static string GenerateRelatedDataSection(
      DynamicModuleType moduleType,
      List<KeyValuePair<string, string>> resources)
    {
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.FieldType == FieldType.RelatedData && f.FieldStatus != DynamicModuleFieldStatus.Removed));
      if (source.Count<DynamicModuleField>() == 0)
        return WidgetTemplateInstaller.emptyLine;
      if (!resources.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (r => r.Key == "Telerik.Sitefinity.Web.UI.ContentUI" && r.Value == "Telerik.Sitefinity")))
        resources.Add(new KeyValuePair<string, string>("Telerik.Sitefinity.Web.UI.ContentUI", "Telerik.Sitefinity"));
      foreach (DynamicModuleField dynamicModuleField in source)
      {
        if ("inline".Equals(dynamicModuleField.FrontendWidgetTypeName))
        {
          stringBuilder.Append(FieldTemplateBuilder.BuildRelatedDataFieldTemplate(dynamicModuleField.FrontendWidgetTypeName, dynamicModuleField.FrontendWidgetLabel, dynamicModuleField.FieldNamespace, dynamicModuleField.RelatedDataType, dynamicModuleField.RelatedDataProvider, dynamicModuleField.Name, dynamicModuleField.CanSelectMultipleItems, false, false));
        }
        else
        {
          Type frontendWidget = TypeResolutionService.ResolveType(dynamicModuleField.FrontendWidgetTypeName, false);
          if (frontendWidget != (Type) null)
          {
            if (!resources.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (r => r.Key == frontendWidget.Namespace && r.Value == frontendWidget.Assembly.GetName().Name)))
              resources.Add(new KeyValuePair<string, string>(frontendWidget.Namespace, frontendWidget.Assembly.GetName().Name));
            stringBuilder.Append(FieldTemplateBuilder.BuildRelatedDataFieldTemplate(dynamicModuleField.FrontendWidgetTypeName, dynamicModuleField.FrontendWidgetLabel, dynamicModuleField.FieldNamespace, dynamicModuleField.RelatedDataType, dynamicModuleField.RelatedDataProvider, dynamicModuleField.Name, dynamicModuleField.CanSelectMultipleItems, false, false));
          }
        }
      }
      return stringBuilder.ToString();
    }

    private static string GenerateRelatedDataResourceSection(
      List<KeyValuePair<string, string>> relatedResources)
    {
      if (relatedResources.Count <= 0)
        return WidgetTemplateInstaller.emptyLine;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (KeyValuePair<string, string> relatedResource in relatedResources)
        stringBuilder.Append(string.Format("      \r\n<%@ Register Assembly=\"{0}\" Namespace=\"{1}\" TagPrefix=\"sf\" %>", (object) relatedResource.Value, (object) relatedResource.Key));
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Returns template according to the type of the image field (Multiple or Single image)
    /// </summary>
    /// <param name="isThumbnail">Sets the IsThumbnail option of the ImageAssetsField if applicable.</param>
    internal static string GetImageFieldTypeTemplate(DynamicModuleField field, bool isThumbnail)
    {
      string empty = string.Empty;
      return !field.AllowMultipleImages ? string.Format("      \r\n            <sf:ImageAssetsField runat=\"server\" DataFieldName=\"{0}\" IsThumbnail=\"{1}\" />", (object) field.Name, (object) isThumbnail) : string.Format("      \r\n            <sf:ImageGalleryAssetsField runat=\"server\" DataFieldName=\"{0}\" ImageGalleryMasterViewName=\"{1}\" />", (object) field.Name, (object) "ImagesFrontendThumbnailsListLightBox");
    }

    internal static string GetMediaFieldTypeTemplate(DynamicModuleField field)
    {
      string mediaType = field.MediaType;
      if (mediaType == "image")
        return WidgetTemplateInstaller.GetImageFieldTypeTemplate(field, false).Trim();
      if (!(mediaType == "video") && !(mediaType == "file"))
        return (string) null;
      return "<sf:AssetsField runat=\"server\" DataFieldName=\"{0}\" />".Arrange((object) field.Name).Trim();
    }

    private static string PreventServerScriptInjection(string data) => Regex.Replace(data, "<script runat=\"server\".*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private static string ResolveMultipleChoiceRenderType(string choiceRenderType)
    {
      string lower = choiceRenderType.ToLower();
      if (lower == "checkbox")
        return "CheckBoxes";
      if (lower == "radiobutton")
        return "RadioButtons";
      if (lower == "dropdownlist" || lower == "custom")
        return "DropDown";
      throw new NotSupportedException("Not supported Render Type");
    }

    private string ReplaceCodeSampleKey(
      Match match,
      DynamicModuleType moduleType,
      List<KeyValuePair<string, string>> resources)
    {
      switch (match.Value)
      {
        case "!#AddressFieldSection#!":
          return WidgetTemplateInstaller.GenerateAddressFieldSection(moduleType);
        case "!#ClassificationSection#!":
          return WidgetTemplateInstaller.GenerateClassificationSection(moduleType);
        case "!#DatesSection#!":
          return WidgetTemplateInstaller.GenerateDatesSection(moduleType);
        case "!#LongFieldsTypeRichTextSection#!":
          return WidgetTemplateInstaller.GenerateLongTextFieldsTypeRichTextSection(moduleType);
        case "!#LongFieldsTypeTextAreaSection#!":
          return WidgetTemplateInstaller.GenerateLongTextFieldsTypeTextAreaSection(moduleType);
        case "!#MainPictureSection#!":
          return WidgetTemplateInstaller.GenerateMainPictureSection(moduleType);
        case "!#MainShortTextField#!":
          return moduleType.MainShortTextFieldName;
        case "!#MainShortTextSection#!":
          return WidgetTemplateInstaller.GenerateMainTextSection(moduleType);
        case "!#MediaFilesSection#!":
          return WidgetTemplateInstaller.GenerateMediaTypeFilesSection(moduleType);
        case "!#MediaTypeImagesSection#!":
          return WidgetTemplateInstaller.GenerateMediaTypeImagesSection(moduleType);
        case "!#MediaVideoSection#!":
          return WidgetTemplateInstaller.GenerateMediaTypeVideoSection(moduleType);
        case "!#MultipleChoiceSection#!":
          return WidgetTemplateInstaller.GenerateMultipleChoiceSection(moduleType);
        case "!#NumberSection#!":
          return WidgetTemplateInstaller.GenerateNumbersSection(moduleType);
        case "!#PriceSection#!":
          return WidgetTemplateInstaller.GeneratePriceSection(moduleType);
        case "!#PublicationDateSection#!":
          return WidgetTemplateInstaller.GeneratePublicationDateSection();
        case "!#RelatedDataResourceSection#!":
          return "!#RelatedDataResourceSection#!";
        case "!#RelatedDataSection#!":
          return WidgetTemplateInstaller.GenerateRelatedDataSection(moduleType, resources);
        case "!#RelatedMediaSection#!":
          return WidgetTemplateInstaller.GenerateRelatedMediaSection(moduleType, resources);
        case "!#ShortTextSection#!":
          return WidgetTemplateInstaller.GenerateShortTextSection(moduleType);
        case "!#YesNoSection#!":
          return WidgetTemplateInstaller.GenerateYesNoChoiceSection(moduleType);
        default:
          throw new NotSupportedException();
      }
    }

    private string ReplaceResourcesSampleKey(
      Match match,
      List<KeyValuePair<string, string>> resources)
    {
      return match.Value == "!#RelatedDataResourceSection#!" ? WidgetTemplateInstaller.GenerateRelatedDataResourceSection(resources) : WidgetTemplateInstaller.emptyLine;
    }

    private Guid InstallWidgetTemplate(
      Type controlType,
      string dataItemTypeFullName,
      string templateName,
      string templateBody,
      string areaName,
      string appliedTo)
    {
      ControlPresentation presentationItem = this.pageManager.Provider.CreatePresentationItem<ControlPresentation>();
      presentationItem.DataType = "ASP_NET_TEMPLATE";
      presentationItem.EmbeddedTemplateName = (string) null;
      presentationItem.ControlType = controlType.FullName;
      presentationItem.Name = templateName;
      presentationItem.Data = WidgetTemplateInstaller.PreventServerScriptInjection(templateBody);
      presentationItem.Condition = dataItemTypeFullName;
      presentationItem.AreaName = areaName;
      presentationItem.FriendlyControlName = appliedTo;
      return presentationItem.Id;
    }

    internal Guid GetWidgetTemplateID(
      Type controlType,
      string areaName,
      string moduleTypeFullName)
    {
      ControlPresentation controlPresentation = this.pageManager.GetPresentationItems<ControlPresentation>().FirstOrDefault<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.DataType == "ASP_NET_TEMPLATE" && p.ControlType == controlType.FullName && p.AreaName == areaName && p.Condition == moduleTypeFullName));
      return controlPresentation == null ? Guid.Empty : controlPresentation.Id;
    }

    private static string RemoveEmptyLines(string template) => Regex.Replace(template, "\\r\\n(\\s)+\\r\\n", "\r\n");

    internal string GetAreaName(string moduleTitle, string typeDisplayName) => moduleTitle + " - " + typeDisplayName;

    private string GetAppliedTo(string moduleTitle, string typeDisplayName, string suffix) => moduleTitle + " - " + WidgetTemplateInstaller.pluralsNameResolver.ToPlural(typeDisplayName) + suffix;
  }
}
