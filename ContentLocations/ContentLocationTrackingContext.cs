// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationTrackingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides context objects used for tracking content location view.
  /// </summary>
  public class ContentLocationTrackingContext
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationTrackingContext" /> class.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="pageData">The page data.</param>
    /// <param name="controlData">The control data.</param>
    /// <param name="culture">The culture.</param>
    public ContentLocationTrackingContext(
      PageNode pageNode,
      PageData pageData,
      PageControl controlData,
      CultureInfo culture)
    {
      this.PageNode = pageNode;
      this.PageData = pageData;
      this.ControlData = controlData;
      this.Culture = culture;
    }

    /// <summary>Gets or sets the page node.</summary>
    /// <value>The page node.</value>
    public PageNode PageNode { get; protected set; }

    /// <summary>Gets or sets the page data.</summary>
    /// <value>The page data.</value>
    public PageData PageData { get; protected set; }

    /// <summary>Gets or sets the control data.</summary>
    /// <value>The control data.</value>
    public PageControl ControlData { get; protected set; }

    /// <summary>Gets or sets the culture.</summary>
    /// <value>The culture.</value>
    public CultureInfo Culture { get; protected set; }
  }
}
