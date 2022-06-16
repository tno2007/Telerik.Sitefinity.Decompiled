// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.WebConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Xml;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Configuration
{
  internal static class WebConfig
  {
    static WebConfig()
    {
      if (!HostingEnvironment.IsHosted)
        return;
      string str = HostingEnvironment.MapPath("~/web.config");
      if (!File.Exists(str))
        return;
      XmlDocument doc = new XmlDocument();
      doc.Load(str);
      WebConfig.LoadResourceProviderRactoryType(doc);
      WebConfig.LoadTagNamespeces(doc);
      WebConfig.LoadCacheMaxAge(doc);
    }

    internal static Type ResourceProviderFactoryType { get; private set; }

    internal static TagNamespace[] TagNamespaces { get; private set; }

    internal static string CacheMaxAge { get; private set; }

    private static void LoadCacheMaxAge(XmlDocument doc) => WebConfig.CacheMaxAge = doc.DocumentElement.SelectNodes("/configuration/system.webServer/staticContent/clientCache")?.Item(0)?.Attributes["cacheControlMaxAge"]?.Value;

    private static void LoadResourceProviderRactoryType(XmlDocument doc)
    {
      XmlNode xmlNode = doc.SelectSingleNode("configuration/system.web/globalization");
      if (xmlNode == null)
        return;
      XmlAttribute attribute = xmlNode.Attributes["resourceProviderFactoryType"];
      if (attribute == null || string.IsNullOrEmpty(attribute.Value))
        return;
      WebConfig.ResourceProviderFactoryType = TypeResolutionService.ResolveType(attribute.Value, false);
    }

    private static void LoadTagNamespeces(XmlDocument doc)
    {
      XmlNode xmlNode = doc.SelectSingleNode("configuration/system.web/pages/controls");
      List<TagNamespace> tagNamespaceList = new List<TagNamespace>();
      if (xmlNode != null && xmlNode.HasChildNodes)
      {
        foreach (XmlNode childNode in xmlNode.ChildNodes)
          tagNamespaceList.Add(new TagNamespace(childNode.Attributes["tagPrefix"].Value, childNode.Attributes["namespace"] != null ? childNode.Attributes["namespace"].Value : string.Empty));
      }
      WebConfig.TagNamespaces = tagNamespaceList.ToArray();
    }
  }
}
