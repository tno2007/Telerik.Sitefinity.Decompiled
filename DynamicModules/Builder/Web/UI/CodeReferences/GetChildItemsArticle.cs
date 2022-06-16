// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.GetChildItemsArticle
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  /// <summary>
  /// Article that demonstrates getting child content items of a dynamic content item
  /// </summary>
  internal class GetChildItemsArticle : ServerSideCodeArticleBase
  {
    /// <inheritdoc />
    public override string GenerateArticleTitle(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return string.Format("Get child items of {0} {1}", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
    }

    /// <inheritdoc />
    public override string GenerateArticleDescription(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("This article demonstrates how to retrieve child items of !#prefix#! !#TypeNameNormal#!. In order to do it, we will use the\r\n            <br/>extension method GetChildItems() which gets a query of child data items. GetChildItems() method has four overloads: \r\n            <br/>&nbsp;- IQueryable<DynamicContent> GetChildItems(this DynamicContent dynamicContent, Type childItemsType)\r\n            <br/>&nbsp;- IQueryable<DynamicContent> GetChildItems(this DynamicContent dynamicContent, string childItemsType)\r\n            <br/>&nbsp;- IEnumerable<DynamicContent> GetChildItems(this DynamicContent dynamicContent, Type childItemsType, string filterExpression, string orderExpression, int? skip, int? take, ref int? totalCount)\r\n            <br/>&nbsp;- IEnumerable<DynamicContent> GetChildItems(this DynamicContent dynamicContent, string childItemsType, string filterExpression, string orderExpression, int? skip, int? take, ref int? totalCount)\r\n            <br/><br/>Another way to get child items is to use the GetValue(string fieldName) method since there are artificial fields added to all dynamic types participating as parents in a hierarchy of types.\r\n            <br/>The name of the added field is equal to the developer name of the child type in plural. The GetValue() method will return live items if the !#TypeNameNormal#! item is live and master items otherwise.\r\n            <br/>What’s more you can use \"Eval\" statements in your widget templates to display list of your child items.", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    /// <inheritdoc />
    protected override string GenerateCSharpCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      IEnumerable<DynamicModuleType> source = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id));
      if (source.Count<DynamicModuleType>() == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\r\n// Demonstrates how child content items can be retrieved\r\npublic IEnumerable&lt;DynamicContent&gt; GetChildItemsOf!#TypeNamePascal#!()\r\n{\r\n    // A list containing all child items from all child types\r\n    List&lt;DynamicContent&gt; allChildItems = new List&lt;DynamicContent&gt;();\r\n\r\n    var providerName = String.Empty;\r\n    DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager(providerName);\r\n    Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n    DynamicContent !#TypeNameCamel#!Item = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type).FirstOrDefault();\r\n    if (!#TypeNameCamel#!Item != null)\r\n    {");
      foreach (DynamicModuleType dynamicModuleType in source)
      {
        stringBuilder.Append("\r\n        // Resolve !#ChildTypeNamePascal#! type\r\n        Type !#ChildTypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#ChildFullTypeName#!\");\r\n        // Get query of child items with live status in the current culture if the !#TypeNameCamel#!Item is live and with master status otherwise.\r\n        IQueryable&lt;DynamicContent&gt; !#ChildTypeNameCamel#!AllItems = !#TypeNameCamel#!Item.GetChildItems(!#ChildTypeNameCamel#!Type);\r\n        // Add them to the result\r\n        allChildItems.AddRange(!#ChildTypeNameCamel#!AllItems);\r\n\r\n        // Get query of child items with live status if the !#TypeNameCamel#!Item is live and with master status otherwise.\r\n        // Can be used in widget templates with \"Eval\" expression (example: &lt;asp:Repeater runat=\"server\" DataSource='&lt;%# Eval(\"!#ChildTypeNamePlural#!\") %&gt;'&gt;).\r\n        IQueryable&lt;DynamicContent&gt; !#ChildTypeNameCamel#!Items = !#TypeNameCamel#!Item.GetValue(\"!#ChildTypeNamePlural#!\") as IQueryable&lt;DynamicContent&gt;;\r\n");
        stringBuilder.Replace("!#ChildTypeNameCamel#!", dynamicModuleType.TypeName.TocamelCase());
        stringBuilder.Replace("!#ChildTypeNamePascal#!", dynamicModuleType.TypeName.ToPascalCase());
        stringBuilder.Replace("!#ChildFullTypeName#!", dynamicModuleType.TypeNamespace + "." + dynamicModuleType.TypeName);
        stringBuilder.Replace("!#ChildTypeNamePlural#!", PluralsResolver.Instance.ToPlural(dynamicModuleType.TypeName));
      }
      stringBuilder.Append("    }\r\n\r\n    return allChildItems;\r\n}");
      return this.ContextRegex.Replace(stringBuilder.ToString(), (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture, module)));
    }

    /// <inheritdoc />
    protected override string GenerateVBCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      IEnumerable<DynamicModuleType> source = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id));
      if (source.Count<DynamicModuleType>() == 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("\r\n' Demonstrates how child content items can be retrieved\r\nPublic Function GetChildItemsOf!#TypeNamePascal#!() As IEnumerable(Of DynamicContent)\r\n    ' A list containing all child items from all child types\r\n    Dim allChildItems As New List(Of DynamicContent)()\r\n\r\n    Dim providerName As String = String.Empty\r\n    Dim dynamicModuleManager_ As DynamicModuleManager = DynamicModuleManager.GetManager(providerName)\r\n    Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n    Dim !#TypeNameCamel#!Item As DynamicContent = dynamicModuleManager_.GetDataItems(!#TypeNameCamel#!Type).FirstOrDefault()\r\n\r\n    If !#TypeNameCamel#!Item IsNot Nothing Then");
      foreach (DynamicModuleType dynamicModuleType in source)
      {
        stringBuilder.Append("\r\n        ' Resolve !#ChildTypeNamePascal#! type\r\n        Dim !#ChildTypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#ChildFullTypeName#!\")\r\n        ' Get query of child items with live status if the !#TypeNameCamel#!Item is live and with master status otherwise.\r\n        Dim !#ChildTypeNameCamel#!AllItems As IQueryable (Of DynamicContent) = !#TypeNameCamel#!Item.GetChildItems(!#ChildTypeNameCamel#!Type)\r\n        ' Add them to the result\r\n        allChildItems.AddRange(!#ChildTypeNameCamel#!AllItems)\r\n\r\n        ' Get query of child items with live status if the !#TypeNameCamel#!Item is live and with master status otherwise.\r\n        ' Can be used in widget templates with \"Eval\" expression (example: &lt;asp:Repeater runat=\"server\" DataSource='&lt;%# Eval(\"!#ChildTypeNamePlural#!\") %&gt;'&gt;).\r\n        Dim !#ChildTypeNameCamel#!Items As IQueryable (Of DynamicContent) = TryCast(!#TypeNameCamel#!Item.GetValue(\"!#ChildTypeNamePlural#!\"), IQueryable (Of DynamicContent))\r\n");
        stringBuilder.Replace("!#ChildTypeNameCamel#!", dynamicModuleType.TypeName.TocamelCase());
        stringBuilder.Replace("!#ChildTypeNamePascal#!", dynamicModuleType.TypeName.ToPascalCase());
        stringBuilder.Replace("!#ChildFullTypeName#!", dynamicModuleType.TypeNamespace + "." + dynamicModuleType.TypeName);
        stringBuilder.Replace("!#ChildTypeNamePlural#!", PluralsResolver.Instance.ToPlural(dynamicModuleType.TypeName));
      }
      stringBuilder.Append("    End If\r\n\r\n    Return allChildItems\r\nEnd Function");
      return this.ContextRegex.Replace(stringBuilder.ToString(), (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture, module)));
    }

    /// <inheritdoc />
    protected override string GenerateCodeExplanation(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we get !#prefix#! !#TypeNameNormal#! Item.</li> \r\n                <li>Then we use the GetChildItems() and GetValue() methods to get queries of child data items.</li>\r\n                <li>We add all retrieved child items to a collection and return the collection.</li>\r\n            </ul>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    /// <summary>Default namespaces</summary>
    protected override string[] DefaultNamespaces => new string[10]
    {
      "System.Collections.Generic",
      "Telerik.Sitefinity",
      "Telerik.Sitefinity.Model",
      "Telerik.Sitefinity.DynamicModules",
      "Telerik.Sitefinity.Data.Linq.Dynamic",
      "Telerik.Sitefinity.DynamicModules.Model",
      "Telerik.Sitefinity.GenericContent.Model",
      "Telerik.Sitefinity.Utilities.TypeConverters",
      "Telerik.Sitefinity.Security",
      "Telerik.Sitefinity.Lifecycle"
    };
  }
}
