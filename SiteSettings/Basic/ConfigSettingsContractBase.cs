// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Basic.ConfigSettingsContractBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.SiteSettings.Basic
{
  [DataContract]
  public abstract class ConfigSettingsContractBase : ISettingsDataContract
  {
    public void LoadDefaults(bool forEdit = false) => this.CopyFrom((object) this.GetConfigElement(forEdit, out ConfigManager _));

    public void SaveDefaults()
    {
      ConfigManager manager;
      ConfigElement configElement = this.GetConfigElement(true, out manager);
      this.CopyTo((object) configElement);
      manager.SaveSection(configElement.Section);
    }

    protected virtual IEnumerable<PropertyInfo> GetProperties() => ((IEnumerable<PropertyInfo>) this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => ((IEnumerable<object>) p.GetCustomAttributes(typeof (DataMemberAttribute), false)).Any<object>()));

    protected abstract ConfigElement GetConfigElement(
      bool forEdit,
      out ConfigManager manager);

    private void CopyTo(object target)
    {
      Type type = target.GetType();
      foreach (PropertyInfo property1 in this.GetProperties())
      {
        PropertyInfo property2 = type.GetProperty(property1.Name);
        if (property2 != (PropertyInfo) null)
          this.Copy(property1, property2, (object) this, target);
      }
    }

    private void CopyFrom(object source)
    {
      Type type = source.GetType();
      foreach (PropertyInfo property1 in this.GetProperties())
      {
        PropertyInfo property2 = type.GetProperty(property1.Name);
        if (property2 != (PropertyInfo) null)
          this.Copy(property2, property1, source, (object) this);
      }
    }

    private void Copy(
      PropertyInfo sourceProperty,
      PropertyInfo targetProperty,
      object source,
      object target)
    {
      targetProperty.SetValue(target, sourceProperty.GetValue(source, (object[]) null), (object[]) null);
    }
  }
}
