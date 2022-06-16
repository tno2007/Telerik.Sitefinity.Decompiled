// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicensedDomainItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>Keeps information for the licensed tenant domain</summary>
  public class LicensedDomainItem
  {
    internal List<string> domains = new List<string>();
    private List<LicensedModuleInfo> licensedModules = new List<LicensedModuleInfo>();
    private int totalPublicPagesLimit;
    private bool disableSubDomains;

    /// <summary>Gets  the list of licensed domains.</summary>
    /// <value>The domains.</value>
    public IEnumerable<string> Domains
    {
      get
      {
        if (this.domains != null)
        {
          for (int i = 0; i < this.domains.Count; ++i)
            yield return this.domains[i];
        }
      }
      internal set => this.domains = new List<string>(value);
    }

    /// <summary>Gets or sets the licensed modules collection.</summary>
    /// <value>The licensed modules.</value>
    public IEnumerable<LicensedModuleInfo> LicensedModules
    {
      get
      {
        if (this.licensedModules != null)
        {
          for (int i = 0; i < this.licensedModules.Count; ++i)
            yield return this.licensedModules[i].Clone();
        }
      }
      internal set => this.licensedModules = new List<LicensedModuleInfo>(value);
    }

    /// <summary>Gets or sets the total public pages.</summary>
    /// <value>The total public pages.</value>
    public int TotalPublicPagesLimit
    {
      get => this.totalPublicPagesLimit;
      internal set => this.totalPublicPagesLimit = value;
    }

    /// <summary>Gets the licensed modules without cloning.</summary>
    /// <returns></returns>
    internal IEnumerable<LicensedModuleInfo> GetLicensedModules()
    {
      if (this.licensedModules != null)
      {
        for (int i = 0; i < this.licensedModules.Count; ++i)
          yield return this.licensedModules[i];
      }
    }

    internal void LoadXml(string xml) => this.LoadXml(XElement.Load(XmlReader.Create((TextReader) new StringReader(xml))));

    internal void LoadXml(XElement element)
    {
      this.domains = LicenseInfo.LoadDomainValues(element);
      this.licensedModules = LicenseInfo.LoadLicensedModules(element);
      this.totalPublicPagesLimit = LicenseInfo.LoadTotalPublicPagesLimit(element);
    }

    protected internal XElement ToXmlElement()
    {
      XElement root = new XElement((XName) nameof (LicensedDomainItem));
      LicenseInfo.StoreDomains(root, this.Domains);
      LicenseInfo.StoreLicensedModules(root, this.LicensedModules);
      LicenseInfo.StorePublicPagesLimit(root, this.TotalPublicPagesLimit);
      return root;
    }

    protected internal string ToXmlString() => this.ToXmlElement().ToString(SaveOptions.DisableFormatting);

    /// <summary>Gets the Site validation errors like missing domains.</summary>
    /// <returns></returns>
    internal string[] GetValidationErrors()
    {
      List<string> errors = new List<string>();
      LicenseInfo.ValidateDomains(this.Domains, errors);
      LicenseInfo.ValidateModules(this.LicensedModules, errors);
      return errors.ToArray();
    }
  }
}
