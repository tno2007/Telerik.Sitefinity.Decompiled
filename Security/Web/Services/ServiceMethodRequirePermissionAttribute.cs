// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.ServiceMethodRequirePermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>A class RequireUserManagementPermissionAttribute</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public class ServiceMethodRequirePermissionAttribute : 
    Attribute,
    IOperationBehavior,
    IParameterInspector
  {
    private bool requireBackendPermissions;
    private string setName;
    private string actionName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.ServiceMethodRequirePermissionAttribute" /> class.
    /// </summary>
    /// <param name="setName">The set name</param>
    /// <param name="actionName">The action name</param>
    /// <param name="requireBackendPermissions">Indicates if the user accessing the method should be a back end user</param>
    public ServiceMethodRequirePermissionAttribute(
      string setName,
      string actionName,
      bool requireBackendPermissions = true)
    {
      this.setName = !string.IsNullOrEmpty(setName) && !string.IsNullOrEmpty(actionName) ? setName : throw new ArgumentException("setName and actionName should not be null or empty");
      this.actionName = actionName;
      this.requireBackendPermissions = requireBackendPermissions;
    }

    /// <inheritdoc />
    public void AddBindingParameters(
      OperationDescription operationDescription,
      BindingParameterCollection bindingParameters)
    {
    }

    /// <inheritdoc />
    public void ApplyClientBehavior(
      OperationDescription operationDescription,
      ClientOperation clientOperation)
    {
    }

    /// <inheritdoc />
    public void ApplyDispatchBehavior(
      OperationDescription operationDescription,
      DispatchOperation dispatchOperation)
    {
      dispatchOperation.ParameterInspectors.Add((IParameterInspector) this);
    }

    /// <inheritdoc />
    public void Validate(OperationDescription operationDescription)
    {
    }

    /// <inheritdoc />
    public void AfterCall(
      string operationName,
      object[] outputs,
      object returnValue,
      object correlationState)
    {
    }

    /// <inheritdoc />
    public object BeforeCall(string operationName, object[] inputs)
    {
      if (this.requireBackendPermissions)
        ServiceUtility.RequestBackendUserAuthentication();
      if (!AppPermission.Root.IsGranted(this.setName, this.actionName))
        throw new WebProtocolException(HttpStatusCode.Forbidden);
      return (object) null;
    }
  }
}
