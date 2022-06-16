// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IContentPlaceHolderContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Interface for retrieving content place holders.</summary>
  public interface IContentPlaceHolderContainer : ITemplate
  {
    /// <summary>
    /// Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to.
    /// These child controls are in turn defined within an inline template.
    /// </summary>
    /// <param name="container">
    /// The <see cref="T:System.Web.UI.Control" /> object to contain the instances of controls from the inline template.
    /// </param>
    /// <returns>
    /// A list of content place holders contained in the template.
    /// </returns>
    PlaceHoldersCollection InstantiateIn(Control container);

    /// <summary>
    /// Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to.
    /// These child controls are in turn defined within an inline template.
    /// </summary>
    /// <param name="container">
    /// The <see cref="T:System.Web.UI.Control" /> object to contain the instances of controls from the inline template.
    /// </param>
    /// <param name="placeHolders">The place holders.</param>
    void InstantiateIn(Control container, PlaceHoldersCollection placeHolders);
  }
}
