// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Validation.ComparingValidatorDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Validation.Config;

namespace Telerik.Sitefinity.Fluent.Definitions.Validation
{
  /// <summary>
  /// Fluent API facade that defines a definition for comparing validator element.
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class ComparingValidatorDefinitionFacade<TParentFacade> where TParentFacade : class
  {
    private ConfigElementList<ComparingValidatorElement> parentElement;
    private TParentFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ComparingValidatorDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public ComparingValidatorDefinitionFacade(
      ConfigElementList<ComparingValidatorElement> parentElement,
      TParentFacade parentFacade)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.parentElement = parentElement != null ? parentElement : throw new ArgumentNullException(nameof (parentElement));
      this.parentFacade = parentFacade;
      this.ComparingValidator = new ComparingValidatorElement((ConfigElement) parentElement);
      parentElement.Add(this.ComparingValidator);
    }

    /// <summary>Gets or sets the current comparing validator element.</summary>
    /// <value>The comparing validator.</value>
    protected ComparingValidatorElement ComparingValidator { get; set; }

    /// <summary>
    /// Gets this <see cref="T:Telerik.Sitefinity.Web.UI.Validation.Config.ComparingValidatorElement" /> instance.
    /// </summary>
    /// <returns></returns>
    public ComparingValidatorElement Get() => this.ComparingValidator;

    /// <summary>
    /// Sets the Id of the control which value is going to be used as range limitation.
    /// </summary>
    /// <param name="control">The control to compare.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ComparingValidatorDefinitionFacade`1" />.
    /// </returns>
    public ComparingValidatorDefinitionFacade<TParentFacade> SetControlToCompare(
      string control)
    {
      this.ComparingValidator.ControlToCompare = !string.IsNullOrEmpty(control) ? control : throw new ArgumentNullException(nameof (control));
      return this;
    }

    /// <summary>Sets the comparison operation to perform.</summary>
    /// <param name="compareOperator">One of the <see cref="T:System.Web.UI.WebControls.ValidationCompareOperator" />
    /// values. The default value is Equal.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ComparingValidatorDefinitionFacade`1" />.
    /// </returns>
    public ComparingValidatorDefinitionFacade<TParentFacade> SetOperator(
      ValidationCompareOperator compareOperator)
    {
      this.ComparingValidator.Operator = compareOperator;
      return this;
    }

    /// <summary>Sets the message to show if the validation failed.</summary>
    /// <param name="message">The validation violation message.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Validation.ComparingValidatorDefinitionFacade`1" />.
    /// </returns>
    public ComparingValidatorDefinitionFacade<TParentFacade> SetValidationViolationMessage(
      string message)
    {
      this.ComparingValidator.ValidationViolationMessage = !string.IsNullOrEmpty(message) ? message : throw new ArgumentNullException(nameof (message));
      return this;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;
  }
}
