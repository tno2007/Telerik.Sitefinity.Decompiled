// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.IContentView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>Interface to be implemented by content view controls.</summary>
  public interface IContentView
  {
    /// <summary>
    /// Gets or sets the URL key prefix. Used when building or evaluating URLs for paging and filtering
    /// </summary>
    /// <value>The URL key prefix.</value>
    string UrlKeyPrefix { get; set; }

    /// <summary>
    /// Gets the definition for the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.IContentView" /> control.
    /// </summary>
    ContentViewControlDefinition ControlDefinition { get; }

    /// <summary>
    /// Gets the definition for the master view specified through the
    /// MasterViewName property.
    /// </summary>
    IContentViewMasterDefinition MasterViewDefinition { get; }

    /// <summary>
    /// Gets the definition for the detail view specified through the
    /// DetailViewName property.
    /// </summary>
    IContentViewDetailDefinition DetailViewDefinition { get; }

    /// <summary>Gets or sets the display mode of the content view.</summary>
    /// <remarks>
    /// Note that this enumeration differs from the FieldDisplayMode.
    /// </remarks>
    ContentViewDisplayMode ContentViewDisplayMode { get; set; }
  }
}
