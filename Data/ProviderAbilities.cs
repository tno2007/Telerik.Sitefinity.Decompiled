// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ProviderAbilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// specified the provider abilities,e.g. what operations are supported and allowed
  /// </summary>
  public class ProviderAbilities : Dictionary<string, ProviderAbility>
  {
    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the type of the provider.</summary>
    /// <value>The type of the provider.</value>
    public string ProviderType { get; set; }

    /// <summary>Adds an ability.</summary>
    /// <param name="operationName">Name of the operation.</param>
    /// <param name="supported">if set to <c>true</c> [supported].</param>
    /// <param name="allowed">if set to <c>true</c> [allowed].</param>
    public void AddAbility(string operationName, bool supported, bool allowed) => this.Add(operationName, new ProviderAbility(operationName, supported, allowed));

    /// <summary>Clones this instance.</summary>
    /// <returns></returns>
    public ProviderAbilities Clone()
    {
      ProviderAbilities providerAbilities = new ProviderAbilities();
      providerAbilities.ProviderName = this.ProviderName;
      providerAbilities.ProviderType = this.ProviderType;
      foreach (ProviderAbility providerAbility in this.Values)
        providerAbilities.AddAbility(providerAbility.OperationName, providerAbility.Supported, providerAbility.Allowed);
      return providerAbilities;
    }
  }
}
