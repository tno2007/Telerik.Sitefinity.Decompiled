// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.Reports.NewslettersReportBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Newsletters.Data.Reports
{
  /// <summary>
  /// Base abstract class to be implemented by all the newsletter reports.
  /// </summary>
  public abstract class NewslettersReportBase
  {
    private string providerName;
    private NewslettersManager manager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Data.Reports.NewslettersReportBase" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public NewslettersReportBase(string providerName) => this.providerName = providerName;

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    protected NewslettersManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = NewslettersManager.GetManager(this.providerName);
        return this.manager;
      }
    }
  }
}
