// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.PageVisitSentence
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Hosting;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services.Statistics;

namespace Telerik.Sitefinity.Clients
{
  /// <summary>
  /// This is a wrapper around ISentence type specialized for
  /// sentences about page visits.
  /// </summary>
  public class PageVisitSentence
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Clients.PageVisitSentence" />.
    /// </summary>
    /// <param name="originalSentence">
    /// An instance of the original sentence.
    /// </param>
    public PageVisitSentence(ISentence originalSentence) => this.TranslateSentence(originalSentence);

    /// <summary>
    /// Gets or sets the relative url of the page that was visited.
    /// </summary>
    public string RelativeUrl { get; set; }

    protected virtual void TranslateSentence(ISentence originalSentence)
    {
      if (string.IsNullOrEmpty(originalSentence.ObjectKey))
        return;
      string str = new Uri(originalSentence.ObjectKey).PathAndQuery;
      string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
      if (applicationVirtualPath.Length > 1)
        str = str.Substring(applicationVirtualPath.Length);
      this.RelativeUrl = str;
    }

    protected virtual PageManager GetPageManager(string providerName) => PageManager.GetManager(providerName);
  }
}
