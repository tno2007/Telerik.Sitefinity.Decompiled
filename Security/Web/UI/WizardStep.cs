// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.WizardNavigationFormsEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Event arguments for wizard specific events.</summary>
  public class WizardNavigationFormsEventArgs : EventArgs
  {
    private bool cancel;
    private int currentStepIndex;
    private int nextStepIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.WizardNavigationFormsEventArgs" /> class.
    /// </summary>
    /// <param name="currentStepIndex">Index of the current step.</param>
    /// <param name="nextStepIndex">Index of the next step.</param>
    public WizardNavigationFormsEventArgs(int currentStepIndex, int nextStepIndex)
    {
      this.currentStepIndex = currentStepIndex;
      this.nextStepIndex = nextStepIndex;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to cancel the operation.
    /// </summary>
    /// <value><c>true</c> if the operation should be canceled; otherwise, <c>false</c>.</value>
    public bool Cancel
    {
      get => this.cancel;
      set => this.cancel = value;
    }

    /// <summary>Gets the index of the current step.</summary>
    /// <value>The index of the current step.</value>
    public int CurrentStepIndex => this.currentStepIndex;

    /// <summary>Gets or sets the index of the next step.</summary>
    /// <value>The index of the next step.</value>
    public int NextStepIndex
    {
      get => this.nextStepIndex;
      set => this.nextStepIndex = value;
    }
  }
}
