// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.InternalWizardStepCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  internal sealed class InternalWizardStepCollection : IList, ICollection, IEnumerable
  {
    private InternalWizard _wizard;

    internal InternalWizardStepCollection(InternalWizard wizard) => this._wizard = wizard;

    /// <summary>Appends the specified <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to the end of the collection.</summary>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to append to the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</param>
    /// <exception cref="T:System.ArgumentNullException">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object passed in is null.</exception>
    public void Add(InternalWizardStep wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.RemoveIfAlreadyExistsInWizard(wizardStep);
      wizardStep.Owner = this._wizard;
      this.Views.Add((Control) wizardStep);
      this.NotifyWizardStepsChanged();
    }

    /// <summary>Adds the specified <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to the collection at the specified index location.</summary>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to append to the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</param>
    /// <param name="index">The index location at which to insert <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object.</param>
    /// <exception cref="T:System.ArgumentNullException">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object passed in is null.</exception>
    public void AddAt(int index, InternalWizardStep wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.RemoveIfAlreadyExistsInWizard(wizardStep);
      wizardStep.Owner = this._wizard;
      this.Views.AddAt(index, (Control) wizardStep);
      this.NotifyWizardStepsChanged();
    }

    /// <summary>Removes all <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects from the collection.</summary>
    public void Clear()
    {
      this.Views.Clear();
      this.NotifyWizardStepsChanged();
    }

    /// <summary>Determines whether the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection contains a specific <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object.</summary>
    /// <returns>true if the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object is found in the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection; otherwise, false.</returns>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to find in the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</param>
    public bool Contains(InternalWizardStep wizardStep) => wizardStep != null ? this.Views.Contains((Control) wizardStep) : throw new ArgumentNullException(nameof (wizardStep));

    /// <summary>Copies all the items from a <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection to a compatible one-dimensional array of <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> objects, starting at the specified index in the target array.</summary>
    /// <param name="array">A zero-based array of <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> objects that receives the items copied from the collection.</param>
    /// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
    public void CopyTo(InternalWizardStep[] array, int index) => this.Views.CopyTo((Array) array, index);

    /// <summary>Returns an <see cref="T:System.Collections.IEnumerator"></see>-implemented object that can be used to iterate through the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects in the collection.</summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator"></see>-implemented object that contains all the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects in the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</returns>
    public IEnumerator GetEnumerator() => this.Views.GetEnumerator();

    private InternalWizardStep GetStepAndVerify(object value) => value is InternalWizardStep internalWizardStep ? internalWizardStep : throw new ArgumentException();

    /// <summary>Determines the index value that represents the position of the specified <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object in the collection.</summary>
    /// <returns>If found, the zero-based index of the first occurrence of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object passed in within the current <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection; otherwise, -1.</returns>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to search for in the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</param>
    /// <exception cref="T:System.ArgumentNullException">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object passed in is null.</exception>
    public int IndexOf(InternalWizardStep wizardStep) => wizardStep != null ? this.Views.IndexOf((Control) wizardStep) : throw new ArgumentNullException(nameof (wizardStep));

    /// <summary>Inserts the specified <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object into the collection at the specified index location.</summary>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to insert into the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</param>
    /// <param name="index">The index location at which to insert the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object.</param>
    public void Insert(int index, InternalWizardStep wizardStep) => this.AddAt(index, wizardStep);

    internal void NotifyWizardStepsChanged() => this._wizard.OnWizardStepsChanged();

    /// <summary>Removes the specified <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object from the collection.</summary>
    /// <param name="wizardStep">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to remove from the collection.</param>
    /// <exception cref="T:System.ArgumentNullException">The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object passed in is null.</exception>
    public void Remove(InternalWizardStep wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.Views.Remove((Control) wizardStep);
      wizardStep.Owner = (InternalWizard) null;
      this.NotifyWizardStepsChanged();
    }

    /// <summary>Removes the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object from the collection at the specified location.</summary>
    /// <param name="index">The index of the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object to remove.</param>
    public void RemoveAt(int index)
    {
      if (this.Views[index] is InternalWizardStep view)
        view.Owner = (InternalWizard) null;
      this.Views.RemoveAt(index);
      this.NotifyWizardStepsChanged();
    }

    private void RemoveIfAlreadyExistsInWizard(InternalWizardStep wizardStep)
    {
      if (wizardStep.Owner == null)
        return;
      wizardStep.Owner.WizardSteps.Remove(wizardStep);
    }

    void ICollection.CopyTo(Array array, int index) => this.Views.CopyTo(array, index);

    int IList.Add(object value)
    {
      InternalWizardStep stepAndVerify = this.GetStepAndVerify(value);
      this.Add(stepAndVerify);
      return this.IndexOf(stepAndVerify);
    }

    bool IList.Contains(object value) => this.Contains(this.GetStepAndVerify(value));

    int IList.IndexOf(object value) => this.IndexOf(this.GetStepAndVerify(value));

    void IList.Insert(int index, object value) => this.AddAt(index, this.GetStepAndVerify(value));

    void IList.Remove(object value) => this.Remove(this.GetStepAndVerify(value));

    /// <summary>Gets the number of <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects in the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control's <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</summary>
    /// <returns>The number of <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects in the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</returns>
    public int Count => this.Views.Count;

    /// <summary>Gets a value indicating whether the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived objects in the collection can be modified.</summary>
    /// <returns>true if the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection can be modified; otherwise, false. </returns>
    public bool IsReadOnly => this.Views.IsReadOnly;

    /// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
    /// <returns>false in all cases.</returns>
    public bool IsSynchronized => false;

    /// <summary>Gets a <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object from the collection at the specified index.</summary>
    /// <returns>The <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>-derived object in the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection at the specified index location.</returns>
    /// <param name="index">The index of the <see cref="T:Telerik.Cms.Web.UI.WizardStep"></see> object to retrieve.</param>
    public InternalWizardStep this[int index] => (InternalWizardStep) this.Views[index];

    /// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
    /// <returns>An object that can be used to synchronize access to the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection.</returns>
    public object SyncRoot => (object) this;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => (object) this.Views[index];
      set
      {
        this.RemoveAt(index);
        this.AddAt(index, this.GetStepAndVerify(value));
      }
    }

    private System.Web.UI.WebControls.ViewCollection Views => this._wizard.MultiView.Views;
  }
}
