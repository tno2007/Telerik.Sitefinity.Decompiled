// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.Services.IModulesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Services.Web.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Web.Services
{
  /// <summary>
  /// Provides contracts for loading and manipulating modules
  /// </summary>
  [ServiceContract]
  internal interface IModulesService
  {
    /// <summary>
    /// Gets the modules registered in Sitefinity. The results are returned in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the modules.</param>
    /// <param name="skip">Number of modules to skip in result set. (used for paging)</param>
    /// <param name="take">Number of modules to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Services.Web.Services.ViewModel.ModuleViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the modules registered in Sitefinity. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/modules?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<ModuleViewModel> GetModules(
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Executes the operation on the given module. The saved module is returned in JSON format.
    /// </summary>
    /// <param name="module">The module to operate with.</param>
    /// <param name="operation">The operation name. (e.g. "activate", "deactivate", "install", "uninstall", "delete".</param>
    /// <returns>The updated module.</returns>
    [WebHelp(Comment = "Executes the operation on the given module. The saved module is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/modules?operation={operation}")]
    [OperationContract]
    ModuleViewModel Execute(ModuleViewModel module, ModuleOperation operation);

    /// <summary>
    /// Executes the operation on each of the given modules. Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array of the ids of the modules to operate with.</param>
    /// <param name="operation">The operation name. (e.g. "activate", "deactivate", "install", "uninstall", "delete".</param>
    /// <returns>
    /// True if all modules have been successfully updated; otherwise false.
    /// </returns>
    [WebHelp(Comment = "Executes the operation on each of the given modules. Returns true if all modules have been successfully updated; otherwise false.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/modules/batch?operation={operation}")]
    [OperationContract]
    bool BatchExecute(string[] ids, string operation);
  }
}
