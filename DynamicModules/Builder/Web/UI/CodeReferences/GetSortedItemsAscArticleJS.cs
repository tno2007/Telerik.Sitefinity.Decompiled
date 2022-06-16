// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.GetSortedItemsAscArticleJS
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class GetSortedItemsAscArticleJS : ClientSideCodeArticleBase
  {
    /// <summary>
    /// Generates the title of the article from the specified module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="!:DynamicModule" /> from which the title should be specified.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="!:DynamicModuleType" /> from which the title should be specified.
    /// </param>
    /// <returns>The title of the article.</returns>
    public override string GenerateArticleTitle(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return string.Format("Get a collection of {0} sorted ASC", (object) this.PluralsNameResolver.ToPlural(this.ModuleType.DisplayName));
    }

    /// <summary>
    /// Generates the description of the article from the specified module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="!:DynamicModule" /> from which the description should be specified.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="!:DynamicModuleType" /> from which the description should be specified.
    /// </param>
    /// <returns>The description of the article.</returns>
    public override string GenerateArticleDescription(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("This is a description for retrieving a collection of !#TypeNamePlural#! sorted ASC. In order to retrieve !#TypeNamePlural#! sorted ASC, we will use the\r\n            <br/>Data.svc service and GET method. Basically will do the same as in the Get a collection of !#TypeNamePlural#! Article and will add sorting expression.\r\n            <br/>Let's see a basic jQuery request example:", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    /// <summary>
    /// Generates the contextualized code sample in JS syntax for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="!:DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="!:DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in c# syntax.</returns>
    protected override string GenerateJSCode(DynamicModule module, DynamicModuleType moduleType) => this.ContextRegex.Replace("\r\n// Creates a new !#TypeNameCamel#! item\r\nfunction createTwo!#TypeNamePascal#!Items() {\r\n!#ObjectDataForNewItemA#!\r\n    $.ajax({\r\n        type: \"PUT\",\r\n        url: \"/Sitefinity/Services/DynamicModules/Data.svc/00000000-0000-0000-0000-000000000000/?itemType=!#FullTypeName#!\",\r\n        data:  JSON.stringify(!#ObjectDataNameForItemA#!),\r\n        dataType: \"json\",\r\n        contentType: \"application/json\",\r\n        success: function (data, textStatus) {\r\n            if (textStatus== \"success\") {\r\n                // You have created new !#TypeNamePascal#! item\r\n            }\r\n            else {\r\n                // There has been an error with the request\r\n            }\r\n        }\r\n    });\r\n!#ObjectDataForNewItemB#!\r\n    $.ajax({\r\n        type: \"PUT\",\r\n        url: \"/Sitefinity/Services/DynamicModules/Data.svc/00000000-0000-0000-0000-000000000000/?itemType=!#FullTypeName#!\",\r\n        data:  JSON.stringify(!#ObjectDataNameForItemB#!),\r\n        dataType: \"json\",\r\n        contentType: \"application/json\",\r\n        success: function (data, textStatus) {\r\n            if (textStatus== \"success\") {\r\n                // You have created new !#TypeNamePascal#! item\r\n            }\r\n            else {\r\n                // There has been an error with the request\r\n            }\r\n        }\r\n    });\r\n}\r\n\r\n// Gets collection of !#TypeNamePlural#! sorted ASC\r\nfunction getCollectionOf!#TypeNamePlural#!SortedASC() {\r\n    var urlData = \"/Sitefinity/Services/DynamicModules/Data.svc/?itemType=!#FullTypeName#!&sortExpression=\\\"!#FirstPropertyName#! ASC\\\"\";\r\n    $.ajax({\r\n        type: \"GET\",\r\n        url: urlData,\r\n        dataType: \"json\",\r\n        contentType: \"application/json\",\r\n        success: function (data, textStatus) {\r\n            if (textStatus == \"success\") {\r\n                // Here data.Items is our collection of !#TypeNamePlural#! which are sorted according to the sortingexpression\r\n                // You can access the items with simple index for example data.Items[0] will be the first item\r\n            }\r\n            else {\r\n                // There has been an error with the request\r\n            }\r\n        }\r\n    });\r\n}\r\n", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));

    /// <summary>
    /// Generates the contextualized code explanation for the given module and module type.
    /// </summary>
    /// <param name="module">
    /// The instance of the <see cref="!:DynamicModule" /> for which the code explanation should be
    /// contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of the <see cref="!:DynamicModuleType" /> for which the code explanation should be
    /// contextualized.
    /// </param>
    /// <returns>The code explanation.</returns>
    protected override string GenerateCodeExplanation(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we are creating two new !#TypeNameCamel#! items. Then retrieve all of the items sorted ASC</li>\r\n                <li>with the function getCollectionOf!#TypeNamePlural#!SortedASC() </li>\r\n            </ul>\r\n            Note: To run this example you need to include the JavaScript file \"json2.js\" so that the functions defined for converting objects to string and parse them can be done.\r\n            This can be downloaded from <a href=\"http://www.json.org/json2.js\">http://www.json.org/json2.js</a>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
