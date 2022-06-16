// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.PublishItemArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class PublishItemArticle : ServerSideCodeArticleBase
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
      return string.Format("Publish/Unpublish {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
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
      return this.ContextRegex.Replace("This is a description for creating !#prefix#! !#TypeNameNormal#! and then using the lifecycle to publish/unpublish the item. \r\n            <br/>In order to create !#prefix#! !#TypeNameNormal#! content item,  we will use the \r\n            <br/>DynamicModuleManager's CreateDataItem() method and then use the content item Lifecycle property to get access to the publish/unpublish. \r\n            <br/>Let's see a basic code example:", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n//Publish a new !#TypeNameCamel#! item\r\npublic void Publish!#TypeNamePascal#!()\r\n{\r\n    // Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    // Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    var providerName = String.Empty;\r\n\r\n    // Set a transaction name and get the version manager\r\n    var transactionName = \"someTransactionName\";\r\n    var versionManager = VersionManager.GetManager(null, transactionName);\r\n    !#SetCultureNameAndThreadCulture#!\r\n    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName);\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.CreateDataItem(!#TypeNameCamel#!Type);\r\n  \r\n    // This is how values for the properties are set\r\n!#DynamicPropertySetting#!\r\n\r\n    // Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false);\r\n    \r\n    // We can now call the following to publish the item\r\n    ILifecycleDataItem published!#TypeNamePascal#!Item = dynamicModuleManager.Lifecycle.Publish(!#TypeNameCamel#!Item);\r\n    \r\n    // You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, \"Published\");\r\n\r\n    // Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, true);\r\n\r\n    // Now the item is published and can be seen in the page\r\n\r\n    // If we want to unpublish an item, we call Unpublish\r\n    dynamicModuleManager.Lifecycle.Unpublish(published!#TypeNamePascal#!Item);\r\n\r\n    // You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, \"Unpublished\");\r\n\r\n    // We can set a date and time in the future, for the item to be published\r\n    dynamicModuleManager.Lifecycle.PublishWithSpecificDate(!#TypeNameCamel#!Item, DateTime.Now.AddMinutes(5));\r\n\r\n    // You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, \"Scheduled\");\r\n\r\n    // Commit the transaction in order for the items to be actually persisted to data store\r\n    TransactionManager.CommitTransaction(transactionName);\r\n}", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n'Publish a new !#TypeNameCamel#! item\r\nPublic Sub Publish!#TypeNamePascal#!()\r\n \r\n    ' Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    ' Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    Dim providerName As String = String.Empty  \r\n\r\n    ' Set a transaction name and get the version manager\r\n    Dim transactionName As String = \"someTransactionName\"\r\n    Dim versionManager As VersionManager = VersionManager.GetManager(Nothing, transactionName) \r\n    !#SetCultureNameAndThreadCultureVBNet#!            \r\n    Dim dynamicModuleManager_ As DynamicModuleManager = DynamicModuleManager.GetManager(providerName, transactionName)\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.CreateDataItem(!#TypeNameCamel#!Type)\r\n            \r\n    ' This is how values for the properties are set\r\n!#DynamicPropertySettingVBNet#!\r\n    ' Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, false)\r\n\r\n    ' We can now call the following to publish the item\r\n    Dim published!#TypeNamePascal#!Item As ILifecycleDataItem = dynamicModuleManager_.Lifecycle.Publish(!#TypeNameCamel#!Item)\r\n    \r\n    'You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager_.Provider.ApplicationName, \"Published\")\r\n\r\n    ' Create a version and commit the transaction in order changes to be persisted to data store\r\n    versionManager.CreateVersion(!#TypeNameCamel#!Item, true)\r\n\r\n    ' Now the item is published and can be seen in the page\r\n\r\n    ' If we want to unpublish an item, we call Unpublish\r\n    dynamicModuleManager_.Lifecycle.Unpublish(published!#TypeNamePascal#!Item)\r\n\r\n    'You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager_.Provider.ApplicationName, \"Unpublished\")\r\n\r\n    ' We can set a date and time in the future, for the item to be published\r\n    dynamicModuleManager_.Lifecycle.PublishWithSpecificDate(!#TypeNameCamel#!Item, DateTime.Now.AddMinutes(5))\r\n\r\n    'You need to set appropriate workflow status\r\n    !#TypeNameCamel#!Item.SetWorkflowStatus(dynamicModuleManager_.Provider.ApplicationName, \"Scheduled\")\r\n\r\n    ' Commit the transaction in order changes to be persisted to data store\r\n    TransactionManager.CommitTransaction(transactionName)\r\n\r\nEnd Sub", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we are getting an instance of DynamicModuleManager class (Defined in Telerik.Sitefinity.DynamicModules namespace).</li> \r\n                <li>Then, we are invoking the CreateDataItem() method, which returns an instance of !#TypeNameNormal#! Item. </li>\r\n                <li>Once we have the item created, some properties should be set.</li>\r\n                <li>Then we can use the Lifecycle property, which contains all methods that are used for working with Lifecycle.</li>\r\n                <li>If we just want to publish the item, we call the dynamicModuleManager.Lifecycle.Publish(item); and call CreateVersion and that's all.</li>\r\n                <li>Also we can retrieve an item by his Id, and then unpublish it, by calling dynamicModuleManager.Lifecycle.Unpublish(item).</li>\r\n                <li>CommitTransaction needs to be called always, so we can persist the item, we have created.</li>\r\n            </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
