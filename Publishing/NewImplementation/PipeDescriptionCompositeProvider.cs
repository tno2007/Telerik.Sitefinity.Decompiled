// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PipeDescriptionCompositeProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  internal class PipeDescriptionCompositeProvider : IPipeDescriptionProvider
  {
    private static readonly object sync = new object();
    private List<IPipeDescriptionProvider> descProviders = new List<IPipeDescriptionProvider>();
    private HashSet<Type> descProvidersTypes = new HashSet<Type>();

    public void AddProvider(IPipeDescriptionProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      lock (PipeDescriptionCompositeProvider.sync)
      {
        Type type = provider.GetType();
        if (this.descProvidersTypes.Contains(type))
          return;
        this.descProvidersTypes.Add(type);
        this.descProviders.Add(provider);
      }
    }

    public bool HasProvider(IPipeDescriptionProvider provider)
    {
      if (provider == null)
        throw new ArgumentNullException(nameof (provider));
      lock (PipeDescriptionCompositeProvider.sync)
        return this.descProvidersTypes.Contains(provider.GetType());
    }

    public bool GetPipeSettingsShortDescription(PipeSettings pipeSettings, out string description)
    {
      for (int index = 0; index < this.descProviders.Count; ++index)
      {
        if (this.descProviders[index].GetPipeSettingsShortDescription(pipeSettings, out description))
          return true;
      }
      description = (string) null;
      return false;
    }
  }
}
