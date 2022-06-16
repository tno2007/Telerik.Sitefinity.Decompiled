// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.ManageRelatedItemsArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  /// <summary>Manage related items article control</summary>
  internal class ManageRelatedItemsArticle : ServerSideCodeArticleBase
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
    /// <param name="culture">The culture info.</param>
    /// <returns>The title of the article.</returns>
    public override string GenerateArticleTitle(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return string.Format("Manage related items of {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
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
    /// <param name="culture">The culture info.</param>
    /// <returns>The description of the article.</returns>
    public override string GenerateArticleDescription(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n                <ul>\r\n                  <li>This is a description for managing related items of !#prefix#! !#TypeNameNormal#!. In order to access related items we will use the Related Data API:</li>\r\n                  <li>- IQueryable<IDataItem> GetRelatedItems(this object item, string fieldName) – Gets child related items by field name. It will return a query with child data items as IDataItem.</li>\r\n                  <li>- IQueryable<IDataItem> GetRelatedParentItems(this object item, string parentItemsTypeName, string parentItemProviderName = null, string fieldName = null) – Gets parent related items by parent type. It will return a query with parent data items as IDataItem. Filtering by parent item field name can be applied. In this case field name is the name of the related field linking to this item, in the parent item.</li>\r\n                  <li>- DeleteRelation(this IDataItem item, IDataItem relatedItem, string fieldName) - This method will delete a relation between item and relatedItem by field name. The item context should have a related data field with the name “fieldName”.</li>\r\n                  <li> Let's see a basic code example:</li>\r\n                </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
    /// <param name="culture">The culture info.</param>
    /// <returns>The code sample in c# syntax.</returns>
    protected override string GenerateCSharpCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n// Demonstrates how to manage related items of !#prefix#! !#TypeNameNormal#!\r\npublic void ManageRelatedItemsOf!#prefix#!!#TypeNameNormal#!()\r\n{\r\n    // Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    // Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    var providerName = String.Empty;\r\n    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    Create!#TypeNamePascal#!Item(dynamicModuleManager, !#TypeNameCamel#!Type);\r\n            \r\n    // This is how we get the !#TypeNameCamel#! item\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type).Last();\r\n    !#ManageRelatedItems#!    \r\n    // You need to call SaveChanges() in order for the items to be actually persisted to data store\r\n    dynamicModuleManager.SaveChanges();\r\n}\r\n            \r\n// Creates a new !#TypeNameCamel#! item \r\nprivate void Create!#TypeNamePascal#!Item(DynamicModuleManager dynamicModuleManager,Type !#TypeNameCamel#!Type)\r\n{   !#SetCultureNameAndThreadCulture#!\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.CreateDataItem(!#TypeNameCamel#!Type);\r\n            \r\n    // This is how values for the properties are set \r\n!#DynamicPropertySetting#!\r\n    // You need to call SaveChanges() in order for the items to be actually persisted to data store\r\n    dynamicModuleManager.SaveChanges();\r\n!#CheckInCheckOutItem#!}", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
    /// <param name="culture">The culture info.</param>
    /// <returns>The code sample in VB.NET syntax.</returns>
    protected override string GenerateVBCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n' Demonstrates how to manage related items of !#prefix#! !#TypeNameNormal#!\r\nPublic Sub ManageRelatedItemsOf!#prefix#!!#TypeNameNormal#!() \r\n    \r\n    ' Set the provider name for the DynamicModuleManager here. All available providers are listed in\r\n    ' Administration -> Settings -> Advanced -> DynamicModules -> Providers\r\n    Dim providerName As String = String.Empty        \r\n    Dim dynamicModuleManager_ As DynamicModuleManager = DynamicModuleManager.GetManager(providerName)\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Create!#TypeNamePascal#!Item(dynamicModuleManager_, !#TypeNameCamel#!Type)\r\n            \r\n    ' This is how we get the !#TypeNameCamel#! item \r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.GetDataItems(!#TypeNameCamel#!Type).Last()\r\n    !#ManageRelatedItemsVB#!  \r\n    ' You need to call SaveChanges() in order for the items to be actually persisted to data store\r\n    dynamicModuleManager_.SaveChanges()\r\nEnd Sub\r\n            \r\n' Creates a new !#TypeNameCamel#! item\r\nPrivate Sub Create!#TypeNamePascal#!Item( dynamicModuleManager_ As DynamicModuleManager,!#TypeNameCamel#!Type As Type)\r\n    !#SetCultureNameAndThreadCultureVBNet#!       \r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.CreateDataItem(!#TypeNameCamel#!Type)\r\n            \r\n    ' This is how values for the properties are set \r\n!#DynamicPropertySettingVBNet#!\r\n    ' You need to call SaveChanges() in order for the items to be actually persisted to data store\r\n    dynamicModuleManager_.SaveChanges()\r\n!#CheckInCheckOutItemVBNet#!\r\nEnd Sub", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
    /// <param name="culture">The culture info.</param>
    /// <returns>The code explanation.</returns>
    protected override string GenerateCodeExplanation(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we are getting an instance of DynamicModuleManager class (Defined in Telerik.Sitefinity.DynamicModules namespace).</li> \r\n                <li>Then, we are creating !#prefix#! !#TypeNameNormal#! Item with predefined ID. Once we have the item created with relations to other items, we can proceed to managing the related items.</li>\r\n                <li>To get the child related items we use the GetRelatedItems() method passing the field name as parameter. We take the first item of the collection in order to demonstrate getting parent related items and deleting a relation.</li>\r\n                <li>Once we have the child item, we can use the GetRelatedParentItems() method to retrieve the parent related items. We are passing the content type name as parameter. In this case - the full type name of !#TypeNameNormal#!.</li>\r\n                <li>In order to remove the relation between the current !#TypeNameNormal#! item and the retrieved child item we use the DeleteRelation() method passing the child item and field name as parameters.</li>\r\n            </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
