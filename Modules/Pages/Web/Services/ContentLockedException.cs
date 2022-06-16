// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ContentLockedException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  public class ContentLockedException : WebProtocolException
  {
    public ContentLockedException(ZoneEditorErrorData errorData)
      : base(HttpStatusCode.InternalServerError, errorData.ItemState.ToString() + ": " + (object) errorData.LockedBy + ":" + errorData.Operation, (object) errorData, (Func<WebMessageFormat, XmlObjectSerializer>) (p => (XmlObjectSerializer) new DataContractJsonSerializer(typeof (ZoneEditorErrorData))), (Exception) null)
    {
    }

    public ContentLockedException(ItemState itemState, Guid lockedById, string operationName)
      : this(new ZoneEditorErrorData(itemState, lockedById, operationName))
    {
    }
  }
}
