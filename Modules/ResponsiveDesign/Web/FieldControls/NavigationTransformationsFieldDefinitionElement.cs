// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.NavigationTransformationsFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls
{
  /// <summary>
  /// Configuration definition for the navigation transformations field.
  /// </summary>
  public class NavigationTransformationsFieldDefinitionElement : 
    FieldControlDefinitionElement,
    INavigationTransformationsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public NavigationTransformationsFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets the default type of the control that represents the field. This property is to be
    /// implemented by specific definitions that have a corresponding control type built in.
    /// </summary>
    public override Type DefaultFieldType => typeof (NavigationTransformationsField);

    /// <summary>
    /// Gets the in-memory definition represented by this configuration definition.
    /// </summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new NavigationTransformationsFieldDefinition((ConfigElement) this);
  }
}
