// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Validation
{
  /// <summary>Provides data for events fired after validating.</summary>
  public class ValidatedEventArgs : EventArgs
  {
    private object value;
    private string errorMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs" /> class.
    /// </summary>
    public ValidatedEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs" /> class.
    /// </summary>
    /// <param name="isValid">if set to <c>true</c> [is valid].</param>
    /// <param name="value">The value.</param>
    /// <param name="errorMessage">The error message.</param>
    public ValidatedEventArgs(ref bool isValid, object value, string errorMessage)
    {
      this.IsValid = isValid;
      this.value = value;
      this.errorMessage = errorMessage;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is valid.
    /// </summary>
    /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
    public bool IsValid { get; set; }

    /// <summary>Gets the value that is being validated.</summary>
    /// <value>The value that is being validated.</value>
    public object Value => this.value;

    /// <summary>Gets the error message.</summary>
    /// <value>The error message.</value>
    public string ErrorMessage => this.errorMessage;
  }
}
