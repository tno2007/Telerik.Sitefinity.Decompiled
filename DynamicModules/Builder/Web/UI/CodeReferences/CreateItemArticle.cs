// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.CreateItemArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class CreateItemArticle : ServerSideCodeArticleBase
  {
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
    public override string GenerateArticleTitle(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return string.Format("Create {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
    }

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
    public override string GenerateArticleDescription(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("This is a description for creating !#prefix#! !#TypeNameNormal#!. In order to create !#prefix#! !#TypeNameNormal#! content item,  we will use the \r\n            <br/>DynamicModuleManager's CreateDataItem() method. \r\n            <br/>Let's see a basic code example:", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    /// <summary>
    /// Generates the contextualized code sample in c# syntax for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in c# syntax.</returns>
    protected override string GenerateCSharpCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n// Creates a new !#TypeNameCamel#! item\r\npublic void Create!#TypeNamePascal#!()\r\n{\r\n    // Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    // Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    var providerName = String.Empty;\r\n\r\n    // Set a transaction name and get the version manager\r\n    var transactionName = \"someTransactionName\";\r\n    var versionManager = VersionManager.GetManager(null, transactionName);\r\n    !#SetCultureNameAndThreadCulture#!\r\n    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.CreateDataItem(!#TypeNameCamel#!Type);\r\n                \r\n    // This is how values for the properties are set\r\n!#DynamicPropertySetting#!!#SetParentCSharp#! !#SetChildCSharp#!\r\n    // Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false);\r\n    TransactionManager.CommitTransaction(transactionName);\r\n!#CheckInCheckOutItem#!}\r\n!#GetFirstParentIdCSharp#!", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture, module)));
    }

    /// <summary>
    /// Generates the contextualized code sample in VB.NET syntax for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in VB.NET syntax.</returns>
    protected override string GenerateVBCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n'Creates a new book item\r\nPublic Sub Create!#TypeNamePascal#!()\r\n\r\n    ' Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    ' Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    Dim providerName As String = String.Empty\r\n\r\n    ' Set a transaction name and get the version manager\r\n    Dim transactionName As String = \"someTransactionName\"\r\n    Dim versionManager As VersionManager = VersionManager.GetManager(Nothing, transactionName)\r\n    !#SetCultureNameAndThreadCultureVBNet#!\r\n    Dim dynamicModuleManager_ As DynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName)\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.CreateDataItem(!#TypeNameCamel#!Type)\r\n            \r\n    ' This is how values for the properties are set\r\n!#DynamicPropertySettingVBNet#!!#SetParentVB#! !#SetChildVB#!\r\n    ' Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false)\r\n    TransactionManager.CommitTransaction(transactionName)\r\n\r\n!#CheckInCheckOutItemVBNet#!\r\nEnd Sub\r\n!#GetFirstParentIdVB#!", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture, module)));
    }

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
    protected override string GenerateCodeExplanation(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we are getting an instance of DynamicModuleManager class (Defined in Telerik.Sitefinity.DynamicModules namespace).</li> \r\n                <li>Then, we are invoking the CreateDataItem() method, which returns an instance of !#TypeNameNormal#! Item. </li>\r\n                <li>Once we have the item created, some properties should be set.</li>\r\n                <li>In the end, we call CommitTransaction() method to persist the item to the data store.</li>\r\n            </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
