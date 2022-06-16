// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.CodeArticleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  /// <summary>Base code article control.</summary>
  public abstract class CodeArticleBase : SimpleView
  {
    private bool? isMultilingual;
    private IndefiniteArticleResolver indefiniteArticleResolver;
    private readonly Regex contextRegex = new Regex("(!#(?<ReplaceKey>([^#]*)?)#!)", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.CodeArticleBase.ascx");
    private readonly string urlFieldName = "UrlName";
    private SortedSet<string> relatedDataNamespaces;

    /// <summary>
    /// Gets or sets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which
    /// the article should be contextualized.
    /// </summary>
    public DynamicModule Module { get; set; }

    /// <summary>
    /// Gets or sets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which
    /// the article should be contextualized.
    /// </summary>
    public DynamicModuleType ModuleType { get; set; }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.UI.IndefiniteArticleResolver" /> used to
    /// resolve indefinite articles.
    /// </summary>
    internal virtual IndefiniteArticleResolver IndefiniteArticleNameResolver
    {
      get
      {
        if (this.indefiniteArticleResolver == null)
          this.indefiniteArticleResolver = new IndefiniteArticleResolver();
        return this.indefiniteArticleResolver;
      }
    }

    /// <summary>Gets the singleton resolver of plural words.</summary>
    internal virtual PluralsResolver PluralsNameResolver => PluralsResolver.Instance;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = CodeArticleBase.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the regular expression for contextualizing code samples.
    /// </summary>
    protected Regex ContextRegex => this.contextRegex;

    /// <summary>
    /// Gets a value indicating whether the module type has localizable fields and the system is in multilingual.
    /// </summary>
    internal bool IsMultilingual
    {
      get
      {
        if (!this.isMultilingual.HasValue)
          this.isMultilingual = new bool?(ModuleInstallerHelper.ContainsLocalizableFields((IEnumerable<IDynamicModuleField>) this.ModuleType.Fields));
        return this.isMultilingual.Value;
      }
    }

    protected SortedSet<string> RelatedDataNamespaces
    {
      get
      {
        if (this.relatedDataNamespaces == null)
          this.relatedDataNamespaces = new SortedSet<string>();
        return this.relatedDataNamespaces;
      }
    }

    /// <summary>
    /// Gets the reference to the control that displays the title of the topic.
    /// </summary>
    protected virtual Label TopicTitle => this.Container.GetControl<Label>("topicTitle", true);

    /// <summary>
    /// Gets the reference to the control that displays the description of the topic.
    /// </summary>
    protected virtual Label TopicDescription => this.Container.GetControl<Label>("topicDescription", true);

    /// <summary>
    /// Gets the reference to the control that displays the topic code.
    /// </summary>
    protected virtual Literal TopicCode => this.Container.GetControl<Literal>("topicCode", true);

    /// <summary>
    /// Gets the reference to the control that displays the explanation of the topic code.
    /// </summary>
    protected virtual Label CodeExplanation => this.Container.GetControl<Label>("codeExplanation", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.IndefiniteArticleNameResolver.ResolveModuleTypeName((IDynamicModuleType) this.ModuleType);
      this.TopicTitle.Text = this.GenerateArticleTitle(this.Module, this.ModuleType);
      this.TopicDescription.Text = this.GenerateArticleDescription(this.Module, this.ModuleType);
      this.CodeExplanation.Text = this.GenerateCodeExplanation(this.Module, this.ModuleType);
    }

    /// <summary>
    /// Generates the title of the article from the specified module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> from which the title should be specified.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> from which the title should be specified.
    /// </param>
    /// <returns>The title of the article.</returns>
    public abstract string GenerateArticleTitle(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null);

    /// <summary>
    /// Generates the description of the article from the specified module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> from which the description should be specified.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> from which the description should be specified.
    /// </param>
    /// <returns>The description of the article.</returns>
    public abstract string GenerateArticleDescription(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null);

    /// <summary>
    /// Generates the contextualized code explanation for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code explanation should be
    /// contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code explanation should be
    /// contextualized.
    /// </param>
    /// <returns>The code explanation.</returns>
    protected abstract string GenerateCodeExplanation(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected string GetCreatedItemVariableName(string typeName) => typeName.TocamelCase() + "Item";

    protected string ReplaceCodeSampleKey(
      Match match,
      DynamicModuleType moduleType,
      CultureInfo culture = null,
      DynamicModule module = null)
    {
      switch (match.Value)
      {
        case "!#CheckInCheckOutItem#!":
          return this.CheckInCheckOutItem(moduleType);
        case "!#CheckInCheckOutItemVBNet#!":
          return this.CheckInCheckOutItemVBNet(moduleType);
        case "!#DynamicPropertySetting#!":
          return this.GeneratePropertiesCode(moduleType);
        case "!#DynamicPropertySettingVBNet#!":
          return this.GeneratePropertiesCodeVBNet(moduleType);
        case "!#FirstPropertyName#!":
          return CodeArticleBase.GetFirstPropertyName(moduleType);
        case "!#FullTypeName#!":
          return moduleType.TypeNamespace + "." + moduleType.TypeName;
        case "!#GetFirstParentIdCSharp#!":
          return CodeArticleBase.GenerateGetFirstParentIdCSharpCode(moduleType);
        case "!#GetFirstParentIdVB#!":
          return CodeArticleBase.GenerateGetFirstParentIdVBNetCode(moduleType);
        case "!#ItemRealGuid#!":
          return Guid.NewGuid().ToString();
        case "!#LocalizableCulture#!":
          return this.LocalizableCulture(moduleType);
        case "!#LocalizableFirstPropertyName#!":
          return this.LocalizableFirstPropertyName(moduleType, culture);
        case "!#LocalizableParamCulture#!":
          return this.LocalizableParamCulture(moduleType);
        case "!#LocalizableParamCultureVBNet#!":
          return this.LocalizableParamCultureVBNet(moduleType);
        case "!#ManageRelatedItems#!":
          return this.GenerateManageRelatedItemsCode(moduleType);
        case "!#ManageRelatedItemsVB#!":
          return this.GenerateManageRelatedItemsVBCode(moduleType);
        case "!#ObjectDataForNewItem#!":
          return CodeArticleBase.GenerateObjectDataForNewItem(moduleType);
        case "!#ObjectDataForNewItemA#!":
          return CodeArticleBase.GenerateObjectDataForNewItemCustom(moduleType, "A");
        case "!#ObjectDataForNewItemB#!":
          return CodeArticleBase.GenerateObjectDataForNewItemCustom(moduleType, "B");
        case "!#ObjectDataNameForItemA#!":
          return string.Format("data{0}", (object) "A");
        case "!#ObjectDataNameForItemB#!":
          return string.Format("data{0}", (object) "B");
        case "!#ParentFullTypeName#!":
          return CodeArticleBase.GetParentFullTypeName(moduleType);
        case "!#ParentTypeNameCamel#!":
          return moduleType.ParentModuleType.TypeName.TocamelCase();
        case "!#SetChildCSharp#!":
          return this.GenerateSetChildCSharpCode(module, moduleType);
        case "!#SetChildVB#!":
          return this.GenerateSetChildVBCode(module, moduleType);
        case "!#SetCultureNameAndThreadCulture#!":
          return this.SetCultureNameAndThreadCulture(moduleType, culture);
        case "!#SetCultureNameAndThreadCultureVBNet#!":
          return this.SetCultureNameAndThreadCultureVBNet(moduleType, culture);
        case "!#SetLocalizableTitle#!":
          return this.SetLocalizableTitle(moduleType);
        case "!#SetLocalizableTitleVBnet#!":
          return this.SetLocalizableTitleVBnet(moduleType);
        case "!#SetParentCSharp#!":
          return this.GenerateSetParentCSharpCode(moduleType);
        case "!#SetParentVB#!":
          return this.GenerateSetParentVBNetCode(moduleType);
        case "!#TypeNameCamel#!":
          return moduleType.TypeName.TocamelCase();
        case "!#TypeNameNormal#!":
          return moduleType.DisplayName;
        case "!#TypeNamePascal#!":
          return moduleType.TypeName.ToPascalCase();
        case "!#TypeNamePlural#!":
          return this.PluralsNameResolver.ToPlural(moduleType.DisplayName);
        case "!#TypeNamePluralPascal#!":
          return this.PluralsNameResolver.ToPlural(moduleType.DisplayName).ToPascalCase();
        case "!#UpdateLocalizableTitle#!":
          return this.SetLocalizableTitle(moduleType, "Checked out item");
        case "!#UpdateLocalizableTitleVBnet#!":
          return this.SetLocalizableTitleVBnet(moduleType, "Checked out item");
        case "!#applicationName#!":
          return Config.Get<DynamicModulesConfig>().Providers.Values.FirstOrDefault<DataProviderSettings>().Parameters["applicationName"];
        case "!#prefix#!":
          return this.IndefiniteArticleNameResolver.Prefix;
        default:
          throw new NotSupportedException();
      }
    }

    private static string GenerateObjectDataForNewItemCustom(
      DynamicModuleType moduleType,
      string customInfo)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = "data" + customInfo;
      stringBuilder.Append("    var " + str + " = new Object();");
      stringBuilder.Append(" " + str + ".Item = new Object();");
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        string name = field.Name;
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    {0}.Item.{1} = \"{2} {3}\";", (object) str, (object) name, (object) name, (object) customInfo));
      }
      return stringBuilder.ToString();
    }

    private static string GenerateObjectDataForNewItem(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("    var data = new Object();");
      stringBuilder.Append(" data.Item = new Object();");
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        string name = field.Name;
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    data.Item.{0} = \"Some {0}\";", (object) name, (object) name));
      }
      return stringBuilder.ToString();
    }

    private string GeneratePropertiesCodeVBNet(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool isMediaFieldAdded = false;
      bool isClassificationFieldAdded = false;
      string itemVariableName = this.GetCreatedItemVariableName(moduleType.TypeName);
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (field.SpecialType == FieldSpecialType.None)
        {
          string fieldItemCodeVbNet = this.GenerateFieldItemCodeVBNet(field, itemVariableName, isMediaFieldAdded, isClassificationFieldAdded, this.IsMultilingual);
          stringBuilder.Append(fieldItemCodeVbNet);
          stringBuilder.AppendLine();
          if (field.FieldType == FieldType.Media)
            isMediaFieldAdded = true;
          else if (field.FieldType == FieldType.Classification)
            isClassificationFieldAdded = true;
        }
        else if (field.SpecialType == FieldSpecialType.UrlName)
        {
          string urlNameCodeVbNet = this.GenerateUrlNameCodeVBNet(itemVariableName, this.IsMultilingual);
          stringBuilder.Append(urlNameCodeVbNet);
          stringBuilder.AppendLine();
        }
        else if (field.SpecialType != FieldSpecialType.Actions && field.SpecialType != FieldSpecialType.UrlName)
        {
          string typeFieldCodeVbNet = CodeArticleBase.GenerateSpecialTypeFieldCodeVBNet(field, itemVariableName);
          stringBuilder.Append(typeFieldCodeVbNet);
          stringBuilder.AppendLine();
        }
      }
      string workflowStatusCodeVbNet = CodeArticleBase.GenerateItemWorkflowStatusCodeVBNet(itemVariableName, this.IsMultilingual);
      stringBuilder.Append(workflowStatusCodeVbNet);
      stringBuilder.AppendLine();
      return stringBuilder.ToString();
    }

    private string GeneratePropertiesCode(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool isMediaFieldAdded = false;
      bool isClassificationAdded = false;
      string itemVariableName = this.GetCreatedItemVariableName(moduleType.TypeName);
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (field.SpecialType == FieldSpecialType.None)
        {
          string fieldItemCode = this.GenerateFieldItemCode(field, itemVariableName, isMediaFieldAdded, isClassificationAdded, this.IsMultilingual);
          stringBuilder.Append(fieldItemCode);
          stringBuilder.AppendLine();
          if (field.FieldType == FieldType.Media)
            isMediaFieldAdded = true;
          else if (field.FieldType == FieldType.Classification)
            isClassificationAdded = true;
        }
        else if (field.SpecialType == FieldSpecialType.UrlName)
        {
          string urlNameCode = this.GenerateUrlNameCode(itemVariableName, this.IsMultilingual);
          stringBuilder.Append(urlNameCode);
          stringBuilder.AppendLine();
        }
        else if (field.SpecialType != FieldSpecialType.Actions && field.SpecialType != FieldSpecialType.UrlName)
        {
          string specialTypeFieldCode = CodeArticleBase.GenerateSpecialTypeFieldCode(field, itemVariableName);
          stringBuilder.Append(specialTypeFieldCode);
          stringBuilder.AppendLine();
        }
      }
      string workflowStatusCode = CodeArticleBase.GenerateItemWorkflowStatusCode(itemVariableName, this.IsMultilingual);
      stringBuilder.Append(workflowStatusCode);
      stringBuilder.AppendLine();
      return stringBuilder.ToString();
    }

    private static string GetFirstPropertyName(DynamicModuleType moduleType)
    {
      IEnumerable<DynamicModuleField> source = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.SpecialType == FieldSpecialType.None && f.FieldType == FieldType.ShortText));
      return source.Count<DynamicModuleField>() < 1 ? "YourPropertyNameHere" : source.First<DynamicModuleField>().Name;
    }

    private static string GetParentFullTypeName(DynamicModuleType moduleType) => moduleType.ParentModuleType.GetFullTypeName();

    private string GenerateFieldItemCode(
      DynamicModuleField field,
      string variableName,
      bool isMediaFieldAdded,
      bool isClassificationAdded,
      bool isModuleTypeMultilingual,
      string title = null)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      switch (field.FieldType)
      {
        case FieldType.ShortText:
        case FieldType.LongText:
          if (title.IsNullOrEmpty())
            title = string.Format("Some {0}", (object) field.Name);
          string str1 = string.Format("    {0}.SetValue(\"{1}\", \"{2}\"", (object) variableName, (object) field.Name, (object) title);
          if (isModuleTypeMultilingual && field.IsLocalizable)
            str1 = str1.Replace(".SetValue(", ".SetString(") + ", cultureName";
          string str2 = str1 + ");";
          stringBuilder1.Append(str2);
          break;
        case FieldType.MultipleChoice:
          stringBuilder1.AppendLine(string.Format("    // Set the selected value "));
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", new string[] {{ \"Option2\" }} );", (object) variableName, (object) field.Name));
          break;
        case FieldType.YesNo:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", true);", (object) variableName, (object) field.Name));
          break;
        case FieldType.DateTime:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", DateTime.Now);", (object) variableName, (object) field.Name));
          break;
        case FieldType.Number:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {2});", (object) variableName, (object) field.Name, (object) 25));
          break;
        case FieldType.Classification:
          string classificationFieldCode = CodeArticleBase.GenerateClassificationFieldCode(field, variableName, isClassificationAdded);
          stringBuilder1.Append(classificationFieldCode);
          break;
        case FieldType.Media:
          string mediaFieldCode = this.GenerateMediaFieldCode(field, variableName, isMediaFieldAdded);
          stringBuilder1.Append(mediaFieldCode);
          break;
        case FieldType.Guid:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", Guid.NewGuid());", (object) variableName, (object) field.Name));
          break;
        case FieldType.GuidArray:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", new Guid[] {{ Guid.NewGuid(), Guid.NewGuid() }});", (object) variableName, (object) field.Name));
          break;
        case FieldType.Choices:
          stringBuilder1.AppendLine(string.Format("    // Set the selected value "));
          if (field.CanSelectMultipleItems)
          {
            stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", new string[] {{ \"Option2\" }} );", (object) variableName, (object) field.Name));
            break;
          }
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\",  \"Option2\");", (object) variableName, (object) field.Name));
          break;
        case FieldType.Address:
          StringBuilder stringBuilder2 = new StringBuilder();
          string newValue1 = string.Format("{0}", (object) field.Name.TocamelCase());
          string newValue2 = string.Format("{0}Country", (object) field.Name.TocamelCase());
          stringBuilder2.Append("    Address #AddressFieldName# = new Address();");
          if (field.AddressFieldMode == AddressFieldMode.FormOnly || field.AddressFieldMode == AddressFieldMode.Hybrid)
          {
            stringBuilder2.AppendLine();
            stringBuilder2.Append("    CountryElement #CountryFieldName# = Config.Get&lt;LocationsConfig&gt;().Countries.Values.First(x => x.Name == \"United States\");");
            stringBuilder2.Append("\r\n    #AddressFieldName#.CountryCode = #CountryFieldName#.IsoCode;\r\n    #AddressFieldName#.StateCode = #CountryFieldName#.StatesProvinces.Values.First().Abbreviation;\r\n    #AddressFieldName#.City = \"Some City\";\r\n    #AddressFieldName#.Street = \"Some Street\";\r\n    #AddressFieldName#.Zip = \"12345\";");
          }
          if (field.AddressFieldMode == AddressFieldMode.MapOnly || field.AddressFieldMode == AddressFieldMode.Hybrid)
            stringBuilder2.Append("\r\n    #AddressFieldName#.Latitude = 0.00;\r\n    #AddressFieldName#.Longitude = 0.00;\r\n    #AddressFieldName#.MapZoomLevel = 8;");
          StringBuilder stringBuilder3 = stringBuilder2.Replace("#AddressFieldName#", newValue1).Replace("#CountryFieldName#", newValue2);
          stringBuilder1.Append(stringBuilder3.ToString());
          stringBuilder1.AppendLine();
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {2});", (object) variableName, (object) field.Name, (object) newValue1));
          stringBuilder1.AppendLine();
          break;
        case FieldType.RelatedMedia:
          stringBuilder1.AppendLine();
          stringBuilder1.Append(this.GenerateRelatedMediaFieldCCharpCode(field, variableName));
          break;
        case FieldType.RelatedData:
          stringBuilder1.AppendLine();
          stringBuilder1.Append(this.GenerateRelatedDataFieldCSharpCode(field, variableName));
          break;
        default:
          throw new NotSupportedException();
      }
      return stringBuilder1.ToString();
    }

    private static string GenerateSpecialTypeFieldCode(
      DynamicModuleField field,
      string variableName)
    {
      string specialTypeFieldCode = string.Empty;
      switch (field.SpecialType)
      {
        case FieldSpecialType.PublicationDate:
          specialTypeFieldCode = string.Format("    {0}.SetValue(\"{1}\", DateTime.UtcNow);", (object) variableName, (object) field.Name);
          break;
        case FieldSpecialType.Author:
          specialTypeFieldCode = string.Format("    {0}.SetValue(\"Owner\", SecurityManager.GetCurrentUserId());", (object) variableName);
          break;
      }
      return specialTypeFieldCode;
    }

    private static string GenerateClassificationFieldCode(
      DynamicModuleField field,
      string variableName,
      bool isClassificationAdded)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      string str = string.Empty;
      ITaxonomy taxonomy = TaxonomyManager.GetManager().GetTaxonomy(field.ClassificationId);
      string taxonName = (string) taxonomy.TaxonName;
      if (typeof (FlatTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        str = "FlatTaxon";
      else if (typeof (HierarchicalTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        str = "HierarchicalTaxon";
      if (!isClassificationAdded)
      {
        stringBuilder.AppendFormat(string.Format("    TaxonomyManager taxonomyManager = TaxonomyManager.GetManager();"));
        stringBuilder.AppendLine();
      }
      stringBuilder.AppendFormat("    var {0} = taxonomyManager.GetTaxa&lt;{1}&gt;().Where(t => t.Taxonomy.Name == \"{2}\").FirstOrDefault();", (object) taxonName, (object) str, (object) taxonomy.Name);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("    if({0} != null)", (object) taxonName);
      stringBuilder.AppendLine();
      stringBuilder.Append("    {");
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("        {0}.Organizer.AddTaxa(\"{1}\", {2}.Id);", (object) variableName, (object) field.Name, (object) taxonName);
      stringBuilder.AppendLine();
      stringBuilder.Append("    }");
      return stringBuilder.ToString();
    }

    private string GenerateMediaFieldCode(
      DynamicModuleField field,
      string variableName,
      bool isMediaFieldAdded)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!isMediaFieldAdded)
      {
        stringBuilder.Append(string.Format("    LibrariesManager libraryManager = LibrariesManager.GetManager();"));
        stringBuilder.AppendLine();
      }
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string mediaType = field.MediaType;
      if (!(mediaType == "image"))
      {
        if (!(mediaType == "video"))
        {
          if (mediaType == "file")
          {
            str1 = "document";
            str2 = "GetDocuments()";
            str3 = "AddFile";
          }
        }
        else
        {
          str1 = "video";
          str2 = "GetVideos()";
          str3 = "AddVideo";
        }
      }
      else
      {
        str1 = "image";
        str2 = "GetImages()";
        str3 = "AddImage";
      }
      stringBuilder.Append(string.Format("    var {0} = libraryManager.{1}.FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live);", (object) str1, (object) str2));
      stringBuilder.AppendLine();
      stringBuilder.Append(string.Format("    if({0} != null)", (object) str1));
      stringBuilder.AppendLine();
      stringBuilder.Append("    {");
      stringBuilder.AppendLine();
      stringBuilder.Append(string.Format("        {0}.{1}(\"{2}\", {3}.Id);", (object) variableName, (object) str3, (object) field.Name, (object) str1));
      stringBuilder.AppendLine();
      stringBuilder.Append("    }");
      return stringBuilder.ToString();
    }

    private string GenerateUrlNameCode(string variableName, bool isModuleTypeMultilingual)
    {
      string str = string.Format("    {0}.SetString(\"{1}\", \"Some{2}\"", (object) variableName, (object) this.urlFieldName, (object) this.urlFieldName);
      if (isModuleTypeMultilingual)
        str += ", cultureName";
      return str + ");";
    }

    private static string GenerateItemWorkflowStatusCode(
      string variableName,
      bool isModuleTypeMultilingual)
    {
      string str = string.Format("    {0}.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, \"Draft\"", (object) variableName);
      if (isModuleTypeMultilingual)
        str += " , new CultureInfo(cultureName)";
      return str + ");";
    }

    private string GenerateSetParentCSharpCode(DynamicModuleType moduleType) => moduleType.ParentModuleTypeId == Guid.Empty ? string.Empty : this.ContextRegex.Replace("\r\n    // Set item parent\r\n    Type !#ParentTypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#ParentFullTypeName#!\");\r\n    Guid parentId = this.GetParentId(dynamicModuleManager, !#ParentTypeNameCamel#!Type);\r\n    !#TypeNameCamel#!Item.SetParent(parentId, !#ParentTypeNameCamel#!Type.FullName);\r\n    ", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));

    private static string GenerateGetFirstParentIdCSharpCode(DynamicModuleType moduleType) => moduleType.ParentModuleTypeId == Guid.Empty ? string.Empty : "\r\n// Gets the id of the first item of the parent type\r\nprivate Guid GetParentId(DynamicModuleManager dynamicModuleManager, Type parentType)\r\n{\r\n    DynamicContent parent = dynamicModuleManager.GetDataItems(parentType)\r\n        .Where(i=>i.Status == ContentLifecycleStatus.Master).First();\r\n    return parent.Id;\r\n}";

    private string GenerateSetChildCSharpCode(DynamicModule module, DynamicModuleType moduleType)
    {
      IEnumerable<DynamicModuleType> source = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id));
      if (source.Count<DynamicModuleType>() == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\r\n    // This is how we add child items");
      foreach (DynamicModuleType dynamicModuleType in source)
      {
        DynamicModuleType childType = dynamicModuleType;
        string input = "\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type).FirstOrDefault();\r\n    if (!#TypeNameCamel#!Item != null)\r\n    {\r\n        !#ParentItem#!.AddChildItem(!#TypeNameCamel#!Item);\r\n    }\r\n".Replace("!#ParentItem#!", moduleType.TypeName.TocamelCase() + "Item");
        stringBuilder.Append(this.ContextRegex.Replace(input, (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, childType))));
      }
      return stringBuilder.ToString();
    }

    private string GenerateRelatedMediaFieldCCharpCode(
      DynamicModuleField field,
      string variableName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      string relatedDataType = field.RelatedDataType;
      string newValue1;
      if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Image"))
      {
        if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Video"))
        {
          if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Document"))
            return string.Empty;
          newValue1 = "GetDocuments()";
        }
        else
          newValue1 = "GetVideos()";
      }
      else
        newValue1 = "GetImages()";
      stringBuilder.Append("    // #RelatedFieldComment#\r\n    LibrariesManager #FieldNameToCamelCase#Manager = LibrariesManager.GetManager(#RelatedTypeProvider#);\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.#LibraryGetMethod#.FirstOrDefault(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master);\r\n    if (#FieldNameToCamelCase#Item != null)\r\n    {\r\n        // This is how we relate an item\r\n        #VariableName#.CreateRelation(#FieldNameToCamelCase#Item, \"#FieldName#\");\r\n    }");
      string newValue2 = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Libraries.Configuration.LibrariesConfig");
      string relatedFieldComment = CodeArticleBase.GetRelatedFieldComment(field.RelatedDataProvider);
      stringBuilder.Replace("#FieldName#", field.Name);
      stringBuilder.Replace("#FieldNameToCamelCase#", field.Name.TocamelCase());
      stringBuilder.Replace("#RelatedTypeProvider#", newValue2);
      stringBuilder.Replace("#RelatedFieldComment#", relatedFieldComment);
      stringBuilder.Replace("#VariableName#", variableName);
      stringBuilder.Replace("#LibraryGetMethod#", newValue1);
      this.RelatedDataNamespaces.Add("Telerik.Sitefinity.RelatedData");
      return stringBuilder.ToString();
    }

    private string GenerateRelatedDataFieldCSharpCode(DynamicModuleField field, string variableName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string relatedDataType = field.RelatedDataType;
      string newValue = string.Empty;
      if (!(relatedDataType == "Telerik.Sitefinity.News.Model.NewsItem"))
      {
        if (!(relatedDataType == "Telerik.Sitefinity.Blogs.Model.BlogPost"))
        {
          if (!(relatedDataType == "Telerik.Sitefinity.Events.Model.Event"))
          {
            if (relatedDataType == "Telerik.Sitefinity.Pages.Model.PageNode")
            {
              stringBuilder.Append("    PageManager #FieldNameToCamelCase#Manager = PageManager.GetManager();\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.GetPageNodes().FirstOrDefault(p => p.Id == SystemManager.CurrentContext.CurrentSite.HomePageId);");
              this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Pages");
              this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Services");
            }
            else
            {
              newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.DynamicModules.Configuration.DynamicModulesConfig");
              stringBuilder.Append("    // #RelatedFieldComment#\r\n    DynamicModuleManager #FieldNameToCamelCase#Manager = DynamicModuleManager.GetManager(#RelatedTypeProvider#);\r\n    var #FieldNameToCamelCase#Type = TypeResolutionService.ResolveType(\"#FieldRelatedTypeName#\");\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.GetDataItems(#FieldNameToCamelCase#Type).FirstOrDefault(d => d.Status == ContentLifecycleStatus.Master);");
            }
          }
          else
          {
            newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Events.Configuration.EventsConfig");
            stringBuilder.Append("    // #RelatedFieldComment#\r\n    EventsManager #FieldNameToCamelCase#Manager = EventsManager.GetManager(#RelatedTypeProvider#);\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.GetEvents().FirstOrDefault(e => e.Status == ContentLifecycleStatus.Master);");
            this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Events");
          }
        }
        else
        {
          newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Blogs.Configuration.BlogsConfig");
          stringBuilder.Append("    // #RelatedFieldComment#\r\n    BlogsManager #FieldNameToCamelCase#Manager = BlogsManager.GetManager(#RelatedTypeProvider#);\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.GetBlogPosts().FirstOrDefault(b => b.Status == ContentLifecycleStatus.Master);");
          this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Blogs");
        }
      }
      else
      {
        newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.News.Configuration.NewsConfig");
        stringBuilder.Append("    // #RelatedFieldComment#\r\n    NewsManager #FieldNameToCamelCase#Manager = NewsManager.GetManager(#RelatedTypeProvider#);\r\n    var #FieldNameToCamelCase#Item = #FieldNameToCamelCase#Manager.GetNewsItems().FirstOrDefault(n => n.Status == ContentLifecycleStatus.Master);");
        this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.News");
      }
      stringBuilder.AppendLine();
      stringBuilder.Append("    if (#FieldNameToCamelCase#Item != null)\r\n    {\r\n        // This is how we relate an item\r\n        #VariableName#.CreateRelation(#FieldNameToCamelCase#Item, \"#FieldName#\");\r\n    }");
      string relatedFieldComment = CodeArticleBase.GetRelatedFieldComment(field.RelatedDataProvider);
      stringBuilder.Replace("#FieldName#", field.Name);
      stringBuilder.Replace("#FieldNameToCamelCase#", field.Name.TocamelCase());
      stringBuilder.Replace("#RelatedTypeProvider#", newValue);
      stringBuilder.Replace("#RelatedFieldComment#", relatedFieldComment);
      stringBuilder.Replace("#VariableName#", variableName);
      stringBuilder.Replace("#FieldRelatedTypeName#", field.RelatedDataType);
      this.RelatedDataNamespaces.Add("Telerik.Sitefinity.RelatedData");
      return stringBuilder.ToString();
    }

    private static string GetRelatedFieldComment(string fieldProviderName)
    {
      string relatedFieldComment = "Get related item manager";
      if (fieldProviderName == "sf-any-site-provider")
        relatedFieldComment = "When the data source for the related data field is \"All sources for the current site\", you can instantiate the manager with any of the providers and thus relate items from them";
      return relatedFieldComment;
    }

    private static string IncludeProviderNameForModule(
      string fieldProviderName,
      string configTypeName)
    {
      string str = string.Empty;
      if (fieldProviderName != "sf-any-site-provider" && fieldProviderName != "sf-site-default-provider")
      {
        bool flag = false;
        Type sectionType = TypeResolutionService.ResolveType(configTypeName, false);
        if (sectionType != (Type) null)
        {
          try
          {
            if (Config.Get(sectionType) is IModuleConfig moduleConfig)
              flag = moduleConfig.Providers.Values.Count<DataProviderSettings>((Func<DataProviderSettings, bool>) (p => !p.Parameters.Keys.Contains("providerGroup") || !"System".Equals(p.Parameters["providerGroup"]))) > 1;
          }
          catch
          {
            flag = false;
          }
        }
        if (flag)
          str = fieldProviderName;
      }
      if (!str.IsNullOrEmpty())
        str = "\"" + str + "\"";
      return str;
    }

    private string LocalizableParamCulture(DynamicModuleType moduleType)
    {
      string str = string.Empty;
      if (this.IsMultilingual)
        str = ", string cultureName";
      return str;
    }

    private string LocalizableFirstPropertyName(DynamicModuleType moduleType, CultureInfo culture = null) => this.ContextRegex.Replace("!#FirstPropertyName#!", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));

    private string SetCultureNameAndThreadCulture(DynamicModuleType moduleType, CultureInfo culture = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsMultilingual)
      {
        string languageKey = culture.GetLanguageKey();
        stringBuilder.AppendLine();
        stringBuilder.Append("    // Set the culture name for the multilingual fields");
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    var cultureName = \"{0}\";", (object) languageKey));
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = new CultureInfo(cultureName);"));
        stringBuilder.AppendLine();
      }
      return stringBuilder.ToString();
    }

    private string CheckInCheckOutItem(DynamicModuleType moduleType, CultureInfo culture = null)
    {
      string input = string.Empty;
      if (this.IsMultilingual)
        input = "\r\n    // Use lifecycle so that LanguageData and other Multilingual related values are correctly created\r\n    DynamicContent checkOut!#TypeNamePascal#!Item = dynamicModuleManager.Lifecycle.CheckOut(!#TypeNameCamel#!Item) as DynamicContent;\r\n    DynamicContent checkIn!#TypeNamePascal#!Item = dynamicModuleManager.Lifecycle.CheckIn(checkOut!#TypeNamePascal#!Item) as DynamicContent;\r\n    versionManager.CreateVersion(checkIn!#TypeNamePascal#!Item, false);\r\n    TransactionManager.CommitTransaction(transactionName);\r\n";
      return this.ContextRegex.Replace(input, (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    private string LocalizableCulture(DynamicModuleType moduleType)
    {
      string str = string.Empty;
      if (this.IsMultilingual)
        str = ", cultureName";
      return str;
    }

    private string SetLocalizableTitle(DynamicModuleType moduleType, string title = null)
    {
      string str = string.Empty;
      string variableName = "checkOut" + moduleType.TypeName.ToPascalCase() + "Item";
      DynamicModuleField field = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == moduleType.MainShortTextFieldName)).FirstOrDefault<DynamicModuleField>();
      if (field != null)
        str = this.GenerateFieldItemCode(field, variableName, false, false, this.IsMultilingual, title);
      return str;
    }

    private string GenerateManageRelatedItemsCode(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string itemVariableName = this.GetCreatedItemVariableName(moduleType.TypeName);
      string fullTypeName = moduleType.GetFullTypeName();
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (field.FieldType == FieldType.RelatedData || field.FieldType == FieldType.RelatedMedia)
        {
          stringBuilder.AppendLine("\r\n    // This is how we get related child items\r\n    IDataItem #ChildItem# = #DynamicItem#.GetRelatedItems(\"#FieldName#\").FirstOrDefault();\r\n    if (#ChildItem# != null)\r\n    {\r\n      // This is how we get related parent items (of type #ModuleTypeName#)\r\n      IDataItem #ParentItem# = #ChildItem#.GetRelatedParentItems(\"#ModuleTypeFullName#\").FirstOrDefault();\r\n      // This is how we delete a relation\r\n      #DynamicItem#.DeleteRelation(#ChildItem#, \"#FieldName#\");\r\n    }");
          stringBuilder.Replace("#ChildItem#", field.Name.TocamelCase() + "Item");
          stringBuilder.Replace("#DynamicItem#", itemVariableName);
          stringBuilder.Replace("#FieldName#", field.Name);
          stringBuilder.Replace("#ModuleTypeFullName#", fullTypeName);
          stringBuilder.Replace("#ModuleTypeName#", moduleType.TypeName);
          stringBuilder.Replace("#ParentItem#", field.Name.TocamelCase() + "Parent" + moduleType.TypeName + "Item");
        }
      }
      return stringBuilder.ToString();
    }

    private string GenerateFieldItemCodeVBNet(
      DynamicModuleField field,
      string variableName,
      bool isMediaFieldAdded,
      bool isClassificationFieldAdded,
      bool isModuleTypeMultilingual,
      string title = null)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      switch (field.FieldType)
      {
        case FieldType.ShortText:
        case FieldType.LongText:
          if (title.IsNullOrEmpty())
            title = string.Format("Some {0}", (object) field.Name);
          string str1 = string.Format("    {0}.SetValue(\"{1}\", \"{2}\"", (object) variableName, (object) field.Name, (object) title);
          if (isModuleTypeMultilingual && field.IsLocalizable)
            str1 = str1.Replace(".SetValue(", ".SetString(") + ", cultureName";
          string str2 = str1 + ")";
          stringBuilder1.Append(str2);
          break;
        case FieldType.MultipleChoice:
          stringBuilder1.AppendLine(string.Format("    ' Set the selected value "));
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {{ \"Option2\" }})", (object) variableName, (object) field.Name));
          break;
        case FieldType.YesNo:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", true)", (object) variableName, (object) field.Name));
          break;
        case FieldType.DateTime:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\",  DateTime.Now)", (object) variableName, (object) field.Name));
          break;
        case FieldType.Number:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {2})", (object) variableName, (object) field.Name, (object) 25));
          break;
        case FieldType.Classification:
          string classificationFieldCodeVbNet = CodeArticleBase.GenerateClassificationFieldCodeVBNet(field, variableName, isClassificationFieldAdded);
          stringBuilder1.Append(classificationFieldCodeVbNet);
          break;
        case FieldType.Media:
          string mediaFieldCodeVbNet = this.GenerateMediaFieldCodeVBNet(field, variableName, isMediaFieldAdded);
          stringBuilder1.Append(mediaFieldCodeVbNet);
          break;
        case FieldType.Guid:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\",  Guid.NewGuid())", (object) variableName, (object) field.Name));
          break;
        case FieldType.GuidArray:
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {{ Guid.NewGuid(), Guid.NewGuid() }})", (object) variableName, (object) field.Name));
          break;
        case FieldType.Choices:
          stringBuilder1.AppendLine(string.Format("    ' Set the selected value "));
          if (field.CanSelectMultipleItems)
          {
            stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {{ \"Option2\" }})", (object) variableName, (object) field.Name));
            break;
          }
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", \"Option2\")", (object) variableName, (object) field.Name));
          break;
        case FieldType.Address:
          StringBuilder stringBuilder2 = new StringBuilder();
          string newValue1 = string.Format("{0}", (object) field.Name.TocamelCase());
          string newValue2 = string.Format("{0}Country", (object) field.Name.TocamelCase());
          stringBuilder2.Append("    Dim #AddressFieldName# As New Address()");
          if (field.AddressFieldMode == AddressFieldMode.FormOnly || field.AddressFieldMode == AddressFieldMode.Hybrid)
          {
            stringBuilder2.AppendLine();
            stringBuilder2.Append("    Dim #CountryFieldName# As CountryElement = Config.[Get](Of LocationsConfig)().Countries.Values.First(Function(x) x.Name = \"United States\")");
            stringBuilder2.Append("\r\n    #AddressFieldName#.CountryCode = #CountryFieldName#.IsoCode\r\n    #AddressFieldName#.StateCode = #CountryFieldName#.StatesProvinces.Values.First().Abbreviation\r\n    #AddressFieldName#.City = \"Some City\"\r\n    #AddressFieldName#.Street = \"Some Street\"\r\n    #AddressFieldName#.Zip = \"12345\"");
          }
          if (field.AddressFieldMode == AddressFieldMode.MapOnly || field.AddressFieldMode == AddressFieldMode.Hybrid)
            stringBuilder2.Append("\r\n    #AddressFieldName#.Latitude = 0.00\r\n    #AddressFieldName#.Longitude = 0.00\r\n    #AddressFieldName#.MapZoomLevel = 8");
          StringBuilder stringBuilder3 = stringBuilder2.Replace("#AddressFieldName#", newValue1).Replace("#CountryFieldName#", newValue2);
          stringBuilder1.Append(stringBuilder3.ToString());
          stringBuilder1.AppendLine();
          stringBuilder1.Append(string.Format("    {0}.SetValue(\"{1}\", {2})", (object) variableName, (object) field.Name, (object) newValue1));
          stringBuilder1.AppendLine();
          break;
        case FieldType.RelatedMedia:
          stringBuilder1.AppendLine();
          stringBuilder1.Append(this.GenerateRelatedMediaFieldVBCode(field, variableName));
          break;
        case FieldType.RelatedData:
          stringBuilder1.AppendLine();
          stringBuilder1.Append(this.GenerateRelatedDataFieldVBCode(field, variableName));
          break;
        default:
          throw new NotSupportedException();
      }
      return stringBuilder1.ToString();
    }

    private static string GenerateSpecialTypeFieldCodeVBNet(
      DynamicModuleField field,
      string variableName)
    {
      string typeFieldCodeVbNet = string.Empty;
      switch (field.SpecialType)
      {
        case FieldSpecialType.PublicationDate:
          typeFieldCodeVbNet = string.Format("    {0}.SetValue(\"{1}\", DateTime.Now)", (object) variableName, (object) field.Name);
          break;
        case FieldSpecialType.Author:
          typeFieldCodeVbNet = string.Format("    {0}.SetValue(\"Owner\", SecurityManager.GetCurrentUserId())", (object) variableName);
          break;
      }
      return typeFieldCodeVbNet;
    }

    private string GenerateMediaFieldCodeVBNet(
      DynamicModuleField field,
      string variableName,
      bool isMediaFieldAdded)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!isMediaFieldAdded)
      {
        stringBuilder.Append(string.Format("    Dim libraryManager As LibrariesManager = LibrariesManager.GetManager()"));
        stringBuilder.AppendLine();
      }
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string str4 = string.Empty;
      string mediaType = field.MediaType;
      if (!(mediaType == "image"))
      {
        if (!(mediaType == "video"))
        {
          if (mediaType == "file")
          {
            str1 = "document";
            str2 = "Document";
            str3 = "GetDocuments()";
            str4 = "AddFile";
          }
        }
        else
        {
          str1 = "video";
          str2 = "Video";
          str3 = "GetVideos()";
          str4 = "AddVideo";
        }
      }
      else
      {
        str1 = "image";
        str2 = "Image";
        str3 = "GetImages()";
        str4 = "AddImage";
      }
      stringBuilder.Append(string.Format("    Dim {0} As Telerik.Sitefinity.Libraries.Model.{1} = libraryManager.{2}.FirstOrDefault(Function(i) i.Status = Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live)", (object) str1, (object) str2, (object) str3));
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat(string.Format("    if Not {0} Is Nothing Then", (object) str1));
      stringBuilder.AppendLine();
      stringBuilder.Append(string.Format("        {0}.{1}(\"{2}\", {3}.Id)", (object) variableName, (object) str4, (object) field.Name, (object) str1));
      stringBuilder.AppendLine();
      stringBuilder.Append("    End If");
      return stringBuilder.ToString();
    }

    private static string GenerateClassificationFieldCodeVBNet(
      DynamicModuleField field,
      string variableName,
      bool isClassificationAdded)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      string str = string.Empty;
      ITaxonomy taxonomy = TaxonomyManager.GetManager().GetTaxonomy(field.ClassificationId);
      string taxonName = (string) taxonomy.TaxonName;
      if (typeof (FlatTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        str = "FlatTaxon";
      else if (typeof (HierarchicalTaxonomy).IsAssignableFrom(taxonomy.GetType()))
        str = "HierarchicalTaxon";
      if (!isClassificationAdded)
      {
        stringBuilder.AppendFormat(string.Format("    Dim taxonomyManager As TaxonomyManager = TaxonomyManager.GetManager()"));
        stringBuilder.AppendLine();
      }
      stringBuilder.AppendFormat("    Dim {0} = taxonomyManager.GetTaxa(Of {1})().Where(Function(t) t.Taxonomy.Name = \"{2}\").FirstOrDefault()", (object) taxonName, (object) str, (object) taxonomy.Name);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("    If Not {0} Is Nothing Then", (object) taxonName);
      stringBuilder.AppendLine();
      stringBuilder.AppendFormat("        {0}.Organizer.AddTaxa(\"{1}\", {2}.Id)", (object) variableName, (object) field.Name, (object) taxonName);
      stringBuilder.AppendLine();
      stringBuilder.Append("    End If");
      return stringBuilder.ToString();
    }

    private string GenerateUrlNameCodeVBNet(string variableName, bool isDynamicTypeMultilingual)
    {
      string str = string.Format("    {0}.SetString(\"{1}\", \"Some{2}\"", (object) variableName, (object) this.urlFieldName, (object) this.urlFieldName);
      if (isDynamicTypeMultilingual)
        str += ", cultureName";
      return str + ")";
    }

    private static string GenerateItemWorkflowStatusCodeVBNet(
      string variableName,
      bool isModuleTypeMultilingual)
    {
      string str = string.Format("    {0}.SetWorkflowStatus(dynamicModuleManager_.Provider.ApplicationName, \"Draft\"", (object) variableName);
      if (isModuleTypeMultilingual)
        str += " , New CultureInfo(cultureName)";
      return str + ")";
    }

    private string GenerateSetParentVBNetCode(DynamicModuleType moduleType) => moduleType.ParentModuleTypeId == Guid.Empty ? string.Empty : this.ContextRegex.Replace("\r\n    ' Set item parent\r\n    Dim !#ParentTypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#ParentFullTypeName#!\")\r\n    Dim parentId As Guid = GetParentId(dynamicModuleManager_, !#ParentTypeNameCamel#!Type)\r\n    !#TypeNameCamel#!Item.SetParent(parentId, !#ParentTypeNameCamel#!Type.FullName)\r\n    ", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));

    private static string GenerateGetFirstParentIdVBNetCode(DynamicModuleType moduleType) => moduleType.ParentModuleTypeId == Guid.Empty ? string.Empty : "\r\n' Gets the id of the first item of the parent type\r\nPrivate Function GetParentId(dynamicModuleMngr_ As DynamicModuleManager, parentType As Type) As Guid\r\n            \r\n    Dim parent As DynamicContent = dynamicModuleMngr_.GetDataItems(parentType) _\r\n        .Where(Function(i) i.Status = ContentLifecycleStatus.Master).First()\r\n    Return parent.Id\r\n\r\nEnd Function";

    private string GenerateSetChildVBCode(DynamicModule module, DynamicModuleType moduleType)
    {
      IEnumerable<DynamicModuleType> source = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id));
      if (source.Count<DynamicModuleType>() == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\r\n    ' This is how we add child items");
      foreach (DynamicModuleType dynamicModuleType in source)
      {
        DynamicModuleType childType = dynamicModuleType;
        string input = "\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.GetDataItems(!#TypeNameCamel#!Type).FirstOrDefault()\r\n    If !#TypeNameCamel#!Item IsNot Nothing Then\r\n        !#ParentItem#!.AddChildItem(!#TypeNameCamel#!Item)\r\n    End If\r\n".Replace("!#ParentItem#!", moduleType.TypeName.TocamelCase() + "Item");
        stringBuilder.Append(this.ContextRegex.Replace(input, (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, childType))));
      }
      return stringBuilder.ToString();
    }

    private string GenerateRelatedMediaFieldVBCode(DynamicModuleField field, string variableName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string empty = string.Empty;
      string relatedDataType = field.RelatedDataType;
      string newValue1;
      if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Image"))
      {
        if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Video"))
        {
          if (!(relatedDataType == "Telerik.Sitefinity.Libraries.Model.Document"))
            return string.Empty;
          newValue1 = "GetDocuments()";
        }
        else
          newValue1 = "GetVideos()";
      }
      else
        newValue1 = "GetImages()";
      stringBuilder.Append("    '  #RelatedFieldComment#\r\n    Dim #FieldNameToCamelCase#Manager As LibrariesManager = LibrariesManager.GetManager(#RelatedTypeProvider#)\r\n    Dim #FieldNameToCamelCase#Item As #RelatedType# = #FieldNameToCamelCase#Manager.#LibraryGetMethod#.FirstOrDefault(Function(i) i.Status = Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master)\r\n    If #FieldNameToCamelCase#Item IsNot Nothing Then\r\n        ' This is how we relate an item\r\n        #VariableName#.CreateRelation(#FieldNameToCamelCase#Item, \"#FieldName#\")\r\n    End If");
      string newValue2 = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Libraries.Configuration.LibrariesConfig");
      string relatedFieldComment = CodeArticleBase.GetRelatedFieldComment(field.RelatedDataProvider);
      stringBuilder.Replace("#FieldName#", field.Name);
      stringBuilder.Replace("#FieldNameToCamelCase#", field.Name.TocamelCase());
      stringBuilder.Replace("#RelatedTypeProvider#", newValue2);
      stringBuilder.Replace("#RelatedFieldComment#", relatedFieldComment);
      stringBuilder.Replace("#VariableName#", variableName);
      stringBuilder.Replace("#LibraryGetMethod#", newValue1);
      stringBuilder.Replace("#RelatedType#", field.RelatedDataType);
      this.RelatedDataNamespaces.Add("Telerik.Sitefinity.RelatedData");
      return stringBuilder.ToString();
    }

    private string GenerateRelatedDataFieldVBCode(DynamicModuleField field, string variableName)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string relatedDataType = field.RelatedDataType;
      string newValue = string.Empty;
      if (!(relatedDataType == "Telerik.Sitefinity.News.Model.NewsItem"))
      {
        if (!(relatedDataType == "Telerik.Sitefinity.Blogs.Model.BlogPost"))
        {
          if (!(relatedDataType == "Telerik.Sitefinity.Events.Model.Event"))
          {
            if (relatedDataType == "Telerik.Sitefinity.Pages.Model.PageNode")
            {
              stringBuilder.Append("    Dim #FieldNameToCamelCase#Manager As PageManager = PageManager.GetManager()\r\n    Dim #FieldNameToCamelCase#Item As Telerik.Sitefinity.Pages.Model.PageNode = #FieldNameToCamelCase#Manager.GetPageNodes().FirstOrDefault(Function(p) p.Id = SystemManager.CurrentContext.CurrentSite.HomePageId)");
              this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Pages");
              this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Services");
            }
            else
            {
              newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.DynamicModules.Configuration.DynamicModulesConfig");
              stringBuilder.Append("    '  #RelatedFieldComment#\r\n    Dim #FieldNameToCamelCase#Manager As DynamicModuleManager = DynamicModuleManager.GetManager(#RelatedTypeProvider#)\r\n    Dim #FieldNameToCamelCase#Type As Type = TypeResolutionService.ResolveType(\"#FieldRelatedTypeName#\")\r\n    Dim #FieldNameToCamelCase#Item As DynamicContent = #FieldNameToCamelCase#Manager.GetDataItems(#FieldNameToCamelCase#Type).FirstOrDefault(Function(d) d.Status = ContentLifecycleStatus.Master)");
            }
          }
          else
          {
            newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Events.Configuration.EventsConfig");
            stringBuilder.Append("    '  #RelatedFieldComment#\r\n    Dim #FieldNameToCamelCase#Manager As EventsManager = EventsManager.GetManager(#RelatedTypeProvider#)\r\n    Dim #FieldNameToCamelCase#Item As Telerik.Sitefinity.Events.Model.Event = #FieldNameToCamelCase#Manager.GetEvents().FirstOrDefault(Function(e) e.Status = ContentLifecycleStatus.Master)");
            this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Events");
          }
        }
        else
        {
          newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.Blogs.Configuration.BlogsConfig");
          stringBuilder.Append("    '  #RelatedFieldComment#\r\n    Dim #FieldNameToCamelCase#Manager As BlogsManager = BlogsManager.GetManager(#RelatedTypeProvider#)\r\n    Dim #FieldNameToCamelCase#Item As Telerik.Sitefinity.Blogs.Model.BlogPost = #FieldNameToCamelCase#Manager.GetBlogPosts().FirstOrDefault(Function(b) b.Status = ContentLifecycleStatus.Master)");
          this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.Blogs");
        }
      }
      else
      {
        newValue = CodeArticleBase.IncludeProviderNameForModule(field.RelatedDataProvider, "Telerik.Sitefinity.Modules.News.Configuration.NewsConfig");
        stringBuilder.Append("    '  #RelatedFieldComment#\r\n    Dim #FieldNameToCamelCase#Manager As NewsManager = NewsManager.GetManager(#RelatedTypeProvider#)\r\n    Dim #FieldNameToCamelCase#Item As Telerik.Sitefinity.News.Model.NewsItem = #FieldNameToCamelCase#Manager.GetNewsItems().FirstOrDefault(Function(n) n.Status = ContentLifecycleStatus.Master)");
        this.RelatedDataNamespaces.Add("Telerik.Sitefinity.Modules.News");
      }
      stringBuilder.AppendLine();
      stringBuilder.Append("    If #FieldNameToCamelCase#Item IsNot Nothing Then\r\n        ' This is how we relate an item\r\n        #VariableName#.CreateRelation(#FieldNameToCamelCase#Item, \"#FieldName#\")\r\n    End If");
      string relatedFieldComment = CodeArticleBase.GetRelatedFieldComment(field.RelatedDataProvider);
      stringBuilder.Replace("#FieldName#", field.Name);
      stringBuilder.Replace("#FieldNameToCamelCase#", field.Name.TocamelCase());
      stringBuilder.Replace("#RelatedTypeProvider#", newValue);
      stringBuilder.Replace("#RelatedFieldComment#", relatedFieldComment);
      stringBuilder.Replace("#VariableName#", variableName);
      stringBuilder.Replace("#FieldRelatedTypeName#", field.RelatedDataType);
      this.RelatedDataNamespaces.Add("Telerik.Sitefinity.RelatedData");
      return stringBuilder.ToString();
    }

    private string LocalizableParamCultureVBNet(DynamicModuleType moduleType)
    {
      string str = string.Empty;
      if (this.IsMultilingual)
        str = ", cultureName As String";
      return str;
    }

    private string CheckInCheckOutItemVBNet(DynamicModuleType moduleType)
    {
      string input = string.Empty;
      if (this.IsMultilingual)
        input = "\r\n    ' Use lifecycle so that LanguageData and other Multilingual related values are correctly created\r\n    Dim checkOut!#TypeNamePascal#!Item As DynamicContent = dynamicModuleManager_.Lifecycle.CheckOut(!#TypeNameCamel#!Item)\r\n    Dim checkIn!#TypeNamePascal#!Item As ILifecycleDataItem = dynamicModuleManager_.Lifecycle.CheckIn(checkOut!#TypeNamePascal#!Item)\r\n    versionManager.CreateVersion(checkIn!#TypeNamePascal#!Item, true)\r\n    TransactionManager.CommitTransaction(transactionName)\r\n";
      return this.ContextRegex.Replace(input, (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));
    }

    private string SetCultureNameAndThreadCultureVBNet(
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsMultilingual)
      {
        string languageKey = culture.GetLanguageKey();
        stringBuilder.AppendLine();
        stringBuilder.Append("    ' Set the culture name for the multilingual fields");
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    Dim cultureName As String = \"{0}\"", (object) languageKey));
        stringBuilder.AppendLine();
        stringBuilder.Append(string.Format("    Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture = New CultureInfo(cultureName)"));
        stringBuilder.AppendLine();
      }
      return stringBuilder.ToString();
    }

    private string SetLocalizableTitleVBnet(DynamicModuleType moduleType, string title = null)
    {
      string str = string.Empty;
      string variableName = "checkOut" + moduleType.TypeName.ToPascalCase() + "Item";
      DynamicModuleField field = ((IEnumerable<DynamicModuleField>) moduleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == moduleType.MainShortTextFieldName)).FirstOrDefault<DynamicModuleField>();
      if (field != null)
        str = this.GenerateFieldItemCodeVBNet(field, variableName, false, false, this.IsMultilingual, title);
      return str;
    }

    private string GenerateManageRelatedItemsVBCode(DynamicModuleType moduleType)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string itemVariableName = this.GetCreatedItemVariableName(moduleType.TypeName);
      string fullTypeName = moduleType.GetFullTypeName();
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (field.FieldType == FieldType.RelatedData || field.FieldType == FieldType.RelatedMedia)
        {
          stringBuilder.AppendLine("\r\n    ' This is how we get related child items\r\n    Dim #ChildItem# As IDataItem = #DynamicItem#.GetRelatedItems(\"#FieldName#\").FirstOrDefault()\r\n    If #ChildItem# IsNot Nothing Then\r\n      ' This is how we get related parent items (of type #ModuleTypeName#)\r\n      Dim #ParentItem# As IDataItem = #ChildItem#.GetRelatedParentItems(\"#ModuleTypeFullName#\").FirstOrDefault()\r\n      ' This is how we delete a relation\r\n      #DynamicItem#.DeleteRelation(#ChildItem#, \"#FieldName#\")\r\n    End If");
          stringBuilder.Replace("#ChildItem#", field.Name.TocamelCase() + "Item");
          stringBuilder.Replace("#DynamicItem#", itemVariableName);
          stringBuilder.Replace("#FieldName#", field.Name);
          stringBuilder.Replace("#ModuleTypeFullName#", fullTypeName);
          stringBuilder.Replace("#ModuleTypeName#", moduleType.TypeName);
          stringBuilder.Replace("#ParentItem#", field.Name.TocamelCase() + "Parent" + moduleType.TypeName + "Item");
        }
      }
      return stringBuilder.ToString();
    }
  }
}
