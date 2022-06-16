// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.RequestRootPermissionsFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using System;
using System.Net;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Adding this filter to your ServiceStack service class this will request provided rights to be satisfied for the user calling the service.
  /// </summary>
  public class RequestRootPermissionsFilterAttribute : RequestFilterAttribute
  {
    private string setName;
    private string actionName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.ServiceStack.Filters.RequestRootPermissionsFilterAttribute" /> class.
    /// </summary>
    /// <param name="setName">The set name</param>
    /// <param name="actionName">The action name</param>
    public RequestRootPermissionsFilterAttribute(string setName, string actionName)
    {
      this.setName = !string.IsNullOrEmpty(setName) && !string.IsNullOrEmpty(actionName) ? setName : throw new ArgumentException("setName and actionName should not be null or empty");
      this.actionName = actionName;
    }

    /// <inheritdoc />
    public override void Execute(IRequest req, IResponse res, object requestDto)
    {
      if (!AppPermission.Root.IsGranted(this.setName, this.actionName))
        throw new WebProtocolException(HttpStatusCode.Forbidden);
    }
  }
}
