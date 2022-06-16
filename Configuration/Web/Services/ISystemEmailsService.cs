// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.ISystemEmailsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web.Services
{
  /// <summary>
  /// Interface that defines the members of the service for system emails.
  /// </summary>
  [ServiceContract]
  internal interface ISystemEmailsService
  {
    /// <summary>Endpoint used to return all system email</summary>
    /// <param name="siteId">The site Id</param>
    /// <param name="sort">Sort by</param>
    /// <param name="skip">Skip count</param>
    /// <param name="take">Take count</param>
    /// <param name="filter">Filtering options</param>
    /// <returns>System emails view model</returns>
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/system_emails?siteId={siteId}&sort={sort}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<SystemEmailsViewModel> GetSystemEmails(
      string siteId,
      string sort,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Endpoint used to save the edited system email template
    /// </summary>
    /// <param name="model">The model</param>
    /// <returns>A value representing success</returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/update_variation")]
    [OperationContract]
    bool UpdateTemplateVariation(SystemEmailsViewModel model);

    /// <summary>Endpoint used to create the template variations</summary>
    /// <param name="siteId">The site id</param>
    /// <returns>A value representing success</returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/create_variations")]
    [OperationContract]
    bool CreateTemplateVariations(string siteId);

    /// <summary>
    /// Endpoint used to delete all template variations for a specific site
    /// </summary>
    /// <param name="siteId">The site id</param>
    /// <returns>A value representing success</returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/delete_variations")]
    [OperationContract]
    bool DeleteTemplateVariations(string siteId);

    /// <summary>Endpoint used to delete a template variation</summary>
    /// <param name="model">The model</param>
    /// <returns>A value representing success</returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/delete_variation")]
    [OperationContract]
    bool DeleteTemplateVariation(SystemEmailsViewModel model);
  }
}
