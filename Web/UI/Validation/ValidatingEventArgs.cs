// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Web.UI.Validation
{
  /// <summary>Provides data for events fired before validating.</summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "It's ok.")]
  public class ValidatingEventArgs : EventArgs
  {
    private object value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs" /> class.
    /// </summary>
    public ValidatingEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public ValidatingEventArgs(object value) => this.value = value;

    /// <summary>
    /// Gets or sets a value indicating whether the current validation should be canceled.
    /// </summary>
    /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    public bool Cancel { get; set; }

    /// <summary>Gets the value that is being validated.</summary>
    /// <value>The value that is being validated.</value>
    public object Value => this.value;
  }
}
