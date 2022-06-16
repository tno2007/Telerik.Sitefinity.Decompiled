// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ISitefinityPage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Handles requests to Sitefinity pages.</summary>
  public interface ISitefinityPage
  {
    /// <summary>
    /// Gets <see cref="P:Telerik.Sitefinity.Web.UI.ISitefinityPage.RequestContext" /> for the page.
    /// </summary>
    RequestContext RequestContext { get; set; }

    /// <summary>Gets the content place holders in this page.</summary>
    /// <value>The place holders.</value>
    PlaceHoldersCollection PlaceHolders { get; set; }

    /// <summary>
    /// Gets or sets the URL evaluation mode - URL segments or query string.
    /// This property is used by all controls on a page that have URL Evaluators. Information for interpreting a url
    /// for a specific item or page is passed either through the URL itself or through the QueryString. The
    /// value of this property indicates which one is used.
    /// </summary>
    UrlEvaluationMode UrlEvaluationMode { get; set; }
  }
}
