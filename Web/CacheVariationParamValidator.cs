// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.CacheVariationParamValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Defines an abstract class to validate parameters values for output cache variations.
  /// Should be serializable.
  /// </summary>
  [Serializable]
  public abstract class CacheVariationParamValidator
  {
    /// <summary>
    /// Initializes a new instance of the Telerik.Sitefinity.Web.CacheVariationParamValidator class.
    /// </summary>
    public CacheVariationParamValidator()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Telerik.Sitefinity.Web.CacheVariationParamValidator class.
    /// </summary>
    /// <param name="arguments">A list of arguments.</param>
    public CacheVariationParamValidator(string[] arguments) => this.Arguments = arguments;

    /// <summary>
    /// Gets or sets a list of arguments that will be passed to the validation method.
    /// </summary>
    public string[] Arguments { get; set; }

    /// <summary>Validates the provided query parameter value.</summary>
    /// <param name="paramValue">Parameter value.</param>
    /// <returns>True if the parameter value is valid, otherwise - false.</returns>
    public bool Validate(string paramValue) => this.Validate(paramValue, this.Arguments);

    /// <summary>Implementation of the custom validation logic.</summary>
    /// <param name="paramValue">Parameter value.</param>
    /// <param name="arguments">A list of arguments.</param>
    /// <returns>True if the parameter value is valid, otherwise - false.</returns>
    protected abstract bool Validate(string paramValue, string[] arguments);
  }
}
