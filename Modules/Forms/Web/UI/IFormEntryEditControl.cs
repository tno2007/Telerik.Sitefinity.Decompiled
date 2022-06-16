// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.IFormEntryEditControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>
  /// Classes that implement this interface should provide UI for editing form entries.
  /// </summary>
  public interface IFormEntryEditControl
  {
    /// <summary>Gets or sets the name of the form.</summary>
    /// <value>The name of the form.</value>
    string FormName { get; set; }
  }
}
