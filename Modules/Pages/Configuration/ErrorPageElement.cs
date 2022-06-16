// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ErrorPageElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.CustomErrorPages;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Defines the Custom Error Pages settings.</summary>
  /// <value>The error pages configuration element.</value>
  public class ErrorPageElement : ConfigElement
  {
    /// <summary>Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ErrorPageElement" /> class.</summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ErrorPageElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether Custom Error Pages are enabled.
    /// </summary>
    [ConfigurationProperty("mode", DefaultValue = CustomErrorPagesMode.Enabled)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ErrorPagesModeDescription", Title = "ErrorPagesModeTitle")]
    public virtual CustomErrorPagesMode Mode
    {
      get => (CustomErrorPagesMode) this["mode"];
      set => this["mode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value representing the default ErrorPages settings.
    /// </summary>
    /// <value>The error pages configuration element.</value>
    [ConfigurationProperty("errorTypes")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "ErrorTypesTitle")]
    public virtual ConfigElementDictionary<string, ErrorPageDataElement> ErrorTypes
    {
      get => (ConfigElementDictionary<string, ErrorPageDataElement>) this["errorTypes"];
      set => this["errorTypes"] = (object) value;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ErrorTypes.Add(new ErrorPageDataElement((ConfigElement) this.ErrorTypes)
      {
        HttpStatusCode = "404",
        RedirectToErrorPage = false,
        PageName = "404"
      });
    }

    /// <summary>
    /// Constants that map CLR property names to configuration file attribute/tag names
    /// </summary>
    public static class FieldNames
    {
      /// <summary>
      /// Name of the Mode property as referred to in the configuration file
      /// </summary>
      public const string Mode = "mode";
      /// <summary>
      /// Name of the ErrorPages property as referred to in the configuration file
      /// </summary>
      public const string ErrorTypes = "errorTypes";
    }
  }
}
