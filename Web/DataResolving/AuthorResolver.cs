// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.DataResolving.AuthorResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.DataResolving
{
  /// <summary>Resolves Author field for content items.</summary>
  public class AuthorResolver : IDataResolver
  {
    private string defaultFormat;

    /// <summary>
    /// Resolves and formats specific data form the specified item.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="format">The format.</param>
    /// <param name="args">Optional arguments.</param>
    /// <returns></returns>
    public virtual string Resolve(object dataItem, string format, string args)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
      if (string.IsNullOrEmpty(args))
        args = "Author";
      string name = args;
      PropertyDescriptor propertyDescriptor = properties[name];
      if (propertyDescriptor != null)
      {
        object obj = propertyDescriptor.GetValue(dataItem);
        if (obj != null)
        {
          string str = obj.ToString();
          if (!string.IsNullOrEmpty(str))
            return str;
        }
      }
      if (dataItem is IOwnership)
      {
        Guid owner = ((IOwnership) dataItem).Owner;
        if (owner != Guid.Empty)
        {
          IEnumerable<DataProviderBase> contextProviders = UserManager.GetManager().GetContextProviders();
          foreach (DataProviderBase dataProviderBase in contextProviders)
            dataProviderBase.SuppressSecurityChecks = true;
          string userDisplayName = UserProfilesHelper.GetUserDisplayName(owner);
          foreach (DataProviderBase dataProviderBase in contextProviders)
            dataProviderBase.SuppressSecurityChecks = false;
          if (string.IsNullOrEmpty(format))
            format = this.defaultFormat;
          return string.Format(format, (object) userDisplayName);
        }
      }
      return string.Empty;
    }

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    public virtual void Initialize(NameValueCollection config)
    {
      this.defaultFormat = config["defaultFormat"];
      if (!string.IsNullOrEmpty(this.defaultFormat))
        return;
      this.defaultFormat = "{0}";
    }
  }
}
