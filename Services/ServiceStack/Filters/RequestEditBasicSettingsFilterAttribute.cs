// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.Filters.RequestEditBasicSettingsFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.ServiceStack.Filters
{
  /// <summary>
  /// Check if the user has permissions to edit system configurations
  /// </summary>
  public class RequestEditBasicSettingsFilterAttribute : RequestFilterAttribute
  {
    /// <inheritdoc />
    public override void Execute(IRequest req, IResponse res, object requestDto) => ServiceUtility.RequestBackendUserAuthentication("Backend", "ChangeConfigurations");
  }
}
