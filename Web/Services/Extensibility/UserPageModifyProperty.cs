// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.UserPageModifyProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A user modify property for retrieving which page from who is last modified.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class UserPageModifyProperty : UserModifyProperty
  {
    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      IUserDisplayNameBuilder displayNameBuilder = ObjectFactory.Resolve<IUserDisplayNameBuilder>();
      List<KeyValuePair<object, PageSiteNode>> list = items.Cast<object>().ToDictionary<object, object, PageSiteNode>((Func<object, object>) (x => x), (Func<object, PageSiteNode>) (y => PropertyHelpers.GetSiteMapNode(y))).ToList<KeyValuePair<object, PageSiteNode>>();
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(typeof (PageDataProxy)).Find(this.ClrPropName, false);
      if (propertyDescriptor != null)
      {
        foreach (KeyValuePair<object, PageSiteNode> keyValuePair in list)
        {
          PageSiteNode pageSiteNode = keyValuePair.Value;
          PageDataProxy component = pageSiteNode == null ? (PageDataProxy) null : pageSiteNode.CurrentPageDataItem;
          if (component != null)
          {
            Guid userId = (Guid) propertyDescriptor.GetValue((object) component);
            string userDisplayName = displayNameBuilder.GetUserDisplayName(userId);
            values.Add(keyValuePair.Key, (object) userDisplayName);
          }
        }
      }
      return (IDictionary<object, object>) values;
    }
  }
}
