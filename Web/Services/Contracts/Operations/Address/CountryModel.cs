// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Address.CountryModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Address
{
  internal class CountryModel
  {
    [DataMember(Name = "isoCode")]
    public string IsoCode { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "states")]
    public IEnumerable<StateModel> States { get; set; }

    [DataMember(Name = "hasStates")]
    public bool HasStates => this.States.Count<StateModel>() > 0;
  }
}
