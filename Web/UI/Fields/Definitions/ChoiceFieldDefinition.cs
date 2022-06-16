// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information that is needed to construct a ChoiceField control.
  /// </summary>
  public class ChoiceFieldDefinition : 
    FieldControlDefinition,
    IChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> choices;
    private bool mutuallyExclusive;
    private RenderChoicesAs renderChoiceAs;
    private ICollection<int> selectedChoicesIndex;
    private bool hideTitle;
    private bool returnValuesAlwaysInArray;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceFieldDefinition" /> class.
    /// </summary>
    public ChoiceFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ChoiceFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the choices
    /// that the control ought to render.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IChoiceDefinition> Choices
    {
      get
      {
        if (this.choices == null)
          this.choices = this.ConfigDefinition == null ? new List<IChoiceDefinition>() : ((IEnumerable<IChoiceDefinition>) ((ChoiceFieldElement) this.ConfigDefinition).ChoicesConfig.Elements.Select<ChoiceElement, ChoiceDefinition>((Func<ChoiceElement, ChoiceDefinition>) (c => new ChoiceDefinition((ConfigElement) c)))).ToList<IChoiceDefinition>();
        return this.choices;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether more than one choice can be made. If only one choice
    /// can be made true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool MutuallyExclusive
    {
      get => this.ResolveProperty<bool>(nameof (MutuallyExclusive), this.mutuallyExclusive);
      set => this.mutuallyExclusive = value;
    }

    /// <summary>
    /// Gets or sets the value indicating how should the choices be rendered.
    /// </summary>
    /// <value></value>
    public RenderChoicesAs RenderChoiceAs
    {
      get => this.ResolveProperty<RenderChoicesAs>(nameof (RenderChoiceAs), this.renderChoiceAs);
      set => this.renderChoiceAs = value;
    }

    /// <summary>
    /// Gets or sets the value indicating which choice(s) is currently selected.
    /// </summary>
    /// <value></value>
    public ICollection<int> SelectedChoicesIndex
    {
      get
      {
        if (this.selectedChoicesIndex == null)
          this.selectedChoicesIndex = (ICollection<int>) new int[0];
        return this.ResolveProperty<ICollection<int>>(nameof (SelectedChoicesIndex), this.selectedChoicesIndex);
      }
      set => this.selectedChoicesIndex = value;
    }

    /// <inheritdoc />
    public bool HideTitle
    {
      get => this.ResolveProperty<bool>(nameof (HideTitle), this.hideTitle);
      set => this.hideTitle = value;
    }

    /// <summary>
    /// Gets or sets the value which indicates that the values returned from the
    /// field control (client side) should always be returned in an array of strings,
    /// regardless if one or more choices have been selected.
    /// </summary>
    public bool ReturnValuesAlwaysInArray
    {
      get => this.ResolveProperty<bool>(nameof (ReturnValuesAlwaysInArray), this.returnValuesAlwaysInArray);
      set => this.returnValuesAlwaysInArray = value;
    }
  }
}
