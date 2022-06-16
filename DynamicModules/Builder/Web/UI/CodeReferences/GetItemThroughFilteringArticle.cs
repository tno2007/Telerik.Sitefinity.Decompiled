// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.GetItemThroughFilteringArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class GetItemThroughFilteringArticle : ServerSideCodeArticleBase
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
      return string.Format("Get {0} {1} through Filtering", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
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
      return this.ContextRegex.Replace("This is a description for retrieving of !#prefix#! !#TypeNameNormal#! through filtering. In order to retrieve !#prefix#! !#TypeNameNormal#! content item through filtering, we will use the\r\n            <br/>DynamicModuleManager's GetDataItems() method which gets the query of dynamic data items. GetDataItems() expects as paramether\r\n            <br/>the Type of data items for which to get the query. Now we will create !#prefix#! !#TypeNameNormal#! item and then will show how to retrieve it.\r\n            <br/>Let's see a basic code example:", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n// Demonstrates how !#TypeNameNormal#! content items can be retrieved through filtering\r\npublic IQueryable&lt;DynamicContent&gt; Retrieve!#TypeNamePascal#!ThroughFiltering()\r\n{\r\n    // Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    // Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    var providerName = String.Empty;\r\n\r\n    // Set a transaction name\r\n    var transactionName = \"someTransactionName\";\r\n\r\n    !#SetCultureNameAndThreadCulture#!\r\n    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    Create!#TypeNamePascal#!Item(dynamicModuleManager, !#TypeNameCamel#!Type!#LocalizableCulture#!, transactionName);\r\n            \r\n    // This is how we get the !#TypeNameCamel#! items through filtering\r\n    var myFilteredCollection = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type).Where(\"!#LocalizableFirstPropertyName#! = \\\"Some !#FirstPropertyName#!\\\"\");\r\n    // At this point myFilteredCollection contains the items that match the lambda expression passed to the Where extension method\r\n    // If you want only the first matching element you can freely get it by \".First()\" extension method like this:\r\n    // var myFirstFilteredItem = myFilteredCollection.First();\r\n    return myFilteredCollection;\r\n}\r\n            \r\n// Creates a new !#TypeNameCamel#! item\r\nprivate void Create!#TypeNamePascal#!Item(DynamicModuleManager dynamicModuleManager,Type !#TypeNameCamel#!Type!#LocalizableParamCulture#!, string transactionName)\r\n{   \r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.CreateDataItem(!#TypeNameCamel#!Type);\r\n            \r\n    // This is how values for the properties are set \r\n!#DynamicPropertySetting#!\r\n    // Create a version and commit the transaction in order changes to be persisted to data store\r\n    var versionManager = VersionManager.GetManager(null, transactionName);\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false);\r\n    TransactionManager.CommitTransaction(transactionName);\r\n!#CheckInCheckOutItem#!}", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n' Demonstrates how !#TypeNameNormal#! content items can be retrieved through filtering\r\nPublic Function Retrieve!#TypeNamePascal#!ThroughFiltering() As IQueryable(Of DynamicContent)\r\n\r\n    ' Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    ' Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    Dim providerName As String = String.Empty   \r\n\r\n    ' Set a transaction name\r\n    Dim transactionName As String = \"someTransactionName\"\r\n\r\n    !#SetCultureNameAndThreadCultureVBNet#!             \r\n    Dim dynamicModuleManager_ As DynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName)\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Create!#TypeNamePascal#!Item(dynamicModuleManager_, !#TypeNameCamel#!Type!#LocalizableCulture#!, transactionName)\r\n            \r\n    ' This is how we get the !#TypeNameCamel#! items through filtering\r\n    Dim myFilteredCollection = dynamicModuleManager_.GetDataItems(!#TypeNameCamel#!Type).Where(\"!#LocalizableFirstPropertyName#! = \"\"Some !#FirstPropertyName#!\"\"\")\r\n    ' At this point myFilteredCollection contains the items that match the lambda expression passed to the Where extension method\r\n    ' If you want only the first matching element you can freely get it by \".First()\" extension method like this:\r\n    ' Dim myFirstFilteredItem = myFilteredCollection.First()\r\n    Return myFilteredCollection\r\nEnd Function\r\n\r\n' Creates a new !#TypeNameCamel#! item\r\nPrivate Sub Create!#TypeNamePascal#!Item(dynamicModuleManager_ As DynamicModuleManager, !#TypeNameCamel#!Type As Type!#LocalizableParamCultureVBNet#!, transactionName As String)\r\n            \r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.CreateDataItem(!#TypeNameCamel#!Type)\r\n            \r\n    ' This is how values for the properties are set \r\n!#DynamicPropertySettingVBNet#!\r\n    ' Create a version and commit the transaction in order changes to be persisted to data store\r\n    Dim versionManager As VersionManager = VersionManager.GetManager(Nothing, transactionName)\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false)\r\n    TransactionManager.CommitTransaction(transactionName)\r\n!#CheckInCheckOutItemVBNet#!\r\nEnd Sub", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>The code is similar to  the way for retrieving a collection of !#TypeNamePlural#! but with the difference that we are applying some</li> \r\n                <li>Filtering to the collection we get with the GetDataItems() method.</li>\r\n            </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
