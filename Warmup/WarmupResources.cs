// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.WarmupResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Warmup
{
  /// <summary>Resource class for the Warmup module</summary>
  [ObjectInfo(typeof (WarmupResources), Description = "WarmupResourcesDescription", Title = "WarmupResourcesTitle")]
  internal class WarmupResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Warmup.WarmupResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public WarmupResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Warmup.WarmupResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider">The <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public WarmupResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Gets the title of this class.</summary>
    [ResourceEntry("WarmupResourcesTitle", Description = "The title of this class.", LastModified = "2016/11/03", Value = "Warmup")]
    public string WarmupResourcesTitle => this[nameof (WarmupResourcesTitle)];

    /// <summary>Gets the description of this class.</summary>
    [ResourceEntry("WarmupResourcesDescription", Description = "The description of this class.", LastModified = "2016/11/03", Value = "Contains localizable resources for Warmup module.")]
    public string WarmupResourcesDescription => this[nameof (WarmupResourcesDescription)];

    /// <summary>Gets the Warmup settings configuration title.</summary>
    [ResourceEntry("WarmupConfigTitle", Description = "The title of this class.", LastModified = "2017/02/15", Value = "Warmup settings")]
    public string WarmupConfigTitle => this[nameof (WarmupConfigTitle)];

    /// <summary>Gets the Warmup settings configuration description.</summary>
    [ResourceEntry("WarmupConfigDescription", Description = "The description of this class.", LastModified = "2016/11/03", Value = "Resource strings for Warmup settings.")]
    public string WarmupConfigDescription => this[nameof (WarmupConfigDescription)];

    /// <summary>Gets the Request Timeout configuration title</summary>
    /// <value>Request Timeout</value>
    [ResourceEntry("RequestTimeoutTitle", Description = "Gets the Request Timeout configuration title", LastModified = "2017/02/14", Value = "Request Timeout")]
    public string RequestTimeoutTitle => this[nameof (RequestTimeoutTitle)];

    /// <summary>Gets the Request Timeout configuration description</summary>
    /// <value>The length of time (in milliseconds) to wait before the HTTP request is terminated.</value>
    [ResourceEntry("RequestTimeoutDescription", Description = "Gets the Request Timeout configuration description", LastModified = "2017/02/15", Value = "The length of time (in milliseconds) to wait before the HTTP request is terminated.")]
    public string RequestTimeoutDescription => this[nameof (RequestTimeoutDescription)];

    /// <summary>Gets the Max Requests On Startup configuration title</summary>
    /// <value>Max Requests On Startup</value>
    [ResourceEntry("MaxRequestsOnStartupTitle", Description = "Gets the Max Requests On Startup configuration title", LastModified = "2017/02/15", Value = "Max Requests On Startup")]
    public string MaxRequestsOnStartupTitle => this[nameof (MaxRequestsOnStartupTitle)];

    /// <summary>
    /// Gets the Max Requests On Startup configuration description
    /// </summary>
    /// <value>The maximum number of requests to execute during system initialization.</value>
    [ResourceEntry("MaxRequestsOnStartupDescription", Description = "Gets the Max Requests On Startup configuration description", LastModified = "2017/02/15", Value = "The maximum number of requests to execute during system initialization.")]
    public string MaxRequestsOnStartupDescription => this[nameof (MaxRequestsOnStartupDescription)];

    /// <summary>Gets the Plugins configuration title</summary>
    /// <value>Plugins title</value>
    [ResourceEntry("PluginsTitle", Description = "Gets the Plugins configuration title", LastModified = "2017/02/14", Value = "Plugins")]
    public string PluginsTitle => this[nameof (PluginsTitle)];

    /// <summary>Gets the Plugins configuration description</summary>
    /// <value>A collection of providers that serve URLs to be requested.</value>
    [ResourceEntry("PluginsDescription", Description = "Gets the Plugins configuration description", LastModified = "2017/02/15", Value = "A collection of Warmup URL providers. Their purpose is to serve URLs that will be requested.")]
    public string PluginsDescription => this[nameof (PluginsDescription)];

    /// <summary>Gets the Plugin type configuration title</summary>
    /// <value>Type title</value>
    [ResourceEntry("PluginTypeTitle", Description = "Gets the Plugin type configuration title", LastModified = "2017/02/15", Value = "Type")]
    public string PluginTypeTitle => this[nameof (PluginTypeTitle)];

    /// <summary>Gets the Plugin type configuration description</summary>
    /// <value>The full name of the Warmup plugin class type. Example: Telerik.Sitefinity.Warmup.Plugins.SitemapPlugin.</value>
    [ResourceEntry("PluginTypeDescription", Description = "Gets the Plugin type configuration description", LastModified = "2017/02/15", Value = "The full type name of the Warmup plugin class, e.g. Telerik.Sitefinity.Warmup.Plugins.SitemapPlugin.")]
    public string PluginTypeDescription => this[nameof (PluginTypeDescription)];

    /// <summary>Gets the Plugin priority configuration title</summary>
    /// <value>Priority title</value>
    [ResourceEntry("PluginPriorityTitle", Description = "Gets the Plugin priority configuration title", LastModified = "2017/02/15", Value = "Priority")]
    public string PluginPriorityTitle => this[nameof (PluginPriorityTitle)];

    /// <summary>Gets the Plugin priority configuration description</summary>
    /// <value>Enables the Warmup module to determine the order in which to acquire URLs from the plugins. A plugin with a higher priority will serve URLs earlier than a plugin with a lower priority.</value>
    [ResourceEntry("PluginPriorityDescription", Description = "Gets the Plugin priority configuration description", LastModified = "2017/02/15", Value = "Enables the Warmup module to determine the order in which to acquire URLs from the plugins. A plugin with a higher priority will serve URLs earlier than a plugin with a lower priority.")]
    public string PluginPriorityDescription => this[nameof (PluginPriorityDescription)];

    /// <summary>Gets the User-Agents configuration title</summary>
    /// <value>User Agents</value>
    [ResourceEntry("UserAgentsTitle", Description = "Gets the User-Agents configuration title", LastModified = "2017/02/14", Value = "User Agents")]
    public string UserAgentsTitle => this[nameof (UserAgentsTitle)];

    /// <summary>Gets the User-Agents configuration description</summary>
    /// <value>The Warmup module acts as a browser regarding the URLs it requests. It uses the User-Agent strings as browser identification during warm-up. If no User-Agent strings are specified, then the module fallbacks to using the User-Agent string of the current HTTP context or the value 'Warmup module' when there is no HTTP context.</value>
    [ResourceEntry("UserAgentsDescription", Description = "Gets the User-Agents configuration description", LastModified = "2017/02/15", Value = "The Warmup module acts as a browser regarding the URLs it requests. It uses the User-Agent strings as browser identification during warm-up. If no User-Agent strings are specified, then the module fallbacks to using the User-Agent string of the current HTTP context or the value 'Warmup module' when there is no HTTP context.")]
    public string UserAgentsDescription => this[nameof (UserAgentsDescription)];

    /// <summary>Gets the site warmup page title</summary>
    /// <value>text: Site warmup</value>
    [ResourceEntry("WarmupModulePageTitle", Description = "Gets the site warmup page title", LastModified = "2016/11/03", Value = "Site warmup")]
    public string WarmupModulePageTitle => this[nameof (WarmupModulePageTitle)];

    /// <summary>Gets the Warmup page description</summary>
    /// <value>Warmup the pages within your Sitefinity project</value>
    [ResourceEntry("WarmupModulePageDescription", Description = "Gets the Warmup page description", LastModified = "2016/11/03", Value = "Warmup the pages within your Sitefinity project")]
    public string WarmupModulePageDescription => this[nameof (WarmupModulePageDescription)];

    /// <summary>Gets Warmup module page URL name</summary>
    /// <value>text: Site-warmup</value>
    [ResourceEntry("WarmupModulePageUrlName", Description = "Warmup module page URL name", LastModified = "2016/11/03", Value = "Site-warmup")]
    public string WarmupModulePageUrlName => this[nameof (WarmupModulePageUrlName)];

    /// <summary>
    /// Gets External link for: Incorrect urls generated when using NLB and Nodes under different ports
    /// </summary>
    [ResourceEntry("ExternalLinkIncorrectUrlsNlbAndNodeDifferentPorts", Description = "External link for: Incorrect urls generated when using NLB and Nodes under different ports or using proxies", LastModified = "2020/04/10", Value = "https://knowledgebase.progress.com/articles/Article/Incorrect-URLs-generated-when-using-NLB-and-nodes-under-different-ports-or-using-proxies")]
    public string ExternalLinkIncorrectUrlsNlbAndNodeDifferentPortsOrUsingProxies => this[nameof (ExternalLinkIncorrectUrlsNlbAndNodeDifferentPortsOrUsingProxies)];
  }
}
