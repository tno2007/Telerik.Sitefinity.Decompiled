// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public abstract class TypeEditorBase : SimpleView
  {
    public virtual object Value { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
    }

    protected abstract override string LayoutTemplateName { get; }

    public abstract string GetCancelButtonClientId();

    public abstract string GetSelectButtonClientId();
  }
}
