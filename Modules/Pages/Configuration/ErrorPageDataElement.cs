// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ErrorPageDataElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration.Validators;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents configuration element for ScriptManger.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Title = "ErrorPagesElementTitle")]
  public class ErrorPageDataElement : ConfigElement
  {
    /// <summary>Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.ErrorPageDataElement" /> class.</summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ErrorPageDataElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets HTTP status code returned in the response that will trigger showing error page to the client. (e.g. 404, 500)
    /// </summary>
    [ConfigurationValidator(typeof (HttpErrorCodeValidator))]
    [ConfigurationProperty("httpStatusCode", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "HttpStatusCodeTitle")]
    public string HttpStatusCode
    {
      get => (string) this["httpStatusCode"];
      set => this["httpStatusCode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the url name of the page that will be displayed for the current http status code.
    /// </summary>
    [ConfigurationProperty("pageName", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageNameDescription", Title = "PageNameTitle")]
    public string PageName
    {
      get => (string) this["pageName"];
      set => this["pageName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the response will instruct client to redirect to that page or (default) the server will render error page on the same URL.
    /// </summary>
    [ConfigurationProperty("redirectToErrorPage", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RedirectToErrorPageDescription", Title = "RedirectToErrorPageTitle")]
    public bool RedirectToErrorPage
    {
      get => this["redirectToErrorPage"] as bool? ?? false;
      set => this["redirectToErrorPage"] = (object) value;
    }

    /// <summary>
    /// Constants that map CLR property names to configuration file attribute/tag names
    /// </summary>
    public static class FieldNames
    {
      /// <summary>
      /// Name of the HttpStatusCode property as referred to in the configuration file
      /// </summary>
      public const string HttpStatusCode = "httpStatusCode";
      /// <summary>
      /// Name of the RedirectToErrorPage property as referred to in the configuration file
      /// </summary>
      public const string RedirectToErrorPage = "redirectToErrorPage";
      /// <summary>
      /// Name of the PageName property as referred to in the configuration file
      /// </summary>
      public const string PageName = "pageName";
    }
  }
}
