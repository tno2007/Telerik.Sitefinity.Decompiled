// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.IPropertiesAdaptorExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors
{
  internal static class IPropertiesAdaptorExtensions
  {
    internal static PropertyContainer GetProviderProperty(
      WcfControlProperty source,
      string managerType)
    {
      PropertyContainer providerProperty = new PropertyContainer();
      providerProperty.Title = "Source";
      providerProperty.Name = "ProviderName";
      providerProperty.Type = "choices";
      PropertiesModel properties = providerProperty.Properties;
      IManager manager = ManagerBase.GetManager(managerType);
      IEnumerable<ChoiceValue> choiceValues = manager.GetContextProviders().Select<DataProviderBase, ChoiceValue>((Func<DataProviderBase, ChoiceValue>) (x => new ChoiceValue(x.Title, x.Name)));
      properties.Properties.Add("AvailableValues", (object) choiceValues);
      string name = manager.GetDefaultContextProvider().Name;
      string.IsNullOrEmpty(source.PropertyValue);
      return providerProperty;
    }
  }
}
