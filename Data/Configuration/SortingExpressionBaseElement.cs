// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.SortingExpressionBaseElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Represents base sorting expression configuration element with basic configuration elements: SortingExpressionTitle and SortingExpression.
  /// </summary>
  public class SortingExpressionBaseElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SortingExpressionBaseElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the content type of the sort expression</summary>
    /// <value>The license service URL.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentTypeDescription", Title = "ContentTypeTitle")]
    [ConfigurationProperty("contentType", IsRequired = true)]
    public string ContentType
    {
      get => (string) this["contentType"];
      set => this["contentType"] = (object) value;
    }

    /// <summary>Gets or sets the sorting expression key.</summary>
    /// <value>The license service URL.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortingExpressionTitleDescription", Title = "SortingExpressionTitle")]
    [ConfigurationProperty("sortingExpressionTitle", IsRequired = true)]
    public string SortingExpressionTitle
    {
      get => (string) this["sortingExpressionTitle"];
      set => this["sortingExpressionTitle"] = (object) value;
    }

    /// <summary>Gets or sets the sorting expression.</summary>
    /// <value>The sorting expression.</value>
    [ConfigurationProperty("sortingExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortingExpressionValueDescription", Title = "SortingExpressionValueTitle")]
    public string SortingExpression
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

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string SortingExpressionTitle = "sortingExpressionTitle";
      public const string SortingExpression = "sortingExpression";
      public const string IsCustom = "isCustom";
      public const string ResourceClassId = "resourceClassId";
      public const string ContentType = "contentType";
    }
  }
}
