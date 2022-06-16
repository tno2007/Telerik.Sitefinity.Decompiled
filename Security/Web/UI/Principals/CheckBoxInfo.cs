// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.CheckBoxInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// A class to encapsulate information for a role checkbox in the GUI
  /// </summary>
  public class CheckBoxInfo
  {
    /// <summary>Text to display in the checkbox label</summary>
    public string Text;
    /// <summary>Value to set for the checkbox on the client side</summary>
    public string Value;
    private WebControl checkBox;
    private WebControl checkBoxLabel;

    /// <summary>Constructor</summary>
    /// <param name="clientId">Client Id of the checkbox</param>
    /// <param name="labelClientId">Client Id of the checkbox related label</param>
    /// <param name="text">Text to display in the checkbox label</param>
    /// <param name="value">Value to set for the checkbox on the client side</param>
    public CheckBoxInfo(WebControl checkBox, WebControl checkBoxLabel, string text, string value)
    {
      this.checkBox = checkBox;
      this.checkBoxLabel = checkBoxLabel;
      this.Text = text;
      this.Value = value;
    }

    /// <summary>Client Id of the checkbox</summary>
    public string ClientId => this.checkBox.ClientID;

    /// <summary>Client Id of the checkbox related label</summary>
    public string LabelClientId => this.checkBoxLabel.ClientID;
  }
}
