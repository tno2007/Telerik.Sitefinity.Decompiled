// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorField" />
  /// </summary>
  public class LibrarySelectorFieldDefinition : 
    FieldControlDefinition,
    ILibrarySelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IExpandableControlDefinition expandableDefinition;
    private Type contentType;
    private bool showOnlySystemLibraries;
    private string sortExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorFieldDefinition" /> class.
    /// </summary>
    public LibrarySelectorFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LibrarySelectorFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public Type ContentType
    {
      get => this.ResolveProperty<Type>(nameof (ContentType), this.contentType);
      set => this.contentType = value;
    }

    /// <summary>
    /// Gets or sets whether to show system libraries or general libraries.
    /// </summary>
    /// <value>True if you want only system libraries to be shown false if you want general libraries to be shown.</value>
    public bool ShowOnlySystemLibraries
    {
      get => this.ResolveProperty<bool>(nameof (ShowOnlySystemLibraries), this.showOnlySystemLibraries);
      set => this.showOnlySystemLibraries = value;
    }

    /// <summary>
    /// Sort expression for the library selector field element.
    /// </summary>
    /// <value></value>
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition
    {
      get
      {
        if (this.expandableDefinition == null)
        {
          this.expandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition();
          this.expandableDefinition.ControlDefinitionName = this.ControlDefinitionName;
          this.expandableDefinition.ViewName = this.ViewName;
          this.expandableDefinition.SectionName = this.SectionName;
          this.expandableDefinition.FieldName = this.FieldName;
        }
        return this.expandableDefinition;
      }
      set => this.expandableDefinition = value;
    }
  }
}
