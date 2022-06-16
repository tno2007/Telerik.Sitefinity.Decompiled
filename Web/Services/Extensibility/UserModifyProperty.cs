// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.UserModifyProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A user modify property for retrieving which item is from who is created or last modified.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class UserModifyProperty : CalculatedProperty
  {
    private string clrPropName;

    /// <summary>Gets the CLR property name</summary>
    internal string ClrPropName => this.clrPropName;

    /// <inheritdoc />
    public override void Initialize(NameValueCollection parameters, Type parentType)
    {
      base.Initialize(parameters, parentType);
      this.clrPropName = parameters["UserIdPropertyName"];
    }

    /// <inheritdoc />
    public override Type ReturnType => typeof (string);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      IUserDisplayNameBuilder displayNameBuilder = ObjectFactory.Resolve<IUserDisplayNameBuilder>();
      List<object> list = items.Cast<object>().ToList<object>();
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(list.First<object>().GetType()).Find(this.clrPropName, false);
      if (propertyDescriptor != null)
      {
        foreach (object obj in list)
        {
          Guid userId = (Guid) propertyDescriptor.GetValue(obj);
          string userDisplayName = displayNameBuilder.GetUserDisplayName(userId);
          values.Add(obj, (object) userDisplayName);
        }
      }
      return (IDictionary<object, object>) values;
    }

    internal new class Constants
    {
      internal const string CreatedBy = "CreatedBy";
      internal const string LastModifiedBy = "LastModifiedBy";
      internal const string UserIdPropertyName = "UserIdPropertyName";
      internal const string Owner = "Owner";
    }
  }
}
