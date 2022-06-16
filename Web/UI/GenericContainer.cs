// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.GenericContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents base implementation for template container.
  /// Template containers are controls that are used as containers for other controls
  /// defined through control templates (ITemplate).
  /// </summary>
  [ToolboxItem(false)]
  public class GenericContainer : Control, INamingContainer
  {
    private bool isExternal;
    private string templateName;
    private Dictionary<string, object> definedControls;

    /// <summary>
    /// Gets or sets the name of the template instantiated in this container.
    /// </summary>
    public string TemplateName
    {
      get => this.templateName;
      set => this.templateName = value;
    }

    /// <summary>
    /// Indicates whether this container contains external template.
    /// </summary>
    public bool IsExternal
    {
      get => this.isExternal;
      set => this.isExternal = value;
    }

    /// <summary>
    /// Gets a dictionary of defined controls and their IDs as keys.
    /// Note, the dictionary is used as temporary cache for controls and there is no guarantee
    /// thta all defined controls are present in the dictionary.
    /// </summary>
    protected Dictionary<string, object> DefinedControls
    {
      get
      {
        if (this.definedControls == null)
          this.definedControls = new Dictionary<string, object>();
        return this.definedControls;
      }
    }

    /// <summary>
    /// Searches the current <see cref="T:System.Web.UI.INamingContainer" /> for a control with the specified ID and
    /// required type and caches the result for fast access.
    /// Note, this method searches only the current <see cref="T:System.Web.UI.INamingContainer" />.
    /// To search the enire control hierarchy, use overload of this method with
    /// <see cref="T:Telerik.Sitefinity.Web.UI.TraverseMethod" /> parameter.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the searched control.</typeparam>
    /// <param name="pageId">ID to search for.</param>
    /// <param name="required">
    /// Indicates whether an exception should be thrown if the searched control is not found.
    /// </param>
    /// <returns>The control if found otherwise null.</returns>
    public virtual T GetControl<T>(string id, bool required)
    {
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException(nameof (id));
      object control;
      if (!this.DefinedControls.TryGetValue(id, out control))
      {
        Control typedControl = this.FindTypedControl(id, typeof (T));
        if (required && typedControl == null)
          this.ThrowException(typeof (T), id);
        control = (object) typedControl;
        this.DefinedControls.Add(id, control);
      }
      return (T) control;
    }

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified type,
    /// using Depth First algorithm.
    /// </summary>
    /// <typeparam name="T">The searched control type.</typeparam>
    /// <returns>The control if found otherwise null.</returns>
    public virtual T GetControl<T>() => this.GetControl<T>(TraverseMethod.DepthFirst);

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified type,
    /// using the specified algorithm.
    /// </summary>
    /// <typeparam name="T">The searched control type.</typeparam>
    /// <param name="traverseMethod">Defines the serach algorithm.</param>
    /// <returns>The control if found otherwise null.</returns>
    public virtual T GetControl<T>(TraverseMethod traverseMethod)
    {
      object control = (object) null;
      string fullName = typeof (T).FullName;
      if (!this.DefinedControls.TryGetValue(fullName, out control))
      {
        control = (object) this.FindTypedControl(typeof (T), traverseMethod);
        if (control != null && control is Control)
          this.DefinedControls.Add(fullName, control);
      }
      return (T) control;
    }

    /// <summary>
    /// Gets a dictionary of searched controls and their IDs as keys.
    /// </summary>
    /// <typeparam name="T">The searched control type.</typeparam>
    /// <returns>A dictionary containing all controls that are assignable from the specified type.</returns>
    public virtual Dictionary<string, Control> GetControls<T>()
    {
      Type type = typeof (T);
      Dictionary<string, Control> controls = new Dictionary<string, Control>();
      foreach (Control control in new ControlTraverser((Control) this, TraverseMethod.DepthFirst))
      {
        if (type.IsAssignableFrom(control.GetType()) && !string.IsNullOrEmpty(control.ID) && !controls.ContainsKey(control.ID))
          controls.Add(control.ID, control);
      }
      return controls;
    }

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified ID,
    /// using the specified search algorithm.
    /// </summary>
    /// <typeparam name="T">The searched control type.</typeparam>
    /// <param name="pageId">ID to search for.</param>
    /// <param name="required">
    /// Indicates whether an exception should be thrown if the searched control is not found.
    /// </param>
    /// <param name="method">Defines the serach algorithm.</param>
    /// <returns>The control if found otherwise null.</returns>
    public virtual T GetControl<T>(string id, bool required, TraverseMethod method)
    {
      Control typedControl = this.FindTypedControl(id, typeof (T), method);
      if (typedControl == null & required)
        this.ThrowException(typeof (T), id);
      return (T) typedControl;
    }

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified type,
    /// using Depth First algorithm.
    /// </summary>
    /// <param name="searchType">The searched control type.</param>
    /// <returns>The control if found otherwise null.</returns>
    protected virtual Control FindTypedControl(Type searchType) => this.FindTypedControl((string) null, searchType, TraverseMethod.DepthFirst);

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified type,
    /// using the specified algorithm.
    /// </summary>
    /// <param name="searchType">The searched control type.</param>
    /// <param name="method">Defines the serach algorithm.</param>
    /// <returns>The control if found otherwise null.</returns>
    protected virtual Control FindTypedControl(Type searchType, TraverseMethod method) => this.FindTypedControl((string) null, searchType, method);

    /// <summary>
    /// Searches the current <see cref="T:System.Web.UI.INamingContainer" /> for a control with the specified ID and
    /// required type. If no ID is specified the method searches for the first occurrence of the
    /// specified type using Depth First algorithm. If no type is specified the type of the
    /// found control is not validated.
    /// Note, if pageId is specified this method searches only the current <see cref="T:System.Web.UI.INamingContainer" />.
    /// To search the enire control hierarchy, use overload of this method with
    /// <see cref="T:Telerik.Sitefinity.Web.UI.TraverseMethod" /> parameter.
    /// </summary>
    /// <param name="pageId">ID to search for.</param>
    /// <param name="searchType">The searched control type.</param>
    /// <returns>The control if found otherwise null.</returns>
    protected virtual Control FindTypedControl(string id, Type searchType)
    {
      if (!string.IsNullOrEmpty(id))
      {
        Control control1 = (Control) null;
        if (this.isExternal)
        {
          bool flag = false;
          foreach (Control control2 in this.Controls)
          {
            if (control2 is UserControl)
            {
              control1 = control2.FindControl(id);
              flag = true;
              break;
            }
          }
          if (!flag)
            control1 = this.FindControl(id);
        }
        else
          control1 = this.FindControl(id);
        if (control1 == null)
          return (Control) null;
        return searchType != (Type) null && !searchType.IsAssignableFrom(control1.GetType()) ? (Control) null : control1;
      }
      return searchType != (Type) null ? this.FindTypedControl((string) null, searchType, TraverseMethod.DepthFirst) : throw new ArgumentException(Res.Get<ErrorMessages>().IdOrTypeNotSpecified);
    }

    /// <summary>
    /// Searches the entire control hierarchy for the first occurrence of the specified ID type,
    /// using Depth First algorithm.
    /// </summary>
    /// <param name="pageId">ID to search for.</param>
    /// <param name="searchType">The searched control type.</param>
    /// <param name="method">Defines the serach algorithm.</param>
    /// <returns>The control if found otherwise null.</returns>
    protected virtual Control FindTypedControl(
      string id,
      Type searchType,
      TraverseMethod method)
    {
      bool flag = !string.IsNullOrEmpty(id);
      ControlTraverser controlTraverser = new ControlTraverser((Control) this, method);
      if (flag && searchType != (Type) null)
      {
        foreach (Control typedControl in controlTraverser)
        {
          if (typedControl.ID == id && searchType.IsAssignableFrom(typedControl.GetType()))
            return typedControl;
        }
      }
      else if (flag)
      {
        foreach (Control typedControl in controlTraverser)
        {
          if (typedControl.ID == id)
            return typedControl;
        }
      }
      else
      {
        if (!(searchType != (Type) null))
          throw new ArgumentException("At least id or searchType have to be specified.");
        foreach (Control typedControl in controlTraverser)
        {
          if (searchType.IsAssignableFrom(typedControl.GetType()))
            return typedControl;
        }
      }
      return (Control) null;
    }

    /// <summary>Throws exception when required control is not found.</summary>
    /// <param name="requiredType">The type of the required contorl.</param>
    /// <param name="controlId">The ID of the searched control.</param>
    protected virtual void ThrowException(Type requiredType, string controlId) => throw new TemplateException(this.templateName, requiredType.FullName, controlId);
  }
}
