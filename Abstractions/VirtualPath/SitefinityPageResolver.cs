// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.SitefinityPageResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.HtmlParsing;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>Sitefinity page resolver</summary>
  public class SitefinityPageResolver : PageResolverBase
  {
    /// <summary>
    /// Determines whether a file with the specified virtual path exists.
    /// NOTE: This resolver should be invoked only with proper route handler
    /// after a page node has been obtained and therefore this method always returns true.
    /// </summary>
    /// <param name="definition">The path definition.</param>
    /// <param name="virtalPath">The virtual path to check.</param>
    /// <returns>Returns true.</returns>
    public override bool Exists(PathDefinition definition, string virtalPath) => true;

    /// <summary>Opens the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns>New stream.</returns>
    public override Stream Open(PathDefinition definition, string virtualPath)
    {
      RequestContext requestContext;
      PageSiteNode node = this.GetRequestedPageNode(out requestContext);
      if (node == null)
        throw new InvalidOperationException("Invalid SiteMap node specified. Either the current group node doesn't have child nodes or the current user does not have rights to view any of the child nodes.");
      PageManager manager = PageManager.GetManager(((SiteMapBase) node.Provider).PageProviderName);
      manager.Provider.FetchAllLanguagesData();
      PageData pageData = (PageData) null;
      Guid? variationKey = this.GetVariationId(virtualPath);
      if (variationKey.HasValue)
        pageData = manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == node.PageId && p.PersonalizationSegmentId == variationKey.Value)).FirstOrDefault<PageData>();
      if (pageData == null)
        pageData = manager.GetPageData(node.PageId);
      StringBuilder output = new StringBuilder();
      this.RenderPage(output, pageData, requestContext, virtualPath);
      byte[] contentWithPreamble = RouteHelper.GetContentWithPreamble(output.ToString());
      Log.Write((object) string.Format("SitefinityPageResolver - Open method called for PageSiteNode with key {0}.", (object) node.PageId.ToString().ToUpperInvariant()), ConfigurationPolicy.TestTracing);
      return (Stream) new MemoryStream(contentWithPreamble);
    }

    /// <summary>Renders page</summary>
    /// <param name="output">The output.</param>
    /// <param name="pageData">The page data.</param>
    /// <param name="context">The request context.</param>
    /// <param name="virtualPath">The virtual path.</param>
    protected virtual void RenderPage(
      StringBuilder output,
      PageData pageData,
      RequestContext context,
      string virtualPath)
    {
      string themeFromVirtualPath = this.GetThemeFromVirtualPath(virtualPath);
      CursorCollection placeHolders = new CursorCollection();
      DirectiveCollection directives = new DirectiveCollection();
      List<IControlsContainer> controlContainers = new List<IControlsContainer>();
      this.ClearContextSecuredControls();
      this.SetPageDirectives(pageData, directives);
      this.BuildPageTemplate(pageData, themeFromVirtualPath, context, output, placeHolders, directives, controlContainers);
      this.BuildControls(pageData, controlContainers, placeHolders, directives);
      placeHolders.WriteRegistrations();
      this.WriteAllHolders(placeHolders, output, string.Empty, pageData, themeFromVirtualPath);
    }

    /// <summary>Appends layout</summary>
    /// <param name="layoutTemplate">The layout template.</param>
    /// <param name="assemblyInfo">The assembly info.</param>
    /// <param name="parentPlaceHolder">The parent placeholder.</param>
    /// <param name="placeHolders">The placeholders.</param>
    /// <param name="layoutId">The layout id.</param>
    /// <param name="directives">The directives.</param>
    protected new virtual void AppendLayout(
      string layoutTemplate,
      string assemblyInfo,
      PlaceHolderCursor parentPlaceHolder,
      CursorCollection placeHolders,
      string layoutId,
      DirectiveCollection directives)
    {
      string template;
      if (layoutTemplate.StartsWith("~/"))
      {
        using (Stream stream = SitefinityFile.Open(layoutTemplate))
        {
          using (StreamReader streamReader = new StreamReader(stream))
            template = streamReader.ReadToEnd();
        }
      }
      else if (layoutTemplate.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
      {
        Type assemblyInfo1 = !string.IsNullOrEmpty(assemblyInfo) ? TypeResolutionService.ResolveType(assemblyInfo, true) : Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
        template = ControlUtilities.GetTextResource(layoutTemplate, assemblyInfo1);
      }
      else
        template = layoutTemplate.StartsWith("<") ? layoutTemplate : throw new ArgumentException("Invalid layout template.");
      this.ProcessLayoutString(template, parentPlaceHolder, placeHolders, layoutId, directives);
    }

    /// <summary>Appends layout markup</summary>
    /// <param name="ctrlType">The control type.</param>
    /// <param name="assemblyInfo">The assembly info.</param>
    /// <param name="parentPlaceHolder">The parent placeholder.</param>
    /// <param name="placeHolders">The placeholders.</param>
    /// <param name="layoutId">The layout id.</param>
    /// <param name="directives">The directives.</param>
    protected virtual void AppendLayoutMarkup(
      Type ctrlType,
      string assemblyInfo,
      PlaceHolderCursor parentPlaceHolder,
      CursorCollection placeHolders,
      string layoutId,
      DirectiveCollection directives)
    {
      this.ProcessLayoutString(string.Format("<{0}:{1} runat=\"server\" />", (object) placeHolders.GetTagPrefix(ctrlType.Namespace.ToString(), ctrlType.Assembly.ToString(), "ctrl"), (object) ctrlType.Name), parentPlaceHolder, placeHolders, layoutId, directives);
    }

    /// <summary>Check whether it it a directive</summary>
    /// <param name="chunk">The chunk.</param>
    /// <param name="directiveName">The directive name.</param>
    /// <returns>A value indication whether it is a directive.</returns>
    protected virtual bool IsDirective(HtmlChunk chunk, string directiveName) => chunk.TagName.StartsWith("%@" + directiveName, StringComparison.OrdinalIgnoreCase) || chunk.TagName == "%@" && chunk.ParamsCount > 0 && chunk.Attributes[0].Equals(directiveName, StringComparison.OrdinalIgnoreCase) || chunk.TagName == "%" && chunk.ParamsCount > 0 && chunk.Attributes[0].Equals("@" + directiveName, StringComparison.OrdinalIgnoreCase) || chunk.TagName == "%" && chunk.ParamsCount > 1 && chunk.Attributes[0] == "@" && chunk.Attributes[1].Equals(directiveName, StringComparison.OrdinalIgnoreCase);

    /// <summary>Clears context secured controls</summary>
    protected void ClearContextSecuredControls()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !currentHttpContext.Items.Contains((object) Telerik.Sitefinity.Web.PageRouteHandler.HttpContextSecuredControls))
        return;
      currentHttpContext.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.HttpContextSecuredControls] = (object) false;
    }
  }
}
