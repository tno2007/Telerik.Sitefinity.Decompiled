// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.ProductContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Provides context information for a product that is exposed in a web service
  /// </summary>
  /// <remarks>
  /// Main benefit of wrapping the product is that WCF interceptions work.
  /// </remarks>
  [DataContract]
  public class ProductContext
  {
    private string[] urlNames;
    private bool allowMultipleUrls;
    private bool additionalUrlsRedirectToDefault;

    /// <summary>Gets or sets the URL names of the product.</summary>
    [DataMember]
    public string[] AdditionalUrlNames
    {
      get => this.urlNames;
      set => this.urlNames = value;
    }

    /// <summary>Gets or sets the flag, if multiple URLs are allowed.</summary>
    [DataMember]
    public bool AllowMultipleUrls
    {
      get => this.allowMultipleUrls;
      set => this.allowMultipleUrls = value;
    }

    /// <summary>
    /// Gets or sets the flag, if all additional URLs will redirect to the default one.
    /// </summary>
    [DataMember]
    public bool AdditionalUrlsRedirectToDefault
    {
      get => this.additionalUrlsRedirectToDefault;
      set => this.additionalUrlsRedirectToDefault = value;
    }
  }
}
