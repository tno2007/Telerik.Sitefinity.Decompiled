// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Contracts.IGlobalCommentsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.SiteSettings;

namespace Telerik.Sitefinity.Modules.GenericContent.Contracts
{
  /// <summary>
  /// Contract for global comments settings configuration properties.
  /// </summary>
  [Obsolete("Settings for comments are in CommentsModuleConfig.")]
  [ConfigSettings("/CommentsConfig/commentsSettings")]
  public interface IGlobalCommentsSettings : ICommentsSettings
  {
    /// <summary>
    /// Gets or sets a value indicating whether to display name field.
    /// </summary>
    /// <value>When set to <c>true</c> name field is displayed; otherwise, <c>false</c>.</value>
    bool DisplayNameField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the name field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if name field is required; otherwise, <c>false</c>.
    /// </value>
    bool IsNameFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display email field.
    /// </summary>
    /// <value>When set to <c>true</c> email field is displayed; otherwise, <c>false</c>.</value>
    bool DisplayEmailField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the email field is required; otherwise, <c>false</c>.
    /// </value>
    bool IsEmailFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display website field.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> website field is displayed; otherwise, <c>false</c>.
    /// </value>
    bool DisplayWebSiteField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the website field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if website field is required; otherwise, <c>false</c>.
    /// </value>
    bool IsWebSiteFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display message field.
    /// </summary>
    /// <value>When set to <c>true</c> message field is displayed; otherwise, <c>false</c>.</value>
    bool DisplayMessageField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the message field is required; otherwise, <c>false</c>.
    /// </value>
    bool IsMessageFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to	<c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    bool UseSpamProtectionImage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to hide comments after specified number of days. The number of days is specified with value of property <see cref="P:Telerik.Sitefinity.Modules.GenericContent.Contracts.IGlobalCommentsSettings.NumberOfDaysToHideComments" />.
    /// The property depends on property
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments are hidden after specified number of days; otherwise, <c>false</c>.
    /// </value>
    bool HideCommentsAfterNumberOfDays { get; set; }

    /// <summary>
    /// Gets or sets the number of days after which to hide comment item.
    /// </summary>
    /// <value>The number of days to hide comments.</value>
    int NumberOfDaysToHideComments { get; set; }
  }
}
