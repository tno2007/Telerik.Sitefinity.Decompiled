// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.DynamicContentWorkflowStatusInfoField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying information for the status of the dynamic content workflow
  /// </summary>
  [RequiresDataItem]
  public class DynamicContentWorkflowStatusInfoField : ContentWorkflowStatusInfoField
  {
    /// <summary>
    /// Initializes the controls. Hides the OpenRevisionHistoryButton for dynamic items
    /// </summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.OpenRevisionHistoryButton.Visible = true;
      base.InitializeControls(container);
    }
  }
}
