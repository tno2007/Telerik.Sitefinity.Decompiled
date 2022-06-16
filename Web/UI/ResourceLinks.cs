// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ResourceLinks
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control used for linking resources such as style sheets, images and java script files.
  /// The control will automatically load the resource from the currently selected backend
  /// theme.
  /// </summary>
  [ToolboxBitmap(typeof (ResourceLinks), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:ResourceLinks runat=\"server\"></{0}:ResourceLinks>")]
  [DefaultProperty("Links")]
  [ParseChildren(true, "Links")]
  public class ResourceLinks : Control
  {
    private string theme;
    private bool? useEmbeddedThemes;
    private static readonly List<string> allowedExtensions = new List<string>()
    {
      ".css",
      ".less",
      ".jpg",
      ".jpeg",
      ".gif",
      ".png",
      ".js"
    };
    private Collection<ResourceFile> links;
    private ClientResourceManager clientResourceManager;
    private object controlsCacheLock = new object();
    private Dictionary<string, Control> controlsCache = new Dictionary<string, Control>();
    private static readonly List<string> systemStyleSheetsList = new List<string>()
    {
      "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css",
      "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_metro_min.css",
      "Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css"
    };

    /// <summary>
    /// Gets or sets the collection of Resources links to be included with the page.
    /// </summary>
    /// <value>The links.</value>
    [Category("Behavior")]
    [Description("Resource links collection")]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual Collection<ResourceFile> Links
    {
      get
      {
        if (this.links == null)
          this.links = new Collection<ResourceFile>();
        return this.links;
      }
    }

    /// <summary>Gets or sets the ordinal.</summary>
    /// <value>The ordinal.</value>
    public virtual int Ordinal { get; set; }

    /// <summary>Gets or sets the theme that this control will use.</summary>
    /// <value>The name of the theme used by this control.</value>
    public virtual string Theme
    {
      get
      {
        bool? useBackendTheme = this.UseBackendTheme;
        bool flag = true;
        if (useBackendTheme.GetValueOrDefault() == flag & useBackendTheme.HasValue)
          this.theme = Config.Get<AppearanceConfig>().BackendTheme;
        else if (string.IsNullOrEmpty(this.theme))
        {
          this.theme = this.Page.Items[(object) "theme"] as string;
          if (string.IsNullOrEmpty(this.theme) && ThemeController.IsBackendPage())
            this.theme = Config.Get<AppearanceConfig>().BackendTheme;
          else
            this.CheckIfContextUsesEmbeddedBackendTheme();
        }
        return this.theme;
      }
      set => this.theme = value;
    }

    /// <summary>
    /// Determines whether control should use backend theme. If the value is set to [true] the configured backend theme will be used
    /// otherwise, the theme is determined automatically depending on the current request/page.
    /// </summary>
    public bool? UseBackendTheme { get; set; }

    /// <summary>
    /// Determines whether control will use embedded themes or external ones. By default
    /// embedded themes are used.
    /// </summary>
    /// <value><c>true</c> if [use embedded themes]; otherwise, <c>false</c>.</value>
    public virtual bool UseEmbeddedThemes
    {
      get
      {
        if (!this.useEmbeddedThemes.HasValue)
          this.useEmbeddedThemes = new bool?(ThemeController.IsThemeEmbedded(this.Theme, this.Page, ThemeController.IsBackendPage() && !ThemeController.IsPreviewPage(this.Page)));
        return this.useEmbeddedThemes.Value;
      }
      set => this.useEmbeddedThemes = new bool?(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether [disable compression].
    /// </summary>
    /// <value><c>true</c> if [disable compression]; otherwise, <c>false</c>.</value>
    public virtual bool DisableCompression { get; set; }

    /// <summary>Gets the client resource manager.</summary>
    /// <value>The client resource manager.</value>
    public virtual ClientResourceManager ClientResourceManager
    {
      get
      {
        if (this.clientResourceManager == null)
          this.clientResourceManager = new ClientResourceManager(true, this.Theme, (Control) this);
        return this.clientResourceManager;
      }
    }

    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      if (this.Page == null || this.GetIndexRenderMode() == IndexRenderModes.NoOutput)
        return;
      this.DisableCompression = true;
      Type resFileType = typeof (ResourceFile);
      Type resPropType = typeof (EmbeddedResourcePropertySetter);
      if (this.DisableCompression)
      {
        foreach (ResourceFile resourceFile in this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => (uint) r.JavaScriptLibrary > 0U)))
          this.RegisterLibrary(resourceFile);
        foreach (ResourceFile resourceFile in this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.JavaScriptLibrary == JavaScriptLibrary.None)))
        {
          Type type = resourceFile.GetType();
          if (type == resFileType)
            this.RegisterResource(resourceFile);
          else if (type == resPropType)
            this.SetResourceProperty((EmbeddedResourcePropertySetter) resourceFile);
        }
      }
      else
      {
        if (this.Page.Header != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("<style type=\"text/css\">");
          stringBuilder.Append(this.ClientResourceManager.CombineResources((IList<ResourceFile>) this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.Name != null && r.Name.ToUpperInvariant().EndsWith(".CSS", StringComparison.OrdinalIgnoreCase))).ToList<ResourceFile>(), new bool?(this.UseEmbeddedThemes), this.Page));
          stringBuilder.Append("</style>");
          this.Page.Header.Controls.Add((Control) new Literal()
          {
            Text = stringBuilder.ToString()
          });
        }
        string script = this.ClientResourceManager.CombineResources((IList<ResourceFile>) this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.JavaScriptLibrary != JavaScriptLibrary.None && r.Name != null && r.Name.ToUpperInvariant().EndsWith(".JS", StringComparison.OrdinalIgnoreCase))).ToList<ResourceFile>(), new bool?(this.UseEmbeddedThemes), this.Page) + this.ClientResourceManager.CombineResources((IList<ResourceFile>) this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.GetType() == resFileType && r.JavaScriptLibrary == JavaScriptLibrary.None && r.Name != null && r.Name.ToUpperInvariant().EndsWith(".JS", StringComparison.OrdinalIgnoreCase))).ToList<ResourceFile>(), new bool?(this.UseEmbeddedThemes), this.Page);
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ID, script, true);
        foreach (EmbeddedResourcePropertySetter resProp in this.Links.Where<ResourceFile>((Func<ResourceFile, bool>) (r => r.GetType() == resPropType)))
          this.SetResourceProperty(resProp);
      }
    }

    /// <summary>Registers the resource.</summary>
    /// <param name="resource">The resource.</param>
    private void RegisterResource(ResourceFile resource)
    {
      Type type1;
      if (!string.IsNullOrEmpty(resource.AssemblyInfo))
      {
        type1 = TypeResolutionService.ResolveType(resource.AssemblyInfo);
      }
      else
      {
        ThemeElement themeElement = ThemeController.GetThemeElement(this.Theme, ThemeController.IsBackendPage(), this.Page);
        type1 = themeElement == null || string.IsNullOrEmpty(themeElement.Namespace) ? Config.Get<ControlsConfig>().ResourcesAssemblyInfo : TypeResolutionService.ResolveType(themeElement.AssemblyInfo);
      }
      WebResourceType? type2 = resource.Type;
      int resourceType;
      if (!type2.HasValue)
      {
        resourceType = (int) this.GetResourceType(resource.Name);
      }
      else
      {
        type2 = resource.Type;
        resourceType = (int) type2.Value;
      }
      switch (resourceType)
      {
        case 0:
          if (this.Page.Header == null)
            break;
          SitefinityStyleSheetManager current1 = SitefinityStyleSheetManager.GetCurrent(this.Page);
          if (current1 == null)
            break;
          string url = this.UseEmbeddedThemes ? this.Page.ClientScript.GetWebResourceUrl(type1, this.ClientResourceManager.GetEmbeddedResourceName(resource.Name, resource.Static)) : this.Page.ResolveUrl(this.ClientResourceManager.GetExternalResourcePath(resource.GetPath(), resource.Static, this.Page));
          List<string> stringList = (List<string>) this.Page.Items[(object) "CssLinks"];
          if (stringList == null)
          {
            stringList = new List<string>();
            this.Page.Items[(object) "CssLinks"] = (object) stringList;
          }
          if (stringList.Contains(url))
            break;
          StyleSheetDefinition styleSheet1;
          if (this.UseEmbeddedThemes)
          {
            StyleSheetReference styleSheet2 = new StyleSheetReference(this.ClientResourceManager.GetEmbeddedResourceName(resource.Name, resource.Static), type1.Assembly.FullName);
            styleSheet1 = new StyleSheetDefinition(url, styleSheet2);
          }
          else
          {
            stringList.Add(url);
            styleSheet1 = new StyleSheetDefinition(url, (StyleSheetReference) null);
          }
          if (SystemManager.IsInlineEditingMode && this.IsSystemStylesheet(resource))
          {
            current1.RegisterSystemStyleSheet(styleSheet1);
            break;
          }
          if (resource.IsFromTheme)
          {
            current1.RegisterThemeStyleSheet(styleSheet1);
            break;
          }
          current1.RegisterNonThemeStyleSheet(styleSheet1);
          break;
        case 1:
          if (this.UseEmbeddedThemes || resource.Static && !resource.Name.StartsWith("~"))
          {
            SitefinityScriptManager current2 = SitefinityScriptManager.GetCurrent(this.Page);
            if (current2 != null)
            {
              string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type1, this.ClientResourceManager.GetEmbeddedResourceName(resource.Name, resource.Static));
              current2.ScriptDefinitions.Add(new ScriptDefinition(webResourceUrl, new ScriptReference(resource.Name, type1.Assembly.FullName)));
            }
            ScriptManager current3 = ScriptManager.GetCurrent(this.Page);
            bool flag = Uri.IsWellFormedUriString(resource.Name, UriKind.Absolute);
            if (current3 != null)
            {
              if (!flag)
              {
                current3.Scripts.Add(new ScriptReference(resource.Name, type1.Assembly.FullName));
                break;
              }
              current3.Scripts.Add(new ScriptReference(resource.Name));
              break;
            }
            if (!flag)
            {
              this.Page.ClientScript.RegisterClientScriptResource(type1, this.ClientResourceManager.GetEmbeddedResourceName(resource.Name, resource.Static));
              break;
            }
            this.Page.ClientScript.RegisterClientScriptInclude(resource.Name, resource.Name);
            break;
          }
          this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), resource.Name, this.Page.ResolveUrl(resource.Name));
          break;
        case 2:
          this.Controls.Add((Control) this.GetImageControl(this.UseEmbeddedThemes ? this.Page.ClientScript.GetWebResourceUrl(type1, this.ClientResourceManager.GetEmbeddedResourceName(resource.Name, resource.Static)) : this.Page.ResolveClientUrl(this.ClientResourceManager.GetExternalResourcePath(resource.Name, resource.Static, this.Page))));
          break;
      }
    }

    private void RegisterLibrary(ResourceFile resourceFile)
    {
      ScriptRef scriptRef = ScriptRef.Empty;
      ScriptRef scripts;
      switch (resourceFile.JavaScriptLibrary)
      {
        case JavaScriptLibrary.JQuery:
          scripts = scriptRef | ScriptRef.JQuery;
          break;
        case JavaScriptLibrary.Mootools:
          scripts = scriptRef | ScriptRef.Mootools;
          break;
        case JavaScriptLibrary.Prototype:
          scripts = scriptRef | ScriptRef.Prototype;
          break;
        case JavaScriptLibrary.JQueryFancyBox:
          scripts = scriptRef | ScriptRef.JQueryFancyBox;
          break;
        case JavaScriptLibrary.KendoAll:
          scripts = scriptRef | ScriptRef.KendoAll;
          break;
        case JavaScriptLibrary.KendoWeb:
          scripts = scriptRef | ScriptRef.KendoWeb;
          break;
        case JavaScriptLibrary.JQueryCookie:
          scripts = scriptRef | ScriptRef.JQueryCookie;
          break;
        case JavaScriptLibrary.JQueryValidate:
          scripts = scriptRef | ScriptRef.JQueryValidate;
          break;
        case JavaScriptLibrary.JQueryUI:
          scripts = scriptRef | ScriptRef.JQueryUI;
          break;
        case JavaScriptLibrary.AngularJS:
          scripts = scriptRef | ScriptRef.AngularJS;
          break;
        default:
          return;
      }
      PageManager.ConfigureScriptManager(this.Page, scripts, false);
    }

    /// <summary>Gets the style sheet link.</summary>
    /// <param name="resourceUrl">The resource URL.</param>
    /// <returns></returns>
    protected virtual HtmlLink GetStyleSheetLink(string resourceUrl)
    {
      if (string.IsNullOrEmpty(resourceUrl))
        throw new ArgumentNullException(resourceUrl);
      HtmlLink styleSheetLink = new HtmlLink();
      styleSheetLink.Attributes.Add("href", resourceUrl);
      styleSheetLink.Attributes.Add("type", "text/css");
      styleSheetLink.Attributes.Add("rel", "stylesheet");
      return styleSheetLink;
    }

    /// <summary>Gets the image control.</summary>
    /// <param name="resourceUrl">The resource URL.</param>
    /// <returns></returns>
    protected virtual System.Web.UI.WebControls.Image GetImageControl(string resourceUrl) => !string.IsNullOrEmpty(resourceUrl) ? new System.Web.UI.WebControls.Image()
    {
      ImageUrl = resourceUrl
    } : throw new ArgumentNullException(nameof (resourceUrl));

    /// <summary>
    /// Determines the type of the resource based on its extension
    /// </summary>
    /// <param name="resourceName">Name of the resource.</param>
    /// <returns>
    /// The type of resource defined in the ResourceName property
    /// </returns>
    protected virtual WebResourceType GetResourceType(string resourceName)
    {
      if (string.IsNullOrEmpty(resourceName))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ResourceLink_ResourceNameNotDefined);
      int length = resourceName.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 ? resourceName.IndexOf("?", StringComparison.OrdinalIgnoreCase) : throw new InvalidOperationException(Res.Get<ErrorMessages>().ResourceLink_ResourceNameMissingExtension);
      if (length > 0)
        resourceName = resourceName.Substring(0, length);
      string str = resourceName.Substring(resourceName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase));
      if (!ResourceLinks.allowedExtensions.Contains(str.ToLowerInvariant()))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ResourceLink_ResourceNameInvalidExtension);
      switch (resourceName.Substring(resourceName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase)).ToLower())
      {
        case ".css":
        case ".less":
          return WebResourceType.StyleSheet;
        case ".gif":
        case ".jpeg":
        case ".jpg":
        case ".png":
          return WebResourceType.Image;
        case ".js":
          return WebResourceType.JavaScript;
        default:
          return WebResourceType.StyleSheet;
      }
    }

    /// <summary>Sets the resource property.</summary>
    /// <param name="resProp">The resource property.</param>
    private void SetResourceProperty(EmbeddedResourcePropertySetter resProp)
    {
      Control control = this.FindControl(resProp.ControlID, (Control) this.Page);
      if (control == null)
        throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().ControlNotFoundOnPage, (object) resProp.ControlID));
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) control).Find(resProp.ControlPropertyName, false);
      if (propertyDescriptor == null)
        throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().PropertyNotFoundInControl, (object) resProp.ControlID, (object) control.GetType(), (object) resProp.ControlPropertyName));
      Type type = Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
      if (!string.IsNullOrEmpty(resProp.AssemblyInfo))
        type = Type.GetType(resProp.AssemblyInfo);
      string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(type, resProp.Name);
      propertyDescriptor.SetValue((object) control, (object) webResourceUrl);
    }

    /// <summary>Find a control recursively and use a cache.</summary>
    /// <param name="ID">pageId to search for</param>
    /// <param name="root">current root control</param>
    /// <returns>Found control or null</returns>
    private Control FindControl(string id, Control root)
    {
      if (string.IsNullOrEmpty(id) || root == null)
        return (Control) null;
      string upperInvariant = id.ToUpperInvariant();
      Control typedControl;
      if (!this.controlsCache.TryGetValue(upperInvariant, out typedControl))
      {
        lock (this.controlsCacheLock)
        {
          if (!this.controlsCache.TryGetValue(upperInvariant, out typedControl))
          {
            typedControl = this.FindTypedControl(id, (Type) null, TraverseMethod.DepthFirst);
            this.controlsCache[upperInvariant] = typedControl;
          }
        }
      }
      return typedControl;
    }

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified ID type,
    /// using Depth First algorithm.
    /// </summary>
    /// <param name="pageId">ID to search for.</param>
    /// <param name="searchType">The searched control type.</param>
    /// <param name="method">Defines the serach algorithm.</param>
    /// <returns>The control if found otherwise null.</returns>
    protected virtual Control FindTypedControl(
      string id,
      Type searchType,
      TraverseMethod method)
    {
      bool flag = !string.IsNullOrEmpty(id);
      ControlTraverser controlTraverser = new ControlTraverser(this.Parent, method);
      if (flag && searchType != (Type) null)
      {
        foreach (Control typedControl in controlTraverser)
        {
          if (typedControl.ID == id && searchType.IsAssignableFrom(typedControl.GetType()))
            return typedControl;
        }
      }
      else if (flag)
      {
        foreach (Control typedControl in controlTraverser)
        {
          if (typedControl.ID == id)
            return typedControl;
        }
      }
      else
      {
        if (!(searchType != (Type) null))
          throw new ArgumentException("At least id or searchType have to be specified.");
        foreach (Control typedControl in controlTraverser)
        {
          if (searchType.IsAssignableFrom(typedControl.GetType()))
            return typedControl;
        }
      }
      return (Control) null;
    }

    private bool IsSystemStylesheet(ResourceFile resource) => ResourceLinks.systemStyleSheetsList.Any<string>((Func<string, bool>) (s => s == resource.Name));

    private void CheckIfContextUsesEmbeddedBackendTheme()
    {
      bool result;
      if (((SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Items == null || !SystemManager.CurrentHttpContext.Items.Contains((object) "sf_use_embedded_backend_theme") ? 0 : (bool.TryParse(SystemManager.CurrentHttpContext.Items[(object) "sf_use_embedded_backend_theme"].ToString(), out result) ? 1 : 0)) & (result ? 1 : 0)) == 0)
        return;
      this.Theme = Config.Get<AppearanceConfig>().BackendTheme;
      this.UseEmbeddedThemes = true;
    }

    internal static IList<RequiresEmbeddedWebResourceAttribute> ContextDynamicWebResources
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (!(currentHttpContext.Items[(object) "sf_dynamic_web_resources"] is IList<RequiresEmbeddedWebResourceAttribute> dynamicWebResources))
        {
          dynamicWebResources = (IList<RequiresEmbeddedWebResourceAttribute>) new List<RequiresEmbeddedWebResourceAttribute>();
          currentHttpContext.Items[(object) "sf_dynamic_web_resources"] = (object) dynamicWebResources;
        }
        return dynamicWebResources;
      }
    }

    internal static void RegisterDynamicWebResourceContainer(Control parent)
    {
      List<RequiresEmbeddedWebResourceAttribute> list = ResourceLinks.ContextDynamicWebResources.Distinct<RequiresEmbeddedWebResourceAttribute>().ToList<RequiresEmbeddedWebResourceAttribute>();
      if (list.Count <= 0)
        return;
      ResourceLinks child = new ResourceLinks();
      child.Theme = "Basic";
      child.UseEmbeddedThemes = true;
      foreach (RequiresEmbeddedWebResourceAttribute resourceAttribute in list)
        child.Links.Add(new ResourceFile()
        {
          Name = resourceAttribute.Name,
          AssemblyInfo = resourceAttribute.TypeFullName,
          Static = true
        });
      parent.Controls.Add((Control) child);
      list.Clear();
    }

    internal class Constants
    {
      internal const string LayoutBasics = "Telerik.Sitefinity.Resources.Themes.LayoutsBasics.css";
      internal const string DynamicWebResources = "sf_dynamic_web_resources";
      internal const string UseEmbeddedBackendTheme = "sf_use_embedded_backend_theme";
    }
  }
}
