// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.WebFormsAndMvcRendererOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Generators;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor
{
  internal class WebFormsAndMvcRendererOperationProvider : IOperationProvider, IModelProvider
  {
    private CompositeGenerator generator;

    public WebFormsAndMvcRendererOperationProvider(CompositeGenerator generator) => this.generator = generator;

    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (!object.Equals((object) clrType, (object) typeof (PageNode)))
        return Enumerable.Empty<OperationData>();
      OperationData operationData = OperationData.Create<string, ControlMetadata>(new Func<OperationContext, string, ControlMetadata>(this.GetComponentMetadata));
      operationData.OperationType = OperationType.PerItem;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private ControlMetadata GetComponentMetadata(
      OperationContext context,
      string componentId)
    {
      string key = context.GetKey();
      PageSiteNode siteMapNodeFromKey = (PageSiteNode) SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(key);
      SystemManager.CurrentHttpContext.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = siteMapNodeFromKey != null ? (object) siteMapNodeFromKey : throw new ItemNotFoundException();
      GetComponentArgs args1 = new GetComponentArgs();
      args1.ContainerResolver = (IContainerResolver) new PageDataContainerResolver(siteMapNodeFromKey);
      args1.ComponentId = componentId;
      Guid pageId;
      ControlData controlData = this.generator.GetControlData((IGetComponentArgs) args1, out pageId);
      CollectionContext<WcfControlProperty> properties = new ControlPropertyService().GetProperties(componentId, (string) null, (string) null, (string) null, (string) null, (string) null, pageId.ToString());
      IPropertiesAdaptor propertiesAdaptor = ComponentAdaptorBase.Create(controlData);
      ControlUtilities.BehaviorResolver.GetBehaviorObjectType(controlData);
      AdaptControlArgs args2 = new AdaptControlArgs()
      {
        WcfProperties = properties.Items,
        ComponentType = ControlUtilities.BehaviorResolver.GetBehaviorObjectType(controlData),
        ControlData = controlData
      };
      return propertiesAdaptor.AdaptControlMetadata((IAdaptControlArgs) args2);
    }

    public IEnumerable<Type> GetModels() => (IEnumerable<Type>) new Type[2]
    {
      typeof (ChoiceValue),
      typeof (ValidationContainer)
    };
  }
}
