// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Validation.Enums
{
  /// <summary>Validation formats of the field controls</summary>
  public enum ValidationFormat
  {
    /// <summary>none by default</summary>
    None,
    /// <summary>Validates alpha numerics</summary>
    AlphaNumeric,
    /// <summary>Validates currency</summary>
    Currency,
    /// <summary>Validates email addresses</summary>
    EmailAddress,
    /// <summary>Validates integers</summary>
    Integer,
    /// <summary>Validates internet urls</summary>
    InternetUrl,
    /// <summary>Validates non aplhanumerics</summary>
    NonAlphaNumeric,
    /// <summary>Validates numerics</summary>
    Numeric,
    /// <summary>Validates percentages</summary>
    Percentage,
    /// <summary>Validates US social security numbers</summary>
    USSocialSecurityNumber,
    /// <summary>Validates US zip codes</summary>
    USZipCode,
    /// <summary>Validates against custom regular expression</summary>
    Custom,
  }
}
