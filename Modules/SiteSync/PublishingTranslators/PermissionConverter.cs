// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.PermissionConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.SiteSync
{
  internal class PermissionConverter : ISiteSyncConverter
  {
    private ISecuredObject securedObject;
    private List<Permission> permissionsToRemove = new List<Permission>();

    public string Serialize(object obj, Type type, object settings)
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) type.FullName)
      });
      this.AddElements(xdocument.Root, obj);
      return xdocument.ToString();
    }

    protected virtual void AddElements(XElement root, object obj)
    {
      Permission permission = (Permission) obj;
      root.Add((object) new XElement((XName) "Deny", (object) permission.Deny));
      root.Add((object) new XElement((XName) "Grant", (object) permission.Grant));
      root.Add((object) new XElement((XName) "ObjectId", (object) permission.ObjectId));
      root.Add((object) new XElement((XName) "PrincipalId", (object) permission.PrincipalId));
      root.Add((object) new XElement((XName) "SetName", (object) permission.SetName));
    }

    public void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args)
    {
      string str1 = value as string;
      IEnumerable<string> strings = value as IEnumerable<string>;
      if (str1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Permission>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Permission), typeof (PermissionConverter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, Permission> target = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, Permission>> p1 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, PermissionConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__0, this, str1, type, args);
        Permission permission = target((CallSite) p1, obj);
        prop.SetValue(instance, (object) permission);
      }
      else
      {
        if (strings == null)
          return;
        // ISSUE: reference to a compiler-generated field
        if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, ISecuredObject>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ISecuredObject), typeof (PermissionConverter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, ISecuredObject> target1 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, ISecuredObject>> p3 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Item", typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__2, args);
        this.securedObject = target1((CallSite) p3, obj1);
        this.permissionsToRemove = this.securedObject.Permissions.ToList<Permission>();
        foreach (string str2 in strings)
        {
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, Permission>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Permission), typeof (PermissionConverter)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, Permission> target2 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__5.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, Permission>> p5 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__5;
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, PermissionConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__4.Target((CallSite) PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__4, this, str2, typeof (Permission), args);
          Permission permission = target2((CallSite) p5, obj2);
          if (!this.securedObject.Permissions.Any<Permission>((Func<Permission, bool>) (p => p.Id == permission.Id)))
            this.securedObject.Permissions.Add(permission);
        }
        foreach (Permission permission in this.permissionsToRemove.Where<Permission>((Func<Permission, bool>) (p =>
        {
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__9 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (PermissionConverter)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, bool> target3 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__9.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, bool>> p9 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__9;
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__8 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__8 = CallSite<Func<CallSite, Guid, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Guid, object, object> target4 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__8.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Guid, object, object>> p8 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__8;
          Guid objectId = p.ObjectId;
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Id", typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target5 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p7 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Item", typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj3 = PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__6.Target((CallSite) PermissionConverter.\u003C\u003Eo__2.\u003C\u003Ep__6, args);
          object obj4 = target5((CallSite) p7, obj3);
          object obj5 = target4((CallSite) p8, objectId, obj4);
          return target3((CallSite) p9, obj5);
        })))
          this.securedObject.Permissions.Remove(permission);
        this.permissionsToRemove.Clear();
        this.securedObject = (ISecuredObject) null;
      }
    }

    public object Deserialize(string str, Type type, object settings)
    {
      XElement root = XDocument.Parse(HttpUtility.HtmlDecode(str)).Root;
      // ISSUE: reference to a compiler-generated field
      if (PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IManager>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IManager), typeof (PermissionConverter)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IManager> target = PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IManager>> p1 = PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Manager", typeof (PermissionConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) PermissionConverter.\u003C\u003Eo__3.\u003C\u003Ep__0, settings);
      IManager manager = target((CallSite) p1, obj);
      Guid objId = new Guid(root.Element((XName) "ObjectId").Value);
      Guid principalId = new Guid(root.Element((XName) "PrincipalId").Value);
      string setName = root.Element((XName) "SetName").Value;
      Permission permission = this.securedObject.Permissions.Where<Permission>((Func<Permission, bool>) (i => i.SetName == setName && i.ObjectId == objId && i.PrincipalId == principalId)).FirstOrDefault<Permission>() ?? (manager.GetPermission(setName, objId, principalId) ?? manager.Provider.GetDirtyItems().OfType<Permission>().Where<Permission>((Func<Permission, bool>) (i => i.SetName == setName && i.ObjectId == objId && i.PrincipalId == principalId)).FirstOrDefault<Permission>()) ?? manager.CreatePermission(setName, objId, principalId);
      permission.Deny = int.Parse(root.Element((XName) "Deny").Value);
      permission.Grant = int.Parse(root.Element((XName) "Grant").Value);
      permission.LastModified = DateTime.UtcNow;
      this.permissionsToRemove.RemoveAll((Predicate<Permission>) (i => i.SetName == setName && i.ObjectId == objId && i.PrincipalId == principalId));
      return (object) permission;
    }
  }
}
