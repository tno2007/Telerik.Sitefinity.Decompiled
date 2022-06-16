// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentBlockControllerAdaptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  internal class ContentBlockControllerAdaptor : ComponentAdaptorBase
  {
    public override IList<PropertyValueContainer> AdaptValuesForSerialization(
      IEnumerable<WcfControlProperty> wcfProperties,
      IAdaptValuesContext context)
    {
      IList<PropertyValueContainer> source = base.AdaptValuesForSerialization(wcfProperties, context);
      PropertyValueContainer propertyValueContainer = source.FirstOrDefault<PropertyValueContainer>((Func<PropertyValueContainer, bool>) (p => p.Name == "ProviderName"));
      string providerName = propertyValueContainer != null ? propertyValueContainer.Value : string.Empty;
      Guid result;
      if (Guid.TryParse(source.FirstOrDefault<PropertyValueContainer>((Func<PropertyValueContainer, bool>) (p => p.Name == "SharedContentID")) != null ? source.FirstOrDefault<PropertyValueContainer>((Func<PropertyValueContainer, bool>) (p => p.Name == "SharedContentID")).Value : string.Empty, out result))
      {
        ContentManager manager = ContentManager.GetManager(providerName);
        ContentItem contentItem = (ContentItem) null;
        try
        {
          using (new ElevatedModeRegion((IManager) manager))
            contentItem = manager.GetContent(result);
        }
        catch (ItemNotFoundException ex)
        {
        }
        if (contentItem != null)
        {
          foreach (DataProviderBase contextProvider in manager.GetContextProviders())
          {
            if (contextProvider.ApplicationName == contentItem.ApplicationName)
              propertyValueContainer.Value = contextProvider.Name;
          }
          if (contentItem.Status == ContentLifecycleStatus.Live)
            source.First<PropertyValueContainer>((Func<PropertyValueContainer, bool>) (p => p.Name == "SharedContentID")).Value = contentItem.OriginalContentId.ToString();
        }
      }
      return source;
    }

    public override IEnumerable<WcfControlProperty> AdaptValuesForPersistence(
      IEnumerable<PropertyValueContainer> properties,
      IEnumerable<WcfControlProperty> sourceProperties)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentBlockControllerAdaptor.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new ContentBlockControllerAdaptor.\u003C\u003Ec__DisplayClass1_0();
      IEnumerable<WcfControlProperty> source = base.AdaptValuesForPersistence(properties, sourceProperties);
      WcfControlProperty wcfControlProperty = source.FirstOrDefault<WcfControlProperty>((Func<WcfControlProperty, bool>) (p => p.PropertyName == "SharedContentID"));
      if (wcfControlProperty == null)
        return source;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.masterContentId = wcfControlProperty.PropertyValue;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      ContentItem contentItem = ContentManager.GetManager((source.FirstOrDefault<WcfControlProperty>((Func<WcfControlProperty, bool>) (p => p.PropertyName == "ProviderName")) ?? sourceProperties.FirstOrDefault<WcfControlProperty>((Func<WcfControlProperty, bool>) (p => p.PropertyName == "ProviderName"))).PropertyValue).GetContent().FirstOrDefault<ContentItem>(Expression.Lambda<Func<ContentItem, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.Call(c.OriginalContentId, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass10, typeof (ContentBlockControllerAdaptor.\u003C\u003Ec__DisplayClass1_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentBlockControllerAdaptor.\u003C\u003Ec__DisplayClass1_0.masterContentId)))), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Content.get_Status))), typeof (int)), (Expression) Expression.Constant((object) 2, typeof (int)))), parameterExpression));
      if (contentItem != null)
        source.First<WcfControlProperty>((Func<WcfControlProperty, bool>) (p => p.PropertyName == "SharedContentID")).PropertyValue = contentItem.Id.ToString();
      return source;
    }

    public override bool CanAdaptComponent(ControlData component) => component.ObjectType == "Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy" && component.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (x => x.Name == "ControllerName")).Value == "Telerik.Sitefinity.Frontend.ContentBlock.Mvc.Controllers.ContentBlockController";

    public override int Priority => 2;

    public override ControlMetadata AdaptControlMetadata(IAdaptControlArgs args)
    {
      ControlMetadata controlMetadata = base.AdaptControlMetadata(args);
      controlMetadata.Name = "ContentBlock";
      return controlMetadata;
    }
  }
}
