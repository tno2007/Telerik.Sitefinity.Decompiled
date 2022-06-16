// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlTraverser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Transforms a control hierarchy to a collection by traversing the hierarchy with a specified algorithm.
  /// The hierarchy is traversed on demand, as items in the collections are been enumerated.
  /// </summary>
  public class ControlTraverser : 
    IEnumerable<Control>,
    IEnumerable,
    IEnumerator<Control>,
    IDisposable,
    IEnumerator
  {
    private int index;
    private int capacity;
    private Control root;
    private Control control;
    private TraverseMethod method;
    private Stack<Control> stack;
    private Queue<Control> queue;

    /// <summary>
    /// Initializes new isntance of <see cref="T:Telerik.Sitefinity.Web.UI.ControlTraverser" /> using the provided control as root of
    /// the hierarchy.
    /// </summary>
    /// <param name="control">The root of the hierarchy.</param>
    /// <param name="method">Specifies the traversal algorithm.</param>
    public ControlTraverser(Control control, TraverseMethod method)
      : this(control, method, 16)
    {
    }

    /// <summary>
    /// Initializes new isntance of <see cref="T:Telerik.Sitefinity.Web.UI.ControlTraverser" /> using the provided control as root of
    /// the hierarchy and sets initial capacity for Stack or Queue buffer.
    /// </summary>
    /// <param name="control">The root of the hierarchy.</param>
    /// <param name="method">Specifies the traversal algorithm.</param>
    /// <param name="capacity">The initial capacity for Stack or Queue buffer.</param>
    public ControlTraverser(Control control, TraverseMethod method, int capacity)
    {
      this.root = control;
      this.method = method;
      this.capacity = capacity;
      this.Reset();
    }

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <returns>
    /// The element in the collection at the current position of the enumerator.
    /// </returns>
    public Control Current => this.control;

    /// <summary>Gets the traversal method.</summary>
    public TraverseMethod Method => this.method;

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    /// true if the enumerator was successfully advanced to the next element;
    /// false if the enumerator has passed the end of the collection.
    /// </returns>
    public Control Next()
    {
      switch (this.method)
      {
        case TraverseMethod.BreadthFirst:
          return this.GetBreadthFirst(false);
        case TraverseMethod.DepthFirst:
          return this.GetDepthFirst();
        default:
          return (Control) null;
      }
    }

    private Control GetDepthFirst()
    {
      if (this.control != null)
      {
        for (int index = this.control.Controls.Count - 1; index > -1; --index)
          this.stack.Push(this.control.Controls[index]);
        this.control = this.stack.Count <= 0 ? (Control) null : this.stack.Pop();
      }
      return this.control;
    }

    private Control GetBreadthFirst(bool isRecursionCall)
    {
      if (this.control != null)
      {
        if (this.queue.Count > 0 && !isRecursionCall)
          this.control = this.control.Parent;
        if (this.index < this.control.Controls.Count)
        {
          this.control = this.control.Controls[this.index++];
          this.queue.Enqueue(this.control);
        }
        else if (this.queue.Count > 0)
        {
          this.index = 0;
          this.control = this.queue.Dequeue();
          this.control = this.GetBreadthFirst(true);
        }
        else
          this.control = (Control) null;
      }
      return this.control;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<Control> GetEnumerator() => (IEnumerator<Control>) this;

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
    }

    object IEnumerator.Current => (object) this.Current;

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>true if the enumerator was successfully advanced to the next element;
    /// false if the enumerator has passed the end of the collection.
    /// </returns>
    public bool MoveNext() => this.Next() != null;

    /// <summary>
    /// Sets the enumerator to its initial position,
    /// which is before the first element in the collection.
    /// </summary>
    public void Reset()
    {
      this.control = this.root;
      switch (this.method)
      {
        case TraverseMethod.BreadthFirst:
          this.queue = new Queue<Control>(this.capacity);
          break;
        case TraverseMethod.DepthFirst:
          this.stack = new Stack<Control>(this.capacity);
          break;
      }
    }
  }
}
