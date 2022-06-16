// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.GetItemByIDArticleJS
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class GetItemByIDArticleJS : ClientSideCodeArticleBase
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
      return string.Format("Get {0} {1} by ID", (object) this.IndefiniteArticleNameResolver.Prefix, (object) this.ModuleType.DisplayName);
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
      return this.ContextRegex.Replace("This is a description for retrieving of !#prefix#! !#TypeNameNormal#! by ID. In order to retrieve !#prefix#! !#TypeNameNormal#! content item by ID, we will use the\r\n            <br/>Data.svc service and GET method. But before we can retrieve !#TypeNameCamel#! item we have to create one.\r\n            <br/>Let's see a basic jQuery request example:", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
    protected override string GenerateJSCode(DynamicModule module, DynamicModuleType moduleType) => this.ContextRegex.Replace("\r\n// Creates a new !#TypeNameCamel#! item\r\nfunction create!#TypeNamePascal#!() {\r\n!#ObjectDataForNewItem#!\r\n    $.ajax({\r\n        type: \"PUT\",\r\n        url: \"/Sitefinity/Services/DynamicModules/Data.svc/00000000-0000-0000-0000-000000000000/?itemType=!#FullTypeName#!\",\r\n        data:  JSON.stringify(data),\r\n        dataType: \"json\",\r\n        contentType: \"application/json\",\r\n        success: function (data, textStatus) {\r\n            if (textStatus == \"success\") {\r\n                var created!#TypeNamePascal#!ID = data.Item.Id;\r\n                get!#TypeNamePascal#!ByID(created!#TypeNamePascal#!ID);\r\n            }\r\n            else {\r\n                // If there has been an error executing the operation you may notify this way:\r\n                // $(\"#result\").text(\"Error creating new !#TypeNameCamel#! item!\");\r\n            }\r\n        }\r\n    });\r\n}\r\n\r\n// Gets !#TypeNameCamel#! item by ID\r\nfunction get!#TypeNamePascal#!ByID(var itemToRetrieveID) {\r\n    var urlData = \"/Sitefinity/Services/DynamicModules/Data.svc/\" + itemToRetrieveID.toString() + \"/?itemType=!#FullTypeName#!\";\r\n    $.ajax({\r\n        type: \"GET\",\r\n        url: urlData,\r\n        dataType: \"json\",\r\n        contentType: \"application/json\",\r\n        success: function (data, textStatus) {\r\n            if (textStatus == \"success\") {\r\n                // If the request was executed successfully you must have the !#TypeNameCamel#!Item in data.Item\r\n                var !#TypeNameCamel#!Item = data.Item;\r\n            }\r\n            else {\r\n                // There has been an error executing the operation you may notify this way:\r\n                // $(\"#result\").text(\"Error retrieving !#TypeNameCamel#! item!\");\r\n            }\r\n        }\r\n    });    \r\n}\r\n", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));

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
      return this.ContextRegex.Replace("<br/>Explanation<br/>\r\n            <ul>\r\n                <li>First, we are creating a new !#TypeNameCamel#! item. Then on success we get the created item id</li> \r\n                <li>and pass it to the function get!#TypeNamePascal#!ByID(var itemToRetrieveID)</li>\r\n            </ul>\r\n            Note: To run this example you need to include the JavaScript file \"json2.js\" so that the functions defined for converting objects to string and parse them can be done.\r\n            This can be downloaded from <a href=\"http://www.json.org/json2.js\">http://www.json.org/json2.js</a>", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }
  }
}
