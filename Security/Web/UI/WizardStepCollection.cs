// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.WizardStepCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Collection of WizardStep controls.</summary>
  public sealed class WizardStepCollection : IList, ICollection, IEnumerable
  {
    private CreateUserWizardForm wizard;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.WizardStepCollection" /> class.
    /// </summary>
    /// <param name="wizard">The wizard.</param>
    public WizardStepCollection(CreateUserWizardForm wizard) => this.wizard = wizard;

    /// <summary>Adds the specified wizard step.</summary>
    /// <param name="wizardStep">The wizard step.</param>
    public void Add(WizardStepBase wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.Views.Add((Control) wizardStep);
    }

    /// <summary>Adds the specified wizard step at given index.</summary>
    /// <param name="index">The index.</param>
    /// <param name="wizardStep">The wizard step.</param>
    public void AddAt(int index, WizardStepBase wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.Views.AddAt(index, (Control) wizardStep);
    }

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.IList" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// </exception>
    public void Clear() => this.Views.Clear();

    /// <summary>
    /// Determines whether the collection contains the specified wizard step.
    /// </summary>
    /// <param name="wizardStep">The wizard step.</param>
    /// <returns>
    /// 	<c>true</c> if collection contains the specified wizard step ; otherwise, <c>false</c>.
    /// </returns>
    public bool Contains(WizardStepBase wizardStep) => wizardStep != null ? this.Views.Contains((Control) wizardStep) : throw new ArgumentNullException(nameof (wizardStep));

    /// <summary>
    /// Copies the wizard steps in the array starting from the specified index.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <param name="index">The index.</param>
    public void CopyTo(WizardStepBase[] array, int index) => this.Views.CopyTo((Array) array, index);

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator GetEnumerator() => this.Views.GetEnumerator();

    private WizardStepBase GetStepAndVerify(object value) => value is WizardStepBase wizardStepBase ? wizardStepBase : throw new ArgumentException("Wizard_WizardStepOnly");

    /// <summary>
    /// Find the index in the collection for the specified wizard step.
    /// </summary>
    /// <param name="wizardStep">The wizard step.</param>
    /// <returns></returns>
    public int IndexOf(WizardStepBase wizardStep) => wizardStep != null ? this.Views.IndexOf((Control) wizardStep) : throw new ArgumentNullException(nameof (wizardStep));

    /// <summary>Inserts the wizard step at the specified index.</summary>
    /// <param name="index">The index.</param>
    /// <param name="wizardStep">The wizard step.</param>
    public void Insert(int index, WizardStepBase wizardStep) => this.AddAt(index, wizardStep);

    /// <summary>Removes the specified wizard step.</summary>
    /// <param name="wizardStep">The wizard step.</param>
    public void Remove(WizardStepBase wizardStep)
    {
      if (wizardStep == null)
        throw new ArgumentNullException(nameof (wizardStep));
      this.Views.Remove((Control) wizardStep);
    }

    /// <summary>
    /// Removes the <see cref="T:System.Collections.IList" /> item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="index" /> is not a valid index in the <see cref="T:System.Collections.IList" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.IList" /> is read-only.
    /// -or-
    /// The <see cref="T:System.Collections.IList" /> has a fixed size.
    /// </exception>
    public void RemoveAt(int index)
    {
      System.Web.UI.WebControls.View view = this.Views[index];
      this.Views.RemoveAt(index);
    }

    void ICollection.CopyTo(Array array, int index) => this.Views.CopyTo(array, index);

    int IList.Add(object value)
    {
      WizardStepBase stepAndVerify = this.GetStepAndVerify(value);
      this.Add(stepAndVerify);
      return this.IndexOf(stepAndVerify);
    }

    bool IList.Contains(object value) => this.Contains(this.GetStepAndVerify(value));

    int IList.IndexOf(object value) => this.IndexOf(this.GetStepAndVerify(value));

    void IList.Insert(int index, object value) => this.AddAt(index, this.GetStepAndVerify(value));

    void IList.Remove(object value) => this.Remove(this.GetStepAndVerify(value));

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of elements contained in the <see cref="T:System.Collections.ICollection" />.
    /// </returns>
    public int Count => this.Views.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, false.
    /// </returns>
    public bool IsReadOnly => this.Views.IsReadOnly;

    /// <summary>
    /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
    /// </summary>
    /// <value></value>
    /// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.
    /// </returns>
    public bool IsSynchronized => false;

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Security.Web.UI.WizardStepBase" /> at the specified index.
    /// </summary>
    /// <value></value>
    public WizardStepBase this[int index] => (WizardStepBase) this.Views[index];

    /// <summary>
    /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
    /// </returns>
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

    private ViewCollection Views => this.wizard.MultiView.Views;
  }
}
