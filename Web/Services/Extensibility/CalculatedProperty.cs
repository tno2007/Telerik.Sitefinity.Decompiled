// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.CalculatedProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// Defines the base interface for constructing calculated properties.
  /// </summary>
  public abstract class CalculatedProperty
  {
    private Type parentType;
    private string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Extensibility.CalculatedProperty" /> class.
    /// </summary>
    /// <param name="parameters">The parameters to initialize the property with.</param>
    /// <param name="parentType">The parent type to which the property belongs.</param>
    public virtual void Initialize(NameValueCollection parameters, Type parentType)
    {
      this.parentType = parentType;
      this.name = parameters["Name"];
    }

    /// <summary>
    /// Gets the annotations defined for this object that will be presented with the CSDL metadata.
    /// </summary>
    /// <returns>The annotations.</returns>
    public virtual IEnumerable<VocabularyAnnotation> GetAnnotations() => (IEnumerable<VocabularyAnnotation>) null;

    /// <summary>Gets the CLR type of the property.</summary>
    public abstract Type ReturnType { get; }

    /// <summary>
    /// Gets the CLR type of the parent containing this property.
    /// </summary>
    public Type ParentType => this.parentType;

    internal string Name => this.name;

    internal virtual string[] PropertiesToIncludeInFetch => (string[]) null;

    /// <summary>Gets the values for the collection of passed items.</summary>
    /// <param name="items">The items.</param>
    /// <param name="manager">The items manager.</param>
    /// <returns>A dictionary mapping between each object and its value for the calculated property.</returns>
    public abstract IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager);

    internal class Constants
    {
      internal const string PropertyName = "Name";
    }
  }
}
