// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderSecurityHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  public static class ModuleBuilderSecurityHelper
  {
    /// <summary>
    /// Determines whether the dynamic field of the specified item is editable.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="dynamicModuleField">The dynamic module field.</param>
    internal static bool IsDynamicFieldEditable(
      this DynamicContent item,
      DynamicModuleField dynamicModuleField)
    {
      dynamicModuleField.Owner = item.Owner;
      return dynamicModuleField.IsEditable(item.GetProviderName());
    }

    /// <summary>
    /// Determines whether the dynamic field of the specified item is visible.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="dynamicModuleField">The dynamic module field.</param>
    internal static bool IsDynamicFieldVisible(
      this DynamicContent item,
      DynamicModuleField dynamicModuleField)
    {
      dynamicModuleField.Owner = item.Owner;
      return dynamicModuleField.IsVisible(item.GetProviderName());
    }

    /// <summary>
    /// Determines whether the dynamic field of the specified item is visible.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="dynamicModuleField">The dynamic module field.</param>
    public static bool IsDynamicFieldEditable(
      this DynamicContent item,
      string typeNamespace,
      string name)
    {
      DynamicModuleField dynamicModuleField = ModuleBuilderManager.GetManager().GetDynamicModuleFields().FirstOrDefault<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (df => df.FieldNamespace == typeNamespace && df.Name == name));
      if (dynamicModuleField == null)
        return true;
      dynamicModuleField.Owner = item.Owner;
      return dynamicModuleField.IsEditable(item.GetProviderName());
    }

    /// <summary>
    /// Determines whether the field do the specified item with the specified name and namespace is visible.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="typeNamespace">The type namespace.</param>
    /// <param name="name">The name.</param>
    public static bool IsDynamicFieldVisible(
      this DynamicContent item,
      string typeNamespace,
      string name)
    {
      DynamicModuleField dynamicModuleField = ModuleBuilderManager.GetManager().GetDynamicModuleFields().FirstOrDefault<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (df => df.FieldNamespace == typeNamespace && df.Name == name));
      if (dynamicModuleField == null)
        return true;
      dynamicModuleField.Owner = item.Owner;
      return dynamicModuleField.IsVisible(item.GetProviderName());
    }

    /// <summary>
    /// Determines whether the specified dynamic content item is visible.
    /// </summary>
    /// <param name="item">The item.</param>
    public static bool IsVisible(this DynamicContent item) => item.GetSecuredObject().IsGranted("General", "View");

    /// <summary>
    /// Determines whether the specified dynamic content item is editable.
    /// </summary>
    /// <param name="item">The item.</param>
    public static bool IsEditable(this DynamicContent item) => item.GetSecuredObject().IsGranted("General", "Modify");

    /// <summary>
    /// Determines whether the specified dynamic content item is delete-able.
    /// </summary>
    /// <param name="item">The item.</param>
    public static bool IsDeletable(this DynamicContent item) => item.GetSecuredObject().IsGranted("General", "Delete");

    /// <summary>
    /// Determines whether the specified dynamic module field is editable.
    /// </summary>
    /// <param name="dynamicModuleField">The dynamic module field.</param>
    /// <returns>
    ///   <c>true</c> if the specified dynamic module field is editable; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsEditable(
      this DynamicModuleField dynamicModuleField,
      string dynamicContentProviderName = null)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      if (!manager.ShouldCheckPermissionsPerField(dynamicModuleField.FieldNamespace))
        return true;
      string permissionSetName = dynamicModuleField.GetPermissionSetName();
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) dynamicModuleField, dynamicContentProviderName).IsGranted(permissionSetName, "Modify");
    }

    /// <summary>
    /// Determines whether the specified dynamic module field is visible.
    /// </summary>
    /// <param name="dynamicModuleField">The dynamic module field.</param>
    /// <returns>
    ///   <c>true</c> if the specified dynamic module field is visible; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsVisible(
      this DynamicModuleField dynamicModuleField,
      string dynamicContentProviderName = null)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      if (!manager.ShouldCheckPermissionsPerField(dynamicModuleField.FieldNamespace))
        return true;
      string permissionSetName = dynamicModuleField.GetPermissionSetName();
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) dynamicModuleField, dynamicContentProviderName).IsGranted(permissionSetName, "View");
    }

    /// <summary>
    /// Determines whether the specified dynamic module field is editable.
    /// </summary>
    /// <param name="typeNamespace">The namespace of the field.</param>
    /// <param name="name">The name of the field.</param>
    /// <returns>
    ///   <c>true</c> if the specified dynamic module field is editable; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDynamicFieldEditable(
      string typeNamespace,
      string name,
      string dynamicContentProviderName = null)
    {
      DynamicModuleField dynamicModuleField = ModuleBuilderManager.GetManager().GetDynamicModuleFields().FirstOrDefault<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (df => df.FieldNamespace == typeNamespace && df.Name == name));
      return dynamicModuleField == null || dynamicModuleField.IsEditable(dynamicContentProviderName);
    }

    /// <summary>
    /// Determines whether the specified dynamic module field is visible.
    /// </summary>
    /// <param name="typeNamespace">The namespace of the field.</param>
    /// <param name="name">The name of the field.</param>
    /// <returns>
    ///   <c>true</c> if the specified dynamic module field is visible; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDynamicFieldVisible(
      string typeNamespace,
      string name,
      string dynamicContentProviderName = null)
    {
      DynamicModuleField dynamicModuleField = ModuleBuilderManager.GetManager().GetDynamicModuleFields().FirstOrDefault<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (df => df.FieldNamespace == typeNamespace && df.Name == name));
      return dynamicModuleField == null || dynamicModuleField.IsVisible(dynamicContentProviderName);
    }

    /// <summary>
    /// Determines whether the specified dynamic module type is visible.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    internal static bool IsVisible(
      this DynamicModuleType dynamicModuleType,
      string dynamicContentProviderName)
    {
      return ModuleBuilderSecurityHelper.GetSecuredObject(dynamicModuleType, dynamicContentProviderName).IsGranted("General", "View");
    }

    /// <summary>
    /// Determines whether the specified dynamic module type is editable.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    internal static bool IsEditable(
      this DynamicModuleType dynamicModuleType,
      string dynamicContentProviderName)
    {
      return ModuleBuilderSecurityHelper.GetSecuredObject(dynamicModuleType, dynamicContentProviderName).IsGranted("General", "Modify");
    }

    /// <summary>
    /// Determines whether the specified dynamic module type is delete-able.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    internal static bool IsDeletable(
      this DynamicModuleType dynamicModuleType,
      string dynamicContentProviderName)
    {
      return ModuleBuilderSecurityHelper.GetSecuredObject(dynamicModuleType, dynamicContentProviderName).IsGranted("General", "Delete");
    }

    /// <summary>
    /// Determines whether the specified dynamic module type is visible.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    public static bool IsDynamicModuleTypeVisible(Type type, string dynamicContentProviderName = null) => ModuleBuilderSecurityHelper.GetSecuredObject(type, dynamicContentProviderName).IsGranted("General", "View");

    /// <summary>
    /// Determines whether the specified dynamic module type is editable.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    public static bool IsDynamicModuleTypeEditable(Type type, string dynamicContentProviderName = null) => ModuleBuilderSecurityHelper.GetSecuredObject(type, dynamicContentProviderName).IsGranted("General", "Modify");

    /// <summary>
    /// Determines whether the specified dynamic module type is delete-able.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    public static bool IsDynamicModuleTypeDeletable(Type type, string dynamicContentProviderName = null) => ModuleBuilderSecurityHelper.GetSecuredObject(type, dynamicContentProviderName).IsGranted("General", "Delete");

    /// <summary>Gets the dynamic type secured object.</summary>
    /// <param name="dataItem">The data item.</param>
    public static ISecuredObject GetSecuredObject(this DynamicContent dataItem)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(dataItem.GetType().FullName);
      dynamicModuleType.Owner = dataItem.Owner;
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) dynamicModuleType, dataItem.GetProviderName());
    }

    /// <summary>
    /// Gets the secured object for the specified dynamic type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static ISecuredObject GetSecuredObject(
      Type type,
      string dynamicContentProviderName)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager, (ISecuredObject) manager.GetDynamicModuleType(type.FullName), dynamicContentProviderName);
    }

    /// <summary>
    /// Gets the secured object for the specified dynamic type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static ISecuredObject GetSecuredObject(
      DynamicModuleType dynamicModuleType,
      string dynamicContentProviderName)
    {
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) ModuleBuilderManager.GetManager(), (ISecuredObject) dynamicModuleType, dynamicContentProviderName);
    }
  }
}
