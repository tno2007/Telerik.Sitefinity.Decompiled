// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences.IntegrationExample
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReferences
{
  public class IntegrationExample : ServerSideCodeArticleBase
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
      return this.ContextRegex.Replace("The example below demonstrates how to retrieve !#TypeNameNormal#! items and display these items in a RadGrid.  \r\n            <br/>This functionality has been wrapped inside an ASP.NET UserControl.  \r\n            <br/>This control than then be added to Sitefinity’s toolbox and dropped onto any page.", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n// ~/DisplayItems.ascx\r\n<%@ Control Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"DisplayItems.ascx.cs\" Inherits=\"SitefinityWebApp.DisplayItems\" %>\r\n<%@ Register TagPrefix=\"telerik\" Namespace=\"Telerik.Web.UI\" Assembly=\"Telerik.Web.UI\" %>\r\n\r\n<!-- ScriptManager or RadScriptManager is required by RadGrid, but this tag probably already exists on the Master Template -->\r\n<!-- <telerik:RadScriptManager ID=\"RadScriptManager1\" runat=\"server\" /> -->\r\n<telerik:RadGrid ID=\"Grid\" runat=\"server\" AutoGenerateColumns=\"False\">\r\n    <MasterTableView>\r\n        <Columns>\r\n            <telerik:GridBoundColumn DataField=\"Title\" HeaderText=\"Title\" />\r\n        </Columns>\r\n    </MasterTableView>\r\n</telerik:RadGrid>\r\n\r\n// ~/DisplayItems.ascx.cs\r\nusing System;\r\nusing System.Linq;\r\nusing System.Collections.Generic;\r\nusing Telerik.Sitefinity.Model;\r\nusing Telerik.Sitefinity.DynamicModules;\r\nusing Telerik.Sitefinity.Data.Linq.Dynamic;\r\nusing Telerik.Sitefinity.DynamicModules.Model;\r\nusing Telerik.Sitefinity.Utilities.TypeConverters;\r\nusing Telerik.Sitefinity.GenericContent.Model;\r\n\r\nnamespace SitefinityWebApp\r\n{\r\n    public partial class DisplayItems : System.Web.UI.UserControl\r\n    {\r\n        protected void Page_Load(object sender, EventArgs e)\r\n        {\r\n            // Fetch a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n            var myCollection = GetDataItems();\r\n\r\n            // Binds the collection of Person items to the RadGrid\r\n            Grid.DataSource = myCollection;\r\n            Grid.DataBind();\r\n        }\r\n\r\n!#GetDataItems#!\r\n    }\r\n}\r\n".Replace("!#GetDataItems#!", this.GenerateCSharpGetItemsCode(module, moduleType)), (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("\r\n' ~/DisplayItems.ascx\r\n<%@ Control Language=\"vb\" AutoEventWireup=\"true\" CodeBehind=\"DisplayItems.ascx.vb\" Inherits=\"SitefinityWebApp.DisplayItems\" %>\r\n<%@ Register TagPrefix=\"telerik\" Namespace=\"Telerik.Web.UI\" Assembly=\"Telerik.Web.UI\" %>\r\n\r\n<!-- ScriptManager or RadScriptManager is required by RadGrid, but this tag probably already exists on the Master Template -->\r\n<!-- <telerik:RadScriptManager ID=\"RadScriptManager1\" runat=\"server\" /> -->\r\n<telerik:RadGrid ID=\"Grid\" runat=\"server\" AutoGenerateColumns=\"False\">\r\n    <MasterTableView>\r\n        <Columns>\r\n            <telerik:GridBoundColumn DataField=\"Title\" HeaderText=\"Title\" />\r\n        </Columns>\r\n    </MasterTableView>\r\n</telerik:RadGrid>\r\n\r\n' ~/DisplayItems.ascx.vb\r\nImports System;\r\nImports System.Linq;\r\nImports System.Collections.Generic;\r\nImports Telerik.Sitefinity.Model;\r\nImports Telerik.Sitefinity.DynamicModules;\r\nImports Telerik.Sitefinity.Data.Linq.Dynamic;\r\nImports Telerik.Sitefinity.DynamicModules.Model;\r\nImports Telerik.Sitefinity.Utilities.TypeConverters;\r\nImports Telerik.Sitefinity.GenericContent.Model;\r\n\r\nPublic Class DisplayItems\r\n    Inherits System.Web.UI.UserControl\r\n\r\n    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load\r\n        \r\n        ' Fetch a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n        var myCollection = GetDataItems()\r\n\r\n        ' Binds the collection of Person items to the RadGrid\r\n        Grid.DataSource = myCollection;\r\n        Grid.DataBind();\r\n    End Sub\r\n!#GetDataItems#!\r\nEnd Class\r\n".Replace("!#GetDataItems#!", this.GenerateVBGetItemsCode(module, moduleType)), (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
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
      return this.ContextRegex.Replace("", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType, culture)));
    }

    /// <summary>
    /// Generates the contextualized code sample usings for c# syntax.
    /// </summary>
    /// <returns>The namespaces that must be used.</returns>
    protected override string GenerateCSharpCodeUsings(DynamicModuleType moduleType) => "";

    /// <summary>
    /// Generates the contextualized code sample imports for vb syntax.
    /// </summary>
    /// <returns>The namespaces that must be imported.</returns>
    protected override string GenerateVBCodeImports(DynamicModuleType moduleType) => "";

    /// <summary>
    /// Generates the contextualized code sample in c# syntax for getting data items.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in c# syntax.</returns>
    /// GenerateCSharpCode
    private string GenerateCSharpGetItemsCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n        // Gets a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n        public IQueryable&lt;DynamicContent&gt; GetDataItems()\r\n        {\r\n            DynamicModuleManager dynamicModuleManager = DynamicModuleManager.GetManager();\r\n            Type !#TypeNameCamel#!Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\");\r\n\r\n            // Fetch a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n            var myCollection = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type)\r\n                .Where(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live && i.Visible == true);\r\n    \r\n            return myCollection;\r\n        }", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));
    }

    /// <summary>
    /// Generates the contextualized code sample in VB syntax for getting data items.
    /// </summary>
    /// <param name="module">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> for which the code sample should be contextualized.
    /// </param>
    /// <param name="moduleType">
    /// The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> for which the code sample should be contextualized.
    /// </param>
    /// <returns>The code sample in VB syntax.</returns>
    private string GenerateVBGetItemsCode(
      DynamicModule module,
      DynamicModuleType moduleType,
      CultureInfo culture = null)
    {
      return this.ContextRegex.Replace("\r\n    ' Gets a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n    Public Function GetDataItems() as IQueryable(Of DynamicContent)\r\n\r\n        Dim dynamicModuleManager As DynamicModuleManager = DynamicModuleManager.GetManager()\r\n        Dim !#TypeNameCamel#!Type As Type = TypeResolutionService.ResolveType(\"!#FullTypeName#!\")\r\n\r\n        ' Fetch a collection of \"live\" and \"visible\" !#TypeNameCamel#! items.\r\n        Dim myCollection As IQueryable(Of DynamicContent) = dynamicModuleManager.GetDataItems(!#TypeNameCamel#!Type) _\r\n            .Where(Function(i) i.Status = Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Live AndAlso i.Visible = True)\r\n    \r\n        return myCollection\r\n    End Function", (MatchEvaluator) (m => this.ReplaceCodeSampleKey(m, moduleType)));
    }
  }
}
