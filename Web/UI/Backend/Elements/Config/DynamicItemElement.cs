// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DynamicItemElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>Represents Dynamic item configuration elememnt.</summary>
  public class DynamicItemElement : DefinitionConfigElement, IDynamicItemDefinition, IDefinition
  {
    /// <summary>
    /// Initializes new isntance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DynamicItemElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new DynamicItemDefinition((ConfigElement) this);

    /// <summary>Gets or sets the sorting expression key.</summary>
    /// <value>The license service URL.</value>
    [ConfigurationProperty("sortingExpressionTitle", IsKey = true, IsRequired = true)]
    public string Title
    {
      get => (string) this["sortingExpressionTitle"];
      set => this["sortingExpressionTitle"] = (object) value;
    }

    /// <summary>Gets or sets the sorting expression.</summary>
    /// <value>The sorting expression.</value>
    [ConfigurationProperty("sortingExpression")]
    public string Value
    {
      get => (string) this["sortingExpression"];
      set => this["sortingExpression"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as SortingExpression will
    /// be used directly; otherwise the values of the property will be used as key for the resource
    /// and localized resource will be loaded instead.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string Title = "sortingExpressionTitle";
      public const string Value = "sortingExpression";
      public const string ResourceClassId = "resourceClassId";
    }
  }
}
