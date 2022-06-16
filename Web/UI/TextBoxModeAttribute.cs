// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.TextBoxModeAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Defines the mode of the TextBox in property editor.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class TextBoxModeAttribute : Attribute
  {
    private TextBoxMode txtBoxMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.TextBoxModeAttribute" /> class.
    /// </summary>
    public TextBoxModeAttribute() => this.txtBoxMode = TextBoxMode.SingleLine;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.TextBoxModeAttribute" /> class.
    /// </summary>
    /// <param name="txtBoxMode">The <see cref="P:Telerik.Sitefinity.Web.UI.TextBoxModeAttribute.TextBoxMode" />.</param>
    public TextBoxModeAttribute(TextBoxMode txtBoxMode) => this.txtBoxMode = txtBoxMode;

    /// <summary>Gets the text box mode.</summary>
    /// <value>The text box mode.</value>
    public TextBoxMode TextBoxMode => this.txtBoxMode;
  }
}
