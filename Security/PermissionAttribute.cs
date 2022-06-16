// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.PermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents base class for Sitefinity permission attributes.
  /// </summary>
  public abstract class PermissionAttribute : Attribute
  {
    private static IDictionary<string, int> values = SystemManager.CreateStaticCache<string, int>();
    private static object valuesLock = new object();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionAttribute" /> class.
    /// </summary>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="actions">The actions to verify.</param>
    /// <param name="permissionSetDescriptionKey">Description key for the action/permission set.</param>
    public PermissionAttribute(string permissionSetName, params string[] actions)
    {
      if (string.IsNullOrEmpty(permissionSetName))
        throw new ArgumentNullException(nameof (permissionSetName));
      this.Value = actions != null && actions.Length != 0 ? PermissionAttribute.GetActionValue(permissionSetName, actions) : throw new ArgumentNullException(nameof (actions), "Value cannot be null or empty array.");
      this.PermissionSetName = permissionSetName;
      this.Actions = actions;
    }

    /// <summary>Gets the name of the permission set.</summary>
    /// <value>The name of the permission set.</value>
    public string PermissionSetName { get; private set; }

    /// <summary>Gets the name of the action.</summary>
    /// <value>The name of the action.</value>
    public string[] Actions { get; private set; }

    /// <summary>Gets the calculated union of all actions.</summary>
    /// <value>The calculated union of all actions.</value>
    public int Value { get; private set; }

    /// <summary>
    /// Gets an optional resource key for description text for the permission set
    /// </summary>
    public string PermissionSetDescriptionKey { get; set; }

    internal static int GetActionValue(string permissionSetName, params string[] actions)
    {
      string key = permissionSetName + string.Join("", actions);
      int actionValue;
      if (!PermissionAttribute.values.TryGetValue(key, out actionValue))
      {
        lock (PermissionAttribute.valuesLock)
        {
          if (!PermissionAttribute.values.TryGetValue(key, out actionValue))
          {
            Permission permission;
            if (!Config.Get<SecurityConfig>().Permissions.TryGetValue(permissionSetName, out permission))
              throw new ArgumentException(Res.Get<ErrorMessages>().NoPermissionSet.Arrange((object) permissionSetName));
            foreach (string action in actions)
            {
              SecurityAction securityAction;
              if (!permission.Actions.TryGetValue(action, out securityAction))
                throw new ArgumentException(Res.Get<ErrorMessages>().NoSuchAction.Arrange((object) action, (object) permissionSetName));
              actionValue |= securityAction.Value;
            }
            PermissionAttribute.values[key] = actionValue;
          }
        }
      }
      return actionValue;
    }
  }
}
