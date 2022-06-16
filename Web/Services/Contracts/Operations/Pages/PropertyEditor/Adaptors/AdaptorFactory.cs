// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors.AdaptorFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors
{
  internal class AdaptorFactory : IAdaptorFactory
  {
    private IPropertiesAdaptor[] adaptors;

    public AdaptorFactory(IPropertiesAdaptor[] adaptors) => this.adaptors = adaptors;

    public IPropertiesAdaptor Create(ControlData controlData) => ((IEnumerable<IPropertiesAdaptor>) this.adaptors).Where<IPropertiesAdaptor>((Func<IPropertiesAdaptor, bool>) (x => x.CanAdaptComponent(controlData))).OrderByDescending<IPropertiesAdaptor, int>((Func<IPropertiesAdaptor, int>) (x => x.Priority)).FirstOrDefault<IPropertiesAdaptor>();
  }
}
