// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.ComparisonViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Versioning.Web.UI
{
  public class ComparisonViewDefinition : 
    ContentViewDefinition,
    IComparisonViewDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private IList<IComparisonFieldDefinition> fields;

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public new ContentViewDefinition GetDefinition() => (ContentViewDefinition) this;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ComparisonViewDefinition(ContentViewDefinitionElement configDefinition)
      : base((ConfigElement) configDefinition)
    {
    }

    /// <summary>
    /// Defines a list of <see cref="!:IContentViewFieldDefinition" />.
    /// </summary>
    /// <value>The fields.</value>
    IEnumerable<IComparisonFieldDefinition> IComparisonViewDefinition.Fields => (IEnumerable<IComparisonFieldDefinition>) this.Fields;

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldDefinition" />.
    /// </summary>
    /// <value>The fields.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual IList<IComparisonFieldDefinition> Fields
    {
      get
      {
        if (this.fields == null)
          this.fields = (IList<IComparisonFieldDefinition>) ((IEnumerable<IComparisonFieldDefinition>) ((ComparisonViewElement) this.ConfigDefinition).Fields.Elements.Select<ComparisonFieldElement, ComparisonFieldDefinition>((Func<ComparisonFieldElement, ComparisonFieldDefinition>) (configField =>
          {
            ComparisonFieldDefinition definition = (ComparisonFieldDefinition) configField.GetDefinition();
            definition.ControlDefinitionName = this.ControlDefinitionName;
            definition.ViewName = this.ViewName;
            definition.FieldName = configField.FieldName;
            return definition;
          }))).ToList<IComparisonFieldDefinition>();
        return this.fields;
      }
    }
  }
}
