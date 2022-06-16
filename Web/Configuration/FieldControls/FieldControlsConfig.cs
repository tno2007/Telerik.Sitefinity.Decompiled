// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.FieldControlsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  /// <summary>
  /// Provides mapping between .net types and corresponding field controls to be used when automatic content rendering is perfomed
  /// </summary>
  public class FieldControlsConfig : ConfigSection
  {
    private const string toolsProperty = "tools";
    private const string toolsMappingsProperty = "toolsMappings";

    /// <summary>Gets the tools.</summary>
    /// <value>The tools.</value>
    [ConfigurationProperty("tools")]
    [ConfigurationCollection(typeof (ToolConfigElement), AddItemName = "add")]
    public ToolConfigElementList Tools => (ToolConfigElementList) this["tools"];

    [ConfigurationProperty("toolsMappings")]
    [ConfigurationCollection(typeof (ToolConfigElement), AddItemName = "add")]
    public ToolsMappingElementDictionary ToolsMappings => (ToolsMappingElementDictionary) this["toolsMappings"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      this.AddMapping(typeof (string), "TextFieldControl");
      this.AddMapping(typeof (int), "IntegerFieldControl");
      this.AddMapping(typeof (int?), "IntegerFieldControl");
      this.AddMapping(typeof (DateTime), "CalendarFieldControl");
      this.AddMapping(typeof (DateTime?), "CalendarFieldControl");
      this.AddMapping(typeof (double), "DoubleFieldControl");
      this.AddMapping(typeof (double?), "DoubleFieldControl");
      this.AddMapping(typeof (bool), "CheckBoxFieldControl");
      this.AddMapping(typeof (bool?), "CheckBoxFieldControl");
      this.AddMapping(typeof (Lstring), "TextFieldControl");
      this.AddMapping(typeof (FlatTaxon), "SingleChoiceFieldControl");
      this.AddMapping(typeof (FlatTaxon[]), "TagSuggestControl");
      this.AddMapping(typeof (HierarchicalTaxon[]), "CategorySelectorControl");
      this.AddMapping(typeof (HierarchicalTaxon), "CategorySelectorControl");
    }

    private void AddMapping(Type type, string toolName)
    {
      if (this.ToolsMappings.ContainsKey(type))
        return;
      this.ToolsMappings.Add(new ToolsMappingElement((ConfigElement) this.ToolsMappings)
      {
        MappedType = type,
        ToolName = toolName
      });
    }
  }
}
