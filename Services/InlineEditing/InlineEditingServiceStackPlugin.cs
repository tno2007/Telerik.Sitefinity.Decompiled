// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.InlineEditingServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Services.InlineEditing.Messages;

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>
  /// Represents a ServiceStack plugin for the inline editing service
  /// </summary>
  public class InlineEditingServiceStackPlugin : IPlugin
  {
    /// <summary>Adding the inline editing service routes</summary>
    /// <param name="appHost"></param>
    public void Register(IAppHost appHost)
    {
      appHost.RegisterService(typeof (InlineEditingService));
      appHost.Routes.Add<EditableItemMessage>("/sitefinity/inlineediting", (string) null).Add<FieldValueMessage>("/sitefinity/inlineediting" + "/" + "fieldValue", (string) null).Add<ContainerInfoMessage>("/sitefinity/inlineediting" + "/" + "containersInfo", "POST").Add<ConfigurationMessage>("/sitefinity/inlineediting" + "/" + "configuration", "GET").Add<WorkflowOperationMessage>("/sitefinity/inlineediting" + "/" + "workflow", (string) null).Add<RenderMessage>("/sitefinity/inlineediting" + "/" + "render", (string) null).Add<EditableItemMessage>("/sitefinity/inlineediting" + "/" + "temp/{ItemId}", (string) null).Add<MultisiteMessage>("/sitefinity/inlineediting" + "/" + "multisitedata", "GET");
    }
  }
}
