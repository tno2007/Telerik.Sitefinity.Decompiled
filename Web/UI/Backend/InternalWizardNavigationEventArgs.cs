// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.InternalWizardNavigationEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  internal class InternalWizardNavigationEventArgs : EventArgs
  {
    private bool _cancel;
    private int _currentStepIndex;
    private int _nextStepIndex;

    /// <summary>Initializes a new instance of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardNavigationEventArgs"></see> class.</summary>
    /// <param name="currentStepIndex">The index of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> object that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard"></see> control.</param>
    /// <param name="nextStepIndex">The index of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> object that the <see cref="T:System.Web.UI.WebControls.Wizard"></see> control will display next.</param>
    public InternalWizardNavigationEventArgs(int currentStepIndex, int nextStepIndex)
    {
      this._currentStepIndex = currentStepIndex;
      this._nextStepIndex = nextStepIndex;
    }

    internal void SetNextStepIndex(int nextStepIndex) => this._nextStepIndex = nextStepIndex;

    /// <summary>Gets or sets a value indicating whether the navigation to the next step in the wizard should be canceled.</summary>
    /// <returns>true if the navigation to the next step should be canceled; otherwise, false. The default value is false.</returns>
    public bool Cancel
    {
      get => this._cancel;
      set => this._cancel = value;
    }

    /// <summary>Gets the index of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> object currently displayed in the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</summary>
    /// <returns>A zero-based index value that represents the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</returns>
    public int CurrentStepIndex => this._currentStepIndex;

    /// <summary>Gets a value that represents the index of the <see cref="T:System.Web.UI.WebControls.WizardStep"></see> object that the <see cref="T:System.Web.UI.WebControls.Wizard"></see> control will display next.</summary>
    /// <returns>A zero-based index value that represents the <see cref="T:System.Web.UI.WebControls.WizardStep"></see> object that the <see cref="T:System.Web.UI.WebControls.Wizard"></see> control will display next.</returns>
    public int NextStepIndex => this._nextStepIndex;
  }
}
