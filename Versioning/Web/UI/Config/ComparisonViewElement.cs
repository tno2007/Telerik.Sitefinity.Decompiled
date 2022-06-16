// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Config.ComparisonViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Versioning.Web.UI.Config
{
  [DefaultProperty("ViewName")]
  public class ComparisonViewElement : 
    ContentViewDefinitionElement,
    IComparisonViewDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ComparisonViewElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("fields")]
    [ConfigurationCollection(typeof (FieldControlDefinitionElement), AddItemName = "field")]
    public ConfigElementDictionary<string, ComparisonFieldElement> Fields => (ConfigElementDictionary<string, ComparisonFieldElement>) this["fields"];

    IEnumerable<IComparisonFieldDefinition> IComparisonViewDefinition.Fields => (IEnumerable<IComparisonFieldDefinition>) this.Fields.Elements.Select<ComparisonFieldElement, ComparisonFieldDefinition>((Func<ComparisonFieldElement, ComparisonFieldDefinition>) (fld =>
    {
      ComparisonFieldDefinition definition = (ComparisonFieldDefinition) fld.GetDefinition();
      definition.ControlDefinitionName = this.ControlDefinitionName;
      definition.ViewName = this.ViewName;
      definition.FieldName = fld.FieldName;
      return definition;
    }));

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ComparisonViewDefinition((ContentViewDefinitionElement) this);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigPropertiesNames
    {
      public const string fields = "fields";
      public const string fieldElementName = "field";
    }
  }
}
