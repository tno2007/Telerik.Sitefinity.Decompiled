// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Utilities.WebConfigReader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Hosting;
using System.Xml;

namespace Telerik.Sitefinity.UsageTracking.Utilities
{
  internal class WebConfigReader
  {
    public WebConfigReader(string webConfigPath)
    {
      if (string.IsNullOrEmpty(webConfigPath))
        throw new ArgumentException("webconfigPath can't be null or empty");
      if (!HostingEnvironment.IsHosted)
        return;
      string filename = HostingEnvironment.MapPath(webConfigPath);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(filename);
      this.WebConfig = xmlDocument;
    }

    protected WebConfigReader()
    {
    }

    public virtual XmlNodeList HttpErrorsElements => this.WebConfig?.DocumentElement.GetElementsByTagName("httpErrors");

    public virtual XmlNodeList CustomErrorsElement => this.WebConfig?.DocumentElement.GetElementsByTagName("customErrors");

    public virtual bool AreCustomErrorsConfigured
    {
      get
      {
        bool flag1 = this.HttpErrorsElements.Count > 0;
        foreach (XmlNode httpErrorsElement in this.HttpErrorsElements)
        {
          if (!httpErrorsElement.HasChildNodes)
          {
            flag1 = false;
            break;
          }
        }
        bool flag2 = this.CustomErrorsElement.Count > 0;
        foreach (XmlNode xmlNode in this.CustomErrorsElement)
        {
          if (!xmlNode.HasChildNodes || xmlNode.Attributes?["mode"]?.Value == "Off")
          {
            flag2 = false;
            break;
          }
        }
        return flag1 & flag2;
      }
    }

    public virtual bool StaticContentCacheSetToUseMaxMage
    {
      get
      {
        XmlNodeList elementsByTagName = this.WebConfig?.DocumentElement.GetElementsByTagName("clientCache");
        if (elementsByTagName.Count == 0)
          return false;
        foreach (XmlNode xmlNode in elementsByTagName)
        {
          if (xmlNode.Attributes?["cacheControlMode"]?.Value != "UseMaxAge")
            return false;
        }
        return true;
      }
    }

    public virtual bool HasInformationWCFServiceTracing
    {
      get
      {
        foreach (XmlNode selectNode in this.WebConfig?.DocumentElement.SelectNodes("/configuration/system.diagnostics/sources/source"))
        {
          string str = selectNode.Attributes["switchValue"]?.Value;
          if (str != null && str.Contains("Information"))
            return true;
        }
        return false;
      }
    }

    public virtual XmlDocument WebConfig { get; protected set; }
  }
}
