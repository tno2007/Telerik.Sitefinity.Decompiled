// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.TemplatedWizardStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Templated wizard step</summary>
  public class TemplatedWizardStep : WizardStepBase
  {
    private Control contentContainer;
    private ITemplate contentTemplate;

    /// <summary>The content template for the wizard step.</summary>
    /// <value>The content template for the wizard step.</value>
    public virtual ITemplate ContentTemplate
    {
      get => this.contentTemplate;
      set => this.contentTemplate = value;
    }

    /// <summary>Gets or sets the content template container.</summary>
    /// <value>The content template container.</value>
    public Control ContentTemplateContainer
    {
      get => this.contentContainer;
      set => this.contentContainer = value;
    }

    /// <summary>Gets or sets the skin to apply to the control.</summary>
    /// <value></value>
    /// <returns>
    /// The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The style sheet has already been applied.
    /// - or -
    /// The Page_PreInit event has already occurred.
    /// - or -
    /// The control was already added to the Controls collection.
    /// </exception>
    public override string SkinID
    {
      get => base.SkinID;
      set => base.SkinID = value;
    }
  }
}
