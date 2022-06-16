// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Contracts.ILanguagesDropDownListWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Contracts
{
  /// <summary>
  /// Specifies the common members for definitions of languages drop down list widget.
  /// </summary>
  public interface ILanguagesDropDownListWidgetDefinition : IWidgetDefinition, IDefinition
  {
    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    LanguageSource LanguageSource { get; set; }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    List<CultureInfo> AvailableCultures { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether an option for all languages should be added.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if an option for all languages is to be added; otherwise, <c>false</c>.
    /// </value>
    bool AddAllLanguagesOption { get; set; }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    string CommandName { get; set; }
  }
}
