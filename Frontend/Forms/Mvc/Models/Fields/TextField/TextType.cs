// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField.TextType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Frontend.Forms.Mvc.Models.Fields.TextField
{
  /// <summary>
  /// Enumeration defines possible types for text field input type.
  /// </summary>
  public enum TextType
  {
    /// <summary>Default. Defines a single-line text field.</summary>
    Text,
    /// <summary>Defines a color picker</summary>
    Color,
    /// <summary>
    /// Defines a date control (year, month and day (no time))
    /// </summary>
    Date,
    /// <summary>
    /// Defines a date and time control (year, month, day, hour, minute, second, and fraction of a second (no time zone)
    /// </summary>
    DateTimeLocal,
    /// <summary>Defines a field for an e-mail address</summary>
    Email,
    /// <summary>Defines a hidden input field</summary>
    Hidden,
    /// <summary>Defines a month and year control (no time zone)</summary>
    Month,
    /// <summary>Defines a field for entering a number</summary>
    Number,
    /// <summary>Defines a password field (characters are masked)</summary>
    Password,
    /// <summary>
    /// Defines a control for entering a number whose exact value is not important (like a slider control)
    /// </summary>
    Range,
    /// <summary>Defines a field for entering a telephone number</summary>
    Tel,
    /// <summary>Defines a control for entering a time (no time zone)</summary>
    Time,
    /// <summary>Defines a field for entering a URL</summary>
    Url,
    /// <summary>Defines a week and year control (no time zone)</summary>
    Week,
  }
}
