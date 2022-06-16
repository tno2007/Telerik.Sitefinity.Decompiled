// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ILocalizableFieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides common properties for all field controls that support visualizing data in a specific culture.
  /// </summary>
  public interface ILocalizableFieldControl : 
    IFieldControl,
    IField,
    IValidatable,
    IHasFieldDisplayMode
  {
    /// <summary>Gets or sets the UI culture of this control.</summary>
    /// <value>The UI culture.</value>
    CultureInfo UiCulture { get; set; }

    /// <summary>Gets or sets the culture of this control.</summary>
    /// <value>The culture.</value>
    CultureInfo Culture { get; set; }
  }
}
