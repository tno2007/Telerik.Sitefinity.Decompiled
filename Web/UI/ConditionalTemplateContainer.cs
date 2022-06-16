// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ConditionalTemplateContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A control that contains a collection of conditional templates.
  /// Renders the first template which matches the defined expression.
  /// </summary>
  public class ConditionalTemplateContainer : WebControl
  {
    private object dataItem;
    private Collection<ConditionalTemplate> templates;

    /// <summary>
    /// Gets a collection of the conditional templates.
    /// Only one will be rendered.
    /// </summary>
    /// <value>The templates.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ConditionalTemplate> Templates
    {
      get
      {
        if (this.templates == null)
          this.templates = new Collection<ConditionalTemplate>();
        return this.templates;
      }
    }

    /// <summary>
    /// Gets or sets the value that determines how will the templates be evaluated.
    /// </summary>
    public TemplateEvalutionMode EvaluationMode { get; set; }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Wrap in DIV, only when in client evaluation mode.</summary>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (this.EvaluationMode != TemplateEvalutionMode.Client)
        return;
      base.RenderBeginTag(writer);
    }

    /// <summary>Wrap in DIV, only when in client evaluation mode.</summary>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (this.EvaluationMode != TemplateEvalutionMode.Client)
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      if (this.EvaluationMode == TemplateEvalutionMode.Server || this.EvaluationMode == TemplateEvalutionMode.None)
        this.InstantiateForServerMode();
      else
        this.InstantiateForClientMode();
    }

    /// <summary>
    /// Gets or sets the binding object used to evaluate the conditional templates.
    /// </summary>
    /// <value>The binding object.</value>
    public virtual void Evaluate(object dataItem)
    {
      this.dataItem = dataItem;
      this.EnsureChildControls();
    }

    public virtual bool MatchTemplate(
      ConditionalTemplate template,
      object component,
      PropertyDescriptorCollection properties)
    {
      return this.IsMatch_private(component, properties, template);
    }

    private void InstantiateForServerMode()
    {
      if (this.dataItem == null)
        return;
      this.EvalTemplates_private(this.dataItem)?.InstantiateIn((Control) this);
    }

    private void InstantiateForClientMode()
    {
      foreach (ITemplate template in this.Templates)
        template.InstantiateIn((Control) this);
    }

    /// <summary>
    /// Evaluates the collection of conditional templates and returns the first one which matches the condition.
    /// </summary>
    /// <returns></returns>
    internal virtual ITemplate EvalTemplates_private(object component)
    {
      ITemplate template1 = (ITemplate) null;
      if (this.Templates.Count > 0)
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
        foreach (ConditionalTemplate template2 in this.Templates)
        {
          if (this.IsMatch_private(component, properties, template2))
          {
            template1 = (ITemplate) template2;
            break;
          }
        }
      }
      return template1;
    }

    /// <summary>
    /// Determines whether the specified component match the condition.
    /// </summary>
    /// <param name="component">The component.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="condition">The condition.</param>
    /// <returns>
    /// 	<c>true</c> if the specified component is match_private; otherwise, <c>false</c>.
    /// </returns>
    internal virtual bool IsMatch_private(
      object component,
      PropertyDescriptorCollection properties,
      ConditionalTemplate condition)
    {
      if (string.IsNullOrEmpty(condition.Left))
        return true;
      PropertyDescriptor property = properties[condition.Left];
      if (property == null)
        throw new InvalidConditionException(string.Format("The type '{0}' does not contain property '{1}'", (object) component.GetType(), (object) condition.Left));
      object obj1 = property.Converter.CanConvertFrom(typeof (string)) ? property.GetValue(component) : throw new InvalidConditionException(string.Format("The type '{0}' of the property '{1}' is not a valid type for the cndition", (object) property.PropertyType.Name, (object) property.Name));
      if (obj1 == null)
        return "null".Equals(condition.Right, StringComparison.OrdinalIgnoreCase);
      object obj2 = property.Converter.ConvertFromInvariantString(condition.Right);
      bool flag = false;
      switch (condition.Operator)
      {
        case ConditionOperators.Equal:
          flag = obj1.Equals(obj2);
          break;
        case ConditionOperators.NotEqual:
          flag = !obj1.Equals(obj2);
          break;
        case ConditionOperators.GreaterThan:
          flag = obj1 is IComparable && ((IComparable) obj1).CompareTo(obj2) > 0;
          break;
        case ConditionOperators.GreaterThanOrEqual:
          flag = !(obj1 is IComparable) ? obj1.Equals(obj2) : ((IComparable) obj1).CompareTo(obj2) >= 0;
          break;
        case ConditionOperators.LessThan:
          flag = obj1 is IComparable && ((IComparable) obj1).CompareTo(obj2) < 0;
          break;
        case ConditionOperators.LessThanOrEqual:
          flag = !(obj1 is IComparable) ? obj1.Equals(obj2) : ((IComparable) obj1).CompareTo(obj2) <= 0;
          break;
      }
      return flag;
    }
  }
}
