// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.HtmlLinkModifier
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Utilities
{
  internal class HtmlLinkModifier
  {
    private List<string> trustedDomains;
    private readonly string anchorTagPattern = "<a.+?href=[\\\"|\\'](?=(?=.*:\\/\\/)|(?=\\/\\/))(.+?)[\\\"|\\']";

    public HtmlLinkModifier()
      : this((string) null, TrustedDomainOptions.None)
    {
    }

    public HtmlLinkModifier(string commaSeparatedDomains, TrustedDomainOptions options)
    {
      this.trustedDomains = new List<string>();
      if (!string.IsNullOrEmpty(commaSeparatedDomains))
        this.IncludeConfigDomainsAsTrusted(commaSeparatedDomains);
      if (options.HasFlag((Enum) TrustedDomainOptions.IncludeSiteDomains))
        this.IncludeSiteDomainsAsTrusted();
      if (options.HasFlag((Enum) TrustedDomainOptions.IncludeLicenseDomains))
        this.IncludeLicenseDomainsAsTrusted();
      this.trustedDomains = this.trustedDomains.Distinct<string>().ToList<string>();
    }

    public IEnumerable<string> TrustedDomains => (IEnumerable<string>) this.trustedDomains;

    public string AppendNoFollowAttributeToAnchorTags(string html)
    {
      if (!string.IsNullOrEmpty(html) && Regex.IsMatch(html, this.anchorTagPattern))
      {
        MatchCollection matchCollection = Regex.Matches(html, this.anchorTagPattern);
        StringBuilder stringBuilder = new StringBuilder(html);
        foreach (Match match in matchCollection)
        {
          string oldValue = match.Value;
          Uri uri;
          if (Uri.TryCreate(match.Groups?[1]?.Value, UriKind.Absolute, out uri))
          {
            if (!this.TrustedDomains.Any<string>((Func<string, bool>) (x => x == uri.Host)) && !oldValue.Contains("rel=\"nofollow\""))
            {
              string newValue = oldValue.Replace("<a ", "<a rel=\"nofollow\" ");
              stringBuilder.Replace(oldValue, newValue);
            }
            else if (this.TrustedDomains.Any<string>((Func<string, bool>) (x => x == uri.Host)) && oldValue.Contains("rel=\"nofollow\""))
            {
              string newValue = oldValue.Replace("rel=\"nofollow\" ", "");
              stringBuilder.Replace(oldValue, newValue);
            }
          }
        }
        html = stringBuilder.ToString();
      }
      return html;
    }

    private void IncludeConfigDomainsAsTrusted(string commaSeparatedDomains)
    {
      string[] strArray1;
      if (commaSeparatedDomains == null)
        strArray1 = (string[]) null;
      else
        strArray1 = commaSeparatedDomains.Split(new string[3]
        {
          ",",
          " ",
          ";"
        }, StringSplitOptions.RemoveEmptyEntries);
      string[] strArray2 = strArray1;
      if (strArray2 == null || ((IEnumerable<string>) strArray2).Count<string>() <= 0)
        return;
      this.trustedDomains.AddRange((IEnumerable<string>) strArray2);
    }

    private void IncludeSiteDomainsAsTrusted()
    {
      IEnumerable<ISite> sites = SystemManager.CurrentContext.GetSites();
      IEnumerable<string> strings1;
      if (sites == null)
      {
        strings1 = (IEnumerable<string>) null;
      }
      else
      {
        IEnumerable<string> source = sites.Select<ISite, string>((Func<ISite, string>) (x => x?.LiveUrl));
        strings1 = source != null ? source.Distinct<string>() : (IEnumerable<string>) null;
      }
      IEnumerable<string> strings2 = strings1;
      if (strings2 == null || !strings2.Any<string>())
        return;
      this.trustedDomains.AddRange(strings2);
    }

    private void IncludeLicenseDomainsAsTrusted()
    {
      IEnumerable<string> domains = LicenseState.Current?.LicenseInfo?.Domains;
      if (domains == null || !domains.Any<string>())
        return;
      this.trustedDomains.AddRange(domains);
    }
  }
}
