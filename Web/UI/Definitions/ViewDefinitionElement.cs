// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.ViewDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  [DefaultProperty("ViewName")]
  internal class ViewDefinitionElement : DefinitionConfigElement, IViewDefinition, IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ViewDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ViewDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    [ConfigurationProperty("viewName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string ViewName
    {
      get => (string) this["viewName"];
      set => this["viewName"] = (object) value;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    [ConfigurationProperty("viewType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ViewType
    {
      get => (Type) this["viewType"];
      set => this["viewType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ControlIdDescription", Title = "ControlIdCaption")]
    [ConfigurationProperty("controlId")]
    public string ControlId
    {
      get => (string) this["controlId"];
      set => this["controlId"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ViewProps
    {
      public const string ViewName = "viewName";
      public const string ViewType = "viewType";
      public const string ControlId = "controlId";
    }
  }
}
