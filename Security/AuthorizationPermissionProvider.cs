// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.AuthorizationPermissionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Principal;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Security;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents an authorization provider that evaluates
  /// permission set against secured object to determine whether
  /// <see cref="T:System.Security.Principal.IPrincipal" /> objects
  /// are authorized.
  /// </summary>
  public class AuthorizationPermissionProvider : AuthorizationProvider
  {
    /// <summary>
    /// When implemented in a derived class, Evaluates the specified authority against the specified context.
    /// </summary>
    /// <param name="principal">Must be an <see cref="T:System.Security.Principal.IPrincipal" /> object.</param>
    /// <param name="context">Must be a string that is the name of the rule to evaluate.</param>
    /// <returns>
    /// 	<c>true</c> if the authority is authorized, otherwise <c>false</c>.
    /// </returns>
    public override bool Authorize(IPrincipal principal, string context) => throw new NotSupportedException();

    /// <summary>
    /// Provides more information by telling which set/action was denied or null if there is no error
    /// </summary>
    /// <remarks>
    /// This should be checked after <c>AuthorizeInput</c> or <c>AuthorizeOutput</c> has been called.
    /// </remarks>
    public virtual string DetailedErrorMessage { get; private set; }

    /// <summary>Authorizes the input.</summary>
    /// <param name="input">The input.</param>
    /// <returns></returns>
    public virtual bool AuthorizeInput(IMethodInvocation input)
    {
      this.DetailedErrorMessage = (string) null;
      return (!(input.Target is ISecuredObject) || this.IsDynamicContentFieldPermissionsGranted(input) && this.IsTypeMethodPermissionsGranted(input) && this.IsMethodPermissionsGranted(input)) && this.IsTypedParameterPermissionsGranted(input) && this.IsParameterPermissionsGranted(input) && this.IsTransactionPermissionsGranted(input) && this.IsGlobalPermissionsGranted(input);
    }

    private bool IsTypeMethodPermissionsGranted(IMethodInvocation input)
    {
      if (input.MethodBase.IsGenericMethod)
      {
        Type genericArgument = input.MethodBase.GetGenericArguments()[0];
        object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (TypedMethodPermissionAttribute), true);
        ISecuredObject target = input.Target as ISecuredObject;
        foreach (TypedMethodPermissionAttribute deniedAttribute in customAttributes)
        {
          if (genericArgument == deniedAttribute.ItemType && target.IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !target.IsGranted(deniedAttribute.PermissionSetName, deniedAttribute.Value))
          {
            this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
            return false;
          }
        }
      }
      return true;
    }

    private bool IsMethodPermissionsGranted(IMethodInvocation input)
    {
      object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (MethodPermissionAttribute), true);
      if (customAttributes.Length != 0)
      {
        ISecuredObject target = input.Target as ISecuredObject;
        foreach (MethodPermissionAttribute deniedAttribute in customAttributes)
        {
          if (target.IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !target.IsGranted(deniedAttribute.PermissionSetName, deniedAttribute.Value))
          {
            this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
            return false;
          }
        }
      }
      return true;
    }

    private bool IsTypedParameterPermissionsGranted(IMethodInvocation input)
    {
      foreach (TypedParameterPermissionAttribute customAttribute in input.MethodBase.GetCustomAttributes(typeof (TypedParameterPermissionAttribute), true))
      {
        object obj = string.IsNullOrEmpty(customAttribute.ParameterName) ? input.Arguments[customAttribute.ParameterIndex] : input.Arguments[customAttribute.ParameterName];
        if (obj == null)
          throw new ArgumentOutOfRangeException("Invalid parameter specified in TypedParameterPermissionAttribute.");
        if (obj is ISecuredObject securedObject && customAttribute.ItemType == obj.GetType() && securedObject.IsPermissionSetSupported(customAttribute.PermissionSetName) && !securedObject.IsGranted(customAttribute.PermissionSetName, customAttribute.Value))
        {
          this.SetDetailedErrorMessage((PermissionAttribute) customAttribute);
          return false;
        }
      }
      return true;
    }

    private bool IsParameterPermissionsGranted(IMethodInvocation input)
    {
      foreach (ParameterPermissionAttribute customAttribute in input.MethodBase.GetCustomAttributes(typeof (ParameterPermissionAttribute), true))
      {
        object obj = string.IsNullOrEmpty(customAttribute.ParameterName) ? input.Arguments[customAttribute.ParameterIndex] : input.Arguments[customAttribute.ParameterName];
        if (obj == null)
          throw new ArgumentOutOfRangeException("Invalid parameter specified in ParameterPermissionAttribute.");
        if (obj is ISecuredObject securedObject && securedObject.IsPermissionSetSupported(customAttribute.PermissionSetName) && !securedObject.IsGranted(customAttribute.PermissionSetName, customAttribute.Value))
        {
          this.SetDetailedErrorMessage((PermissionAttribute) customAttribute);
          return false;
        }
      }
      return true;
    }

    private bool IsTransactionPermissionsGranted(IMethodInvocation input)
    {
      object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (TransactionPermissionAttribute), true);
      if (customAttributes.Length != 0)
      {
        if (!(input.Target is DataProviderBase target))
          throw new ArgumentException("TransactionPermissionAttribute can be specified only on data provider methods.");
        IList dirtyItems = target.GetDirtyItems();
        foreach (TransactionPermissionAttribute deniedAttribute in customAttributes)
        {
          foreach (object obj1 in (IEnumerable) dirtyItems)
          {
            IList list = (IList) null;
            if (deniedAttribute.ItemType.IsAssignableFrom(obj1.GetType()))
            {
              ISecuredObject securedObject = input.Target as ISecuredObject;
              if (!string.IsNullOrEmpty(deniedAttribute.SecuredObjectPropertyName))
              {
                PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj1).Find(deniedAttribute.SecuredObjectPropertyName, true);
                if (propertyDescriptor != null)
                {
                  securedObject = (ISecuredObject) null;
                  object obj2 = propertyDescriptor.GetValue(obj1);
                  if (obj2.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
                    securedObject = (ISecuredObject) propertyDescriptor.GetValue(obj1);
                  else if (obj2 is IList)
                    list = (IList) obj2;
                  if (securedObject == null && list == null)
                    continue;
                }
              }
              else if (obj1.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
                securedObject = (ISecuredObject) obj1;
              else
                continue;
              if (deniedAttribute.ActionType == SecurityConstants.TransactionActionType.None || target.GetDirtyItemStatus(obj1) == deniedAttribute.ActionType)
              {
                IOwnership actualOwnerObject = deniedAttribute.ActionType != SecurityConstants.TransactionActionType.New ? obj1 as IOwnership : (IOwnership) new AuthorizationPermissionProvider.EmptyOwnership();
                if (securedObject != null)
                {
                  if (obj1 is ILifecycleDataItem entity && actualOwnerObject != null && entity.Owner == Guid.Empty && deniedAttribute.ActionType == SecurityConstants.TransactionActionType.Updated && (entity.Status == ContentLifecycleStatus.Temp || entity.Status == ContentLifecycleStatus.PartialTemp))
                  {
                    Guid originalValue = target.GetOriginalValue<Guid>((object) entity, "Owner");
                    if (originalValue == ClaimsManager.GetCurrentUserId())
                      actualOwnerObject = (IOwnership) new AuthorizationPermissionProvider.EmptyOwnership(originalValue);
                  }
                  if (securedObject.IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !securedObject.IsGranted(deniedAttribute.PermissionSetName, deniedAttribute.Value, actualOwnerObject))
                  {
                    this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
                    return false;
                  }
                }
                else if (list != null)
                {
                  foreach (object obj3 in (IEnumerable) list)
                  {
                    if (obj3.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null && ((ISecuredObject) obj3).IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !((ISecuredObject) obj3).IsGranted(deniedAttribute.PermissionSetName, deniedAttribute.Value, actualOwnerObject))
                    {
                      this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
                      return false;
                    }
                  }
                }
              }
            }
          }
        }
      }
      return true;
    }

    private bool IsGlobalPermissionsGranted(IMethodInvocation input)
    {
      object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (GlobalPermissionAttribute), true);
      if (customAttributes.Length != 0)
      {
        foreach (GlobalPermissionAttribute deniedAttribute in customAttributes)
        {
          if (AppPermission.Root.IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !AppPermission.IsGranted(deniedAttribute.Value))
          {
            this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
            return false;
          }
        }
      }
      return true;
    }

    /// <summary>Authorizes the output.</summary>
    /// <param name="output">The output.</param>
    /// <returns></returns>
    public virtual bool AuthorizeOutput(IMethodInvocation input, IMethodReturn output)
    {
      if (output.Exception != null)
        return true;
      this.DetailedErrorMessage = (string) null;
      if (output.ReturnValue is ISecuredObject returnValue)
      {
        object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (TypedValuePermissionAttribute), true);
        if (customAttributes.Length != 0)
        {
          Type type = output.ReturnValue.GetType();
          foreach (TypedValuePermissionAttribute deniedAttribute in customAttributes)
          {
            if (this.IsSameOrSubclass(type, deniedAttribute.ItemType) && returnValue.IsPermissionSetSupported(deniedAttribute.PermissionSetName) && !returnValue.IsGranted(deniedAttribute.PermissionSetName, deniedAttribute.Value))
            {
              this.SetDetailedErrorMessage((PermissionAttribute) deniedAttribute);
              return false;
            }
          }
        }
        foreach (ValuePermissionAttribute customAttribute in input.MethodBase.GetCustomAttributes(typeof (ValuePermissionAttribute), true))
        {
          if (returnValue.IsPermissionSetSupported(customAttribute.PermissionSetName) && !returnValue.IsGranted(customAttribute.PermissionSetName, customAttribute.Value))
          {
            this.SetDetailedErrorMessage((PermissionAttribute) customAttribute);
            return false;
          }
        }
      }
      AuthorizationPermissionProvider.ApplyPermissions<EnumeratorPermissionAttribute>(input, output);
      AuthorizationPermissionProvider.ApplyPermissions<TypedEnumeratorPermissionAttribute>(input, output);
      return true;
    }

    private static void ApplyPermissions<TPermissionAttribute>(
      IMethodInvocation input,
      IMethodReturn output)
      where TPermissionAttribute : PermissionAttribute
    {
      object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (TPermissionAttribute), true);
      if (customAttributes.Length == 0 || !(output.ReturnValue is IPermissionApplier returnValue))
        return;
      returnValue.PermissionsInfo = (PermissionAttribute[]) customAttributes;
    }

    private bool IsSameOrSubclass(Type potentialDescendant, Type potentialBase) => potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;

    private void SetDetailedErrorMessage(PermissionAttribute deniedAttribute) => this.SetDetailedErrorMessage(deniedAttribute.PermissionSetName, deniedAttribute.Actions);

    private void SetDetailedErrorMessage(string permissionSet, string[] actions) => this.DetailedErrorMessage = AuthorizationPermissionProvider.GetDetailedErrorMessage(permissionSet, actions);

    internal static string GetDetailedErrorMessage(string permissionSet, string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission = Config.Get<SecurityConfig>().Permissions[permissionSet];
      string[] strArray = new string[actions.Length];
      for (int index = 0; index < actions.Length; ++index)
      {
        SecurityAction action = permission.Actions[actions[index]];
        strArray[index] = action.Title;
      }
      string str = string.Join(", ", strArray);
      return string.Format(Res.Get<SecurityResources>().NotAuthorizedToDoSetAction, (object) str, (object) permissionSet);
    }

    private bool IsDynamicContentFieldPermissionsGranted(IMethodInvocation input)
    {
      bool flag = true;
      if (input.Target is ISecuredObject target1 && target1 is DynamicModuleDataProvider && (input.MethodBase.Name == "CommitTransaction" || input.MethodBase.Name == "FlushTransaction"))
      {
        DynamicModuleDataProvider target = (DynamicModuleDataProvider) input.Target;
        IList dirtyItems = target.GetDirtyItems();
        if (dirtyItems.Count > 0)
        {
          foreach (object itemInTransaction in (IEnumerable) dirtyItems)
          {
            if (itemInTransaction is DynamicContent dataItem && dataItem.Status != ContentLifecycleStatus.Temp)
            {
              switch (target.GetDirtyItemStatus(itemInTransaction))
              {
                case SecurityConstants.TransactionActionType.New:
                  flag &= dataItem.IsFieldPermissionsGranted("Create");
                  continue;
                case SecurityConstants.TransactionActionType.Updated:
                  flag &= dataItem.IsFieldPermissionsGranted("Modify");
                  continue;
                case SecurityConstants.TransactionActionType.Deleted:
                  flag &= dataItem.IsFieldPermissionsGranted("Delete");
                  continue;
                default:
                  continue;
              }
            }
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Represents an <see cref="T:Telerik.Sitefinity.Model.IOwnership" /> object with an empty owner.
    /// </summary>
    /// <seealso cref="T:Telerik.Sitefinity.Model.IOwnership" />
    private class EmptyOwnership : IOwnership
    {
      public EmptyOwnership() => this.Owner = Guid.Empty;

      public EmptyOwnership(Guid owner) => this.Owner = owner;

      public Guid Owner { get; set; }
    }
  }
}
